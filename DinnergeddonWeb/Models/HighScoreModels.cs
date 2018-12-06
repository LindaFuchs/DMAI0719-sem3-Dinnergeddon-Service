using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DinnergeddonWeb.Models
{
    public class HighScoreModel
    {
        [Display(Name="Username")]
        public String UserName { get; set; }

        [Display(Name = "Score")]
        public int HighScore { get; set; }

        [Display(Name = "Rank")]
        public int Rank { get; set; }
        
    }
}