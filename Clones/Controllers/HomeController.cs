using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Application.Models;
using Application.Services.Interfaces;

namespace Clones.Controllers
{
    public class HomeController : Controller
    {
        IProfileService _profileService;

        public HomeController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var profiles = _profileService.GetProfiles();
            return View(profiles);
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var profile = _profileService.GetProfile(id.Value);

            if (profile == null)
                return HttpNotFound();

            return View(profile);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View(new ProfileModel());
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name")] ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                _profileService.CreateProfile(profile);
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ProfileModel profile = _profileService.GetProfile(id.Value);

            if (profile == null)
                return HttpNotFound();

            return View(profile);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name")] ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                _profileService.UpdateProfile(profile);
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ProfileModel profile = _profileService.GetProfile(id.Value);

            if (profile == null)
                return HttpNotFound();

            return View(profile);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _profileService.DeleteProfile(id);
            return RedirectToAction("Index");
        }
    }
}
