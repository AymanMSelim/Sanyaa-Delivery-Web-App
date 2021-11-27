using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Global.Interfaces
{
    public interface IAPI
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        Task<HttpResponseMessage> PostAsync(string url, object model);
        Task<T> GetResponseAsync<T>(string url);
        Task<JObject> GetResponseJsonAsync(string url);

    }
}
