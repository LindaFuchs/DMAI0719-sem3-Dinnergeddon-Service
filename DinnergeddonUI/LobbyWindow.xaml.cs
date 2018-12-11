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
using DinnergeddonUI.LobbyServiceReference;


using DinnergeddonUI.ViewModels;

namespace DinnergeddonUI
{
    /// <summary>
    /// Interaction logic for LobbyWindow.xaml
    /// </summary>
    public partial class LobbyWindow : Window
    {
        public LobbyWindow(Lobby lobby, Window dashboardWindow )
        {
            //var lobbyViewModel = new LobbyViewModel(lobby, dashboardWindow);
            //DataContext = lobbyViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
