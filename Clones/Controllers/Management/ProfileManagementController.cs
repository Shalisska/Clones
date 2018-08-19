using Application.Management.Interfaces;
using Application.Models;
using Clones.Util;
using System.Net;
using System.Web.Mvc;
using Clones.Models;
using System.Collections.Generic;
using System;

namespace Clones.Controllers.Management
{
    public class ProfileManagementController : Controller
    {
        IProfileManagementService _profileManagementService;

        public ProfileManagementController(IProfileManagementService profileManagementService)
        {
            _profileManagementService = profileManagementService;
        }

        private GridViewModel<ProfileModel> GetGridModel(IEnumerable<ProfileModel> profiles)
        {
            var model = new GridViewModel<ProfileModel>(profiles, "Id");

            var columns = new List<GridColumnViewModel>
            {
                new GridColumnViewModel("Id", ControlType.Hidden),
                new GridColumnViewModel("Name", ControlType.Input)
            };

            model.Columns = columns;

            return model;
        }

        // GET: ProfileManagement
        public ActionResult Index()
        {
            var profiles = _profileManagementService.GetProfiles();
            var model = GetGridModel(profiles);
            return View(model);
        }

        public ActionResult GetTable()
        {
            var profiles = _profileManagementService.GetProfiles();
            var model = GetGridModel(profiles);
            return PartialView("EditTable/_EditTableRows", model);
        }

        [HttpPost]
        public AjaxResult EditProfiles([Bind(Include = "Id,Name")] IEnumerable<ProfileModel> profiles)
        {
            foreach(var profile in profiles)
            {
                if (!ModelState.IsValid)
                    return new AjaxResult(AjaxResultState.Error);

                _profileManagementService.UpdateProfile(profile);
            }
            return new AjaxResult(AjaxResultState.OK);
        }

        [HttpPost]
        public AjaxResult CreateProfile([Bind(Include = "Name")] ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                _profileManagementService.CreateProfile(profile);
                return new AjaxResult(AjaxResultState.OK);
            }

            return new AjaxResult(AjaxResultState.Error);
        }

        [HttpPost]
        public AjaxResult DeleteProfiles(IEnumerable<string> ids)
        {
            foreach(var id in ids)
                _profileManagementService.DeleteProfile(Int32.Parse(id));

            return new AjaxResult(AjaxResultState.OK);
        }

        public ActionResult IndexAccount()
        {
            var accounts = _profileManagementService.GetAccounts();

            BaseViewModel<AccountModel> model = new BaseViewModel<AccountModel> { ModelList = accounts };
            model.Add("Profiles", _profileManagementService.GetProfiles());

            return View("AccountIndex", model);
        }

        public ActionResult GetAccountTable()
        {
            var accounts = _profileManagementService.GetAccounts();

            BaseViewModel<AccountModel> model = new BaseViewModel<AccountModel> { ModelList = accounts };
            model.Add("Profiles", _profileManagementService.GetProfiles());

            return PartialView("_EditAccountTable", model);
        }

        [HttpPost]
        public ActionResult EditAccount([Bind(Include = "Id,ProfileId,Name")] AccountModel account)
        {
            _profileManagementService.UpdateAccount(account);

            ViewData["profiles"] = _profileManagementService.GetProfiles();

            return PartialView("_AccountTableRow", account);
        }

        public ActionResult CreateAccount()
        {
            BaseViewModel<AccountModel> model = new BaseViewModel<AccountModel> {
                ModelList = new List<AccountModel> { new AccountModel() }
            };

            model.Add("Profiles", _profileManagementService.GetProfiles());

            return PartialView("_CreateAccount", model);
        }

        [HttpPost]
        public AjaxResult CreateAccount([Bind(Include = "Id,ProfileId,Name")] AccountModel account)
        {
            if (ModelState.IsValid)
            {
                _profileManagementService.CreateAccount(account);
                return new AjaxResult(AjaxResultState.OK);
            }

            return new AjaxResult(AjaxResultState.Error);
        }

        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            _profileManagementService.DeleteAccount(id);
            return RedirectToAction("Index");
        }
    }
}