using MentorProject.Classes;
using MentorProject.WebUI.Infrastructure.Abstract;
using MentorProject.WebUI.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentorProject.WebUI.Infrastructure.Helpers
{
    public class SessionManager : ISessionManager
    {
        private string LoginSessionName = Types.SessionFields.LoggedInUser.ToString();
        private CustomPrincipalSerializeModel currentUser;
        public CustomPrincipalSerializeModel CurrentUser
        {
            get
            {
                return HttpContext.Current.Session[LoginSessionName] as CustomPrincipalSerializeModel;
            }
            set
            {
                currentUser = value;
            }
        }
        
        public bool IsLoggedIn()
        {
            return HttpContext.Current.Session[LoginSessionName] != null;
        }
        public void SetLoggedInSession(CustomPrincipalSerializeModel lvm)
        {
            HttpContext.Current.Session[LoginSessionName] = lvm;
        }
        public void ClearSession()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}