using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZafarCoursesWebApp.Areas.Admin.Data;
using ZafarCoursesWebApp.Areas.Admin.Models;

namespace ZafarCoursesWebApp.ViewComponents
{
    public class ServicesAllViewComponent:ViewComponent
    {
        private readonly AdminContext _db;
        public ServicesAllViewComponent(AdminContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Service> services = _db.Services.ToList();
            return View(services);
        }
    }
}
