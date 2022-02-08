using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Models
{
    public class Training
    {
        public long Id { get; set; }
        public Guid HeroId { get; set; }
        public DateTime Date { get; set; }

        public virtual Hero Hero { get; set; }
    }
}
