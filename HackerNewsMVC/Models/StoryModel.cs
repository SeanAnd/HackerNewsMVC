using System;
using System.ComponentModel.DataAnnotations;

namespace HackerNewsMVC.Models
{
    public class StoryModel
    {
        public string by { get; set; }
        public string descendants { get; set; }
        public int id { get; set; }
        public string[] kids { get; set; }
        public int score { get; set; }
        public int time { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }
    }
}