using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZafarCoursesWebApp.Areas.Admin.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Lecture> Lectures { get; set; }
        public int? ServiceInfoId { get; set; }
        [Required]
        [MaxLength(200)]
        public string ServicePicture { get; set; }
        public ServiceInfo ServiceInfo { get; set; }
    }
    public class ServiceValidator : AbstractValidator<Service>
    {
        public ServiceValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).MaximumLength(200).NotNull().WithMessage("Ad boş ola bilməz");
            RuleFor(x => x.ServicePicture).MaximumLength(200).NotNull().WithMessage("Şəkil boş ola bilməz");
        }
    }
}
