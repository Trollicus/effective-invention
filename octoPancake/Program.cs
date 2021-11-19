//#define Proxy

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AntiCaptchaAPI;
using Newtonsoft.Json;

namespace octoPancake
{
    public class Program
    {
        public static Program CreateInstance()
        {
            return new Program();
        }

        public class Discord
        {
            [JsonProperty("fingerprint")] public string Fingerprint { get; set; }
        }

        private static readonly HttpClientHandler Handler = new HttpClientHandler();
        
        private static readonly HttpClient Client = new HttpClient(Handler);

        /// <summary>
        ///  The Method represents asynchronous Http Request 
        /// </summary>
        /// <param name="uri">The URI requested to</param>
        /// <param name="method">The Method</param>
        /// <param name="json">Content sent</param>
        /// <param name="headers">Used Headers</param>
        /// <returns>The task object representing the asynchronous operation</returns>
        [DebuggerStepThrough]
        private static async Task<HttpResponseMessage> PostAsync(string uri, HttpMethod method,
            string json, IDictionary<string, string> headers)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = method,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };


            if (headers == null) return await Client.SendAsync(request);

            foreach (var header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return await Client.SendAsync(request);
        }

        private static string Base64Encode(string plainText) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));


        private static readonly Random Random = new Random();

        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private static string RandomString(int length) =>
            new string(Enumerable.Repeat(Chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        
        private static async Task<string> Captcha()
        {
            var captcha = new AntiCaptcha("YOUR_ANTI_CAPTCHA_KEY");
            var funCaptcha =
                await captcha.SolveHCaptcha("f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34", "https://discord.com");

            return funCaptcha.Response;
        }


        public static async Task Main()
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\mails.txt"))
                File.Create("mails.txt").Dispose();

            if (new FileInfo(Directory.GetCurrentDirectory() + "\\mails.txt").Length == 0)
                Console.WriteLine("[!] You do not have e-mails");
            
                
            
            const string proxy = "";

            using var wb = new WebClient();

#if Proxy
            wb.Proxy = new WebProxy(proxy);
            Handler.Proxy = new WebProxy(proxy);
#endif


            var fingerPrint = wb.DownloadString("https://discordapp.com/api/v9/experiments");

            var data = JsonConvert.DeserializeObject<Discord>(fingerPrint);


            var list = new Dictionary<string, string>
            {
                {
                    "x-fingerprint",
                    data?.Fingerprint
                },
                {
                    "Accept",
                    "application/json"
                },
                {
                    "referer",
                    "https://discord.com/register"
                },
                {
                    "x-super-properties",
                    Base64Encode(
                        "{\"os\":\"Windows\",\"browser\":\"Chrome\",\"device\":\"\",\"system_locale\":\"en-US\",\"browser_user_agent\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36\",\"browser_version\":\"94.0\",\"os_version\":\"60\",\"referrer\":\"\",\"referring_domain\":\"\",\"referrer_current\":\"\",\"referring_domain_current\":\"\",\"release_channel\":\"stable\",\"client_build_number\":105304,\"client_event_source\":null}")
                },
                {
                    "authority",
                    "https://discord.com"
                },
                {
                    "origin",
                    "https://discord.com"
                },
                {
                    "sec-fetch-site",
                    "same-origin"
                },
                {
                    "sec-fetch-mode",
                    "cors"
                },
                {
                    "sec-fetch-dest",
                    "empty"
                }
            };

            var password = RandomString(8);
            
            Console.WriteLine($"Your account password: {password}");
            
            var mails =
                new List<string>(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\mails.txt"));
            
            var index = Random.Next(mails.Count);
            
            var response = await PostAsync("https://discordapp.com/api/v9/auth/register", HttpMethod.Post,
                $"{{\"fingerprint\": \"{data?.Fingerprint}\", \"email\": \"{mails[index]}\", \"password\": \"{password}\", \"username\": \"Doogsy\", \"consent\": \"true\", \"date_of_birth\": \"2000-12-01\", \"captcha_key\": \"{Captcha().Result}\", \"invite\": \"null\", \"promotional_email_opt_in\":\"false\"}}",
                list);

            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Console.ReadLine();
        }
    }
}