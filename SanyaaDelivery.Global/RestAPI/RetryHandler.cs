using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Global.RestAPI
{
    public class RetryHandler : DelegatingHandler
    {
        private readonly int MaxRetries = 5;
        public RetryHandler(HttpMessageHandler httpMessageHandler) : base(httpMessageHandler)
        {

        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            for (int i = 0; i < MaxRetries; i++)
            {
                response = await base.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return response;
                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.GatewayTimeout);
        }
    }
}
