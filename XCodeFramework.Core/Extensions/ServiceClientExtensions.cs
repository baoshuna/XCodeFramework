using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace XCodeFramework.Core.Extensions
{
    public static class ServiceClientExtensions
    {
        public static T DisableRetries<T>(this T client)
            where T : ServiceClient<T>
        {
            client.SetRetryPolicy(null);
            return client;
        }

        public static T WithTimeout<T>(this T client, TimeSpan timeout)
            where T : ServiceClient<T>
        {
            client.HttpClient.Timeout = timeout;
            return client;
        }
    }
}
