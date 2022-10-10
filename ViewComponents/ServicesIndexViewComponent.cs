using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZafarCoursesWebApp.Areas.Admin.Data;
using ZafarCoursesWebApp.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
namespace ZafarCoursesWebApp.ViewComponents
{
    public class ServicesIndexViewComponent:ViewComponent
    {
        private readonly AdminContext _db;
        public ServicesIndexViewComponent(AdminContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Service> services = _db.Services.Include(a => a.ServiceInfo).Include(a => a.Teachers).ToList();
            int indexLenght = 3;
            for (int i = 0; i < indexLenght; i++)
            {
                if (services.Count < indexLenght)
                {
                    indexLenght -= 1;
                }
            }
            return View(services.Take(indexLenght).ToList());
        }

    }
}
