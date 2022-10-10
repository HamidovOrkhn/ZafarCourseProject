using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZafarCoursesWebApp.Areas.Admin.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string Username { get; set; }
        [Required]
        [MaxLength(500)]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(200)]
        public string? Token { get; set; }

    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Username).MaximumLength(500).MinimumLength(5).NotNull().WithMessage("İstifadəçi adı boş ola bilməz");
            RuleFor(x => x.Email).EmailAddress().MaximumLength(250).NotNull().WithMessage("Email Adress boş ola bilməz");
            RuleFor(x => x.Password).MinimumLength(6).MaximumLength(32).NotNull().WithMessage("Şifrə minimum 6 və maksimum 32 xarakterdən ibarət ola bilər");
            RuleFor(x => x.Name).MaximumLength(50).NotNull().WithMessage("Ad boş ola bilməz");
            RuleFor(x => x.Surname).MaximumLength(50).NotNull().WithMessage("Soyad boş ola bilməz");

        }
    }
}
