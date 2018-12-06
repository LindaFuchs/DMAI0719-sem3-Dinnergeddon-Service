using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinnergeddonWeb.AccountServiceReference;
using DinnergeddonWeb.Models;

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
    }
}