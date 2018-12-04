using DinnergeddonWeb.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinnergeddonWeb.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string apiKey = ConfigurationManager.AppSettings["ApiKey"];//Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
                    var client = new SendGridClient(apiKey);
                    var from = new EmailAddress(vm.Email);
                    var subject = "contact form";
                    var to = new EmailAddress(ConfigurationManager.AppSettings["Receiver"]);
                    var message = vm.Message;

                    SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

                    // Send the email.
                    if (client != null)
                    {
                        client.SendEmailAsync(msg);
                    }



                    ModelState.Clear();
                    ViewBag.Message = "Thank you for getting in touch! We will get back to you as soon as possible. ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing a problem here {ex.Message}";
                }
            }

            return View();
        }

    }

}
