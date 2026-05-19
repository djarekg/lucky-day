using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;
using System.Data.Common;

#pragma warning disable EF1002

namespace LuckyDay.Db;

internal static class SearchFtsInitializer
{
  private readonly record struct FtsField(string FtsColumn, string PropertyName);

  private readonly record struct FtsDefinition(
    Type EntityType,
    string FtsTable,
    string TriggerPrefix,
    IReadOnlyList<FtsField> Fields);

  private static readonly FtsDefinition[] SearchFtsDefinitions =
  [
    new(
      typeof(User),
      "User_Fts",
      "update_user_fts",
      [
        new("id", nameof(User.Id)),
        new("firstName", nameof(User.FirstName)),
        new("lastName", nameof(User.LastName)),
        new("email", nameof(User.Email)),
        new("jobTitle", nameof(User.JobTitle)),
        new("streetAddress", nameof(User.StreetAddress)),
        new("streetAddress2", nameof(User.StreetAddress2)),
        new("city", nameof(User.City)),
        new("phone", nameof(User.Phone)),
      ]),
    new(
      typeof(Customer),
      "Customer_Fts",
      "update_customer_fts",
      [
        new("id", nameof(Customer.Id)),
        new("name", nameof(Customer.Name)),
        new("streetAddress", nameof(Customer.StreetAddress)),
        new("streetAddress2", nameof(Customer.StreetAddress2)),
        new("city", nameof(Customer.City)),
        new("phone", nameof(Customer.Phone)),
      ]),
    new(
      typeof(CustomerContact),
      "CustomerContact_Fts",
      "update_customer_contact_fts",
      [
        new("id", nameof(CustomerContact.Id)),
        new("customerId", nameof(CustomerContact.CustomerId)),
        new("firstName", nameof(CustomerContact.FirstName)),
        new("lastName", nameof(CustomerContact.LastName)),
        new("email", nameof(CustomerContact.Email)),
        new("streetAddress", nameof(CustomerContact.StreetAddress)),
        new("streetAddress2", nameof(CustomerContact.StreetAddress2)),
        new("city", nameof(CustomerContact.City)),
        new("phone", nameof(CustomerContact.Phone)),
      ]),
    new(
      typeof(Product),
      "Product_Fts",
      "update_product_fts",
      [
        new("id", nameof(Product.Id)),
        new("name", nameof(Product.Name)),
        new("description", nameof(Product.Description)),
        new("price", nameof(Product.Price)),
      ])
  ];

  public static async Task EnsureAsync(LuckyDayDbContext context)
  {
    foreach (var definition in SearchFtsDefinitions)
    {
      await RebuildFtsTableAsync(context, definition);
    }
  }

  private static async Task RebuildFtsTableAsync(LuckyDayDbContext context, FtsDefinition definition)
  {
    var entityType = context.Model.FindEntityType(definition.EntityType)
      ?? throw new InvalidOperationException($"Unable to resolve EF metadata for {definition.EntityType.Name}.");

    var tableName = entityType.GetTableName()
      ?? throw new InvalidOperationException($"Unable to resolve table name for {definition.EntityType.Name}.");

    var storeObject = StoreObjectIdentifier.Table(tableName, entityType.GetSchema());
    var mappedFields = definition.Fields.Select(field =>
    {
      var property = entityType.FindProperty(field.PropertyName)
        ?? throw new InvalidOperationException($"Unable to resolve property {field.PropertyName} on {definition.EntityType.Name}.");

      var sourceColumn = property.GetColumnName(storeObject)
        ?? throw new InvalidOperationException($"Unable to resolve column for property {field.PropertyName} on {definition.EntityType.Name}.");

      return (field.FtsColumn, SourceColumn: sourceColumn);
    }).ToArray();

    var quotedFtsTable = QuoteIdentifier(definition.FtsTable);
    var quotedBaseTable = QuoteIdentifier(tableName);
    var quotedFtsColumns = string.Join(", ", mappedFields.Select(field => QuoteIdentifier(field.FtsColumn)));
    var selectSeedColumns = string.Join(", ", mappedFields.Select(field => $"{QuoteIdentifier(field.SourceColumn)} AS {QuoteIdentifier(field.FtsColumn)}"));
    var insertSeedColumns = string.Join(", ", mappedFields.Select(field => $"NEW.{QuoteIdentifier(field.SourceColumn)}"));
    var updateAssignments = string.Join(", ", mappedFields.Select(field => $"{QuoteIdentifier(field.FtsColumn)} = NEW.{QuoteIdentifier(field.SourceColumn)}"));
    var updateColumns = string.Join(", ", mappedFields.Select(field => QuoteIdentifier(field.SourceColumn)));
    var updateConditions = string.Join(" OR ", mappedFields.Select(field => $"OLD.{QuoteIdentifier(field.SourceColumn)} IS NOT NEW.{QuoteIdentifier(field.SourceColumn)}"));

    var updateTriggerName = QuoteIdentifier(definition.TriggerPrefix + "_after_update");
    var insertTriggerName = QuoteIdentifier(definition.TriggerPrefix + "_after_insert");
    var deleteTriggerName = QuoteIdentifier(definition.TriggerPrefix + "_after_delete");

    var connection = context.Database.GetDbConnection();
    var shouldCloseConnection = connection.State != ConnectionState.Open;
    if (shouldCloseConnection)
    {
      await connection.OpenAsync();
    }

    try
    {
      await ExecuteNonQueryAsync(connection, "DROP TRIGGER IF EXISTS " + updateTriggerName + ";");
      await ExecuteNonQueryAsync(connection, "DROP TRIGGER IF EXISTS " + insertTriggerName + ";");
      await ExecuteNonQueryAsync(connection, "DROP TRIGGER IF EXISTS " + deleteTriggerName + ";");
      await ExecuteNonQueryAsync(connection, "DROP TABLE IF EXISTS " + quotedFtsTable + ";");
      await ExecuteNonQueryAsync(connection, "CREATE VIRTUAL TABLE " + quotedFtsTable + " USING fts5(" + quotedFtsColumns + ", tokenize='trigram');");

      await ExecuteNonQueryAsync(
        connection,
        "INSERT INTO " + quotedFtsTable + " (rowid, " + quotedFtsColumns + ")\n" +
        "SELECT rowid, " + selectSeedColumns + "\n" +
        "FROM " + quotedBaseTable + ";");

      await ExecuteNonQueryAsync(
        connection,
        "CREATE TRIGGER " + updateTriggerName + "\n" +
        "AFTER UPDATE OF " + updateColumns + " ON " + quotedBaseTable + "\n" +
        "FOR EACH ROW\n" +
        "WHEN " + updateConditions + "\n" +
        "BEGIN\n" +
        "  UPDATE " + quotedFtsTable + "\n" +
        "  SET " + updateAssignments + "\n" +
        "  WHERE rowid = NEW.rowid;\n" +
        "END;");

      await ExecuteNonQueryAsync(
        connection,
        "CREATE TRIGGER " + insertTriggerName + "\n" +
        "AFTER INSERT ON " + quotedBaseTable + "\n" +
        "FOR EACH ROW\n" +
        "BEGIN\n" +
        "  INSERT INTO " + quotedFtsTable + " (" + quotedFtsColumns + ")\n" +
        "  VALUES (" + insertSeedColumns + ");\n" +
        "END;");

      await ExecuteNonQueryAsync(
        connection,
        "CREATE TRIGGER " + deleteTriggerName + "\n" +
        "AFTER DELETE ON " + quotedBaseTable + "\n" +
        "FOR EACH ROW\n" +
        "BEGIN\n" +
        "  DELETE FROM " + quotedFtsTable + "\n" +
        "  WHERE rowid = OLD.rowid;\n" +
        "END;");
    }
    finally
    {
      if (shouldCloseConnection)
      {
        await connection.CloseAsync();
      }
    }
  }

  private static string QuoteIdentifier(string identifier)
  {
    return $"\"{identifier.Replace("\"", "\"\"")}\"";
  }

  private static async Task ExecuteNonQueryAsync(DbConnection connection, string sql)
  {
    await using var command = connection.CreateCommand();
    command.CommandText = sql;
    await command.ExecuteNonQueryAsync();
  }
}

#pragma warning restore EF1002
