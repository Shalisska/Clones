using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Application.Management.Interfaces;
using Application.Models;
using Clones.Util;

namespace Clones.Controllers.Management
{
    public class StockManagementController : Controller
    {
        IStockManagementService _stockManagementService;

        public StockManagementController(IStockManagementService stockManagementService)
        {
            _stockManagementService = stockManagementService;
        }

        // GET: StockManagement
        public ActionResult Index()
        {
            var stocks = _stockManagementService.GetStocks();
            return View(stocks);
        }

        public ActionResult GetTable()
        {
            var stocks = _stockManagementService.GetStocks();
            return PartialView("_EditTable", stocks);
        }

        [HttpPost]
        public ActionResult EditStock([Bind(Include = "Id,Name")] StockModel stock)
        {
            _stockManagementService.UpdateStock(stock);

            return PartialView("_StockTableRow", stock);
        }

        [HttpPost]
        public AjaxResult CreateStock([Bind(Include = "Id,Name")] StockModel stock)
        {
            if (ModelState.IsValid)
            {
                _stockManagementService.CreateStock(stock);
                return new AjaxResult(AjaxResultState.OK);
            }

            return new AjaxResult(AjaxResultState.Error);
        }

        [HttpPost]
        public ActionResult DeleteStock(int id)
        {
            _stockManagementService.DeleteStock(id);
            return RedirectToAction("Index");
        }
    }
}