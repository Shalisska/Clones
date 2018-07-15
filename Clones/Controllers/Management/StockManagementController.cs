using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Management.Interfaces;
using Application.Models;

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

        [HttpPost]
        public ActionResult EditStock([Bind(Include = "Id,Name")] StockModel stock)
        {
            _stockManagementService.UpdateStock(stock);

            return RedirectToAction("Index");
        }

        public ActionResult CreateStock()
        {
            return PartialView("_CreateStock", new StockModel());
        }

        [HttpPost]
        public ActionResult CreateStock([Bind(Include = "Id,Name")] StockModel stock)
        {
            if (ModelState.IsValid)
            {
                _stockManagementService.CreateStock(stock);
                return RedirectToAction("Index");
            }

            return PartialView("_CreateStock", stock);
        }

        [HttpPost]
        public ActionResult DeleteStock(int id)
        {
            _stockManagementService.DeleteStock(id);
            return RedirectToAction("Index");
        }
    }
}