using Application.Models;
using System.Collections.Generic;

namespace Application.Management.Interfaces
{
    public interface IStockManagementService
    {
        IEnumerable<StockModel> GetStocks();
        StockModel GetStock(int id);

        void CreateStock(StockModel stock);
        void UpdateStock(StockModel stock);

        void DeleteStock(int id);

        IEnumerable<MoneyModel> GetMoneys();

        void CreateMoney(MoneyModel money);
        void UpdateMoneys(IEnumerable<MoneyModel> moneys);

        void DeleteMoneys(IEnumerable<int> ids);

        void Dispose();
    }
}
