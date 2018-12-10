using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MentorProject.Classes;

namespace SportsStore.WebUI.Infrastructure.Filters
{
	public class AuthActionFilterAttribute : ActionFilterAttribute
	{
		public AuthActionFilterAttribute()
		{

		}

		public AuthActionFilterAttribute( string role )
		{
			
		}

		public override void OnActionExecuting( ActionExecutingContext filterContext )
		{
			bool isAuthorized = (bool?)filterContext.HttpContext.Session[Types.SessionFields.LoggedInUser.ToString()] ?? false;

			if( !isAuthorized )
			{
				filterContext.Result = new RedirectResult( "Login" );
			}

			base.OnActionExecuting( filterContext );
		}
	}
}