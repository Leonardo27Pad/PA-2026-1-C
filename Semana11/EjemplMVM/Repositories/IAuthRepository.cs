using System;
using System.Collections.Generic;
using System.Text;

namespace EjemplMVM.Repositories
{
    public interface IAuthRepository
    {
        public bool ValidarUsuario(string usuario, string password);
    }
}
