using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookLogicCommon
{
    public class HttpHelper
    {
        public static string Get(string url,string reffer)
        {
            var request = WebRequest.CreateHttp(url);
            request.Method = "GET";
            request.ProtocolVersion = new Version(1, 1);
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36";
            //request.Headers.Add("X-Device-Id", "631cf212b409f949264fad9ba1ba1daa");
            request.Referer = "";
            var response = request.GetResponse();
            var repStream = response.GetResponseStream();
            var reader = new StreamReader(repStream);
            var result = reader.ReadToEnd();
            reader.Close();
            return result;
        }
    }
}
