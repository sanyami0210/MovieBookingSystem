using MovieBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieBookingSystem.Controllers
{
    public class MovieController : Controller
    {
        private DBHelper db = new DBHelper();
        // GET: Movie
        public ActionResult Index()
        {
            var movies = db.GetMovies();

            return View(movies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            MovieModel model = new MovieModel();

            model.CatList = db.GetCategories();

            return View(model);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CatList = db.GetCategories();
                return View(model);
            }
            try
            {
                if (db.AddMovie(model))
                {
                    TempData["Message"] =
                        "Movie added successfully.";

                    return RedirectToAction("Index");
                }

                ViewBag.Message =
                    "Movie could not be added.";

                model.CatList = db.GetCategories();

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                model.CatList = db.GetCategories();

                return View(model);
            }
        }
        public JsonResult GetMoviesByCategory(int catId)
        {
            var movies =
                db.GetMoviesByCategory(catId);

            return Json(
                movies,
                JsonRequestBehavior.AllowGet);
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
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

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
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
