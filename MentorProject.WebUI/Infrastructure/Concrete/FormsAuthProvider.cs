﻿using System;
using System.Web.Security;
using MentorProject.WebUI.Infrastructure.Abstract;

namespace MentorProject.WebUI.Infrastructure.Concrete {

	[Obsolete("This class is depricated, use AuthorizationManager instead")]
    public class FormsAuthProvider : IAuthProvider {

        public bool Authenticate(string username, string password) {

			bool result = false;

			if( username == "admin" && password == "secret" )
			{


				result = true;
			}

            //bool result = FormsAuthentication.Authenticate(username, password);
            //if (result) {
            //    FormsAuthentication.SetAuthCookie(username, false);
            //}
            return result;
        }
    }
}
