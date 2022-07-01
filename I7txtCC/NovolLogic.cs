using BookLogicCommon;
using BookLogicCommon.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
 
using HtmlAgilityPack;
using AZHttpClientPool;
using System.Security.Policy;
using System.Net.Http;

namespace I7txtCC
{
    public class NovolLogic : NovolLogicBase
    {

        const string searchUrl = "http://www.i7txt.cc/modules/article/search.php?searchtype=articlename&searchkey=";

        const string host = "http://www.i7txt.cc/";

        AHttpClientPool _pool;

        //HttpClient client;


        public NovolLogic()
        {

            //var uri = new Uri(host);

            _pool = new AHttpClientPool(baseUrl: "http://www.i7txt.cc/", poolSize:10);

            //client = new HttpClient();
            //client.BaseAddress = new Uri(host);

          

        }

        /// <summary>
        /// 搜索小说
        /// </summary>
        /// <param name="query">关键词</param>
        /// <param name="start">结果开始Index</param>
        /// <param name="limit">结果数量限制</param>
        /// <returns></returns>

        public override QueryBookInfo[] fuzzySearch(string query)
        {
            List<QueryBookInfo> bookList = new List<QueryBookInfo>();


            var surl = searchUrl + HttpUtility.UrlEncode(query);


            //var result = _pool.GetClient().GetStringAsync(surl).Result;

            var result = HttpHelper.Get(surl,host);// client.GetStringAsync(surl).Result;

            if (!string.IsNullOrEmpty(result))
            {
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(result);

                var ul = htmlDoc.DocumentNode.SelectSingleNode($"/html/body/section/div/ul");

                foreach (var n in ul.ChildNodes)
                {
                    var main = n.SelectSingleNode($"./a");

                    var bookMainUrl = main.GetAttributeValue("href", String.Empty); // 简介地址url
                    var mainName = main.GetAttributeValue("title", String.Empty);//书名

                    var imgNode = main.SelectSingleNode($"./img");

                    var imgUrl = imgNode.GetAttributeValue("src", String.Empty); // 封面图片url

                    var bookId = bookMainUrl.Replace("http://www.i7txt.cc/book/", String.Empty).Replace("/", String.Empty); // 小说编号

                    var title = n.SelectSingleNode($"./div[@class='title']");

                    var brnode = title.SelectSingleNode($"./h3/a");

                    var bookIndexUrl = brnode.GetAttributeValue("href", String.Empty); // 小说目录页面url

                    var authorNode = title.SelectSingleNode($"./span[@class='author']");

                    var autherName = authorNode.InnerText; //作者


                    var status = n.SelectSingleNode($"./div[@class='status']");

                    var statusText = "未知";
                    try
                    {
                        statusText = status.ChildNodes[0].InnerText;  // 状态
                    }
                    catch(Exception ex)
                    {
                        //ignore
                    }
                      

                    var newNode = status.SelectSingleNode($"./span/a");

                    var latestChapter = newNode.InnerText; // 最新章节名称

                    var data = n.SelectSingleNode($"./div[@class='data']");

                    var spans = data.SelectNodes($"./span");

                    //< span > 类型：都市言情 </ span >
                    //< span > 总点击：7248 </ span >
                    //< span > 总推荐：2 </ span >
                    //< span > 总收藏：</ span >
                    //< span > 更新：21 - 04 - 20 </ span >
                    string type = String.Empty;
                    string clickCount = string.Empty;
                    string niceCount = string.Empty; 
                    var  favCount = string.Empty; 
                    var lastUpdate =String.Empty;

                    foreach (var s in spans)
                    {
                        var intext = s.InnerText.Trim();
                        if (intext.StartsWith("类型"))
                        {
                            type = intext;
                        }

                        if (intext.StartsWith("总点击"))
                        {
                            clickCount = intext;
                        }

                        if (intext.StartsWith("总推荐"))
                        {
                            niceCount = intext;
                        }

                        if (intext.StartsWith("总收藏"))
                        {
                            favCount = intext;
                        }

                        if (intext.StartsWith("更新"))
                        {
                            lastUpdate = intext;
                        }
                    }


                    var qbi = new QueryBookInfo();
                    qbi._id = bookId;
                    qbi.title = mainName;
                    qbi.author = autherName;
                    qbi.lastChapter = latestChapter;
                    qbi.cover = imgUrl;
                    qbi.detailUrl = bookMainUrl;
                    qbi.indexUrl = bookIndexUrl;
                    qbi.type= type;
                    qbi.status = statusText;
                    qbi.lastDate = lastUpdate;
                    qbi.retentionRatio = favCount;
                    bookList.Add(qbi);
                }

            }

            //var param_query = new KeyValuePair<string, string>("query", query);
            //var param_start = new KeyValuePair<string, string>("start", start.ToString());
            //var param_limit = new KeyValuePair<string, string>("limit", limit.ToString());
            //var ret = HttpHelper.GET_JsonObject(host, param_query, param_start, param_limit);
            //var ok = ret["ok"].ToObject<bool>();
            //if (ok)
            //{
            //    var books = ret["books"];
            //    foreach (var book in books)
            //    {
            //        QueryBookInfo bookInfo = book.ToObject<QueryBookInfo>();
            //        bookList.Add(bookInfo);
            //    }
            //}
            return bookList.ToArray();
        }



        /// <summary>
        /// 获得章节列表
        /// </summary>
        /// <param name="tocid">书源ID</param>
        /// <returns></returns>

        public override TocChaperListInfo getChaperList(string tocUrl, string bookName, string bookId)
        {
            //string host = string.Format(tocUrl);
            //var ret = HttpHelper.GET_JsonObject(host);
            //return ret.ToObject<TocChaperListInfo>();

            var chapterList = new TocChaperListInfo();
            chapterList.link = tocUrl;
            chapterList.name = bookName;
            chapterList._id = bookId;

//var restult = _pool.GetClient().GetStringAsync(tocUrl).Result;

            var restult = HttpHelper.Get(tocUrl, host); // client.GetStringAsync(tocUrl).Result;


            var doc = new HtmlDocument();
            doc.LoadHtml(restult);

            var dl = doc.DocumentNode.SelectSingleNode($"/html/body/section[@class='ml_main']/dl");

            var dds = dl.SelectNodes($"./dd");

            var chapList = new List<tocChaperInfo>();

            var i = 0;
            foreach (var dd in dds)
            {
                i++;
                if (!dd.HasChildNodes)
                {
                    continue;
                }
                var a = dd.SelectSingleNode($"./a");
                var chaptName = a.InnerText;

                //<a href="/6/6493/6245365.html" title="新书感言">新书感言</a>
                var chaptUrl = a.GetAttributeValue("href", String.Empty);

                var chapId = chaptUrl.Replace(".html", string.Empty);

                var ci = new tocChaperInfo()
                {
                    link = chaptUrl,
                    title = chaptName
                    ,
                    id = chapId
                    ,
                    index = i
                };

                chapList.Add(ci);

            }

            chapterList.chapters = chapList.ToArray();


            return chapterList;
        }
        /// <summary>
        /// 获得章节内容
        /// </summary>
        /// <param name="link">章节列表中的Link</param>
        /// <returns></returns>
        public override async Task<ChapterInfo> getChapter(string chapterLink,string chapterName,string chapterId,int chIndex)
        {
            int timestamp = ConvertDateTimeInt(DateTime.Now);
            //chaperLink = HttpUtility.UrlEncode(chaperLink, Encoding.UTF8);

            var durl = chapterLink;
            if (!durl.StartsWith("http"))
            {
                durl = host + chapterLink;
            }

            var ret =await _pool.LoadUrlAsyncTask2(durl);

            //var ret = HttpHelper.Get(durl, chapterLink);// client.GetStringAsync(durl).Result;

            //var ok = ret["ok"].ToObject<bool>();
            //if (ok)
            //{
            //    var info = ret["chapter"].ToObject<ChapterInfo>();
            //    return info;
            //}

            if (!string.IsNullOrEmpty(ret))
            {
                var doc = new HtmlDocument();

                doc.LoadHtml(ret);

                var contentNode = doc.DocumentNode.SelectSingleNode($"/html/body/div[@class='novel']/div[@class='ydleft']/div[@class='yd_text2']");

                var content = contentNode.InnerText.Replace("&nbsp;&nbsp;","  ");


                var ci = new ChapterInfo();

                ci.id = chapterId;
                ci.body = content;
                ci.title = chapterName;
                ci.index = chIndex;
                return ci;
            }
            return null;
        }



    }
}
