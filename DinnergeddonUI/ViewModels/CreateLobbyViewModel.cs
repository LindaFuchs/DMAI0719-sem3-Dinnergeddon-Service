using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DinnergeddonUI.Helpers;

namespace DinnergeddonUI.ViewModels
{
    class CreateLobbyViewModel : BaseViewModel, IPageViewModel
    {
        private int _isSuccess;
        private string _lobbyName;
        private ICommand _createLobby; 

        public int IsSuccess { get { return _isSuccess; } set { _isSuccess = value; } }

        public ICommand CreateLobbyCommand
        {
            get
            {
                if(_createLobby == null)
                {
                    _createLobby = new RelayCommand(CreateLobby);
                }
                return _createLobby;
            }
        }
        public string LobbyName
        {
            get
            {
                return _lobbyName;
            }
            set
            {
                _lobbyName = value;
                OnPropertyChanged("LobbyName");
            }
        }

        private void CreateLobby(object parameter)
        {
            string ln = _lobbyName;
            int nr = _isSuccess;
        }


    }
}
