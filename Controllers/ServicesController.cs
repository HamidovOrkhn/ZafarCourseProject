using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZafarCoursesWebApp.Areas.Admin.Data;
using ZafarCoursesWebApp.Areas.Admin.Models;
using ZafarCoursesWebApp.Areas.Admin.ViewModels;

namespace ZafarCoursesWebApp.Controllers
{
    public class ServicesController : Controller
    {
        private readonly AdminContext _db;
        public ServicesController(AdminContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Courses()
        {
            List<Service> services = _db.Services.Include(a => a.ServiceInfo).Include(a => a.Teachers).ToList();
            return View(services);
        }
        public IActionResult Teachers()
        {
            var teachers = _db.Teachers.Include(a => a.Category).ToList().GroupBy(a=>a.Name).Select(a=>new Teacher { Category = a.FirstOrDefault().Category, Name = a.FirstOrDefault().Name, Surname = a.FirstOrDefault().Surname, PictureSource = a.FirstOrDefault().PictureSource }).ToList();
            return View(teachers);
        }
        public IActionResult Details(int id)
        {
            Service model = new Service();
            Service services = _db.Services
                                .Where(a => a.Id == id)
                                .Include(a => a.ServiceInfo)
                                .Include(a => a.Teachers)
                                .ThenInclude(a=>a.Category)
                                .Include(a => a.Lectures)
                                .ThenInclude(a => a.LectureParticles).FirstOrDefault();
            if (services is object)
            {
                model = services;
            }
            return View(model);
        }
    }
}
