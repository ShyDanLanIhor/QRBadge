using AdminProgram.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AdminProgram.Repositories;
using System.Net;
using System.Threading;
using System.Security.Principal;

namespace AdminProgram.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IAdminRepository adminRepository;

        public string Username { get => _username; set { _username = value; OnPropertyChanged(nameof(Username)); }  }
        public SecureString Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsViewVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); } }

        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        public LoginViewModel()
        {
            adminRepository = new AdminRepository();
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new RelayCommand(ExecuteRecoverPasswordCommand);
        }
        
        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = adminRepository.AuthenticateAdmin(new NetworkCredential(Username,Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "Неправильний логін або пароль";
            }
        }

        private void ExecuteRecoverPasswordCommand(object obj)
        {

        }
        
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
            { validData = false; }
            else
            { validData = true; }
            return validData;
        }
    }
}
