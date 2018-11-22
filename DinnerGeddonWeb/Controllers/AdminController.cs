using DinnergeddonWeb.AccountServiceReference;
using DinnergeddonWeb.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DinnergeddonWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly AccountServiceReference.AccountServiceClient _proxy;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
            _proxy = new AccountServiceReference.AccountServiceClient();
        }

        public AdminController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        /// <summary>
        /// Initialize User Manager
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
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
                    displayUserModels.Add(new DisplayUserModel { Id = account.Id, Email = account.Email, UserName = account.Username });
            }
            return View(displayUserModels);
        }

        // GET: /Admin/Edit/5
        /// <summary>
        /// Edit User account with chosen id
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Edit(Guid? id)
        {
            //Checks if the id is null and checks for an error if it is.
            if (id == null)
            {
                // TODO: Show error as opposed to returning to Index.
                return RedirectToAction("Index");
            }

            //Safely casts the id to a non-nullable Guid after the check.
            id = (Guid) id;
            
            //Finds the user by id and stores it in an object instance.
            User user = await UserManager.FindByIdAsync(id.ToString());

            //Checks if the user is null and produces an error in the case that it is.
            if (user == null)
            {
                // TODO: Show error as opposed to returning to Index.
                return RedirectToAction("Index");
            }

            //Creates a page with the found user information.
            EditUserModel editUserModel = new EditUserModel { Id = user.UserId, Email = user.Email, UserName = user.UserName};

            return View(editUserModel);
        }

        // POST: /Admin/Edit/5
        /// <summary>
        /// Posts the changes to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                //Retrieves the user object from the database.
                User dbUser = await UserManager.FindByIdAsync(model.StringId);
                if (dbUser != null)
                {
                    //Changes the editable variables.
                    dbUser.UserName = model.UserName;
                    dbUser.Email = model.Email;

                    //Updates the user information in the database.
                    var result = await UserManager.UpdateAsync(dbUser);
                    
                    //Checks that the update was successful.
                    if (result.Succeeded)
                    {
                        //If it was successful it checks if the fields were updated.
                        var changedUser = await UserManager.FindByIdAsync(model.StringId);
                        if (changedUser != null && changedUser.UserName == model.UserName)
                        {
                            //Goes to the Index page upon successful validation.
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            //When an error or unexpected thing occurs it redisplays the page.
            return View(model);
        }
    }
}