using Application.Models;
using System.Collections.Generic;

namespace Application.Management.Interfaces
{
    public interface IStockManagementService
    {
        IEnumerable<StockModel> GetStocks();

        void CreateStock(StockModel stock);
        void UpdateStock(StockModel stock);

        void DeleteStock(int id);

        void Dispose();
    }
}
