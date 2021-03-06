using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vokabular.MainService.DataContracts.Clients.Extensions
{
    internal static class HttpRestClientExtensions
    {
        private static string JsonContentType = "application/json";
        private static string WwwFormUrlEncodedContentType = "application/x-www-form-urlencoded";

        private static JsonSerializer CreateJsonSerializer()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };
            return JsonSerializer.Create(settings);
        }

        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            using (var stream = await content.ReadAsStreamAsync())
            using (var textReader = new StreamReader(stream))
            {
                var contentString = await textReader.ReadToEndAsync();

                using (var stringReader = new StringReader(contentString))
                using (var jsonReader = new JsonTextReader(stringReader))
                {
                    var serializer = CreateJsonSerializer();
                    var item = serializer.Deserialize<T>(jsonReader);

                    return item;
                }
            }
        }

        public static async Task<HttpResponseMessage> SendAsJsonAsync(this HttpClient httpClient, HttpRequestMessage requestMessage, object value)
        {
            var stream = new MemoryStream(); // Using block isn't used. Stream is disposed automatically by httpClient.SendAsync method.
            var textWriter = new StreamWriter(stream);
            var jsonWriter = new JsonTextWriter(textWriter);

            var serializer = CreateJsonSerializer();
            serializer.Serialize(jsonWriter, value);

            await jsonWriter.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);

            requestMessage.Content = new StreamContent(stream);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(JsonContentType);

            var result = await httpClient.SendAsync(requestMessage);
            return result;
        }

        public static async Task<HttpResponseMessage> SendAsWwwFormUrlEncodedAsync(this HttpClient httpClient,
            HttpRequestMessage requestMessage, IEnumerable<KeyValuePair<string, string>> formData)
        {
            var sb = new StringBuilder();
            foreach (var dataItem in formData)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }

                sb.AppendFormat("{0}={1}", dataItem.Key.EncodeQueryString(), dataItem.Value?.EncodeQueryString());
            }

            requestMessage.Content = new StringContent(sb.ToString(), Encoding.UTF8, WwwFormUrlEncodedContentType);

            var result = await httpClient.SendAsync(requestMessage);
            return result;
        }
    }
}