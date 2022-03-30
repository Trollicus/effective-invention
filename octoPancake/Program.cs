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
        
        private static string Token { get; set; }

        private static readonly HttpClientHandler Handler = new HttpClientHandler();
        
        private static readonly HttpClient Client = new HttpClient(Handler);
        
        /// <summary>
        /// Asynchronous HTTP Request
        /// </summary>
        /// <param name="uri">URI Requested to</param>
        /// <param name="method">HTTPMethod</param>
        /// <returns>The task object representing the asynchronous operation</returns>
        private static async Task<HttpResponseMessage> PostAsync(string uri, HttpMethod method)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = method,
            };

            return await Client.SendAsync(request);
        }
        
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
            string json, IDictionary<string, string> headers = null)
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
            var captcha = new AntiCaptcha("YOUR_ANTICAPTCHA_KEY");
            var funCaptcha =
                await captcha.SolveHCaptcha("4c672d35-0701-42b2-88c3-78380b0db560", "https://discord.com");

            return funCaptcha.Response;
        }
        
       
        //TODO: Cleaner code

        public static async Task Main()
        {
            
            // ReSharper disable once UnusedVariable
            const string proxy = ""; 

            using var wb = new WebClient();

#if Proxy
            wb.Proxy = new WebProxy(proxy);
            Handler.Proxy = new WebProxy(proxy);
#endif


            var fingerPrint = wb.DownloadString("https://discord.com/api/v9/experiments");

            var data = JsonConvert.DeserializeObject<Discord>(fingerPrint);


             var list = new Dictionary<string, string>
            {
                {
                    "X-Fingerprint",
                    data?.Fingerprint
                },
                {
                    "Accept",
                    "application/json"
                },
                {
                    "Referer",
                    "https://discord.com/register"
                },
                {
                    "X-Super-Properties",
                    Base64Encode(
                        "{\"os\":\"Linux\",\"browser\":\"\",\"device\":\"\",\"system_locale\":\"en-US\",\"browser_user_agent\":\"Links (2.3pre1; Linux 2.6.38-8-generic x86_64; 170x48)\",\"browser_version\":\"\",\"os_version\":\"2.6.38\",\"referrer\":\"\",\"referring_domain\":\"\",\"referrer_current\":\"\",\"referring_domain_current\":\"\",\"release_channel\":\"stable\",\"client_build_number\":121017,\"client_event_source\":null}")
                },
                {
                    "Origin",
                    "https://discord.com"
                },
                {
                    "X-Debug-Options",
                    "bugReporterEnabled"
                },
                {
                    "X-Discord-Locale",
                    "en-US"
                },
                {
                    "User-Agent",
                    "Links (2.3pre1; Linux 2.6.38-8-generic x86_64; 170x48)"
                }
            };

            var password = RandomString(8);
            
            Console.WriteLine($"Your account password: {password}");

            var response = await PostAsync("https://discord.com/api/v9/auth/register", HttpMethod.Post,
                $"{{\"fingerprint\": \"{data?.Fingerprint}\", \"email\": \"YOUR-EMAIL-HERE\", \"password\": \"{password}\", \"username\": \"DoubtYuo\", \"consent\": \"true\", \"date_of_birth\": \"1999-11-02\", \"captcha_key\": \"{Captcha().Result}\", \"invite\": \"null\", \"gift_code_sku_id\":\"null\"}}",
                list);

            Token = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine(Token);


            string path = Directory.GetCurrentDirectory() + "\\tokens.txt";
            
            if (!File.Exists(path))
                File.Create("tokens.txt").Dispose();
            
            using (var outputFile = new StreamWriter(path))
            {
                await outputFile.WriteLineAsync(Token + Environment.NewLine);
            }
            
            //TODO: Begin verification
            
            //Verify e-mail: expected in week and a half.
            
            //Reset Password 
            
            //Verify Phone number
            
            Console.ReadLine();
        }
    }
}
