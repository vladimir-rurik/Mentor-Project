using MentorProject.WebUI.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentorProject.WebUI.Infrastructure.Abstract
{
    public interface ISessionManager
    {
        CustomPrincipalSerializeModel CurrentUser { get; set; }
        bool IsLoggedIn();
        void SetLoggedInSession(CustomPrincipalSerializeModel lvm);
        void ClearSession();

    }
}