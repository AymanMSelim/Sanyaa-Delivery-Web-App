using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App.Global.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using static App.Global.Enums;

namespace App.Global.RestAPI
{
    public class APIService : IAPI
    {
        private readonly HttpClient _httpClient;
        private string _token;
        private Uri _baseURL;

        static APIService()
        {
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
        }

        public APIService(string baseUrl, TokenType tokenType = TokenType.None, string token = null)
        {
            _httpClient = new HttpClient(new RetryHandler(new HttpClientHandler()));
            _baseURL = new Uri(baseUrl);
            _httpClient.BaseAddress = _baseURL;
            if (!string.IsNullOrEmpty(token))
            {
                SetTokenHeader(token, tokenType);
            }
        }

        public void SetToken(string token)
        {
            _token = token;
        }

        public void SetTokenHeader(string token, TokenType tokenType)
        {
            _token = token;
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
            switch (tokenType)
            {
                case TokenType.Bearer:
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                    break;
                default:
                    break;
            }
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _httpClient.GetAsync(url);
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return _httpClient.PostAsync(url, content);
        }

        public Task<HttpResponseMessage> PostAsync(string url, object model)
        {
            var modelData = JsonConvert.SerializeObject(model);
            var content = new StringContent(modelData, Encoding.UTF8, "application/json");
            return _httpClient.PostAsync(url, content);
        }
        public async Task<T> PostAsync<T>(string url, object model)
        {
            HttpResponseMessage httpResponseMessage;
            if (model != null)
            {
                var modelData = JsonConvert.SerializeObject(model, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy
                        {
                            OverrideSpecifiedNames = false
                        }
                    }
                });
                var content = new StringContent(modelData, Encoding.UTF8, "application/json");
                httpResponseMessage = await _httpClient.PostAsync(url, content);
            }
            else
            {
                httpResponseMessage = await _httpClient.PostAsync(url, null);
            }
            return await HelperClass.GetResponseAsync<T>(httpResponseMessage);
        }

        public async Task<JObject> GetResponseJsonAsync(string url)
        {
            try
            {
                var httpResponseMessage = await GetAsync(url);
                return await HelperClass.GetResponseJsonAsync(httpResponseMessage);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<T> GetResponseAsync<T>(string url)
        {
            try
            {
                var httpResponseMessage = await GetAsync(url);
                return await HelperClass.GetResponseAsync<T>(httpResponseMessage);
            }
            catch (Exception)
            {
                return default;
            }

        }

    }
}
