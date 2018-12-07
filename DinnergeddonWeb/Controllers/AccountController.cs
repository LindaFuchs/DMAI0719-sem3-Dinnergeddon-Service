using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DinnergeddonWeb.Models;
using DinnergeddonWeb.Identity;
using Microsoft.Owin.Security.DataProtection;

namespace DinnergeddonWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public UserManager<User> UserManager { get; private set; }
        private ApplicationSignInManager _signInManager;

        public AccountController()
            : this(new UserManager<User>(new UserStore()))

        {
        }

        public AccountController(UserManager<User> userManager)
        {
            var provider = new DpapiDataProtectionProvider("DinnergeddonWeb");
            userManager.UserTokenProvider = new DataProtectorTokenProvider<User>(provider.Create("SampleTokenName"));
            userManager.EmailService = new EmailService();
            UserManager = userManager;

            //  SignInManager = signInManager;

        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index", "Home");
            return View();

            //    ViewBag.ReturnUrl = returnUrl;
            //    return View();
            //
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            //This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //    switch (result)
            //    {
            //        case SignInStatus.Success:
            //            return RedirectToLocal(returnUrl);
            //        case SignInStatus.LockedOut:
            //            return View("Lockout");
            //        case SignInStatus.RequiresVerification:
            //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //        case SignInStatus.Failure:
            //        default:
            //            ModelState.AddModelError("", "Invalid login attempt.");
            //            return View(model);
            //    }
            //}

            if (ModelState.IsValid)
            {
                string userName = model.Email;
                var userForEmail = await UserManager.FindByEmailAsync(model.Email);
                if (userForEmail != null)
                {
                    userName = userForEmail.UserName;
                }

                var user = await UserManager.FindAsync(userName, model.Password);
                if (user != null)
                {
                    if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                    {
                        ModelState.AddModelError("", "You need to confirm your email.");
                        return View(model);
                    }

                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "invalid email or password.");
                }
            }

            // if we got this far, something failed, redisplay form
            return View(model);
        }





        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User() { UserId = Guid.NewGuid(), Email = model.Email, UserName = model.UserName };
                var findUser = UserManager.FindByEmail(model.Email);
                if (findUser != null)
                {
                    ModelState.AddModelError("", "Email already taken");
                }
                else
                {
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded && findUser == null)
                    {

                        //  await SignInAsync(user, isPersistent: false);
                        // return RedirectToAction("Index", "Home");

                        //email confirm
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        string htmlMessage = "<div style=\"background:#fbfbfb; padding:15px;font-family:Segoe UI\"> <h1 style=\"color:black \">Confirm your email</h1> <p style=\"color:black \"> Please click on the button below to confirm your email. </p> <br> <a href=" + callbackUrl+" style=\"background:#c36800; color:white; padding:14px; text-decoration: none;\">Confirm your email</a> </div>";
                        await UserManager.SendEmailAsync(user.Id,
                          "Confirm your account",
                           htmlMessage);
                        ////return to email confirm view
                        return View("DisplayEmail");
    }
                    else
                    {
                        AddErrors(result);
}
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        //
        // POST: /Account/LogOff
        [HttpPost]
[ValidateAntiForgeryToken]
public ActionResult LogOff()
{

    AuthenticationManager.SignOut();
    return RedirectToAction("Index", "Home");
}




private async Task SignInAsync(User user, bool isPersistent)
{
    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
}

private void AddErrors(IdentityResult result)
{
    foreach (var error in result.Errors)
    {
        ModelState.AddModelError("", error);
    }
}

private bool HasPassword()
{
    var user = UserManager.FindById(User.Identity.GetUserId());
    if (user != null)
    {
        return user.PasswordHash != null;
    }
    return false;
}

public enum ManageMessageId
{
    ChangePasswordSuccess,
    SetPasswordSuccess,
    RemoveLoginSuccess,
    Error
}

private ActionResult RedirectToLocal(string returnUrl)
{
    if (Url.IsLocalUrl(returnUrl))
    {
        return Redirect(returnUrl);
    }
    else
    {
        return RedirectToAction("Index", "Home");
    }
}






// GET: /Account/ConfirmEmail
[AllowAnonymous]
public async Task<ActionResult> ConfirmEmail(string userId, string code)
{
    if (userId == null || code == null)
    {
        return View("Error");
    }
    var result = await UserManager.ConfirmEmailAsync(userId, code);
    return View(result.Succeeded ? "ConfirmEmail" : "Error");
}

//
// GET: /Account/ForgotPassword
[AllowAnonymous]
public ActionResult ForgotPassword()
{
    return View();
}

//
// POST: /Account/ForgotPassword
[HttpPost]
[AllowAnonymous]
[ValidateAntiForgeryToken]
public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
{
    if (ModelState.IsValid)
    {
        var user = await UserManager.FindByEmailAsync(model.Email);
        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        {
            // Don't reveal that the user does not exist or is not confirmed
            return View("ForgotPasswordConfirmation");
        }

              
         string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
}

//
// GET: /Account/ForgotPasswordConfirmation
[AllowAnonymous]
public ActionResult ForgotPasswordConfirmation()
{
    return View();
}

//
// GET: /Account/ResetPassword
[AllowAnonymous]
public ActionResult ResetPassword(string code)
{
    return code == null ? View("Error") : View();
}

//
// POST: /Account/ResetPassword
[HttpPost]
[AllowAnonymous]
[ValidateAntiForgeryToken]
public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }
    var user = await UserManager.FindByEmailAsync(model.Email);
    if (user == null)
    {
        // Don't reveal that the user does not exist
        return RedirectToAction("ResetPasswordConfirmation", "Account");
    }
    var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
    if (result.Succeeded)
    {
        return RedirectToAction("ResetPasswordConfirmation", "Account");
    }
    AddErrors(result);
    return View();
}

//
// GET: /Account/ResetPasswordConfirmation
[AllowAnonymous]
public ActionResult ResetPasswordConfirmation()
{
    return View();
}


//
// GET: /Account/SendCode
[AllowAnonymous]
public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
{
    var userId = await SignInManager.GetVerifiedUserIdAsync();
    if (userId == null)
    {
        return View("Error");
    }
    var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
}


// POST: /Account/SendCode
[HttpPost]
[AllowAnonymous]
[ValidateAntiForgeryToken]
public async Task<ActionResult> SendCode(SendCodeViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View();
    }

    // Generate the token and send it
    if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
    {
        return View("Error");
    }
    return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
}



protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        if (UserManager != null)
        {
            UserManager.Dispose();
            UserManager = null;
        }

        if (_signInManager != null)
        {
            _signInManager.Dispose();
            _signInManager = null;
        }
    }

    base.Dispose(disposing);
}


    }
}