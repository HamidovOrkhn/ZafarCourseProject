using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZafarCoursesWebApp.Areas.Admin.Resources
{
    public class LoginDto
    {
        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string Password { get; set; }
        public bool Rememberme { get; set; }
    }
}
