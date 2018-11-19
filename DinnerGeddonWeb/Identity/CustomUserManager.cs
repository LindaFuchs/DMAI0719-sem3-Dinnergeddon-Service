using DinnergeddonWeb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DinnergeddonWeb.Identity
{
    public class CustomUserManager : UserManager<User>
    {
        public CustomUserManager() : base(new UserStore()) { }

        public override async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword,
            string newPassword)
        {
           // ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
                    userId));
            }
            if (await VerifyPasswordAsync(passwordStore, user, currentPassword).WithCurrentCulture())
            {
                var result = await UpdatePassword(passwordStore, user, newPassword).WithCurrentCulture();
                if (!result.Succeeded)
                {
                    return result;
                }
                return await UpdateAsync(oldUser, user).WithCurrentCulture();
            }
            return IdentityResult.Failed(Resources.PasswordMismatch);
        }
    }
}