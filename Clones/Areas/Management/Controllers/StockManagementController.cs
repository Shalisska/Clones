using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Application.Management.Interfaces;
using Application.Models;
using Clones.Models;
using Clones.Util;
using Newtonsoft.Json;

namespace Clones.Areas.Management.Controllers
{
    public class StockManagementController : Controller
    {
        IStockManagementService _stockManagementService;

        public StockManagementController(IStockManagementService stockManagementService)
        {
            _stockManagementService = stockManagementService;
        }

        private GridViewModel<StockModel> GetGridModel(IEnumerable<StockModel> stocks)
        {
            var model = new GridViewModel<StockModel>(stocks, "Id");

            var columns = new List<GridColumnViewModel>
            {
                new GridColumnViewModel("Id", ControlType.Hidden),
                new GridColumnViewModel("Name", ControlType.Input)
            };

            model.Columns = columns;

            return model;
        }

        // GET: StockManagement
        public ActionResult Index()
        {
            var stocks = _stockManagementService.GetStocks();

            var model = GetGridModel(stocks);

            return View(model);
        }

        public ActionResult GetTable()
        {
            var stocks = _stockManagementService.GetStocks();
            var model = GetGridModel(stocks);
            return PartialView("EditTable/_EditTableRows", model);
        }

        [HttpPost]
        public AjaxResult EditStocks([Bind(Include = "Id,Name")] IEnumerable<StockModel> stocks)
        {
            foreach (var stock in stocks)
            {
                if (!ModelState.IsValid)
                    return new AjaxResult(AjaxResultState.Error);

                _stockManagementService.UpdateStock(stock);
            }
                
            return new AjaxResult(AjaxResultState.OK);
        }

        [HttpPost]
        public AjaxResult CreateStock([Bind(Include = "Name")] StockModel stock)
        {
            if (ModelState.IsValid)
            {
                _stockManagementService.CreateStock(stock);
                return new AjaxResult(AjaxResultState.OK);
            }

            return new AjaxResult(AjaxResultState.Error);
        }

        [HttpPost]
        public AjaxResult DeleteStocks(IEnumerable<string> ids)
        {
            foreach (var id in ids)
                _stockManagementService.DeleteStock(Int32.Parse(id));

            return new AjaxResult(AjaxResultState.OK);
        }
    }
}