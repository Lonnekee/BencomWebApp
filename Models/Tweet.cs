using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BencomWebApp.Util;

namespace BencomWebApp.Models
{
    public class Tweet
    {
        // Properties
        public string Name { get; private set; }
        public string UserName { get; private set; }
        public string Text { get; private set; }
        public DateTime Date { get; private set; }
        public string MediaUrlImage { get; private set; }

        // Constructor
        public Tweet(string name, string userName, string text, DateTime date, string mediaUrlImage)
        {
            Name = name;
            UserName = userName;
            Text = text;
            Date = date;
            MediaUrlImage = mediaUrlImage;

            /* The url to the image should only be removed from the text when it contains an image.
             * There can also be an url in the text when the tweet is too long.
             */
            if (mediaUrlImage != null)
            {
                RemoveImageFromText();
            }
        }

        /* Each picture that is inluded in a Tweet is also included as a link that starts with
         * 'https://t.co/' in the Text. We want to get this link out of the text and instead
         * show the image with the link in mediaUrlImage (which is a showable jpg-file).
         */
        private void RemoveImageFromText()
        {
            string url = Constants.TwitterImageBaseUrl;

            if (Text.Contains(url))
            {
                Regex r = new Regex(@"\s");
                string[] blocks = r.Split(Text); // The text is splitted at the white-spaces.
                foreach (var i in blocks)
                {
                    if (i.Contains(url)) // The first block that contains the url is removed from the text.
                    {
                        Text = Text.Remove(Text.IndexOf(i), i.Length); // Note that this may result in two spaces after each other.
                        return;
                    }
                }
            }
            return;
        }
    }
}
