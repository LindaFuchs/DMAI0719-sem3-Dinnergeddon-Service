using System;
using ServiceClientTest.AccountService;

namespace ServiceClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IAccountService client = new AccountServiceClient();

            client.Registration("Nikola", "@gmail", "123", 1);

        }
    }
}
