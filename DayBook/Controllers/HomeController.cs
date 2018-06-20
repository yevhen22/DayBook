using DayBook.Content;
using DayBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DayBook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Contact()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("burdash22@gmail.com", "Yevhen");
                    var receiverEmail = new MailAddress(model.EmailAdress, "Receiver");
                    var password = "yevhen_987654";
                    var sub = "invitation";
                    var body = model.Message + "\n" + ConstHelper.RegistrationLinkTemplate + TokenHelper.GenerateToken();
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = "Invitation",
                        Body = body
                    })
                    {
                        await Task.Run(() => smtp.Send(mess));
                        model = null;
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }
    }
}