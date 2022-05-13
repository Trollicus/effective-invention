namespace SupremeSystem.Handlers;

public class IHttpHandler
{
    public static class HttpHandler
    {
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// Asynchronously sends a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="uri">URI requested TO</param>
        /// <param name="method">HTTPMethod</param>
        /// <returns>Content</returns>
        public static async Task<HttpResponseMessage> PostAsync(string uri, HttpMethod method)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = method,
            };

            return await Client.SendAsync(request);
        }

        /// <summary>
        /// Asynchronously sends a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="method"></param>
        /// <param name="json"></param>
        /// <returns>Content</returns>
        public static async Task<HttpResponseMessage> PostAsync(string uri, HttpMethod method,
            string json)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = method,
                Content = new StringContent(json)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                },
            };

            return await Client.SendAsync(request);
        }

        /// <summary>
        /// Asynchronously sends a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="uri">URI requested TO</param>
        /// <param name="method">HTTPMethod</param>
        /// <param name="json">Content</param>
        /// <param name="requestHeaders">RequestHeadersEX</param>
        /// <returns>Content</returns>
        public static async Task<HttpResponseMessage> PostAsync(string uri, HttpMethod method,
            string json, RequestHeadersEx[] requestHeaders)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = method,
                Content = new StringContent(json)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                },
            };

            foreach (var requestHeader in requestHeaders)
            {
                request.Headers.Add(requestHeader.Key, requestHeader.Value);
            }

            return await Client.SendAsync(request);
        }
    }

    /// <summary>
    /// HttpRequestHeaders
    /// Better way of adding headers
    /// </summary>
    public class RequestHeadersEx
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public RequestHeadersEx(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}