using System;
using System.Windows;
using DinnergeddonUI.ViewModels;

namespace DinnergeddonUI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Create a custom principal with an anonymous identity at startup
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            base.OnStartup(e);

            MainWindow loginWindow = new MainWindow();

            //Show the login view
            MainWindowViewModel viewModel = new MainWindowViewModel();

            loginWindow.DataContext = viewModel;
            loginWindow.Show();
        }
    }
}
