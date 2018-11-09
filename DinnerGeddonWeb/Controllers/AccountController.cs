using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinnerGeddonWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Display the register page and form
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            ViewBag.Message = "You're on the register page now!";
            return View();
        }
    }
}