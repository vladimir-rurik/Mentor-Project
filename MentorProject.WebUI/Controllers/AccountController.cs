using System;
using System.Web;
using System.Web.Mvc;
using MentorProject.WebUI.Infrastructure.Abstract;
using MentorProject.WebUI.Models;
using SportsStore.WebUI.Infrastructure.Filters;

namespace MentorProject.WebUI.Controllers {

    public class AccountController : Controller {
        IAuthProvider authProvider;

		//TODO: Install package to inject dependencies in the controller
		private ICookieSetup _cookieSetup;
		private IAuthorizationManager _authManager;
		public AccountController( IAuthorizationManager _authManager, ICookieSetup _cookieSetup )
		{
			this._authManager = _authManager;
			this._cookieSetup = _cookieSetup;
		}

		public AccountController(IAuthProvider auth) {
            authProvider = auth;
        }

        public ViewResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl) {

			if (ModelState.IsValid) {
                if (_authManager.Login(model.UserName, model.Password)) {
                    return Redirect(returnUrl ?? Url.Action("AuthorizedPage"));
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

		public ActionResult AuthorizedPage()
		{
			return Content( "Authorized Page" );
		}

		public ActionResult Logout()
		{
			foreach( HttpCookie cookie in Request.Cookies )
			{
				Response.Cookies.Set( new HttpCookie( cookie.Name, null ) { Expires = DateTime.Now.AddDays( -7 ) } );
			}

			return RedirectToAction( "Home" );
		}

		[AuthActionFilter("Role1,RoleAdmin")]
		public ActionResult Role1Page()
		{
			return Content( "Role1 Page" );
		}
	}
}
