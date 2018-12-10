using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinnergeddonWeb.AccountServiceReference;
using DinnergeddonWeb.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DinnergeddonWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly AccountServiceClient _proxy;
        private readonly HighscoreServiceClient _highScoreProxy;
        public HomeController()
        {
            _proxy = new AccountServiceClient();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    

        public ActionResult HighScores()
        {
            IEnumerable<Account> accounts = _proxy.GetAccounts();

            ICollection<HighScoreModel> highScoreModels = new List<HighScoreModel>();

            
            //TODO: Uncomment once we have highscores for the users 
           //Dictionary<Guid ,  int> highScores=  _highScoreProxy.GetHighscores();
           // foreach(KeyValuePair<Guid,int> kvp in highScores)
           // {
           //     HighScoreModel hm = new HighScoreModel { UserName = _proxy.FindById(kvp.Key).Username, HighScore = kvp.Value };
           //     highScoreModels.Add(hm);
           // }

            Random rnd = new Random();
            int rank = 0;
            foreach (Account account in accounts)
            {
                rank++;
                highScoreModels.Add(new HighScoreModel { Rank= rank ,UserName = account.Username, HighScore = rnd.Next(1, 9999) });


            }
            return View(highScoreModels);
        }

        public FileResult Download()
        {
            //The path to the file to download.
            //TODO: Change this so it isn't a hard path and to the .exe when it's made.
            String filePath = @"D:\anoobis\CODE N SHIT\3rd Semester Project\Dinnergeddon\DinnergeddonWeb\images\logo.png";
            //Converts the file content into an array of bytes.
            byte[] fileContent = System.IO.File.ReadAllBytes(filePath);
            
            //Returns the file content along with the type for the download.
            //TODO: REMEMBER TO CHANGE THE FILE TYPE
            return File(fileContent, "application/png", "DinnergeddonInstaller.png");
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string apiKey = ConfigurationManager.AppSettings["ApiKey"];//Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
                    var client = new SendGridClient(apiKey);
                    var from = new EmailAddress(vm.Email);
                    var subject = "contact form";
                    var to = new EmailAddress(ConfigurationManager.AppSettings["Receiver"]);
                    var message = vm.Message;

                    SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

                    // Send the email.
                    if (client != null)
                    {
                        client.SendEmailAsync(msg);
                    }



                    ModelState.Clear();
                    ViewBag.Message = "Thank you for getting in touch! We will get back to you as soon as possible. ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing a problem here {ex.Message}";
                }
            }

            return View();
        }

    }

}