using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZafarCoursesWebApp.Areas.Admin.Models
{
    public class ServiceInfo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Header { get; set; }
        //[Required]
        //[MaxLength(2000)]
        public string Body { get; set; }
        public int LectureCount { get; set; }
        public int LectureHour { get; set; }
        public int WeekCount { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
    public class ServiceİnfoValidator : AbstractValidator<ServiceInfo>
    {
        public ServiceİnfoValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Header).MaximumLength(200).NotNull().WithMessage("Başlıq boş ola bilməz");
        }
    }
}
