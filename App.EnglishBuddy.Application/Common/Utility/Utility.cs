using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace App.EnglishBuddy.Application.Common.Utility
{
    public class Utility
    {

        public static async Task<string> CallAPIsAsync(string url, string methodType, object param = null)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string jsonString = string.Empty;
            using (var httpClient = new HttpClient())
            {
                if (methodType == "Get")
                {
                     response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        jsonString = await response.Content.ReadAsStringAsync();
                    }
                } else
                {
                    var modifiedAssetJSON = JsonConvert.SerializeObject(param);
                    StringContent  requestContent = new StringContent(modifiedAssetJSON, Encoding.UTF8, "application/json");

                    httpClient.BaseAddress = new Uri(url);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    response = httpClient.PostAsync(url, requestContent).Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContent content = response.Content;
                        jsonString = content.ReadAsStringAsync().Result;
                    }
                }

            }
            return jsonString;
        }

    }
}
