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

namespace DinnergeddonUI
{
    /// <summary>
    /// Interaction logic for CreateLobbyDialog.xaml
    /// </summary>
    public partial class CreateLobbyDialog : Window
    {
        public CreateLobbyDialog()
        {
            InitializeComponent();
        }

        private void CreateLobby(object sender, RoutedEventArgs e)
        {

            int playerCount = GetPlayerCount();
            if (string.IsNullOrEmpty(LobbyNameTextBox.Text) || LobbyNameTextBox.Text.Length < 4)
            {
            LobbyNameBorder.BorderBrush = (Brush)FindResource("IncorrectColor");

            }
            if(PasswordCheckbox.IsChecked == true)
            {
                ValidatePassword();
            }
        }

        private bool ValidatePassword()
        {
            string pass = PasswordTextBox.Password;
            string passConf = ConfirmPasswordTextBox.Password;

            if (string.IsNullOrEmpty(pass) || pass.Length<4)
            {
                PasswordBorder.BorderBrush = (Brush)FindResource("IncorrectColor");
                ConfirmPasswordBorder.BorderBrush = (Brush)FindResource("IncorrectColor");

            }

            if (PasswordTextBox.Password != ConfirmPasswordTextBox.Password)
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
