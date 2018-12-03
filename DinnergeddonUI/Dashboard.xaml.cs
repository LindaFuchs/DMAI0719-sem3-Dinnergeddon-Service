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
using DinnergeddonUI.DinnergeddonService;

using DinnergeddonUI.Interfaces;


namespace DinnergeddonUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        LobbyServiceClient _proxy = new LobbyServiceClient();
        //private ObservableCollection<DinnergeddonService.Lobby> lobbies;
        public Dashboard()
        {
            DataContext = new DashboardViewModel(new AuthenticationService(), this);

            InitializeComponent();
            
            //RefreshLobbies();
        }

        public IViewModel ViewModel
        {
            get { return DataContext as IViewModel; }
            set { DataContext = value; }
        }

        public List<DinnergeddonService.Lobby> Lobbies
        {
            get;
            set;
        }

        private void RefreshLobbies()
        {
          //  var items = new ObservableCollection<DinnergeddonService.Lobby>(_proxy.GetLobbies());
           // LobbiesListView.ItemsSource = items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {




            // Open the child window
            CreateLobbyDialog createLobbyDialog = new CreateLobbyDialog();


            createLobbyDialog.ShowDialog();

            RefreshLobbies();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //MainWindow mw = new MainWindow();
            //mw.Show();
            //this.Close();
        }
    }
}
