namespace LuckyDay.Db.Data;

public class LuckyDayDbContext : DbContext
{
    private readonly string? _connectionString;

    public LuckyDayDbContext() : base()
    {
    }

    public LuckyDayDbContext(DbContextOptions<LuckyDayDbContext> options) : base(options)
    {
    }

    public LuckyDayDbContext(string connectionString) : base()
    {
        _connectionString = connectionString;
    }

    public DbSet<State> States => Set<State>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserCredential> UserCredentials => Set<UserCredential>();
    public DbSet<TokenRevocation> TokenRevocations => Set<TokenRevocation>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerContact> CustomerContacts => Set<CustomerContact>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductColor> ProductColors => Set<ProductColor>();
    public DbSet<ProductInventory> ProductInventories => Set<ProductInventory>();
    public DbSet<ProductSale> ProductSales => Set<ProductSale>();
    public DbSet<DashboardWidget> DashboardWidgets => Set<DashboardWidget>();
    public DbSet<UserDashboard> UserDashboards => Set<UserDashboard>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = _connectionString ?? "Data Source=lucky-day.db";
            optionsBuilder.UseSqlite(connectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // State Configuration
        modelBuilder.Entity<State>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<State>()
            .Property(s => s.Name)
            .IsRequired();

        modelBuilder.Entity<State>()
            .Property(s => s.Code)
            .IsRequired();

        // User Configuration
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.State)
            .WithMany(s => s.Users)
            .HasForeignKey(u => u.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserCredential)
            .WithOne(uc => uc.User)
            .HasForeignKey<UserCredential>(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // UserCredential Configuration
        modelBuilder.Entity<UserCredential>()
            .HasKey(uc => uc.Id);

        modelBuilder.Entity<UserCredential>()
            .HasIndex(uc => uc.UserId)
            .IsUnique();

        // TokenRevocation Configuration
        modelBuilder.Entity<TokenRevocation>()
            .HasKey(tr => tr.Id);

        modelBuilder.Entity<TokenRevocation>()
            .Property(tr => tr.Email)
            .IsRequired();

        modelBuilder.Entity<TokenRevocation>()
            .HasIndex(tr => tr.ExpiresAtUtc);

        // Customer Configuration
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Customer>()
            .HasOne(c => c.State)
            .WithMany(s => s.Customers)
            .HasForeignKey(c => c.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        // CustomerContact Configuration
        modelBuilder.Entity<CustomerContact>()
            .HasKey(cc => cc.Id);

        modelBuilder.Entity<CustomerContact>()
            .HasOne(cc => cc.Customer)
            .WithMany(c => c.CustomerContacts)
            .HasForeignKey(cc => cc.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CustomerContact>()
            .HasOne(cc => cc.State)
            .WithMany(s => s.CustomerContacts)
            .HasForeignKey(cc => cc.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product Configuration
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        // ProductColor Configuration
        modelBuilder.Entity<ProductColor>()
            .HasKey(pc => pc.Id);

        modelBuilder.Entity<ProductColor>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductColors)
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // ProductInventory Configuration
        modelBuilder.Entity<ProductInventory>()
            .HasKey(pi => pi.Id);

        modelBuilder.Entity<ProductInventory>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductInventories)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // ProductSale Configuration
        modelBuilder.Entity<ProductSale>()
            .HasKey(ps => ps.Id);

        modelBuilder.Entity<ProductSale>()
            .Property(ps => ps.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ProductSale>()
            .HasOne(ps => ps.Product)
            .WithMany(p => p.ProductSales)
            .HasForeignKey(ps => ps.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductSale>()
            .HasOne(ps => ps.Customer)
            .WithMany(c => c.ProductSales)
            .HasForeignKey(ps => ps.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductSale>()
            .HasOne(ps => ps.User)
            .WithMany(u => u.ProductSales)
            .HasForeignKey(ps => ps.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // DashboardWidget Configuration
        modelBuilder.Entity<DashboardWidget>()
            .HasKey(dw => dw.Id);

        // UserDashboard Configuration
        modelBuilder.Entity<UserDashboard>()
            .HasKey(ud => ud.Id);

        modelBuilder.Entity<UserDashboard>()
            .HasOne(ud => ud.User)
            .WithMany(u => u.UserDashboards)
            .HasForeignKey(ud => ud.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserDashboard>()
            .HasOne(ud => ud.DashboardWidget)
            .WithMany(dw => dw.UserDashboards)
            .HasForeignKey(ud => ud.DashboardWidgetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
