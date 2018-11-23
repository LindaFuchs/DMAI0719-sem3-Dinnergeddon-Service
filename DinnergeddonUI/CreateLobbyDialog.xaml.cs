using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DinnergeddonUI.DinnergeddonService;

namespace DinnergeddonUI
{
    /// <summary>
    /// Interaction logic for CreateLobbyDialog.xaml
    /// </summary>
    public partial class CreateLobbyDialog : Window
    {
        LobbyServiceClient _proxy = new LobbyServiceClient();

        public CreateLobbyDialog()
        {
            InitializeComponent();

        }

        private void CreateLobby(object sender, RoutedEventArgs e)
        {
            LobbyNameBorder.BorderBrush = (Brush)FindResource("InputFormColor");
            PasswordBorder.BorderBrush= (Brush)FindResource("InputFormColor");
            ConfirmPasswordBorder.BorderBrush = (Brush)FindResource("InputFormColor");

            string lobbyName = LobbyNameTextBox.Text;
            int playerCount = GetPlayerCount();
            if (string.IsNullOrEmpty(lobbyName) || lobbyName.Length < 3)
            {
            LobbyNameBorder.BorderBrush = (Brush)FindResource("IncorrectColor");
                return;
            }
            if(PasswordCheckbox.IsChecked == true)
            {
                string pass = PasswordTextBox.Password;
                string passConf = ConfirmPasswordTextBox.Password;
               if(ValidatePassword(pass, passConf))
                {
                    _proxy.CreatePrivateLobby(lobbyName, playerCount, pass);
                    this.DialogResult = true;

                }
            }
            else
            {
                // string pass = PasswordTextBox.Password;
                _proxy.CreateLobby(lobbyName, playerCount);
                    //LobbyCreateTest lb = LobbyCreateTest.Instance;
                    //lb.CreateLobby(lobbyName, playerCount);

                    this.DialogResult = true;

                
            }
            
        }

        private bool ValidatePassword(String pass, String passConf)
        {
            

            if (string.IsNullOrEmpty(pass) || pass.Length<4)
            {
                PasswordBorder.BorderBrush = (Brush)FindResource("IncorrectColor");
                ConfirmPasswordBorder.BorderBrush = (Brush)FindResource("IncorrectColor");
                

            }

            if (string.IsNullOrEmpty(passConf) || PasswordTextBox.Password != ConfirmPasswordTextBox.Password)
            {
                ConfirmPasswordBorder.BorderBrush = (Brush)FindResource("IncorrectColor");
                return false;
            }

            return true;

           
        }

        private  int GetPlayerCount()
        {
            if (Convert.ToBoolean(fivePlayers.IsChecked))
            {
                return 5;
            }
            if (Convert.ToBoolean(fourPlayers.IsChecked))
            {
                return 4;
            }
            if (Convert.ToBoolean(threePlayers.IsChecked))
            {
                return 3;
            }
            if (Convert.ToBoolean(twoPlayers.IsChecked))
            {
                return 2;
            }
            return 4;
        }
    }
}
