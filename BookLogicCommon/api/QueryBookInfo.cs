using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLogicCommon.api
{
    public class QueryBookInfo
    {
        public string _id;
    
        public string title;
        public string type;
        public string status;

        public string author;
        public string detailUrl;
        public string cover;
        public string shortIntro;
        public string lastChapter;
        public string lastDate;
        public string retentionRatio;

        public string indexUrl;
        public long wordCount;
    }
}
