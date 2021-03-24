using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Timers;
using _2CaptchaAPI;
using Newtonsoft.Json.Linq;

namespace DiscordAccountGen
{
    class Program
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        static void Main(string[] args)
        {
            Console.Title = "Discord Account Generator";

            string finger = "";
            using (var wb = new WebClient())
            {
                finger = wb.DownloadString("https://discordapp.com/api/v8/experiments");
            }

            JObject obj = JObject.Parse(finger);
            string FingerPrint = obj["fingerprint"].Value<string>();


            HttpWebRequest webRequest = (HttpWebRequest) HttpWebRequest.Create("https://discordapp.com/api/v8/auth/register");
            webRequest.Method = "POST";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36";
            webRequest.ContentType = "application/json";
            webRequest.Accept = "*/*";
            webRequest.Referer = "https://discord.com/register";
            string super_properties = Base64Encode("{\"os\": \"Windows\", \"browser\": \"Chrome\", \"device\": \"\", \"browser_user_agent\": \"" + "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36" + "\", \"browser_version\": \"88.0.4324.190\", \"os_version\": \"10\", \"referrer\": \"\", \"referring_domain\": \"\", \"referrer_current\": \"\", \"referring_domain_current\": \"\", \"release_channel\": \"stable\", \"client_build_number\": 78727, \"client_event_source\": null}");
            webRequest.Headers["x-fingerprint"] = FingerPrint;
            webRequest.Headers["x-super-properties"] = super_properties;
            webRequest.Headers["authority"] = "discord.com";
            webRequest.Headers["accept-language"] = "en-US";
            webRequest.Headers["origin"] = "https://discord.com";
            webRequest.Headers["sec-fetch-site"] = "same-origin";
            webRequest.Headers["sec-fetch-mode"] = "cors";
            webRequest.Headers["sec-fetch-dest"] = "empty";
            
            var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\proxies.txt");
            var random1 = new Random();
            var randomLineNumber = random1.Next(0, lines.Length - 1);
            var line1 = lines[randomLineNumber];
            webRequest.Proxy = new WebProxy(line1);

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            var mails = new StreamReader(Directory.GetCurrentDirectory() + "\\mails.txt");
            string line;
            while ((line = mails.ReadLine()) != null)
            {
                
                var lines11 = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\meaningless.txt");
                var random11 = new Random();
                var randomLineNumber1 = random11.Next(0, lines11.Length - 1);
                var line11 = lines11[randomLineNumber1];
                
                string data = "{ \"fingerprint\": \"" + FingerPrint + "\", \"email\": \"" + line + "\", \"username\": \"" + line11 + "\", \"password\": \"SOMERANDOMPASSWORD123F\", \"invite\": \"InvitationCode\", \"consent\":\"true\",\"date_of_birth\":\"2000-12-01\",\"gift_code_sku_id\":\"null\",\"captcha_key\":\"" + new _2Captcha("YOUR2CAPTCHAKEY").SolveHCaptcha("f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34", "https://discord.com").Result.Response + "\"}";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    streamWriter.WriteLine(data);
                }

                var httpResponse = (HttpWebResponse) webRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    Console.WriteLine(streamReader.ReadToEnd());
                    Console.ReadLine();
                }
            }
        }
    }
}
