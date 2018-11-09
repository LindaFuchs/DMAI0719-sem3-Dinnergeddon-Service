using System;
using System.Collections.Generic;
using System.ServiceModel;
using DinnergeddonService;

namespace Dinnergeddon
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8733/DinnergeddonService/");

            ServiceHost proxy = new ServiceHost(typeof(AccountService), baseAddress);

            try
            {
                proxy.AddServiceEndpoint(typeof(IAccountService), new BasicHttpBinding(), "AccountService");

                proxy.Open();

                Console.WriteLine("Service has been started");
                Console.WriteLine("Press enter to terminate service...");
                Console.ReadLine();
                
                proxy.Close();
            }
            catch(CommunicationException e)
            {
                Console.WriteLine(e.Message);
                proxy.Abort();
            }
        }
    }
}
