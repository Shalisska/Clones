using Application.Management.Interfaces;
using Application.Models;
using Clones.Util;
using System.Net;
using System.Web.Mvc;
using Clones.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Clones.Areas.Management.Controllers
{
    public class ProfileManagementController : Controller
    {
        IProfileManagementService _profileManagementService;

        public ProfileManagementController(IProfileManagementService profileManagementService)
        {
            _profileManagementService = profileManagementService;
        }

        #region Profile
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
        #endregion

        #region Account
        private GridViewModel<AccountModel> GetGridModel(IEnumerable<AccountModel> accounts)
        {
            var model = new GridViewModel<AccountModel>(accounts.OrderBy(a => a.ProfileId), "Id");
            var profiles = _profileManagementService.GetProfiles();

            var columns = new List<GridColumnViewModel>
            {
                new GridColumnViewModel("Id", ControlType.Hidden),
                new GridColumnViewModel("ProfileId", ControlType.Select, new SelectList(profiles, "Id", "Name")),
                new GridColumnViewModel("Name", ControlType.Input)
            };

            model.Columns = columns;

            return model;
        }

        public ActionResult IndexAccount()
        {
            var accounts = _profileManagementService.GetAccounts();

            var model = GetGridModel(accounts);

            return View("AccountIndex", model);
        }

        public ActionResult GetAccountTable()
        {
            var accounts = _profileManagementService.GetAccounts();
            var model = GetGridModel(accounts);

            return PartialView("EditTable/_EditTableRows", model);
        }

        [HttpPost]
        public AjaxResult EditAccounts([Bind(Include = "Id,ProfileId,Name")] IEnumerable<AccountModel> accounts)
        {
            foreach(var account in accounts)
            {
                if (!ModelState.IsValid)
                    return new AjaxResult(AjaxResultState.Error);

                _profileManagementService.UpdateAccount(account);
            }

            return new AjaxResult(AjaxResultState.OK);
        }

        [HttpPost]
        public AjaxResult CreateAccount([Bind(Include = "ProfileId,Name")] AccountModel account)
        {
            if (ModelState.IsValid)
            {
                _profileManagementService.CreateAccount(account);
                return new AjaxResult(AjaxResultState.OK);
            }

            return new AjaxResult(AjaxResultState.Error);
        }

        [HttpPost]
        public AjaxResult DeleteAccounts(IEnumerable<string> ids)
        {
            foreach(var id in ids)
                _profileManagementService.DeleteAccount(Int32.Parse(id));

            return new AjaxResult(AjaxResultState.OK);
        }
        #endregion
    }
}