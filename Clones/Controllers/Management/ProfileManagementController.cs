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
        }

        [HttpPost]
        public ActionResult DeleteProfile(int id)
        {
            _profileManagementService.DeleteProfile(id);
            return RedirectToAction("Index");
        }
    }
}