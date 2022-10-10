using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZafarCoursesWebApp.Areas.Admin.Models
{
    public class LectureParticle
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }
    }
    public class LectureParticleValidator : AbstractValidator<LectureParticle>
    {
        public LectureParticleValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).MaximumLength(200).NotNull().WithMessage("Ad boş ola bilməz");
        }
    }
}
