using HEROES.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.DTOs
{
    public class TrainerDto
    {
        [Required]
        public string Token { get; set; }

    }
}
