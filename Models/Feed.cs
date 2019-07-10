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
        [TwitterUserName] // Custom-made attribute. It can be found in the folder "Util".
        public string UserName { get; set; }

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

                /* DateTime is parsed from string in the following format:
                 * "ddd MMM dd HH:mm:ss zzz yyyy" = 
                 * "'day of the week' 'month in letters' 'day of the month with two numbers' 'up to 24 two-digit hour' 
                 * 'two-digit minutes' 'two-digit seconds' 'timezone' 'four-digit year'".
                 */
                ds = ds.Insert(23, ":"); // The timezone needs to have the following format: +00:00.
                CultureInfo provider = new CultureInfo("en-US"); // Without culture info, names of months and weekdays won't be recognized.
                DateTime date = DateTime.ParseExact(ds, "ddd MMM dd HH:mm:ss zzz yyyy", provider); 

                AllTweets.Add(new Tweet(name, userName, text, date, mediaUrlImage));
            }
        }
    }
}
