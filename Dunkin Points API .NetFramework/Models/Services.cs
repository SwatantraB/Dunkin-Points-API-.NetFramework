using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Dunkin_Points_API.NetFramework.Models
{
    public static class Services
    {
        public static string BaseUrl = "https://productiondd.buzzparade.com";
        public static async Task<object> PointsAPI(string url, string encryptedValue, string httpMethod, string contentType, string functionName)
        {
            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var authorization = Utility.getAuthHeader(httpMethod, contentType, functionName);
                var basicAuth = Utility.BasicAuth();

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl + url);
                request.Headers.Add("Authorization", string.Format("Basic {0}", basicAuth));
                var content = new StringContent("{\"encryptedtext\":\"" + encryptedValue + "\"}", null, contentType);
                request.Content = content;
                var response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return (json);
                }
                else
                {
                    return response.ReasonPhrase;
                }

                return (await response.Content.ReadAsStringAsync());


                //var options = new RestClientOptions(BaseUrl)
                //{
                //    MaxTimeout = -1,
                //};
                //var client = new RestClient(options);
                //var request = new RestRequest(url, Method.Post);
                //request.AddHeader("Content-Type", "application/json");
                //request.AddHeader("Authorization", string.Format("Basic {0}", basicAuth));
                ////request.AddHeader("Authorization", "Basic bG9hZDpsb2Fk");
                //var body = "{\"encryptedtext\":\"" + encryptedValue + "\"}";
                //request.AddStringBody(body, DataFormat.Json);
                //RestResponse response = client.Execute<string>(request);

                //using (HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult())
                //{
                //    using (HttpContent content = response.Content)
                //    {
                //        var json = content.ReadAsStringAsync().GetAwaiter().GetResult();
                //    }
                //}
                //if (response.IsSuccessful)
                //{
                //    return (response.Content);
                //}
                //else
                //{
                //    return response.ErrorException;
                //}
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}