using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AZHttpClientPool
{
    public class MyHttpClient : HttpClient
    {
        public int ReqCount
        {
            get => _reqCount;
            set => _reqCount = value;
        }

        private int _reqCount;

        public MyHttpClient(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
        {
            //this.MaxResponseContentBufferSize = 4 * 1024 * 1024;
            this.Timeout = TimeSpan.FromHours(24);


        }
        public MyHttpClient(HttpMessageHandler handler) : base(handler)
        {
            //this.MaxResponseContentBufferSize = 4 * 1024 * 1024;
            this.Timeout = TimeSpan.FromHours(24);



        }

        public void IncrementReqCount()
        {
            Interlocked.Increment(ref _reqCount);
        }

        public void DecrementReqCount()
        {
            Interlocked.Decrement(ref _reqCount);
        }

        public new Task<string> GetStringAsync(string url)
        {
            IncrementReqCount();

            //var stream = base.GetStreamAsync(url).Result;

            //var buffer = new byte[stream.Length];
            //stream.Read(buffer, 0, buffer.Length);

            //var ret=Encoding.UTF8.GetString(buffer);

            var tt= base.GetAsync(url).ContinueWith( t =>
            {
                //if (t.Result.StatusCode == HttpStatusCode.GatewayTimeout)
                //{
                //    return this.GetStringAsync(url);
                //}
                //else
                //{
                //    return t.Result.Content.ReadAsStringAsync();
                //}

                var ret = string.Empty;

                if (t.Result.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    ret=    GetStringAsync(url).Result;
                }
                else
                {
                    ret=   t.Result.Content.ReadAsStringAsync().Result;

                    //var bytesart = t.Result.Content.ReadAsByteArrayAsync().Result;

                    //var utf8str = Encoding.UTF8.GetString(bytesart);
                    //var gb2312str = Encoding.GetEncoding("gb2312").GetString(bytesart);
                    //var gbkStr=Encoding.GetEncoding("GBK").GetString(bytesart);

                    //var str = Encoding.ASCII.GetString(bytesart);
                }

                return ret;
            });

            return tt;
            //if (rep.Result.StatusCode == HttpStatusCode.GatewayTimeout)
            //{
            //    return this.GetStringAsync(url);
            //}
            //else
            //{
            //    return rep.Result.Content.ReadAsStringAsync();
            //}
        }
    }
}
