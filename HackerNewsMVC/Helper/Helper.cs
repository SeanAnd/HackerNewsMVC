using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNewsMVC.Helper
{
    public class HackerNewsAPI
    {
        // *Slap's function* this baby returns the client
        public HttpClient Initial()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
            return httpClient;
        }
    }
}