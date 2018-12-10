using DinnergeddonUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DinnergeddonUI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
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
        private ICommand _goToLobbies;
        private ICommand _logout;
        private ICommand _goToProfile;
        private bool _isAuthenticated = false;

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
        public ICommand LogoutCommand
        {
            get
            {
                return _logout ?? (_logout = new RelayCommand(x =>
                {
                    Mediator.Notify("Logout", "");
                }));
            }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set
            {
                _isAuthenticated = value;
                OnPropertyChanged("IsAuthenticated");
            }

        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void OnGo1Screen(object obj)
        {
            ChangeViewModel(PageViewModels[0]);
        }

        private void OnGoToLobbies(object obj)
        {
            ChangeViewModel(PageViewModels[0]);
        }

        private void OnGo2Screen(object obj)
        {
            ChangeViewModel(PageViewModels[1]);
        }

        private void SetTrue(object obj)
        {
            IsAuthenticated = true;
            CurrentPageViewModel = PageViewModels[1];
        }
        private void Logout(object obj)
        {
            IsAuthenticated = false;
            CurrentPageViewModel = PageViewModels[0];
        }

        public MainWindowViewModel()
        {
            // Add available pages and set page
            PageViewModels.Add(new LoginViewModel());
            PageViewModels.Add(new LobbiesViewModel());
            //PageViewModels.Add(new LobbiesViewModel());
            //PageViewModels.Add(new LobbyViewModel());
            //PageViewModels.Add(new ProfileViewModel());

            CurrentPageViewModel = PageViewModels[0];

            Mediator.Subscribe("GoTo1Screen", OnGo1Screen);
            Mediator.Subscribe("GoTo2Screen", OnGo2Screen);
            Mediator.Subscribe("GoToLobbies", OnGoToLobbies);
            Mediator.Subscribe("Login", SetTrue);
            Mediator.Subscribe("Logout", Logout);


        }
    }
}
