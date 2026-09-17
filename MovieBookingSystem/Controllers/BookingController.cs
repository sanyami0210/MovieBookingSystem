using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieBookingSystem.Models
{
    public class BookingController : Controller
    {
        private DBHelper db = new DBHelper();
        // GET: booking
        public ActionResult Index()
        {
            int userId =
                Convert.ToInt32(Session["User_ID"]);

            var bookings =
                db.GetUserBookings(userId);

            return View(bookings);
        }

        // GET: booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: booking/Create
        public ActionResult Create()
        {
            BookingModel model =
                new BookingModel();

            model.CatList =
                db.GetCategories();

            return View(model);
        }

        // POST: booking/Create
        [HttpPost]
        public ActionResult Create(BookingModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CatList =
                    db.GetCategories();

                return View(model);
            }

            model.User_ID =
                Convert.ToInt32(Session["User_ID"]);
            try
            {
                if (db.AddBooking(model))
                {
                    TempData["Message"] =
                        "Booking successful.";

                    return RedirectToAction("Index");
                }

                ViewBag.Message =
                    "Booking failed.";

                model.CatList =
                    db.GetCategories();

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

                model.CatList =
                    db.GetCategories();

                return View(model);
            }
        }
        public JsonResult GetMovies(int catId)
        {
            var movies =
                db.GetMoviesForCategory(catId);

            return Json(
                movies,
                JsonRequestBehavior.AllowGet);
        }

        // GET: booking/Edit/5
        public ActionResult Edit(int id)
        {
            int userId =
        Convert.ToInt32(Session["User_ID"]);

            BookingModel model =
                db.GetBooking(id, userId);

            if (model == null)
            {
                return HttpNotFound();
            }

            model.CatList =
                db.GetCategories();

            if (model.Cat_ID > 0)
            {
                model.MovieList =
                    db.GetMoviesForCategory(
                        model.Cat_ID);
            }

            return View(model);
        }

        // POST: booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BookingModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CatList =
                    db.GetCategories();

                model.MovieList =
                    db.GetMoviesForCategory(
                        model.Cat_ID);

                return View(model);
            }

            model.User_ID =
                Convert.ToInt32(Session["User_ID"]);
            if (db.UpdateBooking(model))
            {
                TempData["Message"] =
                    "Booking updated successfully.";

                return RedirectToAction("Index");
            }

            ViewBag.Message =
                "Booking update failed.";

            model.CatList =
                db.GetCategories();

            model.MovieList =
                db.GetMoviesForCategory(
                    model.Cat_ID);

            return View(model);
        }

        // GET: booking/Delete/5
        public ActionResult Delete(int id)
        {
            int userId =
        Convert.ToInt32(Session["User_ID"]);

            BookingModel model =
                db.GetBooking(id, userId);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            int userId =
        Convert.ToInt32(Session["User_ID"]);

            if (db.DeleteBooking(id, userId))
            {
                TempData["Message"] =
                    "Booking deleted successfully.";
            }

            return RedirectToAction("Index");
        }
    }
}
