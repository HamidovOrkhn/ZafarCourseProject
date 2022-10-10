using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZafarCoursesWebApp.Areas.Admin.Models;
using ZafarCoursesWebApp.Areas.Admin.ViewModels;
namespace ZafarCoursesWebApp.Areas.Admin.Data
{
    public class AdminContext:DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<LectureParticle> LectureParticles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherCategory> TeacherCategories { get; set; }
        public DbSet<ServiceInfo> ServiceInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
             .HasOne(a => a.ServiceInfo)
             .WithOne(a => a.Service)
             .HasForeignKey<ServiceInfo>(c => c.ServiceId);
        }
        public DbSet<ZafarCoursesWebApp.Areas.Admin.ViewModels.CreateServiceViewModel> CreateServiceViewModel { get; set; }
    }
}
