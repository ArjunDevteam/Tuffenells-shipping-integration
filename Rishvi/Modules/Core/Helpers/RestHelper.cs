using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rishvi.Modules.Core.Helpers
{
    public static class RestHelper
    {

        public static T SendSerialize<T>(string baseUrl, string url, string filterJson, string token)
        {
            //LinkworkUrl: https://eu-ext.linnworks.net//api/Dashboards/ExecuteCustomScriptQuery
            var client = new RestClient(baseUrl + url);
            //client.Authenticator = new HttpBasicAuthenticator("myclientid", "myclientsecret");

            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", token);
            request.AddHeader("Content-Type", "application/json");
            //request.RequestFormat = DataFormat.Json;
            //var locationJSON = @"{'fulfilmentCenter': '00000000-0000-0000-0000-000000000000',
            //                        'pageNumber': 1,
            //                        'entriesPerPage': 500
            //                     }";
            request.AddParameter("application/json", filterJson, ParameterType.RequestBody);
            //request.AddParameter("grant_type", "client_credentials");

            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
        public static IRestResponse Send(string baseUrl, string url, string filterJson, string token)
        {
            var client = new RestClient(baseUrl + url);
            //client.Authenticator = new HttpBasicAuthenticator("myclientid", "myclientsecret");

            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", token);
            request.AddHeader("Content-Type", "application/json");
            //request.RequestFormat = DataFormat.Json;
            //var locationJSON = @"{'fulfilmentCenter': '00000000-0000-0000-0000-000000000000',
            //                        'pageNumber': 1,
            //                        'entriesPerPage': 500
            //                     }";
            request.AddParameter("application/json", filterJson, ParameterType.RequestBody);
            //request.AddParameter("grant_type", "client_credentials");

            IRestResponse response = client.Execute(request);
            return response;
        }
    }

    public class RestResponseContent
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
