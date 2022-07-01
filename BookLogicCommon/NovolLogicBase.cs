using BookLogicCommon.api;
using System;
using System.Threading.Tasks;

namespace BookLogicCommon
{
    public abstract class NovolLogicBase : INovolLogic
    {
        public abstract QueryBookInfo[] fuzzySearch(string query);
        public abstract TocChaperListInfo getChaperList(string tocUrl,string bookName, string bookId);
        public   abstract Task<ChapterInfo> getChapter(string chapterLink, string chapterName, string chapterId,int chIndex);
 

        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public int ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}