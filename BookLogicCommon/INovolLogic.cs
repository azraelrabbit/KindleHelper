using BookLogicCommon.api;
using System;
using System.Threading.Tasks;

namespace BookLogicCommon
{
    public interface INovolLogic
    {
        int ConvertDateTimeInt(DateTime time);
        QueryBookInfo[] fuzzySearch(string query);
        TocChaperListInfo getChaperList(string tocUrl,string bookName,string bookId);
         Task<ChapterInfo> getChapter(string chapterLink, string chapterName, string chapterId,int chIndex);
     
    }
}