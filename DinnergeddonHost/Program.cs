using DinnergeddonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DinnergeddonHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8733/DinnergeddonService/");

            ServiceHost host = new ServiceHost(typeof(IAccountService), baseAddress);

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
