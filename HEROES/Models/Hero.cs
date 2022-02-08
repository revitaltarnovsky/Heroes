using HEROES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Models
{
    public class Hero
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AbilityType Ability { get; set; }
        public DateTime StartTraining { get; set; } = DateTime.Now;
        public ColorName SuitColors { get; set; }
        public decimal StartingPower { get; set; } = 0.0M;
        public decimal CurrentPower { get; set; } = 0.0M;
        public long TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }
        public virtual ICollection<Training> Trainings { get; set; }
    }
}
