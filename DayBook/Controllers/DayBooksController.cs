using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DayBook.Content;
using DayBook.Models;
using DayBook.Models.Pagination;
using DayBook.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace DayBook.Controllers
{
    public class DayBooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1)
        {
            string currentUserId = User.Identity.GetUserId();
            var daybooks = await db.DayBooks.Where(p => p.ApplicationUserId.Equals(currentUserId)).ToListAsync();
            if (daybooks != null)
            {
                if (daybooks.Count > 0)
                    daybooks = DectyptRecords(daybooks);
            }
            int pageSize = 3;
            IEnumerable<DayBookModel> recordsPerPage = daybooks.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = daybooks.Count };
            Models.Pagination.IndexViewModel ivm = new Models.Pagination.IndexViewModel { PageInfo = pageInfo, Records = recordsPerPage };
            return View(ivm);
        }

        /// <summary>
        /// Get all coincidences that found in the database records 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<ActionResult> SearchedItems(string searchString, int page = 1)
        {
            List<DayBookModel> Mathes = new List<DayBookModel>();
            IEnumerable<DayBookModel> matchedRecords;
            string currentUserId = User.Identity.GetUserId();
            var daybooks = await db.DayBooks.Where(p => p.ApplicationUserId.Equals(currentUserId)).ToListAsync();
            if (daybooks != null)
            {
                if (daybooks.Count > 0)
                    daybooks = DectyptRecords(daybooks);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                matchedRecords = daybooks.Where(p => p.DayRecord.Contains(searchString));
                Mathes = matchedRecords.ToList();
            }

            int pageSize = 3;
            IEnumerable<DayBookModel> recordsPerPage = Mathes.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Mathes.Count };
            Models.Pagination.IndexViewModel ivm = new Models.Pagination.IndexViewModel { PageInfo = pageInfo, Records = recordsPerPage };
            return View(ivm);
        }

        /// <summary>
        /// Decode day record row from database to initial view
        /// </summary>
        /// <param name="dayBooks"></param>
        /// <returns></returns>
        private List<DayBookModel> DectyptRecords(List<DayBookModel> dayBooks)
        {
            for (int i = 0; i < dayBooks.Count; i++)
            {
                dayBooks[i].DayRecord = Transposition.GetDecryptedString(dayBooks[i].DayRecord);
            }
            return dayBooks;
        }



       /// <summary>
       /// Get record details and show full data description which all images
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayRecordViewModel viewModel = new DayRecordViewModel();
            DayBookModel dayBook = await db.DayBooks.FindAsync(id);
            var Images = db.ImageModels.Where(p => p.DayBookModelId == dayBook.DayBookModelId);
            viewModel.DayRecord = Transposition.GetDecryptedString(dayBook.DayRecord);
            viewModel.ImageModels = await db.ImageModels.Where(p => p.DayBookModelId.Equals(dayBook.DayBookModelId)).ToListAsync(); ;

            if (dayBook == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // GET: DayBooks/Create
        public ActionResult Create()
        {
            return View();
        }

       
        /// <summary>
        /// Create new record with day description and one image 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="uploadImage"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DayRecordViewModel record, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                DayBookModel dayBook = new DayBookModel();
                dayBook.ApplicationUser = currentUser;
                dayBook.ApplicationUserId = User.Identity.GetUserId();
                dayBook.CreationTime = DateTime.Now;
                dayBook.DayRecord = Transposition.GetEncryptedString(record.DayRecord);
                db.DayBooks.Add(dayBook);

                ImageModel imageModel = new ImageModel();
                byte[] imageData;
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }

                imageModel.ImageTitle = record.ImageTitle;
                imageModel.ImageByte = imageData;
                imageModel.DayBookModel = dayBook;
                db.ImageModels.Add(imageModel);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(record);
        }


        // GET: DayBooks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayBookModel dayBook = await db.DayBooks.FindAsync(id);
            dayBook.DayRecord = Transposition.GetDecryptedString(dayBook.DayRecord);
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
                dayBook.DayRecord = Transposition.GetEncryptedString(dayBook.DayRecord);
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
            dayBook.DayRecord = Transposition.GetDecryptedString(dayBook.DayRecord);
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

        /// <summary>
        /// Check whether expired two days from creation time
        /// </summary>
        /// <param name="dayBook"></param>
        /// <returns></returns>
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
