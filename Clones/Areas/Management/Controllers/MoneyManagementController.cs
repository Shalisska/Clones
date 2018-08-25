using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Management.Interfaces;
using Application.Models;
using Clones.Models;
using Clones.Util;

namespace Clones.Areas.Management.Controllers
{
    public class MoneyManagementController : Controller
    {
        IStockManagementService _stockManagementService;

        public MoneyManagementController(IStockManagementService stockManagementService)
        {
            _stockManagementService = stockManagementService;
        }

        private GridViewModel<MoneyModel> GetGridViewModel(IEnumerable<MoneyModel> money)
        {
            var model = new GridViewModel<MoneyModel>(money);
            var stocks = _stockManagementService.GetStocks();

            var columns = new List<GridColumnViewModel>
            {
                new GridColumnViewModel("Id"),
                new GridColumnViewModel("StockId", ControlType.Select, new SelectList(stocks, "Id", "Name")),
                new GridColumnViewModel("Name"),
                new GridColumnViewModel("BuyPrice"),
                new GridColumnViewModel("SellPrice")
            };

            model.Columns = columns;

            return model;
        }

        public ActionResult Index()
        {
            var money = _stockManagementService.GetMoneys();
            var model = GetGridViewModel(money);
            return View(model);
        }

        public ActionResult GetTable()
        {
            var money = _stockManagementService.GetMoneys();
            var model = GetGridViewModel(money);
            return PartialView("EditTable/_EditTableRows", model);
        }

        [HttpPost]
        public AjaxResult CreateMoney(MoneyModel money)
        {
            if (ModelState.IsValid)
            {
                _stockManagementService.CreateMoney(money);
                return new AjaxResult(AjaxResultState.OK);
            }

            return new AjaxResult(AjaxResultState.Error);
        }

        [HttpPost]
        public AjaxResult EditMoney(IEnumerable<MoneyModel> money)
        {
            List<MoneyModel> moneyToUpdate = new List<MoneyModel>();
            List<MoneyModel> errors = new List<MoneyModel>();

            foreach (var m in money)
                if (ModelState.IsValid)
                    moneyToUpdate.Add(m);
                else
                    errors.Add(m);

            if (moneyToUpdate.Count > 0)
                _stockManagementService.UpdateMoneys(moneyToUpdate);

            if (errors.Count > 0)
                return new AjaxResult(AjaxResultState.Error, errors);

            return new AjaxResult(AjaxResultState.OK);
        }

        [HttpPost]
        public AjaxResult DeleteMoney(IEnumerable<string> ids)
        {
            var idsInt = ids.Select(i => Int32.Parse(i));

            _stockManagementService.DeleteMoneys(idsInt);

            return new AjaxResult(AjaxResultState.OK);
        }
    }
}