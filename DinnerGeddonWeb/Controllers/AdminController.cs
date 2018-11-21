using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinnergeddonWeb.AccountServiceReference;
using DinnergeddonWeb.Models;

namespace DinnergeddonWeb.Controllers
{
    [Authorize(Roles = "admin")]
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
            IEnumerable<Account> accounts = _proxy.GetAccounts();

            ICollection<DisplayUserModel> displayUserModels = new List<DisplayUserModel>();
            foreach (Account account in accounts)
            {

                displayUserModels.Add(new DisplayUserModel { Email = account.Email, UserName = account.Username });
            }
            return View(displayUserModels);
        }


    }
}