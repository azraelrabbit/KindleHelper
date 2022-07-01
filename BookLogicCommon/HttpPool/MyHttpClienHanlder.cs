using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AZHttpClientPool
{
    public class MyHttpClienHanlder :  HttpClientHandler
    {

        public MyHttpClienHanlder() : base()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate|DecompressionMethods.None;
             
            this.Proxy=null;

            
           
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Console.WriteLine("max conn per server"+this.MaxConnectionsPerServer);
            //request.Headers.Referrer = new Uri("http://www.google.com/");

            //request.Headers.Add("UserAgent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727)");

            //var headerName = "User-Agent";
            //if (!request.Headers.Contains(headerName))
            //{
            //    request.Headers.Add(headerName, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36");
            //}


            //request.Headers.ExpectContinue = true;
            request.Headers.MaxForwards = 1024;
            request.Headers.Referrer = null;
            


            //Task<HttpResponseMessage> task = base.SendAsync(request, cancellationToken);
            //HttpResponseMessage response = task.Result;
            //MediaTypeHeaderValue contentType = response.Content.Headers.ContentType;
            //if (string.IsNullOrEmpty(contentType.CharSet))
            //{
            //    contentType.CharSet = "GBK";
            //}

            //return task;

            //return await base.SendAsync(request, cancellationToken);

            //var  cantok=new CancellationToken(false);
            

           //return  base.SendAsync(request,cancellationToken).ContinueWith(t =>
           // {
           //     var contentType = t.Result.Content.Headers.ContentType;
           //     //contentType.CharSet = await getCharSetAsync(response.Content);
           //     if (string.IsNullOrEmpty(contentType.CharSet))
           //     {
           //         contentType.CharSet = "GBK";
           //     }
           //     return t.Result;
           // });

            var result = await base.SendAsync(request, cancellationToken);


            var contentType = result.Content.Headers.ContentType;
            //contentType.CharSet = await getCharSetAsync(response.Content);


            //var bytesart = result.Content.ReadAsByteArrayAsync().Result;

            //var utf8str = Encoding.UTF8.GetString(bytesart);
            //var gb2312str = Encoding.GetEncoding("gb2312").GetString(bytesart);
            //var gbkStr=Encoding.GetEncoding("GBK").GetString(bytesart);

            //var str = Encoding.ASCII.GetString(bytesart);



            if (contentType != null)
            {
                if (string.IsNullOrEmpty(contentType.CharSet))
                {
                    contentType.CharSet = "GBK";
                }

                //contentType.CharSet = "UTF-8";
                //contentType.CharSet = "ASCII";
            }
          

            return result;

            //var contentType = response.Content.Headers.ContentType;
            ////contentType.CharSet = await getCharSetAsync(response.Content);
            //if (string.IsNullOrEmpty(contentType.CharSet))
            //{
            //    contentType.CharSet = "GBK";
            //}
            //return response;



            //return await SendAsync(request, cancellationToken).ContinueWith(t =>
            //{
            //    var contentType = t.Result.Content.Headers.ContentType;

            //    if (string.IsNullOrEmpty(contentType.CharSet))
            //    {
            //        contentType.CharSet =  "GBK";
            //    }
            //    return t.Result;
            //});

        }

        private async Task<string> getCharSetAsync(HttpContent httpContent)
        {
            var charset = httpContent.Headers.ContentType.CharSet;
            if (!string.IsNullOrEmpty(charset))
                return charset;

            var content = await httpContent.ReadAsStringAsync();
            var match = Regex.Match(content, @"charset=(?<charset>.+?)""", RegexOptions.IgnoreCase);
            if (!match.Success)
                return charset;

            return match.Groups["charset"].Value;
        }
    }
}