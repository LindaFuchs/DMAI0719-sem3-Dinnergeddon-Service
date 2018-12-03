using DinnergeddonUI.DinnergeddonService;
using DinnergeddonUI.Interfaces;
using DinnergeddonUI.Model;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DinnergeddonUI.ViewModels
{
    // TODO: remove comented code after refactoring
    public class MainWindowViewModel : IViewModel, INotifyPropertyChanged
    {
        private readonly AccountServiceClient _proxy;

        private readonly DelegateCommand _loginCommand;
        //private readonly DelegateCommand _logoutCommand;
        //private readonly DelegateCommand _closeWindowCommand;
        private string _username;
        private string _status;

        public MainWindowViewModel()
        {
            _proxy = new AccountServiceClient();

            _loginCommand = new DelegateCommand(Login, CanLogin);
            //_logoutCommand = new DelegateCommand(Logout, CanLogout);
            //_closeWindowCommand = new DelegateCommand(CloseWindow, CanLogin);
        }

        #region Properties
        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyPropertyChanged("Username"); }
        }

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format("Signed in as {0}. {1}",
                          Thread.CurrentPrincipal.Identity.Name,
                          Thread.CurrentPrincipal.IsInRole("admin") ? "You are an administrator!"
                              : "You are NOT a member of the administrators group.");

                return "Not authenticated!";
            }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged("Status"); }
        }
        #endregion

        #region Commands
        public DelegateCommand LoginCommand { get { return _loginCommand; } }

        // As far as I'm aware (Nikola) these shouldn't be here. They're most likely here because of testing

        //public ICommand CloseCommand { get; private set; }
        
        //public DelegateCommand LogoutCommand { get { return _logoutCommand; } }

        //public DelegateCommand CloseWindowCommand { get { return _closeWindowCommand; } }

        #endregion

        //private void CloseWindow(object w)
        //{
        //    // checks if w is Window and assigns window to w
        //    if (w is Window window)
        //        window.Close();
        //}

        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns></returns>
        private User AuthenticateUser(string email, string password)
        {
            Account account = _proxy.VerifyCredentials(email, password);

            // Invalid credentials
            // (either password is incorrect or account wasn't found)
            if (account == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

            // Valid credentials, proceed
            return new User(account.Id, account.Username, account.Email, _proxy.GetRoles(account.Id));
        }

        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string password = passwordBox.Password;

            try
            {
                //Validate credentials through the authentication service
                User user = AuthenticateUser(Username, password);

                //Get the current principal object

                if (!(Thread.CurrentPrincipal is CustomPrincipal customPrincipal))
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                //Authenticate the user
                customPrincipal.Identity = new CustomIdentity(user.Id, user.Username, user.Email, user.Roles);

                //Update UI
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");

                _loginCommand.RaiseCanExecuteChanged();
                //_logoutCommand.RaiseCanExecuteChanged();
                
                Dashboard dashboard = new Dashboard();
                dashboard.Show();

                Application.Current.MainWindow.Close();

            }
            catch (UnauthorizedAccessException)
            {
                Status = "Login failed! Please provide some valid credentials.";
                // TODO: change color of ui
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        //private void Logout(object parameter)
        //{
        //    Window dashboard = parameter as Window;
        //    CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
        //    if (customPrincipal != null)
        //    {
        //        customPrincipal.Identity = new AnonymousIdentity();
        //        NotifyPropertyChanged("AuthenticatedUser");
        //        NotifyPropertyChanged("IsAuthenticated");
        //        _loginCommand.RaiseCanExecuteChanged();
        //        //_logoutCommand.RaiseCanExecuteChanged();
        //        Status = string.Empty;
        //        dashboard.Close();
        //        //MainWindow mw = new MainWindow();
        //        //mw.Show();
        //    }
        //}

        //private bool CanLogout(object parameter)
        //{
        //    return IsAuthenticated;
        //}

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
