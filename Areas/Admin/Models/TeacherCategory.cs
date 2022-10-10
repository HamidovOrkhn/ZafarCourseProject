using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZafarCoursesWebApp.Areas.Admin.Models
{
    public class TeacherCategory
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
    public class TeacherCategoryValidator : AbstractValidator<TeacherCategory>
    {
        public TeacherCategoryValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).MaximumLength(150).NotNull().WithMessage("Ad boş qoyula bilməz");
        }
    }
}