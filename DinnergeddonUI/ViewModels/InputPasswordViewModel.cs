using DinnergeddonUI.DinnergeddonServiceReference;
using DinnergeddonUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DinnergeddonUI.ViewModels
{
    class InputPasswordViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand _connectCommand;
        private LobbyProxy _proxy;
        private string _errorMessage;
        private Lobby _lobby;
        private CustomPrincipal customPrincipal;

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
        public ICommand ConnectCommand
        {
            get
            {
                if (_connectCommand == null)
                {
                    _connectCommand = new RelayCommand(Connect);
                }
                return _connectCommand;
            }

        }

        public InputPasswordViewModel()
        {
            customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;

            _proxy = new LobbyProxy();
            //_proxy.LobbyJoined += OnJoinLobby;
            //_proxy.GetLobbyByIdResponse += OnLobbyRecieved;
            //Mediator.Subscribe("SendLobbyId", SetLobby);
        }

        private  void SetLobby(object parameter)
        {
            _lobby = _proxy.GetLobbyById((Guid)parameter);
        }



        private void OnJoinLobby(object sender, bool success)
        {
            //if (!success) {
            //    ErrorMessage = "Incorrect password of Lobby is full.";
            //}
        }

        private void Connect(object parameter)
        {
            PasswordBox pb = parameter as PasswordBox;
            string password = pb.Password;

            // _proxy.JoinLobby(customPrincipal.Identity.Id, _lobby.Id, password);
            Mediator.Notify("PassWordCorrect",password);


        }
    }
}
