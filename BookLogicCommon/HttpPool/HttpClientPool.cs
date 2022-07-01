using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HttpClientPool.core;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;

namespace AZHttpClientPool
{
    public class AHttpClientPool : IDisposable
    {

        //ConcurrentBag<MyHttpClient> _httpClientPool;

        private ConcurrentQueue<MyHttpClient> _httpClientPool;

        private int _poolSize;

        private bool _keepAlive;

        private string _baseUrl;

        public AHttpClientPool(string baseUrl, int poolSize = 5, bool keepAlive = false)
        {

            //System.Net.Http.UseManagedHttpClientHandler=true;




            //SocketsHttpHandler

            _baseUrl = baseUrl;


            if (!_baseUrl.EndsWith("/"))
            {
                _baseUrl += "/";
            }

            //_httpClientPool = new ConcurrentBag<MyHttpClient>();

            _httpClientPool=new ConcurrentQueue<MyHttpClient>();

            _keepAlive = keepAlive;

            GrantPool(poolSize);
        }

        public void GrantPool(int poolSize)
        {
            _poolSize = poolSize;

            var toSize = _poolSize;
            var boolAdd = false;
            if (_httpClientPool.Count < poolSize)
            {
                toSize = poolSize - _httpClientPool.Count;
                boolAdd = true;
            }
            else
            {
                toSize = _httpClientPool.Count - poolSize;
                boolAdd = false;
            }

            if (boolAdd)
            {
                for (var i = 0; i < toSize; i++)
                {


                    var myHandler =
                        new MyHttpClienHanlder() { UseProxy = false, UseCookies = false, AllowAutoRedirect = false };

                    //var myHandler =
                    //    new MyHttpMessageHandler(){ InnerHandler=new SocketsHttpHandler(){UseProxy = false, UseCookies = true, AllowAutoRedirect = true} };

                    myHandler.MaxConnectionsPerServer = 10;
                    myHandler.MaxResponseHeadersLength = 65535;
                
                  

                    var newclient = new MyHttpClient(myHandler, false);

                  

                    newclient.DefaultRequestHeaders.MaxForwards = 1024;
                    //newclient.DefaultRequestHeaders.ExpectContinue = true;
                    
                    //newclient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests","1");
                    //newclient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("GBK"));

                    //newclient.DefaultRequestHeaders.Add("Accept-Charset", "GBK");

                    
                    CacheControlHeaderValue cacheControl = new CacheControlHeaderValue();
                    cacheControl.NoCache = true;
                    cacheControl.NoStore = true;

                    newclient.DefaultRequestHeaders.CacheControl = cacheControl;// 不使用缓存\
                    newclient.DefaultRequestHeaders.Pragma.Clear();
                    newclient.DefaultRequestHeaders.Pragma.Add(new NameValueHeaderValue("no-cache"));

                    //now disable
                    //newclient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
                    //newclient.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");



                    //accept 
                    //text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
                    newclient.DefaultRequestHeaders.Accept.Clear();
                    newclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                    newclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
                    newclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
                    newclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
                    newclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/apng"));
                  
                    newclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));

                    newclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/signed-exchange"));



                  
                  


                    //                    Accept-Encoding: gzip, deflate, br
                    //Accept-Language: zh-CN,zh;q=0.9,en;q=0.8
                    //Cache-Control: no-cache
                    //Connection: keep-alive
                    //Cookie: Hm_lvt_b962ca1dfd40d002f593299845383c36=1528184292; Hm_lpvt_b962ca1dfd40d002f593299845383c36=1529671763
                    //DNT: 1
                    //Referer: https://www.88dus.com/
                    //pgrade-Insecure-Requests: 1
                    //User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36


                    //referer  _baseUrl
                    //newclient.DefaultRequestHeaders.Referrer = new Uri(_baseUrl);

                    //now disable
                    ////accept-encoding 
                    //newclient.DefaultRequestHeaders.AcceptEncoding.Clear();
                    //newclient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    //newclient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                    //newclient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));

                    ////Accept-Language: zh-CN,zh;q=0.9,en;q=0.8
                    //newclient.DefaultRequestHeaders.AcceptLanguage.Clear();
                    //newclient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("zh-CN"));
                    //newclient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("zh", 0.9));
                    //newclient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en", 0.8));

                    //Cookie: Hm_lvt_b962ca1dfd40d002f593299845383c36=1528184292; Hm_lpvt_b962ca1dfd40d002f593299845383c36=1529671763
                    //ignore
                    newclient.DefaultRequestHeaders.UserAgent.Clear();

                    //var productinfo=new ProductInfoHeaderValue(new ProductHeaderValue("Mozilla","5.0"));
                    //newclient.DefaultRequestHeaders.UserAgent.Add(productinfo);

                    //newclient.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36");
                  
                    newclient.DefaultRequestHeaders.UserAgent.ParseAdd("  Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36");

                    if (_keepAlive)
                    {
                        newclient.DefaultRequestHeaders.Connection.Add("keep-alive");

                        //newclient.DefaultRequestHeaders.CacheControl.NoStore=true;
                    }

                    if (!string.IsNullOrEmpty(_baseUrl))
                    {
                        //帮HttpClient热身
                        newclient.SendAsync(new HttpRequestMessage
                        {
                            Method = new HttpMethod("GET"),
                            RequestUri = new Uri(_baseUrl )
                        }).ContinueWith(t => {


                            var ret = t.Result;
                        
                        });
                    }

                    _httpClientPool.Enqueue(newclient);
                }
            }
            else
            {
                //remove count of to size from pool

                for (var i = 0; i < toSize; i++)
                {
                    MyHttpClient client;

                    if (_httpClientPool.TryDequeue(out client))
                    {
                        try
                        {
                            client?.Dispose();
                        }
                        catch (Exception ex)
                        {
                            // ignored
                        }
                    }
                }
            }
        }

        static int _currentIndex = 0;

        //public Task<string> LoadUrlAsyncTask(string url)
        //{


        //    _httpClientPool.TryDequeue(out MyHttpClient client);

        //    _httpClientPool.Enqueue(client);

        //    //if (client == null)
        //    //{
        //    //    var msg = "client null";
        //    //}

        //    var ret = client?.GetStringAsync(url).ContinueWith(t =>
        //          {
        //              client.DecrementReqCount();
        //              if (t.IsCompleted)
        //              {
        //                  return t.Result;
        //              }
        //              else
        //              {

        //                  return string.Empty;
        //              }

        //          });
        //    return ret;



        //}

        public string LoadUrl(string url)
        {


            _httpClientPool.TryDequeue(out MyHttpClient client);

            _httpClientPool.Enqueue(client);

            //if (client == null)
            //{
            //    var msg = "client null";
            //}

            //var ret = client?.GetStringAsync(url).ContinueWith(t =>
            //{
            //    client.DecrementReqCount();
            //    if (t.IsCompleted)
            //    {
            //        return t.Result;
            //    }
            //    else
            //    {

            //        return string.Empty;
            //    }

            //});

            var ret = client?.GetStringAsync(url).Result;

            return ret;
        }


        public   Task<string> LoadUrlAsyncTask2(string url)
          {

              return Task.Run(() => {
                  
                  
                  _httpClientPool.TryDequeue(out MyHttpClient client);

                  _httpClientPool.Enqueue(client);



                  var ret = client?.GetStringAsync(url).Result;


                  return ret;
              });
        

            //_httpClientPool.TryDequeue(out MyHttpClient client);

            //_httpClientPool.Enqueue(client);

            //if (client != null)
            //{
            //    var ret1 = await client?.GetStringAsync(url);


            //    client.DecrementReqCount();
                 

                
            //    return ret1;
            //}

            //return string.Empty;
        }

        public void Dispose()
        {
            if (_httpClientPool != null && _httpClientPool.Any())
            {
                foreach (var httpClient in _httpClientPool)
                {
                    try
                    {
                        httpClient?.Dispose();
                    }
                    catch (Exception ex)
                    {
                        // ignored
                    }
                }

            }
           
            
            _httpClientPool = null;
        }
    }
}
