using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Hairdressers.Filters {
    public class AuthorizeUsersAttribute : AuthorizeAttribute, IAuthorizationFilter {

        public void OnAuthorization(AuthorizationFilterContext context) {
            var user = context.HttpContext.User;
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
