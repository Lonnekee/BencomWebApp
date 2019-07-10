using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace BencomWebApp.Util
{
    public static class Constants
    {
        public static const string BearerTokenTwitter = "AAAAAAAAAAAAAAAAAAAAAAnq%2FAAAAAAA55Pv3OaiypsfmLaDj%2BX0mzuZW8k%3D509Uq8EZojS9C49dqIW2u8kNWWUpmRMK3YUfvO6maonu6HfeBA";
        public static const string AccessTokenInstagram = "360949981257304|uIKWn1PdZxXPZAx-Uqmqe3NBR_E";
        public static const int MaxLengthTwitterUserName = 15;
        public static const string TwitterImageBaseUrl = "https://t.co/";
        public static const string TwitterURL = "https://api.twitter.com/";
    }
}
