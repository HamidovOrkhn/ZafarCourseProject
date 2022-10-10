using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZafarCoursesWebApp.Areas.Admin.Data;
using ZafarCoursesWebApp.Areas.Admin.Models;
using CryptoHelper;
using ZafarCoursesWebApp.Areas.Admin.Resources;
using Microsoft.AspNetCore.Http;
using ZafarCoursesWebApp.Areas.Admin.Filters;

namespace ZafarCoursesWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly AdminContext _db;
        public UserController(AdminContext db)
        {
            _db = db;
        }
        [TypeFilter(typeof(LoginFilter))]
        public IActionResult Index()
        {
            return View(_db.Users.ToList());
        }
        [TypeFilter(typeof(LogoutFilter))]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [TypeFilter(typeof(LogoutFilter))]
        [HttpPost]
        public IActionResult Login(LoginDto request)
        {
            User user = _db.Users.FirstOrDefault(a => a.Username == request.Username);
            if (user is null)
            {
                TempData["UserError"] = "Belə istifadəçi mövcud deyil";
                return RedirectToAction("Login");
            }

            if (!Crypto.VerifyHashedPassword(user.Password, request.Password))
            {
                TempData["UserError"] = "Şifrə və ya istifadəçi adı səhvdir";
                return RedirectToAction("Login");
            }

            string token = Guid.NewGuid().ToString();

            user.Token = token;

            int days = 0;

            if (request.Rememberme)
            {
                HttpContext.Response.Cookies.Append("token", token, new CookieOptions {  Expires = DateTimeOffset.UtcNow.AddDays(100) });
                HttpContext.Response.Cookies.Append("uid", Convert.ToString(user.Id), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(100) });
            }
            else
            {
                HttpContext.Response.Cookies.Append("token", token);
                HttpContext.Response.Cookies.Append("uid", Convert.ToString(user.Id));
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [TypeFilter(typeof(LoginFilter))]
        public IActionResult Create()
        {
            return View();
        }
        [TypeFilter(typeof(LoginFilter))]
        [HttpPost]
        public IActionResult Create(User request)
        {
            User data = _db.Users.Where(a => a.Username == request.Username).FirstOrDefault();
            if (data is object)
            {
                TempData["UserErrorCreate"] = "İstifadəçi adı artıq mövcuddur";
                return RedirectToAction("Create");
            }
            request.Password = Crypto.HashPassword(request.Password);
            _db.Users.Add(request);
            _db.SaveChanges();
            TempData["SuccessUserCreate"] = "Istifadəçi yaradıldı";
            return RedirectToAction("Index");
        }
        [TypeFilter(typeof(LoginFilter))]
        public IActionResult Delete(int id)
        {
            User user = _db.Users.Where(a=>a.Id == id).FirstOrDefault();
            if (user is object)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
            TempData["SuccessUserDelete"] = "Uğurlu əməliyyat";
            return RedirectToAction("Index", "User");
        }
        [TypeFilter(typeof(LoginFilter))]
        [HttpGet]
        public IActionResult Logout()
        {
            if (HttpContext.Request.Cookies["token"] is object)
            {
                HttpContext.Response.Cookies.Delete("token");
                HttpContext.Response.Cookies.Delete("uid");
            }
            return RedirectToAction("Login", "User");
        }

    }
}
