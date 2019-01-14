using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MentorProject.Classes;
using MentorProject.WebUI.Infrastructure.Abstract;

namespace SportsStore.WebUI.Infrastructure.Filters
{
	public class AuthActionFilterAttribute : ActionFilterAttribute
	{
		private readonly IAuthorizationManager _auth_manager;
		private List<string> _roles = new List<string>();

		public AuthActionFilterAttribute( IAuthorizationManager auth_manager )
		{
			_auth_manager = auth_manager;
		}

		public AuthActionFilterAttribute( string roles )
		{
			_roles = roles.Split(',').ToList();
		}

		public override void OnActionExecuting( ActionExecutingContext filterContext )
		{
			//bool isAuthorized = (bool?)filterContext.HttpContext.Session[Types.SessionFields.LoggedInUser.ToString()] ?? false;

			if( !_auth_manager.IsAuthorized )
			{
				filterContext.Result = new RedirectResult( "Login" );
			}

			base.OnActionExecuting( filterContext );
		}
	}
}