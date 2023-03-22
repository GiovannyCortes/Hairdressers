using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Hairdressers.Filters {
    public class AuthorizeUsersAttribute : AuthorizeAttribute, IAuthorizationFilter {

        public void OnAuthorization(AuthorizationFilterContext context) {
            var user = context.HttpContext.User;

            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();

            ITempDataProvider provider = context.HttpContext.RequestServices.GetService<ITempDataProvider>();

            var TempData = provider.LoadTempData(context.HttpContext);
                TempData["controller"] = controller;
                TempData["action"] = action;

            provider.SaveTempData(context.HttpContext, TempData);

            if (user.Identity.IsAuthenticated == false) { // Usuario no identificado
                context.Result = this.GetRoute("Managed", "LogIn");
            }
        }

        private RedirectToRouteResult GetRoute (string controller, string action) {
            RouteValueDictionary ruta =
                new RouteValueDictionary(new {
                    controller = controller,
                    action = action
                }
            );
            RedirectToRouteResult result = new RedirectToRouteResult(ruta);
            return result;
        }

    }
}
