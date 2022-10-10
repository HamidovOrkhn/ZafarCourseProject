using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZafarCoursesWebApp.Areas.Admin.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public int ServiceId { get; set; }
        public List<LectureParticle> LectureParticles { get; set; }
        public Service Service { get; set; }
    }
    public class LectureValidator : AbstractValidator<Lecture>
    {
        public LectureValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).MaximumLength(200).NotNull().WithMessage("Ad boş ola bilməz");
        }
    }
}
