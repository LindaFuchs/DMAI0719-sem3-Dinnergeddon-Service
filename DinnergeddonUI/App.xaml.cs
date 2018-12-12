using DinnergeddonUI.Helpers;
using DinnergeddonUI.ViewModels;
using DinnergeddonUI.Views;
using System;
using System.Windows;


namespace DinnergeddonUI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Create a custom principal with an anonymous identity at startup
            //CustomPrincipal customPrincipal = new CustomPrincipal();
            //AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);
            base.OnStartup(e);

            MainWindow app = new DinnergeddonUI.Views.MainWindow();
            //Show the login view
            MainWindowViewModel context = new MainWindowViewModel();

            app.DataContext = context;
            app.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                Mediator.Notify("LeaveLobbyOnExit", "");
            }
            finally
            {
                base.OnExit(e);
            }
        }
    }
}
