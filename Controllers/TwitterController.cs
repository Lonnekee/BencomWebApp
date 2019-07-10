using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using BencomWebApp.Models;
using Microsoft.Extensions.Configuration;
using BencomWebApp.Util;

namespace BencomWebApp.Controllers
{
    public class TwitterController : Controller
    {   
        // The first page that is loaded is Index(), where the user is asked for input.
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Index(Feed formModel) is called when the Index page has received input (formModel).
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(Feed formModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ShowFeed", "Twitter", formModel);
            }
            else
            {
                return View();
            }
        }

        // ShowFeed loads when Index redirects to it.
        public IActionResult ShowFeed(Feed givenModel)
        {
            Feed viewModel = RequestTweets(givenModel);
            return View(viewModel);
        }

        /* 
         * RequestTweets contains an asynchronous process that blocks the program until
         * a response from the Twitter API is received and one that converts the response to
         * a string.
         */
        private Feed RequestTweets(Feed model)
        {
            string userName = model.UserName;
            string UrlParameters = $"1.1/search/tweets.json?l=&q=from%3A{userName}&src=typd";
            string bearerToken = Constants.BearerTokenTwitter;
            HttpClient client = new HttpClient { BaseAddress = new Uri(Constants.TwitterURL) };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");

            /* Request all tweets send by the given user in the past week,
             * up to a maximum of 15 tweets. */
            HttpResponseMessage response = client.GetAsync(UrlParameters).Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Convert the response to a string, so that it can be parsed by Newtonsoft.Json.Linq's JObject.
                string jsonResponse = response.Content.ReadAsStringAsync().Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.
                JObject parsedTweets = JObject.Parse(jsonResponse);
                ViewData["Original"] = parsedTweets;
                return new Feed(parsedTweets);
            }
            else
            {
                return new Feed();
            }
        }
    }
}