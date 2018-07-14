using Application.Management.Interfaces;
using Application.Models;
using System.Net;
using System.Web.Mvc;

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

        [HttpPost]
        public ActionResult EditProfile([Bind(Include = "Id,Name")] ProfileModel profile)
        {
            _profileManagementService.UpdateProfile(profile);

            return RedirectToAction("Index");
        }

        public ActionResult CreateProfile()
        {
            return PartialView("_CreateProfile", new ProfileModel());
        }

        [HttpPost]
        public ActionResult CreateProfile([Bind(Include = "Id,Name")] ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                _profileManagementService.CreateProfile(profile);
                return RedirectToAction("Index");
            }

            return PartialView("_CreateProfile", profile);
            //return Content("Error");
        }

        [HttpPost]
        public ActionResult DeleteProfile(int id)
        {
            _profileManagementService.DeleteProfile(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditAccount([Bind(Include = "Id,ProfileId,Name")] AccountModel account)
        {
            _profileManagementService.UpdateAccount(account);

            return RedirectToAction("Index");
        }

        public ActionResult CreateAccount(int id)
        {
            return PartialView("_CreateAccount", new AccountModel() { ProfileId = id});
        }

        [HttpPost]
        public ActionResult CreateAccount([Bind(Include = "Id,ProfileId,Name")] AccountModel account)
        {
            if (ModelState.IsValid)
            {
                _profileManagementService.CreateAccount(account);
                return RedirectToAction("Index");
            }

            return PartialView("_CreateAccount", account);
        }

        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            _profileManagementService.DeleteAccount(id);
            return RedirectToAction("Index");
        }
    }
}