using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Proyecto.Filters
{
    public class RolAttribute : ActionFilterAttribute
    {
        private readonly string _rolRequerido;

        public RolAttribute(string rolRequerido)
        {
            _rolRequerido = rolRequerido;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var rol = context.HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || rol != _rolRequerido)
            {
                context.Result = new RedirectToActionResult("Login", "Usuarios", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
