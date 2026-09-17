using MovieBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieBookingSystem.Controllers
{
    public class ProfileController : Controller
    {
        private DBHelper db = new DBHelper();
        // GET: Profile
        public ActionResult Index()
        {
            int userId =Convert.ToInt32(Session["User_ID"]);

            UserModel model =
                db.GetUser(userId);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.User_ID =
                Convert.ToInt32(Session["User_ID"]);

            if (db.UpdateUser(model))
            {
                Session["User_Name"] =
                    model.User_Name;

                TempData["Message"] =
                    "Profile updated successfully.";

                return RedirectToAction("Index");
            }

            ViewBag.Message =
                "Profile update failed.";

            return View(model);
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
