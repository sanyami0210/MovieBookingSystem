using MovieBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MovieBookingSystem.Controllers
{
    public class LoginController : Controller
    {
        private DBHelper db = new DBHelper();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            UserModel user = db.Login(model);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(
                    user.User_Name,
                    false);

                Session["User_ID"] =
                    user.User_ID;

                Session["User_Name"] =
                    user.User_Name;

                return RedirectToAction(
                    "Index",
                    "Booking");
            }

            ViewBag.Message =
                "Invalid username or password.";

            return View(model);
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session.Clear();

            return RedirectToAction("Index");
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
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

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
