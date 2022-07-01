﻿using BookLogicCommon.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace KindleHelper
{
    public class Book
    {
        public string id;
        public string name;
        public string author;
        public ChapterInfo[] chapters;
    }
}
