using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.DTOs
{
    public class TrainerMemberDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 3)]
        public string Username { get; set; }
        [Required]
        public ICollection<HeroDto> Heroes { get; set; }
    }
}
