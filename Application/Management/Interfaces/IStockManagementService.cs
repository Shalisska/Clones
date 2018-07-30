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

        void Dispose();
    }
}
