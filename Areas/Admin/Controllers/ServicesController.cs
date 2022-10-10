using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZafarCoursesWebApp.Areas.Admin.Data;
using ZafarCoursesWebApp.Areas.Admin.Filters;
using ZafarCoursesWebApp.Areas.Admin.Models;
using ZafarCoursesWebApp.Areas.Admin.ViewModels;
using ZafarCoursesWebApp.Libs;

namespace ZafarCoursesWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(LoginFilter))]
    public class ServicesController : Controller
    {
        private readonly AdminContext _db;
        public ServicesController(AdminContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Services.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateServiceViewModel request)
        {
            var files = request.TeacherPictures;
            Service service = new Service();
            List<Teacher> teachers = new List<Teacher>();
            List<Lecture> questions = new List<Lecture>();
            ServiceInfo serviceInfo = new ServiceInfo();
            service.Name = request.Name;
            if (request.ServicePicture is object)
            {
                string pictureName = FileManager.IFormSaveLocal(request.ServicePicture, "services");
                service.ServicePicture = pictureName;
            }
            else
            {
                if (request.ServicePicture is null)
                {
                    TempData["ServiceErrorAdd"] = "Əlavə etmə prosesi uğursuzdur yenidən təkrar edin";
                    return RedirectToAction("Index", "Services");
                }
            }
            _db.Services.Add(service);
            //_db.SaveChanges();

            serviceInfo.Body = request.Body;
            serviceInfo.Header = request.Header;
            serviceInfo.LectureCount = request.LectureCount;
            serviceInfo.WeekCount = request.WeekCount;
            serviceInfo.LectureHour = request.LectureHour;
            if (files is null)
            {
                TempData["ServiceErrorAdd"] = "Əlavə etmə prosesi uğursuzdur yenidən təkrar edin";
                return RedirectToAction("Index", "Services");
            }
            if (files.Count > 0)
            {
                if (request.Surnames.Count > 0 && request.Names.Count > 0 && files.Count > 0)
                {
                    for (int i = 0; i < request.Names.Count; i++)
                    {
                        try
                        {
                            if (request.Names[i] is object && request.Surnames[i] is object && files[i] is object)
                            {
                                string pictureName = FileManager.IFormSaveLocal(files[i], "teachers");
                                teachers.Add(new Teacher { CategoryId = request.CategoryIds[i], Surname = request.Surnames[i], Name = request.Names[i], PictureSource = pictureName });
                            }
                        }
                        catch (Exception)
                        {
                            TempData["ServiceErrorAdd"] = "Əlavə etmə prosesi uğursuzdur yenidən təkrar edin";
                            return RedirectToAction("Index", "Services");
                        }

                    }
                }
            }
            if (request.Questions is null)
            {
                TempData["ServiceErrorAdd"] = "Əlavə etmə prosesi uğursuzdur yenidən təkrar edin";
                return RedirectToAction("Index", "Services");
            }
            if (request.Questions.Count > 0)
            {
                int cache = 0;
                for (int i = 0; i < request.Questions.Count; i++)
                {
                    var lectures = new List<LectureParticle>();
                    var stepquestionCount = request.SteppOrders.Where(a => Convert.ToInt32(a) == i).ToList().Count;
                    if (request.QuestionStepps.Count > 0)
                    {
                        for (int s = 0; s < stepquestionCount; s++)
                        {
                            if (request.QuestionStepps[cache] is null)
                            {
                                TempData["ServiceErrorAdd"] = "Əlavə etmə prosesi uğursuzdur yenidən təkrar edin";
                                return RedirectToAction("Index", "Services");
                            }
                            lectures.Add(new LectureParticle { Name = request.QuestionStepps[cache] });
                            cache += 1;
                        }
                    }
                    questions.Add(new Lecture { Name = request.Questions[i], LectureParticles = lectures });
                }
            }

            service.ServiceInfo = serviceInfo;
            service.Teachers = teachers;
            service.Lectures = questions;

            _db.SaveChanges();

            TempData["ServiceSuccess"] = "Uğurlu əməliyyat";
            return RedirectToAction("Index", "Services");
        }
        public IActionResult Delete(int id)
        {
            Service data = _db.Services.Where(a => a.Id == id).FirstOrDefault();
            _db.Services.Remove(data);
            _db.SaveChanges();
            TempData["ServiceSuccessDelete"] = "Uğurlu əməliyyat";
            return RedirectToAction("Index", "Services");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            EditServiceViewModel model = new EditServiceViewModel();
            Service services = _db.Services
                                .Where(a => a.Id == id)
                                .Include(a => a.ServiceInfo)
                                .Include(a => a.Teachers)
                                .Include(a => a.Lectures)
                                .ThenInclude(a => a.LectureParticles).FirstOrDefault();
            if (services is object)
            {
                model.Services = services;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit([FromForm] EditServiceViewModel request, [FromRoute] int id)
        {
            var files = request.TeacherPictures;
            Service service = _db.Services
                    .Where(a => a.Id == id).FirstOrDefault();

            var lecture = _db.Lectures.Where(a => a.ServiceId == id).ToList();
            if (lecture.Count > 0)
            {
                foreach (var item in lecture)
                {
                    _db.Lectures.Remove(item);
                }
            }
            var teacher = _db.Teachers.Where(a => a.ServiceId == id).ToList();
            if (teacher.Count > 0)
            {
                foreach (var item in teacher)
                {
                    if (files is object)
                    {
                        if (files.Count > 0)
                        {
                            string name = item.PictureSource.Remove(0, 8);
                            FileManager.Delete(name, "teachers");
                            _db.Teachers.Remove(item);
                        }
                    }
                }
            }
            var serviceInfo = _db.ServiceInfos.Where(a => a.ServiceId == id).FirstOrDefault();
            if (serviceInfo is object)
            {
                serviceInfo.Body = request.Body;
                serviceInfo.Header = request.Header;
                serviceInfo.LectureCount = request.LectureCount;
                serviceInfo.WeekCount = request.WeekCount;
                serviceInfo.LectureHour = request.LectureHour;
            }
            if (request.ServicePicture is object)
            {
                string name = service.ServicePicture.Remove(0, 8);
                FileManager.Delete(name, "services");
                service.ServicePicture = FileManager.IFormSaveLocal(request.ServicePicture, "teachers");
            }

            List<Teacher> teachers = new List<Teacher>();
            List<Lecture> questions = new List<Lecture>();
            service.Name = request.Name;
            if (request.Names is object && request.Names.Count > 0)
            {
                for (int i = 0; i < request.Names.Count; i++)
                {
                    try
                    {
                        if (request.Names[i] is object && request.Surnames[i] is object)
                        {
                            string pictureName = teacher[i].PictureSource;
                            if (files is object && request.PictureIndexes.Any(a => a == i))
                            {
                                int g = i;
                                if (i == files.Count)
                                {
                                    g = i - 1;
                                }
                                pictureName = FileManager.IFormSaveLocal(files[g], "teachers");
                                teachers.Add(new Teacher { CategoryId = request.CategoryIds[i], Surname = request.Surnames[i], Name = request.Names[i], PictureSource = pictureName });
                            }
                            else
                            {
                                teachers.Add(new Teacher { CategoryId = request.CategoryIds[i], Surname = request.Surnames[i], Name = request.Names[i], PictureSource = teacher[i].PictureSource });
                            }
                        }
                    }
                    catch (Exception)
                    {
                        TempData["ServiceErrorAdd"] = "Əlavə etmə prosesi uğursuzdur yenidən təkrar edin";
                        return RedirectToAction("Index", "Services");
                    }
                }
            }
            if (request.Questions is null)
            {
                TempData["ServiceErrorAdd"] = "Əlavə etmə prosesi uğursuzdur yenidən təkrar edin";
                return RedirectToAction("Index", "Services");
            }
            if (request.Questions.Count > 0)
            {
                int cache = 0;
                for (int i = 0; i < request.Questions.Count; i++)
                {
                    var lectures = new List<LectureParticle>();
                    if (request.SteppOrders is null)
                    {
                        TempData["ServiceErrorAdd"] = "Əlavə etmə prosesi uğursuzdur yenidən təkrar edin";
                        return RedirectToAction("Index", "Services");
                    }
                    var stepquestionCount = request.SteppOrders.Where(a => Convert.ToInt32(a) == i).ToList().Count;
                    if (request.QuestionStepps.Count > 0)
                    {
                        if (request.QuestionStepps[cache] is null)
                        {
                            TempData["ServiceErrorAdd"] = "Əlavə etmə prosesi uğursuzdur yenidən təkrar edin";
                            return RedirectToAction("Index", "Services");
                        }
                        for (int s = 0; s < stepquestionCount; s++)
                        {
                            lectures.Add(new LectureParticle { Name = request.QuestionStepps[cache] });
                            cache += 1;
                        }
                    }
                    questions.Add(new Lecture { Name = request.Questions[i], LectureParticles = lectures });
                }
            }

            service.ServiceInfo = serviceInfo;
            service.Teachers = teachers;
            service.Lectures = questions;

            _db.SaveChanges();

            TempData["ServiceSuccessUpdate"] = "Uğurlu əməliyyat";
            return RedirectToAction("Index", "Services");
        }
    }
}
