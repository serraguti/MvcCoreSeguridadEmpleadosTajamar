using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcCoreSeguridadEmpleados.Filters
{
    public class AuthorizeEmpleadosAttribute : AuthorizeAttribute,
        IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //NOS DA IGUAL QUIEN SE HA VALIDADO POR AHORA
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                context.Result = this.GetRoute("Managed", "LogIn");
            }
            else
            {
                //NOS INTERESA SABER MAS CARACTERISTICAS DEL USUARIO
                if (user.IsInRole("PRESIDENTE") == false 
                    && user.IsInRole("DIRECTOR") == false
                    && user.IsInRole("ANALISTA") == false)
                {
                    context.Result =
                        this.GetRoute("Managed", "ErrorAcceso");
                }
            }
        }

        //COMO HAREMOS VARIAS REDIRECCIONES, CREAMOS UN METODO
        //PARA CREAR LAS RUTAS
        private RedirectToRouteResult GetRoute
            (string controller, string action)
        {
            RouteValueDictionary ruta = 
                new RouteValueDictionary(new {
                    controller = controller
                    , action = action
                });
            RedirectToRouteResult result =
                new RedirectToRouteResult(ruta);
            return result;
        }
    }
}
