using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZafarCoursesWebApp.Areas.Admin.Filters;

namespace ZafarCoursesWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [TypeFilter(typeof(LoginFilter))]
        public IActionResult Index()
        {
            return View();
        }
    }
}
