using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentorProject.WebUI.Infrastructure.Abstract
{
    public interface IAuthorizationManager
    {
        bool Login(string username, string password);
        void Logout();

    }
}