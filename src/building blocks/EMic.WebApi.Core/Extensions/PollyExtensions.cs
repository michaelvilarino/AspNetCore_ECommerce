using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace EMic.WebApi.Core.Extensions
{
    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
        {
            //Policy para tentativas de chamadas do serviço
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(sleepDurations: new[]
                {
                       TimeSpan.FromSeconds(1),
                       TimeSpan.FromSeconds(5),
                       TimeSpan.FromSeconds(10)
                });

            return retry;
        }
    }
}
