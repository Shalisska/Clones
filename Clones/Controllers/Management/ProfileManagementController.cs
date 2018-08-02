using Application.Management.Interfaces;
using Application.Models;
using Clones.Util;
using System.Net;
using System.Web.Mvc;
using Clones.Models;
using System.Collections.Generic;

namespace Clones.Controllers.Management
{
    public class ProfileManagementController : Controller
    {
        IProfileManagementService _profileManagementService;

        public ProfileManagementController(IProfileManagementService profileManagementService)
        {
            _profileManagementService = profileManagementService;
        }

        // GET: ProfileManagement
        public ActionResult Index()
        {
            var profiles = _profileManagementService.GetProfiles();
            return View(profiles);
        }

        public ActionResult GetTable()
        {
            var profiles = _profileManagementService.GetProfiles();
            return PartialView("_EditTable", profiles);
        }

        [HttpPost]
        public ActionResult EditProfile([Bind(Include = "Id,Name")] ProfileModel profile)
        {
            _profileManagementService.UpdateProfile(profile);

            return PartialView("_ProfileTableRow", profile);
        }

        [HttpPost]
        public AjaxResult CreateProfile([Bind(Include = "Id,Name")] ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                _profileManagementService.CreateProfile(profile);
                return new AjaxResult(AjaxResultState.OK);
            }

            return new AjaxResult(AjaxResultState.Error);
        }

        [HttpPost]
        public ActionResult DeleteProfile(int id)
        {
            _profileManagementService.DeleteProfile(id);
            return RedirectToAction("Index");
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