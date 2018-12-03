using DinnergeddonUI.Interfaces;
using DinnergeddonUI.ViewModels;
using System.Windows;

namespace DinnergeddonUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IView
    {
        public LoginWindow(AuthenticationViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get { return DataContext as IViewModel; }
            set { DataContext = value; }
        }
    }
}
