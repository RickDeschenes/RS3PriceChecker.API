
namespace RS3PriceChecker.Services;

internal static class Utilities
{
    internal static string GetRuneScapeResponse(string url)
    {
        var t = Task.Run(() => GetResults(url));
        t.Wait();
        return t.Result;
    }

    private static async Task<string> GetResults(string url)
    {
        string results = string.Empty;

        using var client = new HttpClient();
        using (HttpResponseMessage response = await client.GetAsync(url))
        using (HttpContent content = response.Content)
        {
            // ... Read the string.
            string result = await content.ReadAsStringAsync();

            // ... Display the result.
            if (result != null)
                results = result;
        }
        return results;
    }
}