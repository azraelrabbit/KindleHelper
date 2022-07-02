using BookLogicCommon;
using BookLogicCommon.api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace KindleHelper
{
    public partial class FormSearchResult : Form
    {

        INovolLogic _logic;

        private QueryBookInfo[] cacheResults;

        public FormSearchResult()
        {
            InitializeComponent();
        }

        private void FormSearchResult_Load(object sender, EventArgs e)
        {
            listview_result.DrawColumnHeader += Listview_result_DrawColumnHeader;
            //listview_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            ColumnHeader nameHeader = new ColumnHeader();

            listview_result.Columns.Add("书名",180, HorizontalAlignment.Left);
            listview_result.Columns.Add("作者", 180, HorizontalAlignment.Left);

            listview_result.Columns.Add("类型", 180, HorizontalAlignment.Left);
            listview_result.Columns.Add("状态", 180, HorizontalAlignment.Left);
            //listview_result.Columns.Add("网站", 80, HorizontalAlignment.Left);
            //listview_result.Columns.Add("字数", 60, HorizontalAlignment.Left);
            //listview_result.Columns.Add("留存率", 60, HorizontalAlignment.Left);
            listview_result.Columns.Add("最后更新章节", 280, HorizontalAlignment.Left);
            listview_result.Columns.Add("最后更新时间", 280, HorizontalAlignment.Left);
            //listview_result.Columns.Add("简介", 250, HorizontalAlignment.Left);

            
        }

       
     

        public void ShowResult(QueryBookInfo[] results,INovolLogic logic)
        {
            _logic = logic;

            if (results == null) return;
            listview_result.BeginUpdate();
            foreach (var result in results) {
                ListViewItem item = new ListViewItem();
                item.Text = result.title;
                item.SubItems.Add(result.author);
                //item.SubItems.Add(result.detailUrl);
                //item.SubItems.Add(formatWordCount(result.wordCount));
                //if (!string.IsNullOrWhiteSpace(result.retentionRatio)) {
                //    item.SubItems.Add(result.retentionRatio + "%");
                //} else {
                //    item.SubItems.Add("无");
                //}
                item.SubItems.Add(result.type);
                item.SubItems.Add(result.status);
                item.SubItems.Add(result.lastChapter);
                item.SubItems.Add(result.lastDate);
                //item.SubItems.Add(result.shortIntro);
                listview_result.Items.Add(item);
            }

           
            listview_result.EndUpdate();

            
            this.Show();
            cacheResults = results;


            listview_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listview_result.Invalidate();
        }


        static string formatWordCount(long wordCount)
        {
            //if (wordCount > 10000) {
            //    return (wordCount / 10000) + " 万";
            //}
            //return wordCount.ToString();
            return "N/A";
        }

        private void listview_result_DoubleClick(object sender, EventArgs e)
        {
            var lv = listview_result;
            if (lv.SelectedIndices.Count > 0) {
                var index = lv.SelectedIndices[0];
                var detailForm = new FormBookDetail();
                detailForm.ShowBook(cacheResults[index],_logic);
            }
        }

        private void Listview_result_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            e.DrawText();
            using (Pen p1 = new Pen(Color.FromArgb(227, 227, 227), 1))
            {
                e.Graphics.DrawLine(p1, e.Bounds.X + e.Bounds.Width, e.Bounds.Y, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
                e.Graphics.DrawLine(p1, e.Bounds.X, e.Bounds.Y, e.Bounds.X + e.Bounds.Width, e.Bounds.Y);
                e.Graphics.DrawLine(p1, e.Bounds.X, e.Bounds.Y + e.Bounds.Height - 1, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height - 1);
            }
        }
    }
}
