using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.DTOs
{
    public class AuthDto
    {
        [Required]
        [StringLength(8, MinimumLength = 2)]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^(?=[A-Z]{1})(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }
    }
}
