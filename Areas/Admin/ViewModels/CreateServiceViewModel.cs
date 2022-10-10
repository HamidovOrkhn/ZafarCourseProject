using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZafarCoursesWebApp.Areas.Admin.ViewModels
{
    [NotMapped]
    public class CreateServiceViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Header { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Body { get; set; }
        [Required]
        public int LectureCount { get; set; }
        [Required]
        public int LectureHour { get; set; }
        [Required]
        public IFormFile ServicePicture { get; set; }
        public IFormFileCollection TeacherPictures { get; set; }
        public int WeekCount { get; set; }
        public List<string> Names { get; set; }
        public List<string> Surnames { get; set; }
        public List<int> CategoryIds { get; set; }
        //public IFormFileCollection Pictures { get; set; }
        public List<string> Questions { get; set; }
        public List<string> QuestionStepps { get; set; }
        public List<int> SteppOrders { get; set; }
    }
}
