using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _2CaptchaAPI;
using Newtonsoft.Json;
using RestSharp;

namespace DiscordAccountGenerator2
{
    internal class Program
    {
        public class Discord
        {
            [JsonProperty("fingerprint")]
            public string fingerprint { get; private set; }
            
            [JsonProperty("token")]
            public string token { get; private set; }
        }
        
        class getState
        {
            [JsonProperty("number")] public string number { get; private set; }

            [JsonProperty("msg")] public string msg { get; private set; }
        }

        class Phone
        {
            [JsonProperty("phone_token")] public string phone_token { get; private set; }
        }

        private static string Base64Encode(string plainText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }

        private static Random random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static readonly List<string> proxies =
            new List<string>(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\proxies.txt"));

        private static readonly List<string> mails =
            new List<string>(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\mails.txt"));


        private static int num = 0;
        
        public static void Main(string[] args)
        {
            Parallel.ForEach(proxies, new ParallelOptions() {MaxDegreeOfParallelism = 300}, (i) =>
            {
                string data = "";
                using var wb = new WebClient();
                data = wb.DownloadString("https://discordapp.com/api/v9/experiments");

                var discord = JsonConvert.DeserializeObject<Discord>(data);

                var client = new RestClient("https://discordapp.com/api/v9/auth/register")
                {
                    UserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36",
                    Proxy = new WebProxy(i)
                };

                var request = new RestRequest();
                request.Method = Method.POST;

                request.AddHeader("Accept", "application/json");
                request.AddHeader("referer", "https://discord.com/register");
                request.AddHeader("x-fingerprint", discord.fingerprint);
                request.AddHeader("x-super-properties",
                    Base64Encode(
                        "{\"os\": \"Windows\", \"browser\": \"Chrome\", \"device\": \"\", \"browser_user_agent\": \"" +
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.190 Safari/537.36" +
                        "\", \"browser_version\": \"88.0.4324.190\", \"os_version\": \"10\", \"referrer\": \"\", \"referring_domain\": \"\", \"referrer_current\": \"\", \"referring_domain_current\": \"\", \"release_channel\": \"stable\", \"client_build_number\": 90176, \"client_event_source\": null}"));
                request.AddHeader("authority", "https://discord.com");
                request.AddHeader("accept-language", "en-US");
                request.AddHeader("origin", "https://discord.com");
                request.AddHeader("sec-fetch-site", "same-origin");
                request.AddHeader("sec-fetch-mode", "cors");
                request.AddHeader("sec-fetch-dest", "empty");
                

                string json = "{ \"fingerprint\": \"" + discord.fingerprint + "\", \"email\": \"" +
                              mails[random.Next(mails.Count)] + "\", \"username\": \"" + RandomString(8) +
                              "\", \"password\": \"SOMERANDOMPASSWORD123F\", \"invite\": \"null\", \"consent\":\"true\",\"date_of_birth\":\"2000-12-01\",\"gift_code_sku_id\":\"null\",\"captcha_key\":\"" +
                              new _2Captcha("7e3b92f0e2cfc6dc39de3bfeb0ba137d")
                      .SolveHCaptcha("f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34", "https://discord.com").Result
                      .Response + "\"}";

                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var response = client.Execute(request);

                var jsonConvert = JsonConvert.DeserializeObject<Discord>(response.Content);

                #region Verify Phone

                #region Generate Phone

                var GeneratePhone = new RestClient("https://onlinesim.ru/api/getNum.php?apikey=23701c3ae7f2d6a29e55721b1742cbbd&service=discord");
                var GenPhoneRequest = new RestRequest();
                GenPhoneRequest.Method = Method.GET;
                var GenPhoneResponse = GeneratePhone.Execute(GenPhoneRequest); 
                
                Console.WriteLine(GenPhoneResponse.Content);

                #endregion

                #region Get Phone Number

                var _client =
                    new RestClient("https://onlinesim.ru/api/getState.php?apikey=23701c3ae7f2d6a29e55721b1742cbbd");

                var _request = new RestRequest();
                _request.Method = Method.GET;

                var _response = _client.Execute(_request);

                var _credentials = JsonConvert.DeserializeObject<List<getState>>(_response.Content);

                Console.WriteLine(_credentials[num++].number);

                #endregion

                #region Put Credentials

                var discord1 = new RestClient("https://discord.com/api/v9/users/@me/phone")
                {
                    UserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) discord/0.1.8 Chrome/83.0.4103.122 Electron/9.4.4 Safari/537.36"
                };

                var _discord = new RestRequest();
                _discord.Method = Method.POST;
                _discord.AddHeader("Accept", "application/json");
                _discord.AddHeader("Authorization", jsonConvert.token);
                _discord.AddHeader("Referer", "https://discord.com/channels/@me");
                _discord.AddHeader("X-Super-Properties", "");
                _discord.AddHeader("accept-language", "en-US");
                _discord.AddHeader("origin", "https://discord.com");
                _discord.AddHeader("sec-fetch-site", "same-origin");
                _discord.AddHeader("sec-fetch-mode", "cors");
                _discord.AddHeader("sec-fetch-dest", "empty");

                string json1 = "{ \"phone\": \"" + _credentials[0].number + "\"}";

                _discord.AddParameter("application/json", json1, ParameterType.RequestBody);

                var discordResponse = discord1.Execute(_discord);

                Console.WriteLine(discordResponse.Content);

                #endregion

                #region Actuall Verification

                var verify = new RestClient("https://discord.com/api/v9/phone-verifications/verify");

            var _verify = new RestRequest();
            _verify.Method = Method.POST;

            _verify.AddHeader("Accept", "application/json");
            _verify.AddHeader("Authorization", jsonConvert.token);
            _verify.AddHeader("X-Super-Properties", "");
            _verify.AddHeader("Referer", "https://discord.com/channels/@me");
            _verify.AddHeader("X-Super-Properties", "");
            _verify.AddHeader("accept-language", "en-US");
            _verify.AddHeader("origin", "https://discord.com");
            _verify.AddHeader("sec-fetch-site", "same-origin");
            _verify.AddHeader("sec-fetch-mode", "cors");
            _verify.AddHeader("sec-fetch-dest", "empty");


            Thread.Sleep(5000);
            
            var _client1 =
                new RestClient("https://onlinesim.ru/api/getState.php?apikey=23701c3ae7f2d6a29e55721b1742cbbd");

            var _request1 = new RestRequest();
            _request1.Method = Method.GET;

            var _response1 = _client1.Execute(_request1);

            var _credentials1 = JsonConvert.DeserializeObject<List<getState>>(_response1.Content);

            Console.WriteLine(_credentials1[0].msg);

            string json11 = "{ \"phone\": \"" + _credentials[0].number + "\", \"code\":\"" + _credentials1[0].msg +
                           "\"}";

            _verify.AddParameter("application/json", json11, ParameterType.RequestBody);

            var verifyResponse = verify.Execute(_verify);

            Console.WriteLine(verifyResponse.Content);

            var yareyareWhyNot = JsonConvert.DeserializeObject<Phone>(verifyResponse.Content);

            
            var asd = new RestClient("https://discord.com/api/v9/users/@me/phone");
            
            var asd2 = new RestRequest();

            asd2.AddHeader("Accept", "application/json");
            asd2.AddHeader("Authorization", jsonConvert.token);
            
            string json2 = "{ \"phone_token\": \"" + yareyareWhyNot.phone_token + "\", \"password\":\"" + "SOMERANDOMPASSWORD123F" +
                           "\"}";
            
            asd2.AddParameter("application/json", json2, ParameterType.RequestBody);
            var asdResponse = asd.Execute(asd2);

                #endregion
                
                #endregion
                
                Console.WriteLine(response.Content);
            });
            Console.ReadLine();
        }
    }
}