using Application.Data.UnitsOfWork;
using Application.Management.Interfaces;
using Application.Models;
using System.Collections.Generic;

namespace Application.Management
{
    public class StockManagementService : IStockManagementService
    {
        IStockUnitOfWork _stockUOW;

        public StockManagementService(IStockUnitOfWork stockUOW)
        {
            _stockUOW = stockUOW;
        }

        public IEnumerable<StockModel> GetStocks()
        {
            var stocks = _stockUOW.Stocks.GetAll();

            List<StockModel> models = new List<StockModel>();

            if (stocks != null)
                foreach (var item in stocks)
                    models.Add(new StockModel(item));

            return models;
        }

        public StockModel GetStock(int id)
        {
            var stock = _stockUOW.Stocks.Get(id);
            return new StockModel(stock);
        }

        public void CreateStock(StockModel stock)
        {
            _stockUOW.Stocks.Create(stock);
            _stockUOW.Save();
        }

        public void UpdateStock(StockModel stock)
        {
            _stockUOW.Stocks.Update(stock);
            _stockUOW.Save();
        }

        public void DeleteStock(int id)
        {
            _stockUOW.Stocks.Delete(id);
            _stockUOW.Save();
        }

        public IEnumerable<MoneyModel> GetMoneys()
        {
            var moneys = _stockUOW.Money.GetAll();
            List<MoneyModel> models = new List<MoneyModel>();

            if (moneys != null)
                foreach (var item in moneys)
                    models.Add(new MoneyModel(item));

            return models;
        }

        public void CreateMoney(MoneyModel money)
        {
            _stockUOW.Money.Create(money);
            _stockUOW.Save();
        }

        public void UpdateMoneys(IEnumerable<MoneyModel> moneys)
        {
            foreach(var money in moneys)
                _stockUOW.Money.Update(money);
            _stockUOW.Save();
        }

        public void DeleteMoneys(IEnumerable<int> ids)
        {
            foreach(var id in ids)
                _stockUOW.Money.Delete(id);
            _stockUOW.Save();
        }

        public void Dispose()
        {
            _stockUOW.Dispose();
        }
    }
}
