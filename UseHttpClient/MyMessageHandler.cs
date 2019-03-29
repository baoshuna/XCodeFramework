using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UseHttpClient
{
    public class MyMessageHandler : DelegatingHandler
    {
        private static int counter = 0;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-zidingyi-header", (++counter).ToString());

            return base.SendAsync(request, cancellationToken);
        }
    }
}
