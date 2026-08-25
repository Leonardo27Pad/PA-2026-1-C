using EjemplMVM.Commands;
using EjemplMVM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EjemplMVM.ViewModels
{
    public class LoginViewModel
    {
        public string usuario { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;

        public ICommand LoginCommand { get; set; }

        public Action? OnLoginExitoso { get; set; }
        public Action<string>? OnLoginError { get; set; }

        private IAuthRepository _repository;

        public LoginViewModel()
        {
            _repository = new AuthRepositoryImpl();
            LoginCommand = new RelayCommand(EjcutarLogin);

        }

        private void EjcutarLogin()
        {
            bool esCorrecto = _repository.ValidarUsuario(usuario, password);
            if (esCorrecto)
            {
                OnLoginExitoso.Invoke();
            }
            else
            {
                OnLoginError.Invoke("Login Incorrecto, credenciales inválidas");
            }
        }
    }
}