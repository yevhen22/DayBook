﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DayBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DayBook.Controllers
{
    public class DayBooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DayBooks
        public async Task<ActionResult> Index()
        {
            string currentUserId = User.Identity.GetUserId();
            var daybooks = await db.DayBooks.Where(p => p.ApplicationUserId.Equals(currentUserId)).ToListAsync();
            if (daybooks != null)
            {
                return View(daybooks);
            }
            return View();
        }


        // GET: DayBooks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayBookModel dayBook = await db.DayBooks.FindAsync(id);
            if (dayBook == null)
            {
                return HttpNotFound();
            }
            return View(dayBook);
        }

        // GET: DayBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DayBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DayBoolKey,DayRecord,CreationTime,ApplicationUserId")] DayBookModel dayBook)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                dayBook.ApplicationUser = currentUser;
                dayBook.ApplicationUserId = User.Identity.GetUserId();
                dayBook.CreationTime = DateTime.Now;
                //dayBook.DayRecord = CryptoHelper.Encode(dayBook.DayRecord).ToString();
                db.DayBooks.Add(dayBook);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dayBook);
        }

        // GET: DayBooks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayBookModel dayBook = await db.DayBooks.FindAsync(id);
            if (dayBook == null)
            {
                return HttpNotFound();
            }
            return View(dayBook);
        }

        // POST: DayBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DayBoolKey,DayRecord,CreationTime,ApplicationUserId")] DayBookModel dayBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dayBook).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dayBook);
        }

        // GET: DayBooks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayBookModel dayBook = await db.DayBooks.FindAsync(id);

            if (dayBook == null)
            {
                return HttpNotFound();
            }
            return View(dayBook);
        }

        // POST: DayBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DayBookModel dayBook = db.DayBooks.Find(id);
            if (CheckCreationalTime(dayBook))
            {
                db.DayBooks.Remove(dayBook);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        private bool CheckCreationalTime(DayBookModel dayBook)
        {
            double differenceInDays = (DateTime.Now - dayBook.CreationTime).TotalDays;
            if (differenceInDays > 2)
                return false;
            else
                return true;
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