using App.EnglishBuddy.Application.Common.AppMessage;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Newtonsoft.Json;

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
