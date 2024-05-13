using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exemplo.Domain.Entities;

namespace Exemplo.Domain.Interfaces
{
    public interface IUserHelper
    {
        UserIdentity LoggedUser { get; }
    }
}




