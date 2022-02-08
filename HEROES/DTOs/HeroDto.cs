using HEROES.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.DTOs
{
    public class HeroDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        public AbilityType Ability { get; set; }
        [Required]
        public DateTime StartTraining { get; set; }
        [Required] 
        public ColorName SuitColors { get; set; }
        [Required]
        public decimal StartingPower { get; set; }
        [Required]
        public decimal CurrentPower { get; set; }
        [Required]
        public long TrainerId { get; set; }
    }
}
