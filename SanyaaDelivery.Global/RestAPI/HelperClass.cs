using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Global.RestAPI
{
    public class HelperClass
    {
        public static async Task<string> GetResponseAsync(HttpResponseMessage httpResponseMessage)
        {
            if(httpResponseMessage.Content == null)
            {
                return null;
            }
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        public static async Task<JObject> GetResponseJsonAsync(HttpResponseMessage httpResponseMessage)
        {
            try
            {
                if(httpResponseMessage.Content == null)
                {
                    return null;
                }
                var responstText = await httpResponseMessage.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responstText))
                {
                    return null;
                }
                else
                {
                    return JObject.Parse(responstText);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<T> GetResponseAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            try
            {
                var responeText = await GetResponseAsync(httpResponseMessage);
                if (string.IsNullOrEmpty(responeText))
                {
                    return default;
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(responeText.Replace("\r\n", ""));
                }
            }
            catch (Exception)
            {
                return default;
            }

        }
    }
}
