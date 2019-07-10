using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace BencomWebApp.Util
{
    public static class Constants
    {
        public const string BearerTokenTwitter = "AAAAAAAAAAAAAAAAAAAAAAnq%2FAAAAAAA55Pv3OaiypsfmLaDj%2BX0mzuZW8k%3D509Uq8EZojS9C49dqIW2u8kNWWUpmRMK3YUfvO6maonu6HfeBA";
        public const int MaxLengthTwitterUserName = 15;
        public const string TwitterImageBaseUrl = "https://t.co/";
        public const string TwitterURL = "https://api.twitter.com/";
    }
}
