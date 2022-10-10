using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZafarCoursesWebApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statuscode}")]
        public IActionResult ExceptionHandler(int statuscode)
        {
            ViewData["ErrorCode"] = statuscode;
            switch (statuscode)
            {
                case 404:
                    ViewData["ErrorHead"] = "Səhifə tapılmadı";
                    ViewData["ErrorMessage"] = "Yazılan link ya dəyişdirilmişdir, ya da hal-hazırda mövcud deyil";
                    ViewData["BackLink"] = "/";
                    break;
                case 500:
                    ViewData["ErrorHead"] = "Serverlərlə bağlı problem var";
                    ViewData["ErrorMessage"] = HttpContext.Features.Get<IExceptionHandlerPathFeature>().Error.Message;
                    ViewData["BackLink"] = "/";
                    break;
                default:
                    break;
            }
            return View("ErrorView");

        }
    }
}
