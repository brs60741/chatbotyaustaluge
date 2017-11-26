using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TweetSharp;

namespace gospaderabotai
{
    class Program
    {
        private static string customer_key = "c3xxd0P6VWkXCO4nxdrZVktvv";
        private static string customer_key_secret = "OYIcNet7JJdk0gS6pJQb4WhPsFOTsTz31bcsiNOnfXN44v3xEc";
        private static string access_token = "4823527966-blLDQDmpcr2uori492sfhTSXAe5Da8iyDVEIhNq";
        private static string access_token_secret = "3PrLNyfEZpU4hz9vUWjM9DcGWoHzldjtuonisxkDtaz50";

        private static TwitterService service = new TwitterService(customer_key, customer_key_secret, access_token, access_token_secret);

        private static int currentImageID = 0;
        private static List<string> imageList = new List<string> { $"C:/plzrabotau.jpg" };


        static void Main(string[] args)
        {
            Console.WriteLine($"<{DateTime.Now}> - Bot Started");
            //  SendTweet("Госпаде наконец-то");
            SendMediaTweet("plz rabotau", currentImageID);
            Console.Read();
        }

        private static void SendTweet(string _status)
        {
            service.SendTweet(new SendTweetOptions { Status = _status }, (tweet, response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<ERROR> " + response.Error.Message);
                    Console.ResetColor();
                }
            });
        }

        private static void SendMediaTweet(string _status, int imageID)
        {
            using (var stream = new FileStream(imageList[imageID], FileMode.Open))
            {
                service.SendTweetWithMedia(new SendTweetWithMediaOptions
                {
                    Status = _status,
                    Images = new Dictionary<string, Stream> { { imageList[imageID], stream } }
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                Console.ResetColor();

                if ((currentImageID +1) == imageList.Count)
                    {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<BOT> - End of Image Array");
                    Console.ResetColor();
                    currentImageID = 0;
                }
                else
                {
                    currentImageID++;
                }
            }
        }
    }
}
