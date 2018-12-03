using System;
using System.Windows;
using DinnergeddonUI.ViewModel;

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
            AuthenticationViewModel viewModel = new AuthenticationViewModel();

            loginWindow.DataContext = viewModel;
            loginWindow.Show();

            //IView loginWindow = new LoginWindow(viewModel);
            //loginWindow.Show();


        }
    }
}
