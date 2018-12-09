using System;
using System.Web;
using System.Web.Mvc;
using MentorProject.WebUI.Infrastructure.Abstract;
using MentorProject.WebUI.Models;
using SportsStore.WebUI.Infrastructure.Filters;

namespace MentorProject.WebUI.Controllers {

    public class AccountController : Controller {
        IAuthProvider authProvider;

        public AccountController(IAuthProvider auth) {
            authProvider = auth;
        }

        public ViewResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl) {

            if (ModelState.IsValid) {
                if (authProvider.Authenticate(model.UserName, model.Password)) {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                } else {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            } else {
                return View();
            }
        }

		public ActionResult Home()
		{
			return Content( "Home Page" );
		}

		public ActionResult Logout()
		{
			foreach( HttpCookie cookie in Request.Cookies )
			{
				Response.Cookies.Set( new HttpCookie( cookie.Name, null ) { Expires = DateTime.Now.AddDays( -7 ) } );
			}

			return RedirectToAction( "Home" );
		}

		[AuthActionFilter]
		public ActionResult Role1Page()
		{
			return Content( "Role1 Page" );
		}
	}
}
