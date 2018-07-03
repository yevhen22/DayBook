using DayBook.DataLayer;
using DayBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayBook.Controllers
{
    public class DeleteAccountController : Controller
    {
        private IDataRepository<DeleteUserModel> _db = new DataRepository<DeleteUserModel>();

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteUserModel model)
        {
            model.UserId = User.Identity.GetUserId();
            model.AddedUserTime = DateTime.Now;

            _db.CreateRecord(model);

            return RedirectToAction("Index","Home");
        }
    }
}