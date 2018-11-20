using System;
using System.ServiceModel;
using DinnergeddonService;


namespace DinnergeddonHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8000/DinnergeddonService/");

            ServiceHost host = new ServiceHost(typeof(AccountService), baseAddress);

            try
            {
                host.AddServiceEndpoint(typeof(IAccountService), new BasicHttpBinding(), "AccountService");

                host.Open();

                Console.WriteLine("Service has been started");
                Console.WriteLine("Press enter to terminate service...");
                Console.ReadLine();

                host.Close();
            }
            catch (CommunicationException e)
            {
                Console.WriteLine(e.Message);
                host.Abort();
            }
        }
    }
}
