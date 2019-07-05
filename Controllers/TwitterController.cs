using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BencomWebApp.Controllers
{
    public class TwitterController : Controller
    {
        private const string URL = "https://api.twitter.com/";
        private string urlParameters = "1.1/search/tweets.json?l=&q=from%3ALonneke56539540&src=typd";
        private string bearerToken = "todo";

        /* Before loading the page, the twitter data is requested inside this controller. */
        public IActionResult Index()
        {
            Authorize();
            requestTweets();
            return View();
        }

        private void Authorize()
        {
            HttpResponseMessage bearerResult = RequestBearerToken().Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.

            if (bearerResult.IsSuccessStatusCode)
            {
                string bearerData = bearerResult.Content.ReadAsStringAsync().Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.
                bearerToken = JObject.Parse(bearerData)["access_token"].ToString();
            }
        }

        private async Task<HttpResponseMessage> RequestBearerToken()
        {
            string encodedPair = "YlhITjE0Z1IyczNuRURGZTljN0lQb3d6VDpuV1lVbE52aFFIY1BSZDNxRWpLWE15amRDTWc5R1V3ZFo4QjljVHl3QzNPWlJwMEN5aA==";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL + "oauth2/token");

            HttpRequestMessage requestToken = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.twitter.com/oauth2/token"),
                Content = new StringContent("grant_type=client_credentials")
            };

            requestToken.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };
            requestToken.Headers.TryAddWithoutValidation("Authorization", $"Basic {encodedPair}");

            return await client.SendAsync(requestToken);
        }

        /* This controller does not load a page, but is a helper function for Index() */
        private void requestTweets()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");

            ViewData["Auth"] = bearerToken;

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                ViewData["Message"] = response.Content.ReadAsStringAsync().Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.

                // TODO: when json files are converted to Tweet objects.
                // dynamic parsedTweet = JObject.Parse(jsonResponse);


            }
            else
            {
                ViewData["Message"] = "No tweets are received.";
            }

            client.Dispose();
        }
    }
}