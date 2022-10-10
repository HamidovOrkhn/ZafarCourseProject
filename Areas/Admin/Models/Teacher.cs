using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZafarCoursesWebApp.Areas.Admin.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int CategoryId { get; set; }
        public TeacherCategory Category { get; set; }
        [Required]
        [MaxLength(200)]
        public string PictureSource { get; set; }
    }
    public class TeacherValidator : AbstractValidator<Teacher>
    {
        public TeacherValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.PictureSource).MaximumLength(200).NotNull().WithMessage("Şəkil boş ola bilməz");
            RuleFor(x => x.Name).MaximumLength(50).NotNull().WithMessage("Ad boş ola bilməz");
            RuleFor(x => x.Surname).MaximumLength(50).NotNull().WithMessage("Soyad boş ola bilməz");

        }
    }
}
