using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientPool.core
{
    public class MyHttpMessageHandler:System.Net.Http.DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //return  base.SendAsync(request,cancellationToken).ContinueWith(t =>
            //{
            //    var contentType = t.Result.Content.Headers.ContentType;
            //    //contentType.CharSet = await getCharSetAsync(response.Content);
            //    if (string.IsNullOrEmpty(contentType.CharSet))
            //    {
            //        contentType.CharSet = "GBK";
            //    }
            //    return t.Result;
            //});

            //return base.SendAsync(request, cancellationToken);

            

            var result = await base.SendAsync(request, cancellationToken);


            var contentType = result.Content.Headers.ContentType;
            //contentType.CharSet = await getCharSetAsync(response.Content);
            if (string.IsNullOrEmpty(contentType.CharSet))
            {
                contentType.CharSet = "GBK";
            }

            //result.Content.Headers.ContentEncoding.Add("GBK");

           

            return result;
        }
    }
}
