namespace KindleHelper
{
    partial class FormBookDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBookDetail));
            this.picturebox_cover = new System.Windows.Forms.PictureBox();
            this.label_name = new System.Windows.Forms.Label();
            this.label_baseinfo = new System.Windows.Forms.Label();
            this.label_Type = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.label_lastChapter = new System.Windows.Forms.Label();
            this.label_lastdate = new System.Windows.Forms.Label();
            this.label_jianjie = new System.Windows.Forms.Label();
            this.textBox_shortIntro = new System.Windows.Forms.TextBox();
            this.button_download = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listview_chapers = new System.Windows.Forms.ListView();
            this.progressbar_download = new System.Windows.Forms.ProgressBar();
            this.label_downloadinfo = new System.Windows.Forms.Label();
            this.backgroundworker_download = new System.ComponentModel.BackgroundWorker();
            this.btnDownloadParts = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.NumericUpDown();
            this.txtTo = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblChapterCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_cover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).BeginInit();
            this.SuspendLayout();
            // 
            // picturebox_cover
            // 
            this.picturebox_cover.Location = new System.Drawing.Point(24, 24);
            this.picturebox_cover.Margin = new System.Windows.Forms.Padding(6);
            this.picturebox_cover.Name = "picturebox_cover";
            this.picturebox_cover.Size = new System.Drawing.Size(240, 304);
            this.picturebox_cover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picturebox_cover.TabIndex = 0;
            this.picturebox_cover.TabStop = false;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_name.Location = new System.Drawing.Point(278, 26);
            this.label_name.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(174, 50);
            this.label_name.TabIndex = 1;
            this.label_name.Text = "完美世界";
            this.label_name.Click += new System.EventHandler(this.label1_Click);
            // 
            // label_baseinfo
            // 
            this.label_baseinfo.AutoSize = true;
            this.label_baseinfo.Location = new System.Drawing.Point(288, 102);
            this.label_baseinfo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_baseinfo.Name = "label_baseinfo";
            this.label_baseinfo.Size = new System.Drawing.Size(262, 24);
            this.label_baseinfo.TabIndex = 2;
            this.label_baseinfo.Text = "天使 | 玄幻 | 64 万字";
            // 
            // label_Type
            // 
            this.label_Type.AutoSize = true;
            this.label_Type.Location = new System.Drawing.Point(288, 146);
            this.label_Type.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_Type.Name = "label_Type";
            this.label_Type.Size = new System.Drawing.Size(58, 24);
            this.label_Type.TabIndex = 3;
            this.label_Type.Text = "类型";
            this.label_Type.Visible = false;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Location = new System.Drawing.Point(288, 188);
            this.label_status.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(58, 24);
            this.label_status.TabIndex = 4;
            this.label_status.Text = "状态";
            // 
            // label_lastChapter
            // 
            this.label_lastChapter.AutoSize = true;
            this.label_lastChapter.Location = new System.Drawing.Point(288, 230);
            this.label_lastChapter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_lastChapter.Name = "label_lastChapter";
            this.label_lastChapter.Size = new System.Drawing.Size(202, 24);
            this.label_lastChapter.TabIndex = 5;
            this.label_lastChapter.Text = "最近更新:第100章";
            // 
            // label_lastdate
            // 
            this.label_lastdate.AutoSize = true;
            this.label_lastdate.Location = new System.Drawing.Point(288, 272);
            this.label_lastdate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_lastdate.Name = "label_lastdate";
            this.label_lastdate.Size = new System.Drawing.Size(106, 24);
            this.label_lastdate.TabIndex = 6;
            this.label_lastdate.Text = "更新日期";
            // 
            // label_jianjie
            // 
            this.label_jianjie.AutoSize = true;
            this.label_jianjie.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_jianjie.Location = new System.Drawing.Point(26, 334);
            this.label_jianjie.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_jianjie.Name = "label_jianjie";
            this.label_jianjie.Size = new System.Drawing.Size(107, 50);
            this.label_jianjie.TabIndex = 7;
            this.label_jianjie.Text = "简介:";
            // 
            // textBox_shortIntro
            // 
            this.textBox_shortIntro.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_shortIntro.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_shortIntro.Location = new System.Drawing.Point(24, 390);
            this.textBox_shortIntro.Margin = new System.Windows.Forms.Padding(6);
            this.textBox_shortIntro.Multiline = true;
            this.textBox_shortIntro.Name = "textBox_shortIntro";
            this.textBox_shortIntro.ReadOnly = true;
            this.textBox_shortIntro.Size = new System.Drawing.Size(974, 278);
            this.textBox_shortIntro.TabIndex = 8;
            this.textBox_shortIntro.Text = "一粒尘可填海，一根草斩尽日月星辰，弹指间天翻地覆";
            // 
            // button_download
            // 
            this.button_download.Location = new System.Drawing.Point(848, 700);
            this.button_download.Margin = new System.Windows.Forms.Padding(6);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(150, 46);
            this.button_download.TabIndex = 11;
            this.button_download.Text = "全本下载";
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 716);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 13;
            this.label1.Text = "章节列表:";
            // 
            // listview_chapers
            // 
            this.listview_chapers.FullRowSelect = true;
            this.listview_chapers.HideSelection = false;
            this.listview_chapers.Location = new System.Drawing.Point(42, 856);
            this.listview_chapers.Margin = new System.Windows.Forms.Padding(6);
            this.listview_chapers.Name = "listview_chapers";
            this.listview_chapers.Size = new System.Drawing.Size(952, 278);
            this.listview_chapers.TabIndex = 14;
            this.listview_chapers.UseCompatibleStateImageBehavior = false;
            this.listview_chapers.View = System.Windows.Forms.View.Details;
            this.listview_chapers.DoubleClick += new System.EventHandler(this.listview_chapers_DoubleClick);
            // 
            // progressbar_download
            // 
            this.progressbar_download.Location = new System.Drawing.Point(154, 700);
            this.progressbar_download.Margin = new System.Windows.Forms.Padding(6);
            this.progressbar_download.Name = "progressbar_download";
            this.progressbar_download.Size = new System.Drawing.Size(670, 46);
            this.progressbar_download.Step = 1;
            this.progressbar_download.TabIndex = 15;
            this.progressbar_download.Visible = false;
            // 
            // label_downloadinfo
            // 
            this.label_downloadinfo.AutoSize = true;
            this.label_downloadinfo.Location = new System.Drawing.Point(150, 674);
            this.label_downloadinfo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_downloadinfo.Name = "label_downloadinfo";
            this.label_downloadinfo.Size = new System.Drawing.Size(358, 24);
            this.label_downloadinfo.TabIndex = 16;
            this.label_downloadinfo.Text = "正在下载:第三百章 111/999 95%";
            this.label_downloadinfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_downloadinfo.Visible = false;
            // 
            // backgroundworker_download
            // 
            this.backgroundworker_download.WorkerReportsProgress = true;
            this.backgroundworker_download.WorkerSupportsCancellation = true;
            this.backgroundworker_download.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundworker_download_DoWork);
            this.backgroundworker_download.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundworker_download_ProgressChanged);
            this.backgroundworker_download.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundworker_download_RunWorkerCompleted);
            // 
            // btnDownloadParts
            // 
            this.btnDownloadParts.Location = new System.Drawing.Point(848, 760);
            this.btnDownloadParts.Margin = new System.Windows.Forms.Padding(6);
            this.btnDownloadParts.Name = "btnDownloadParts";
            this.btnDownloadParts.Size = new System.Drawing.Size(150, 46);
            this.btnDownloadParts.TabIndex = 17;
            this.btnDownloadParts.Text = "部分下载";
            this.btnDownloadParts.UseVisualStyleBackColor = true;
            this.btnDownloadParts.Visible = false;
            this.btnDownloadParts.Click += new System.EventHandler(this.btnDownloadParts_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(382, 782);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 24);
            this.label3.TabIndex = 20;
            this.label3.Text = "到";
            this.label3.Visible = false;
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(170, 770);
            this.txtFrom.Margin = new System.Windows.Forms.Padding(6);
            this.txtFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(200, 35);
            this.txtFrom.TabIndex = 22;
            this.txtFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtFrom.Visible = false;
            this.txtFrom.ValueChanged += new System.EventHandler(this.txtFrom_ValueChanged);
            this.txtFrom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFrom_KeyUp);
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(428, 770);
            this.txtTo.Margin = new System.Windows.Forms.Padding(6);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(200, 35);
            this.txtTo.TabIndex = 23;
            this.txtTo.Visible = false;
            this.txtTo.ValueChanged += new System.EventHandler(this.txtTo_ValueChanged);
            this.txtTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTo_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 782);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 24);
            this.label2.TabIndex = 24;
            this.label2.Text = "从：";
            this.label2.Visible = false;
            // 
            // lblChapterCount
            // 
            this.lblChapterCount.AutoSize = true;
            this.lblChapterCount.Location = new System.Drawing.Point(640, 782);
            this.lblChapterCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblChapterCount.Name = "lblChapterCount";
            this.lblChapterCount.Size = new System.Drawing.Size(0, 24);
            this.lblChapterCount.TabIndex = 25;
            // 
            // FormBookDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 1098);
            this.Controls.Add(this.lblChapterCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDownloadParts);
            this.Controls.Add(this.label_downloadinfo);
            this.Controls.Add(this.progressbar_download);
            this.Controls.Add(this.listview_chapers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_download);
            this.Controls.Add(this.textBox_shortIntro);
            this.Controls.Add(this.label_jianjie);
            this.Controls.Add(this.label_lastdate);
            this.Controls.Add(this.label_lastChapter);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label_Type);
            this.Controls.Add(this.label_baseinfo);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.picturebox_cover);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1028, 1169);
            this.MinimumSize = new System.Drawing.Size(1028, 1169);
            this.Name = "FormBookDetail";
            this.Text = "书籍详情";
            this.Load += new System.EventHandler(this.FormBookDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_cover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picturebox_cover;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_baseinfo;
        private System.Windows.Forms.Label label_Type;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Label label_lastChapter;
        private System.Windows.Forms.Label label_lastdate;
        private System.Windows.Forms.Label label_jianjie;
        private System.Windows.Forms.TextBox textBox_shortIntro;
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listview_chapers;
        private System.Windows.Forms.ProgressBar progressbar_download;
        private System.Windows.Forms.Label label_downloadinfo;
        private System.ComponentModel.BackgroundWorker backgroundworker_download;
        private System.Windows.Forms.Button btnDownloadParts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtFrom;
        private System.Windows.Forms.NumericUpDown txtTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblChapterCount;
    }
}