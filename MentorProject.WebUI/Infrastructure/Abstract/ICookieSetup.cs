using MentorProject.WebUI.Models.Authorization;
using System.Web;

namespace MentorProject.WebUI.Infrastructure.Abstract
{
    public interface ICookieSetup
    {
        HttpCookie CreateEncryptedAuthenticationCookie(string username, string password);
        void ClearAuthenticationCookie();
        CustomPrincipal RetrieveUserFromCookie(HttpCookie authCookie);
    }
}