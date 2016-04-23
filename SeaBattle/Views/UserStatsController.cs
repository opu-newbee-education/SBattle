using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SeaBattle.Models;

namespace SeaBattle.Views
{
    public class UserStatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserStats
        public ActionResult Index()
        {
            var userStats = db.UserStats.Include(u => u.User);
            return View(userStats.ToList());
        }

        // GET: UserStats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStats userStats = db.UserStats.Find(id);
            if (userStats == null)
            {
                return HttpNotFound();
            }
            return View(userStats);
        }

        // GET: UserStats/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: UserStats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,GamesPlayed,GamesWon,TotalPlayTime,LongestTimePlayed,ShortestTimePlayed")] UserStats userStats)
        {
            if (ModelState.IsValid)
            {
                db.UserStats.Add(userStats);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", userStats.UserId);
            return View(userStats);
        }

        // GET: UserStats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStats userStats = db.UserStats.Find(id);
            if (userStats == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", userStats.UserId);
            return View(userStats);
        }

        // POST: UserStats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,GamesPlayed,GamesWon,TotalPlayTime,LongestTimePlayed,ShortestTimePlayed")] UserStats userStats)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userStats).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", userStats.UserId);
            return View(userStats);
        }

        // GET: UserStats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStats userStats = db.UserStats.Find(id);
            if (userStats == null)
            {
                return HttpNotFound();
            }
            return View(userStats);
        }

        // POST: UserStats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserStats userStats = db.UserStats.Find(id);
            db.UserStats.Remove(userStats);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
