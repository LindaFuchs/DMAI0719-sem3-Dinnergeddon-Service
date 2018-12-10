using DinnergeddonUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DinnergeddonUI.ViewModels
{
    public class LoginViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand _login;
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value;
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
        private IPageViewModel _currentPageViewModel;



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
        private ICommand _goToLobbies;
        private ICommand _goToProfile;

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
            Mediator.Notify("Login", "");
        }
        public LoginViewModel()
        {




            //Mediator.Subscribe("GoTo2Screen", OnGo2Screen);
            //Mediator.Subscribe("GoToLobbies", OnGoToLobbies);
            //Mediator.Subscribe("GoToProfile", OnGoToProfile);

        }

    }
}
