using DinnergeddonUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DinnergeddonUI.DinnergeddonServiceReference;

namespace DinnergeddonUI.ViewModels
{
    public class LoginViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand _login;
        private string _email;
        private AccountServiceClient _accountProxy;
        private string _errorMessage;
        private IPageViewModel _currentPageViewModel;
        private ICommand _goToLobbies;
        private ICommand _goToProfile;

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }

        }
        public ICommand LoginCommand
        {
            get
            {
                if (_login == null)
                {
                    _login = new RelayCommand<object>(Login);
                }

                return _login;
            }
        }



        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }


        public ICommand GoToLobbies
        {
            get
            {
                return _goToLobbies ?? (_goToLobbies = new RelayCommand(x =>
                {
                    Mediator.Notify("GoTo2Screen", "");
                }));
            }
        }

        public ICommand GoToProfile
        {
            get
            {
                return _goToProfile ?? (_goToProfile = new RelayCommand(x =>
                {
                    Mediator.Notify("GoToProfile", "");
                }));
            }
        }

        //private void ChangeViewModel(IPageViewModel viewModel)
        //{
        //    if (!PageViewModels.Contains(viewModel))
        //        PageViewModels.Add(viewModel);

        //    CurrentPageViewModel = PageViewModels
        //        .FirstOrDefault(vm => vm == viewModel);
        //}

        //private void OnGo1Screen(object obj)
        //{
        //    ChangeViewModel(PageViewModels[0]);
        //}

        //private void OnGoToLobbies(object obj)
        //{
        //    ChangeViewModel(PageViewModels[0]);
        //}

        //private void OnGo2Screen(object obj)
        //{
        //    ChangeViewModel(PageViewModels[1]);
        //}


        private void Login(object parameter)
        {
            PasswordBox pb = parameter as PasswordBox;
            string email = _email;
            string password = pb.Password;

            if (ValidInput(email, password)){



                //Validatecredentials through the authentication service

                Account account = _accountProxy.VerifyCredentials(email, password);
                if (account != null)
                {
                    //Get the current principal object
                    CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                    if (customPrincipal == null)
                        throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                    //Authenticate the user
                    customPrincipal.Identity = new CustomIdentity(account.Id, account.Username, account.Email, new string[] { "" });

                    //Update UI
                    OnPropertyChanged("AuthenticatedUser");
                    OnPropertyChanged("IsAuthenticated");
                    Mediator.Notify("Login", "");
                }
                else
                {
                    ErrorMessage = "Login failed! Please provide some valid credentials.";
                    //OnPropertyChanged("ErrorMessage");
                    // Status = "Login failed! Please provide some valid credentials.";
                    // TODO: change color of ui
                }

            }
            else
            {
                ErrorMessage = "Login failed! Please provide some valid credentials.";

            }


        }

        private bool ValidInput(string email, string password)
        {
            if (email == "" || password == "")
            {
                return false;
            }
            return true;
        }

        public LoginViewModel()
        {

            _accountProxy = new AccountServiceClient();



            //Mediator.Subscribe("GoTo2Screen", OnGo2Screen);
            //Mediator.Subscribe("GoToLobbies", OnGoToLobbies);
            //Mediator.Subscribe("GoToProfile", OnGoToProfile);

        }

    }
}
