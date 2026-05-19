using LuckyDay.Api.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class DashboardService(IUnitOfWork uow)
{
  public async Task<IEnumerable<UserSalesTotalResponseModel>> GetTopUserSalesByYearAsync(int year, int take)
  {
    var results = await uow.ProductSales.GetTopUserSalesByYearAsync(year, take);
    return results.Select(r => new UserSalesTotalResponseModel(r.Item1, r.Item2, r.Item3, r.Item4));
  }
}
