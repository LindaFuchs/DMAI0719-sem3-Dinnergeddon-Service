using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DinnergeddonUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            LobbyCreateTest lb = LobbyCreateTest.Instance;
            ObservableCollection<Lobby> items = lb.lobbies;
            LobbiesListView.ItemsSource = items;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {




            // Open the child window
            CreateLobbyDialog createLobbyDialog = new CreateLobbyDialog();


            createLobbyDialog.ShowDialog();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //MainWindow mw = new MainWindow();
            //mw.Show();
            //this.Close();
        }
    }
}
