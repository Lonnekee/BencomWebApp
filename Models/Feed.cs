using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using BencomWebApp.Util;
using Newtonsoft.Json.Linq;

namespace BencomWebApp.Models
{
    public class Feed
    {
        // Properties
        public List<Tweet> AllTweets { get; private set; }

        [Required]
        [TwitterUserName] // Custom-made attribute. It can be found in folder "util".
        public string UserName { get; set; }
        public string BearerToken { get; set; }

        // Constructors
        public Feed()
        {
            AllTweets = new List<Tweet>();
        }

        public Feed(JObject parsedJsonFeed)
        {
            AllTweets = new List<Tweet>();

            foreach (var item in parsedJsonFeed["statuses"])
            {
                string name = (string)item["user"]["name"];
                string userName = (string)item["user"]["screen_name"];
                string text = (string)item["text"];
                string ds = (string)item["created_at"];
                string mediaUrlImage = null;
                if (item.SelectToken("entities.media[0].media_url") != null)
                {
                    mediaUrlImage = (string)item.SelectToken("entities.media[0].media_url");
                }

                // Convert the string with the date to a DateTime object
                ds = ds.Insert(23, ":"); // The timezone needs to have the following format: +00:00
                CultureInfo provider = new CultureInfo("en-US"); // Without culture info, names of months and weekdays won't be recognized.
                DateTime date = DateTime.ParseExact(ds, "ddd MMM dd HH:mm:ss zzz yyyy", provider);

                AllTweets.Add(new Tweet(name, userName, text, date, mediaUrlImage));
            }
        }

        public void AddTweet(Tweet t)
        {
            AllTweets.Add(t);
        }

    }
}
