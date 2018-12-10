using MentorProject.WebUI.Controllers;
using MentorProject.WebUI.Infrastructure.Abstract;
using MentorProject.WebUI.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MentorProject.WebUI.Infrastructure.Helpers
{
    public class CookieSetup : ICookieSetup
    {
		private int _id = 1; // hard coded just for simple usage

		public void ClearAuthenticationCookie()
        {
            FormsAuthentication.SignOut();
        }

        public HttpCookie CreateEncryptedAuthenticationCookie(string username, string password)
        {
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = _id;
            serializeModel.FirstName = username;
            serializeModel.LastName = password;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        _id,
                        username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(15),
                        false,
                        userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            return faCookie;
        }

        public CustomPrincipal RetrieveUserFromCookie(HttpCookie authCookie)
        {

            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);

            CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
            newUser.Id = serializeModel.Id;
            newUser.FirstName = serializeModel.FirstName;
            newUser.LastName = serializeModel.LastName;
            return newUser;
        }

      
    }
}