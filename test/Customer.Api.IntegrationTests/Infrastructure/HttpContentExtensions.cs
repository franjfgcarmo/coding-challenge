using System.Text;
using Newtonsoft.Json;

namespace Customer.Api.IntegrationTests.Infrastructure;

public static class HttpContentExtensions
{
    public static async Task<T?> ReadAsAsync<T>(this HttpContent content)
    {
        return JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
    }

    public static async Task<HttpResponseMessage> PostWithContentAsync<T>(this HttpClient client,
        string url,
        T content,
        CancellationToken cancellationToken = default)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        return await client.PostAsync(url, stringContent, cancellationToken);
    }
}