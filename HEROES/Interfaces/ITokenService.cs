using HEROES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Trainer trainer);
    }
}
