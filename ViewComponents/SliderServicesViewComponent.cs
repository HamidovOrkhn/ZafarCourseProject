using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZafarCoursesWebApp.Areas.Admin.Data;
using ZafarCoursesWebApp.Areas.Admin.Models;

namespace ZafarCoursesWebApp.ViewComponents
{
    public class SliderServicesViewComponent : ViewComponent
    {
        private readonly AdminContext _db;
        public SliderServicesViewComponent(AdminContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Service> services = _db.Services.Include(a=>a.ServiceInfo).Include(a=>a.Teachers).ToList();
            return View(services);
        }
    }
}
