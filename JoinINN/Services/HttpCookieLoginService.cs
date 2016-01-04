using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace JoinINN.Services
{
    public class HttpCookieLoginService
    {
        public string TryGetSignedInUserId()
        {
            if (HttpContext.Current.User == null) { return null; }

            var identity = HttpContext.Current.User.Identity;
            if (!identity.IsAuthenticated) { return null; }

            var saltedUserUid = identity.Name;
            var username = saltedUserUid.Substring(saltedUserUid.LastIndexOf('#') + 1);

            return username;
        }

        public void SignIn(string username, bool isSessionLong)
        {
            var cookieValue = string.Format(username);
            FormsAuthentication.SetAuthCookie(cookieValue, true);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}