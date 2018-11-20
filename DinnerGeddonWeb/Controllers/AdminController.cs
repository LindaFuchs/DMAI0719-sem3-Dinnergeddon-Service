using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinnergeddonWeb.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly AccountServiceReference.AccountServiceClient _proxy;

        public AdminController()
        {
            this._proxy = new AccountServiceReference.AccountServiceClient();
        }

        // GET: Admin
        public ActionResult Index()
        {
            _proxy.GetA

            return View();
        }


    }
}