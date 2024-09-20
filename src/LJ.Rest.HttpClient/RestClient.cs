using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;

namespace LJ.Rest.HttpClient
{
    public class RestClient(IHttpClientFactory httpClientFactory ) : IRestClient
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public Dictionary<string, string> QueryParameters = [];

        public Dictionary<string, string> HeaderParameters = [];
        public object? BodyParameter { get; set; }

        public IRestClient AddQueryParameter(string key, string value)
        {
            QueryParameters[key] = value;
            return this;
        }

        public IRestClient AddHeaderParameter(string key, string value)
        {
            HeaderParameters[key] = value;
            return this;
        }

        public IRestClient AddBodyParameter(object bodyParameter)
        {
            BodyParameter = bodyParameter;
            return this;
        }

        public async Task<RestResult<T>> SendAsyc<T>(string url, RestType restType, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var uriBuilder = new UriBuilder(url);

            if (QueryParameters is not null)
            {

                var nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (var queryParameter in QueryParameters)
                {
                    nameValueCollection.Add(queryParameter.Key, queryParameter.Value);
                }
                uriBuilder.Query = nameValueCollection.ToString();
            }

            var httpMethod = restType switch
            {
                RestType.Get => HttpMethod.Get,
                RestType.Post => HttpMethod.Post,
                RestType.Delete => HttpMethod.Delete,
                RestType.Put => HttpMethod.Put,
                RestType.Patch => HttpMethod.Patch,
                _ => throw new NotImplementedException(nameof(restType)),
            };

            var httpRequestMessage = new HttpRequestMessage(httpMethod, uriBuilder.Uri);

            if (HeaderParameters is not null)
            {
                foreach (var headerParameter in HeaderParameters)
                {
                    httpRequestMessage.Headers.Add(headerParameter.Key, headerParameter.Value);
                }
            }

            if (BodyParameter is not null)
            {
                HttpContent httpContent = new StringContent(JsonSerializer.Serialize(BodyParameter, JsonSerializerOptions.Default));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json")
                {
                    CharSet = "utf-8"
                };
                httpRequestMessage.Content = httpContent;
            }
          
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

            var headers = new Dictionary<string, IEnumerable<string>>();
            foreach (var header in httpResponseMessage.Headers)
            {
                headers.Add(header.Key, header.Value);
            }
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var json = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
                var value = JsonSerializer.Deserialize<T>(json);
                return new RestResult<T>(httpResponseMessage.IsSuccessStatusCode, value, headers, default);
            }
            else
            {
                var exceptionMessage = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
                return new RestResult<T>(httpResponseMessage.IsSuccessStatusCode, default, headers, exceptionMessage);
            }
        }

    }
}
