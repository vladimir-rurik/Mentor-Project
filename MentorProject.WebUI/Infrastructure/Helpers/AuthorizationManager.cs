using MentorProject.WebUI.Infrastructure.Abstract;
using MentorProject.WebUI.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentorProject.WebUI.Infrastructure.Helpers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private ICookieSetup _cookieSetup;
        private ISessionManager _sessionManager;
        public AuthorizationManager(ICookieSetup cookieSetup, ISessionManager sessionManager)
        {
            _cookieSetup = cookieSetup;
            _sessionManager = sessionManager;
        }

		public bool IsAuthorized
		{
			get
			{
				return _sessionManager.IsLoggedIn();
			}
		}

		public bool Login(string username, string password)
        {
			if( username == "admin" && password == "secret" )
			{
				//Cookie is encrypted using AES(using machineKey) and transmitted to the client. Key is on the server. Cannot realistically be decrypted. Password not transmitted in this case.
				var cookie = _cookieSetup.CreateEncryptedAuthenticationCookie("admin", "secret"); 
                HttpContext.Current.Response.Cookies.Add(cookie);
                _sessionManager.SetLoggedInSession(new CustomPrincipalSerializeModel { FirstName = "Eugene", LastName = "Denisov" });
                return true;

            }
            else
            {
                return false;
            }
        }

        public void Logout()
        {
            _sessionManager.ClearSession();
            _cookieSetup.ClearAuthenticationCookie();
        }
    }
}