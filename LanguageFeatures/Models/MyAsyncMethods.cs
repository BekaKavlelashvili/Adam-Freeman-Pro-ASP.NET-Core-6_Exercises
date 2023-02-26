using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace LanguageFeatures.Models
{
    public class MyAsyncMethods
    {
        public async static Task<long?> GetPageLength()
        {
            HttpClient httpClient = new HttpClient();
            var httpMessage = await httpClient.GetAsync("http://apress.com");
            return httpMessage.Content.Headers.ContentLength;
        }


        public static async IAsyncEnumerable<long?> GetPageLengths(List<string> output, params string[] urls)
        {
            HttpClient httpClient = new HttpClient();
            foreach (string url in urls)
            {
                output.Add($"Started request for {url}");
                var httpMessage = await httpClient.GetAsync($"http://{url}");
                output.Add($"Completed request for {url}");
                yield return httpMessage.Content.Headers.ContentLength;
            }
        }
    }
}
