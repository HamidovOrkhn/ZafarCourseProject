using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZafarCoursesWebApp.Areas.Admin.Data;
using ZafarCoursesWebApp.Areas.Admin.Models;

namespace ZafarCoursesWebApp.Areas.ViewComponents
{
    public class UserViewComponent:ViewComponent
    {
        private readonly AdminContext _db;
        private readonly IHttpContextAccessor _context;
        public UserViewComponent(AdminContext db, IHttpContextAccessor context)
        {
            _db = db;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            User user = new User();
            if (_context.HttpContext.Request.Cookies["uid"] is object)
            {
                user = _db.Users.Where(a => a.Id == Convert.ToInt32(_context.HttpContext.Request.Cookies["uid"])).FirstOrDefault();
            }
            return View(user);
        }
    }
}
