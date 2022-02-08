using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Models
{
    public class Trainer
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Hero> Heroes { get; set; }

    }
}
