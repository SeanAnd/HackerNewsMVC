using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackerNewsMVC.Models;
using HackerNewsMVC.Helper;
using Newtonsoft.Json;
using System.Net.Http;

namespace HackerNewsMVC.Controllers
{

    public class HomeController : Controller
    {
        HackerNewsAPI myAPI = new HackerNewsAPI();

        public async Task<ActionResult> Index()
        {
            List<StoryModel> storyList = new List<StoryModel>();
            HttpClient httpClient = myAPI.Initial();
            HttpResponseMessage response = await httpClient.GetAsync("topstories.json?print=pretty");

            if (response.IsSuccessStatusCode)
            {
                //store the response as a string, clean it up, and make an array from it.
                string result = response.Content.ReadAsStringAsync().Result.ToString();
                var chars = new string[]{ "]", "[", "\""};
                foreach (var c in chars)
                {
                    result = result.Replace(c, string.Empty);
                }
                string trimmedResult = result.Trim();
                int[] storyIDs = Array.ConvertAll(trimmedResult.Split(','), int.Parse);
                //reduced the result set due to azure timing out.
                Array.Resize(ref storyIDs, storyIDs.Length - 400);

                //it puts the reponse in the list and does what it's told or else it gets the loop again.
                foreach(int id in storyIDs)
                {
                    HttpResponseMessage newResponse = await httpClient.GetAsync("item/" + id + ".json?print=pretty");
                    if (newResponse.IsSuccessStatusCode)
                    {
                        var storyResult = newResponse.Content.ReadAsStringAsync().Result;
                        storyList.Add(JsonConvert.DeserializeObject<StoryModel>(storyResult));
                    }
                }
            }           
            return View(storyList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<ActionResult> Details(int? id)
        {
            StoryModel story = new StoryModel();
            HttpClient httpClient = myAPI.Initial();
            HttpResponseMessage response = await httpClient.GetAsync("item/" + id + ".json?print=pretty");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                story = JsonConvert.DeserializeObject<StoryModel>(result);
            }

            return View(story);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
