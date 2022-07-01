using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 using System.Net;
using System.Threading;
using System.IO;
using BookLogicCommon.api;
using BookLogicCommon;
using System.Diagnostics;

namespace KindleHelper
{
    public partial class FormBookDetail : Form
    {
        INovolLogic _logic;

        public FormBookDetail()
        {
            InitializeComponent();
        }
        QueryBookInfo mBook;
        tocChaperInfo[] mChapers;
        //TocSummmaryInfo[] mTocs;
        //MixTocInfo mMixToc;
        List<tocChaperInfo> preDownLoadChapters = new List<tocChaperInfo>();

        public void ShowBook(QueryBookInfo book, INovolLogic logic)
        {
            _logic= logic;

            mBook = book;
            label_name.Text = book.title;

            label_baseinfo.Text = book.type;

            //string wordCount = book.wordCount + "字";
            //if (book.wordCount >= 10000)
            //{
            //    wordCount = (book.wordCount / 10000) + " 万字";
            //}
            //label_baseinfo.Text = book.author + " | " + book.cat + " | " + wordCount;
            //if (!string.IsNullOrWhiteSpace(book.retentionRatio))
            //{
            //    label_retentionRatio.Text = "追书留存率:" + book.retentionRatio + "%";
            //}
            //else
            //{
            //    label_retentionRatio.Text = "追书留存率:无数据";
            //}

            label_status.Text ="状态: "+ book.status;// "追书人数:" + book.retentionRatio + " 人";
            label_lastChapter.Text = "最新章节: "+  book.lastChapter;
            //label_site.Text = "首发网站:" + book.site;\

            label_lastdate.Text = book.lastDate;

            textBox_shortIntro.Text = book.shortIntro;
            string url = book.cover;
            int urlStartIndex = url.IndexOf("http:");
            if (urlStartIndex >= 0)
            {
                url = url.Substring(urlStartIndex);
                picturebox_cover.ImageLocation = url;
            }

           var chi= _logic.getChaperList(book.indexUrl, book.title, book._id);

            this.mChapers = chi.chapters;

            //mMixToc = LibZhuiShu.getMixToc(mBook._id);
            //mTocs = LibZhuiShu.getTocSummary(mBook._id);
            //if(mMixToc!=null)
            //{
            //    changeToc(-1);
            //}
            //else if(mTocs!=null && mTocs.Length > 0)
            //{
            //    changeToc(0);
            //}
            //else
            //{
            //    MessageBox.Show("无可用的书源!");
            //    return;
            //}

            this.Show();

        }



        //void changeToc(int index)
        //{
        //    if (index < 0)
        //    {
        //        //混合源
        //        if (mMixToc != null)
        //        {
        //            mChapers = mMixToc.chapters;
        //        }
        //        label_toc.Text = "当前书源:混合源";
        //    }
        //    else
        //    {
        //        var toc = mTocs[index];
        //        var info = LibZhuiShu.getChaperList(toc._id);
        //        if (info != null)
        //        {
        //            mChapers = info.chapters;
        //        }
        //        label_toc.Text = "当前书源:" + toc.name;
        //    }

        //    if (mChapers != null)
        //    {
        //        UpdateChapterList(mChapers);
        //    }

        //    txtFrom.Maximum = mChapers.Length;
        //    txtTo.Maximum = mChapers.Length + 1;
        //    txtTo.Value = txtTo.Maximum;
        //    lblChapterCount.Text = $"共 {mChapers.Length + 1} 章";
        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormBookDetail_Load(object sender, EventArgs e)
        {
            listview_chapers.Columns.Add("章节名称", 120);
            listview_chapers.Columns.Add("链接", 280);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void button_toc_Click(object sender, EventArgs e)
        //{
        //    if (backgroundworker_download.IsBusy) return;
        //    FormTocList formToc = new FormTocList();
        //    formToc.ShowTocList(mMixToc, mTocs, (index) =>
        //    {
        //        changeToc(index);
        //    });
        //}

        private void button_download_Click(object sender, EventArgs e)
        {
            preDownLoadChapters = new List<tocChaperInfo>(mChapers);
            DownloadClik();
        }

        private void btnDownloadParts_Click(object sender, EventArgs e)
        {
            GetPreDownLoadChapters();

            DownloadClik();
        }

        private void GetPreDownLoadChapters()
        {
            int fromChapter = (int)txtFrom.Value - 1;
            int toChapter = (int)txtTo.Value - 1;
            if (fromChapter > toChapter)
            {
                MessageBox.Show("开始章节不能大于结束章节");
                return;
            }


            preDownLoadChapters.Clear();
            for (int i = fromChapter; i < toChapter; i++)
            {
                preDownLoadChapters.Add(mChapers[i]);
            }

        }

        private void DownloadClik()
        {
            if (backgroundworker_download.IsBusy && backgroundworker_download.CancellationPending)
            {
                return;
            }

            if (backgroundworker_download.IsBusy)
            {
                stopDownload();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Kindel（*.mobi）|*.mobi|文本文件（*.txt）|*.txt";
            sfd.FilterIndex = 0;
            sfd.FileName = mBook.title;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fn = sfd.FileName;
                startDownload(fn);
            }
        }

        private void startDownload(string savePath)
        {
            button_download.Text = "取消";
            label_downloadinfo.Text = "即将开始...";
            progressbar_download.Value = 0;
            progressbar_download.Maximum = mChapers.Length;
            progressbar_download.Visible = true;
            label_downloadinfo.Visible = true;
            backgroundworker_download.RunWorkerAsync(savePath);
        }

        private void stopDownload()
        {
            if (backgroundworker_download.IsBusy)
            {
                backgroundworker_download.CancelAsync();
                button_download.Text = "正在取消";
            }
        }

        private void downloadComplate()
        {
            progressbar_download.Visible = false;
            label_downloadinfo.Visible = false;
            button_download.Text = "全本下载";
        }


        //private void backgroundworker_download_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    var chapters = preDownLoadChapters.ToArray();
        //    var pb = progressbar_download;
        //    var label = label_downloadinfo;
        //    string savePath = e.Argument.ToString();
        //    List<ChapterInfo> chaperInfoList = new List<ChapterInfo>();
        //    for (int i = 0; i < chapters.Length; i++)
        //    {
        //        if (backgroundworker_download.CancellationPending) return;
        //        var chapter = chapters[i];
        //        float progress = (float)(i + 1) / (float)chapters.Length;
        //        string info = string.Format("正在下载:{0} {1}/{2} {3:F2}%", chapter.title, i + 1, chapters.Length,
        //           progress * 100);
        //        backgroundworker_download.ReportProgress(i, info);

        //        while (true)
        //        {
        //            bool downloadSucess = false;
        //            string errMsg = "";
        //            for (int j = 0; j < 3; j++)
        //            {
        //                try
        //                {
        //                    var chapterInfo = _logic.getChapter(chapter.link,chapter.title,chapter.id);
        //                    if (chapterInfo != null)
        //                    {
        //                        chapterInfo.title = chapter.title;
        //                        chaperInfoList.Add(chapterInfo);
        //                        downloadSucess = true;
        //                        break;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    errMsg = ex.Message;
        //                }
        //            }
        //            if (!downloadSucess)
        //            {
        //                var result = MessageBox.Show(errMsg, "章节 " + chapter.title + " 下载失败", MessageBoxButtons.AbortRetryIgnore);
        //                if (result == DialogResult.Abort)
        //                {
        //                    return;
        //                }
        //                else if (result == DialogResult.Ignore)
        //                {
        //                    var emptyChaper = new ChapterInfo();
        //                    emptyChaper.title = chapter.title;
        //                    emptyChaper.body = "本章下载失败了，失败原因:\n " + errMsg;
        //                    chaperInfoList.Add(emptyChaper);
        //                    downloadSucess = true;
        //                    break;
        //                }

        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    backgroundworker_download.ReportProgress(chapters.Length, "正在生成电子书请稍后....");
        //    string ext = Path.GetExtension(savePath);
        //    Book book = new Book();
        //    book.name = mBook.title;
        //    book.author = mBook.author;
        //    book.id = mBook._id;
        //    book.chapters = chaperInfoList.ToArray();
        //    if (ext.ToLower() == ".txt")
        //    {
        //        Kindlegen.book2Txt(book, savePath);
        //    }
        //    else if (ext.ToLower() == ".mobi")
        //    {
        //        Kindlegen.book2Mobi(book, savePath);
        //    }
        //    MessageBox.Show("下载完成,文件保存在:" + savePath);
        //}

        private void backgroundworker_download_DoWork(object sender, DoWorkEventArgs e)
        {
            var chapters = preDownLoadChapters.ToArray();
            var pb = progressbar_download;
            var label = label_downloadinfo;
            string savePath = e.Argument.ToString();
            List<ChapterInfo> chaperInfoList = new List<ChapterInfo>();

            var i = 0;

            var tasklist = new List<Task>();

            foreach (var t in chapters)
            {
                var mt = Task.Run(async () => {

                    if (backgroundworker_download.CancellationPending) return;
                    var chapter = t;

                    Interlocked.Increment(ref i);

                    float progress = (float)(i) / (float)chapters.Length;
                    string info = string.Format("正在下载:{0} {1}/{2} {3:F2}%", chapter.title, i + 1, chapters.Length,
                       progress * 100);

                    backgroundworker_download.ReportProgress(i, info);

                    bool downloadSucess = false;
                    string errMsg = "";

                    try
                    {
                        var chapterInfo = await _logic.getChapter(chapter.link, chapter.title, chapter.id, chapter.index).ConfigureAwait(false);
                        if (chapterInfo != null)
                        {
                            chapterInfo.title = chapter.title;
                            chaperInfoList.Add(chapterInfo);
                            downloadSucess = true;

                        }
                    }
                    catch (Exception ex)
                    {
                        errMsg = ex.Message;
                    }

                    if (!downloadSucess)
                    {
                        var result = MessageBox.Show(errMsg, "章节 " + chapter.title + " 下载失败", MessageBoxButtons.AbortRetryIgnore);
                        if (result == DialogResult.Abort)
                        {
                            return;
                        }
                        else if (result == DialogResult.Ignore)
                        {
                            var emptyChaper = new ChapterInfo();
                            emptyChaper.title = chapter.title;
                            emptyChaper.body = "本章下载失败了，失败原因:\n " + errMsg;
                            chaperInfoList.Add(emptyChaper);
                            downloadSucess = true;

                        }

                    }

                });

                tasklist.Add(mt);

            }

            Task.WaitAll(tasklist.ToArray());

            //Parallel.ForEach(chapters, async t => {

            //    if (backgroundworker_download.CancellationPending) return;

            //    var chapter = t;

            //    Interlocked.Increment(ref i);

            //    float progress= (float)(i)/ (float)chapters.Length; 
            //    string info = string.Format("正在下载:{0} {1}/{2} {3:F2}%", chapter.title, i + 1, chapters.Length,
            //       progress * 100);

            //    backgroundworker_download.ReportProgress(i, info);

            //    bool downloadSucess = false;
            //    string errMsg = "";

            //    try
            //    {
            //        var chapterInfo = await _logic.getChapter(chapter.link, chapter.title, chapter.id, chapter.index);
            //        if (chapterInfo != null)
            //        {
            //            chapterInfo.title = chapter.title;
            //            chaperInfoList.Add(chapterInfo);
            //            downloadSucess = true;

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        errMsg = ex.Message;
            //    }

            //    if (!downloadSucess)
            //    {
            //        var result = MessageBox.Show(errMsg, "章节 " + chapter.title + " 下载失败", MessageBoxButtons.AbortRetryIgnore);
            //        if (result == DialogResult.Abort)
            //        {
            //            return;
            //        }
            //        else if (result == DialogResult.Ignore)
            //        {
            //            var emptyChaper = new ChapterInfo();
            //            emptyChaper.title = chapter.title;
            //            emptyChaper.body = "本章下载失败了，失败原因:\n " + errMsg;
            //            chaperInfoList.Add(emptyChaper);
            //            downloadSucess = true;

            //        }

            //    }

            //});


            //for (int i = 0; i < chapters.Length; i++)
            //{
            //    if (backgroundworker_download.CancellationPending) return;
            //    var chapter = chapters[i];
            //    float progress = (float)(i + 1) / (float)chapters.Length;
            //    string info = string.Format("正在下载:{0} {1}/{2} {3:F2}%", chapter.title, i + 1, chapters.Length,
            //       progress * 100);
            //    backgroundworker_download.ReportProgress(i, info);



            //    while (true)
            //    {
            //        bool downloadSucess = false;
            //        string errMsg = "";
            //        for (int j = 0; j < 3; j++)
            //        {
            //            try
            //            {
            //                var chapterInfo = _logic.getChapter(chapter.link, chapter.title, chapter.id,chapter.index);
            //                if (chapterInfo != null)
            //                {
            //                    chapterInfo.title = chapter.title;
            //                    chaperInfoList.Add(chapterInfo);
            //                    downloadSucess = true;
            //                    break;
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                errMsg = ex.Message;
            //            }
            //        }
            //        if (!downloadSucess)
            //        {
            //            var result = MessageBox.Show(errMsg, "章节 " + chapter.title + " 下载失败", MessageBoxButtons.AbortRetryIgnore);
            //            if (result == DialogResult.Abort)
            //            {
            //                return;
            //            }
            //            else if (result == DialogResult.Ignore)
            //            {
            //                var emptyChaper = new ChapterInfo();
            //                emptyChaper.title = chapter.title;
            //                emptyChaper.body = "本章下载失败了，失败原因:\n " + errMsg;
            //                chaperInfoList.Add(emptyChaper);
            //                downloadSucess = true;
            //                break;
            //            }

            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }
            //}


            if (backgroundworker_download.CancellationPending) return;

            backgroundworker_download.ReportProgress(chapters.Length, "正在生成电子书请稍后....");
            string ext = Path.GetExtension(savePath);
            Book book = new Book();
            book.name = mBook.title;
            book.author = mBook.author;
            book.id = mBook._id;
            book.chapters = chaperInfoList.OrderBy(p=>p.index).ToArray();
            if (ext.ToLower() == ".txt")
            {
                Kindlegen.book2Txt(book, savePath);
            }
            else if (ext.ToLower() == ".mobi")
            {
                Kindlegen.book2Mobi(book, savePath);
            }
            MessageBox.Show("下载完成,文件保存在:" + savePath);
        }

        private void backgroundworker_download_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            downloadComplate();
        }

        private void backgroundworker_download_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressbar_download.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                label_downloadinfo.Text = e.UserState.ToString();
            }
        }

        private void listview_chapers_DoubleClick(object sender, EventArgs e)
        {
            if (listview_chapers.SelectedIndices.Count > 0)
            {
                int index = listview_chapers.SelectedIndices[0];
                var chapter = mChapers[index];
                System.Diagnostics.Process.Start(chapter.link);
            }
        }

        private void txtFrom_ValueChanged(object sender, EventArgs e)
        {
            FromChange();
        }

        private void txtTo_ValueChanged(object sender, EventArgs e)
        {
            ToChange();
        }

        private void UpdateChapterList(tocChaperInfo[] chapters)
        {
            listview_chapers.BeginUpdate();
            listview_chapers.Items.Clear();
            foreach (var chaper in chapters)
            {
                ListViewItem item = new ListViewItem();
                item.Text = chaper.title;
                item.SubItems.Add(chaper.link);
                listview_chapers.Items.Add(item);
            }
            listview_chapers.EndUpdate();
        }

        private void txtFrom_KeyUp(object sender, KeyEventArgs e)
        {
            FromChange();
        }

        private void txtTo_KeyUp(object sender, KeyEventArgs e)
        {
            ToChange();
        }

        private void FromChange()
        {
            if (txtFrom.Value < 1) return;

            txtTo.Minimum = txtFrom.Value;

            if (txtTo.Value <= txtFrom.Value)
            {
                txtTo.Value = txtFrom.Value + 1;
            }

            GetPreDownLoadChapters();
            UpdateChapterList(preDownLoadChapters.ToArray());
        }

        private void ToChange()
        {
            GetPreDownLoadChapters();
            UpdateChapterList(preDownLoadChapters.ToArray());
        }


    }
}
