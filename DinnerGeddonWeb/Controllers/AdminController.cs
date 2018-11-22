using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DinnergeddonWeb.AccountServiceReference;
using DinnergeddonWeb.Models;
using Microsoft.AspNet.Identity.Owin;

namespace DinnergeddonWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly AccountServiceReference.AccountServiceClient _proxy;
        private ApplicationUserManager _userManager;

        public AdminController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            _proxy = new AccountServiceReference.AccountServiceClient();
        }

        /// <summary>
        /// Initialize User Manager
        /// </summary>
        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        // GET: Admin
        /// <summary>
        /// List all user accounts
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IEnumerable<Account> accounts = _proxy.GetAccounts();

            // Create a list with just user accounts, not including accounts with Admin role
            ICollection<DisplayUserModel> displayUserModels = new List<DisplayUserModel>();
            foreach (Account account in accounts)
            {
                if(_proxy.IsInRole(account.Id, "admin")) {
                    displayUserModels.Add(new DisplayUserModel { Email = account.Email, UserName = account.Username });
                }
                
            }
            return View(displayUserModels);
        }

        // GET: /Admin/Edit/5
        /// <summary>
        /// Edit User account with chosen id
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int? id) {
            if(id == null) {
                return RedirectToAction("Index");
            }

        }


    }
}