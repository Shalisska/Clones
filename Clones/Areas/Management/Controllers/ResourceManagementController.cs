using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Management.Interfaces;
using Application.Models;
using Application.Data;
using Clones.Models;
using Clones.Util;

namespace Clones.Areas.Management.Controllers
{
    public class ResourceManagementController : Controller
    {
        IResourceManagementService _resourceManagementService;
        IStockManagementService _stockManagementService;

        public ResourceManagementController(
            IResourceManagementService resourceManagementService,
            IStockManagementService stockManagementService)
        {
            _resourceManagementService = resourceManagementService;
            _stockManagementService = stockManagementService;
        }

        private GridViewModel<ResourceModel> GetGridModel(IEnumerable<ResourceModel> resources)
        {
            var model = new GridViewModel<ResourceModel>(resources, "Id");
            var stocks = _stockManagementService.GetStocks();

            var stocksId = new HashSet<int>(resources.Select(r => r.StockId).ToList());
            List<string> stocksWithResourcesNames = new List<string>();
            foreach(var id in stocksId)
                stocksWithResourcesNames.Add(stocks.Where(s=>s.Id==id).Select(s=>s.Name).FirstOrDefault());

            var columns = new List<GridColumnViewModel>
            {
                new GridColumnViewModel("Id", ControlType.Hidden),
                new GridColumnViewModel("StockId", ControlType.Select, new SelectList(stocks, "Id", "Name"), true, columnValues: stocksWithResourcesNames.OrderBy(s => s)),
                new GridColumnViewModel("Name", ControlType.Input, isEditable: true, columnValues: resources.Select(n => n.Name).OrderBy(n => n)),
                new GridColumnViewModel("PriceBase", ControlType.Input),
                new GridColumnViewModel("Price", ControlType.Input)
            };

            model.Columns = columns;

            return model;
        }

        public ActionResult Index()
        {
            var resources = _resourceManagementService.GetResources();
            var model = GetGridModel(resources);
            return View(model);
        }

        public ActionResult GetTable()
        {
            var resources = _resourceManagementService.GetResources();
            var model = GetGridModel(resources);
            return PartialView("EditTable/_EditTableRows", model);
        }

        [HttpPost]
        public ActionResult GetTable(TableQueryParameters parameters)
        {
            var resources = _resourceManagementService.GetParametricalResources(parameters);
            var model = GetGridModel(resources);
            return PartialView("EditTable/_EditTableRows", model);
        }

        [HttpPost]
        public AjaxResult CreateResource(ResourceModel resource)
        {
            if (ModelState.IsValid)
            {
                _resourceManagementService.CreateResource(resource);
                return new AjaxResult(AjaxResultState.OK);
            }

            return new AjaxResult(AjaxResultState.Error);
        }

        [HttpPost]
        public AjaxResult EditResources(IEnumerable<ResourceModel> resources)
        {
            List<ResourceModel> resourcesToUpdate = new List<ResourceModel>();
            List<ResourceModel> errors = new List<ResourceModel>();

            foreach (var resource in resources)
                if (ModelState.IsValid)
                    resourcesToUpdate.Add(resource);
                else
                    errors.Add(resource);

            if (resourcesToUpdate.Count > 0)
                _resourceManagementService.UpdateResources(resourcesToUpdate);

            if (errors.Count > 0)
                return new AjaxResult(AjaxResultState.Error, errors);

            return new AjaxResult(AjaxResultState.OK);
        }

        [HttpPost]
        public AjaxResult DeleteResources(IEnumerable<string> ids)
        {
            var idsInt = ids.Select(i => Int32.Parse(i));

            _resourceManagementService.DeleteResources(idsInt);

            return new AjaxResult(AjaxResultState.OK);
        }

        public ActionResult UpdateResourcesFromXML()
        {
            _resourceManagementService.UpdateFromXML();

            return RedirectToAction("GetTable");
        }
    }
}