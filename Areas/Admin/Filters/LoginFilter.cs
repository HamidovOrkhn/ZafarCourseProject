using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZafarCoursesWebApp.Areas.Admin.Data;
using ZafarCoursesWebApp.Areas.Admin.Helper;

namespace ZafarCoursesWebApp.Areas.Admin.Filters
{
    public class LoginFilter : Attribute, IActionFilter
    {
        private readonly AdminContext _db;
        public LoginFilter(AdminContext db)
        {
            _db = db;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("ms");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Cookies["token"] is object)
            {
                if (!_db.IsAuthenticated(Convert.ToInt32(context.HttpContext.Request.Cookies["uid"]), context.HttpContext.Request.Cookies["token"]))
                {
                   context.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                    { "controller", "User" },
                    { "action", "Login" }
                   });
                }
            }
            else
            {
                context.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                    { "controller", "User" },
                    { "action", "Login" }
                   });
            }
        }
    }
}
