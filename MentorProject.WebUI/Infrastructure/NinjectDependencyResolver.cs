using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using MentorProject.WebUI.Infrastructure.Abstract;
using MentorProject.WebUI.Infrastructure.Concrete;
using MentorProject.WebUI.Infrastructure.Helpers;

namespace MentorProject.WebUI.Infrastructure {

    public class NinjectDependencyResolver : IDependencyResolver {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam) {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings() {

            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
			kernel.Bind<IAuthorizationManager>().To<AuthorizationManager>();
			kernel.Bind<ICookieSetup>().To<CookieSetup>();
			kernel.Bind<ISessionManager>().To<SessionManager>();

		}
	}
}
