using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces;

namespace Exemplo.Service.Helper
{
    public class UsuarioHelper: IUserHelper
    {
        public UsuarioHelper() { }

        public UserIdentity LoggedUser
        {
            get
            {
                return new UserIdentity(1, "admin", 1, 1, 1, true, 1);
            }
        }
    }
}









