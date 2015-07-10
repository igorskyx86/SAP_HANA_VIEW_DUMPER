namespace HANA_XML_Dumper.ExcelAddIn
{
    partial class AnalyticDumpDialog
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
            if (disposing && (components != null))
            {
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxSrc = new System.Windows.Forms.ListBox();
            this.listBoxDest = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.checkedListBoxSections = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxViewName = new System.Windows.Forms.CheckBox();
            this.rbAllViewsOneSheetOption = new System.Windows.Forms.RadioButton();
            this.rbEachViewSheetOption = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder:";
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Location = new System.Drawing.Point(52, 12);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(476, 20);
            this.textBoxFolder.TabIndex = 1;
            this.textBoxFolder.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBoxFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxFolder_KeyDown);
            this.textBoxFolder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFolder_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(534, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBoxSrc
            // 
            this.listBoxSrc.FormattingEnabled = true;
            this.listBoxSrc.Location = new System.Drawing.Point(12, 62);
            this.listBoxSrc.Name = "listBoxSrc";
            this.listBoxSrc.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxSrc.Size = new System.Drawing.Size(279, 212);
            this.listBoxSrc.TabIndex = 3;
            this.listBoxSrc.SelectedIndexChanged += new System.EventHandler(this.listBoxSrc_SelectedIndexChanged);
            this.listBoxSrc.DoubleClick += new System.EventHandler(this.listBoxSrc_DoubleClick);
            // 
            // listBoxDest
            // 
            this.listBoxDest.FormattingEnabled = true;
            this.listBoxDest.Location = new System.Drawing.Point(330, 62);
            this.listBoxDest.Name = "listBoxDest";
            this.listBoxDest.Size = new System.Drawing.Size(279, 212);
            this.listBoxDest.TabIndex = 4;
            this.listBoxDest.SelectedIndexChanged += new System.EventHandler(this.listBoxDest_SelectedIndexChanged);
            this.listBoxDest.DoubleClick += new System.EventHandler(this.listBoxDest_DoubleClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(297, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(297, 91);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = ">>";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(297, 251);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(27, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "<<";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(297, 222);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(27, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "<";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(465, 404);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(72, 28);
            this.button6.TabIndex = 9;
            this.button6.Text = "&Dump";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // checkedListBoxSections
            // 
            this.checkedListBoxSections.FormattingEnabled = true;
            this.checkedListBoxSections.Location = new System.Drawing.Point(14, 304);
            this.checkedListBoxSections.Name = "checkedListBoxSections";
            this.checkedListBoxSections.Size = new System.Drawing.Size(277, 94);
            this.checkedListBoxSections.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Available analytic views:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(327, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Selected analytic views:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 288);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Sections to dump:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxViewName);
            this.groupBox1.Controls.Add(this.rbAllViewsOneSheetOption);
            this.groupBox1.Controls.Add(this.rbEachViewSheetOption);
            this.groupBox1.Location = new System.Drawing.Point(331, 288);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 110);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dump options";
            // 
            // checkBoxViewName
            // 
            this.checkBoxViewName.AutoSize = true;
            this.checkBoxViewName.Checked = true;
            this.checkBoxViewName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxViewName.Location = new System.Drawing.Point(10, 72);
            this.checkBoxViewName.Name = "checkBoxViewName";
            this.checkBoxViewName.Size = new System.Drawing.Size(167, 17);
            this.checkBoxViewName.TabIndex = 2;
            this.checkBoxViewName.Text = "Add view name as 1st column";
            this.checkBoxViewName.UseVisualStyleBackColor = true;
            // 
            // rbAllViewsOneSheetOption
            // 
            this.rbAllViewsOneSheetOption.AutoSize = true;
            this.rbAllViewsOneSheetOption.Enabled = false;
            this.rbAllViewsOneSheetOption.Location = new System.Drawing.Point(10, 49);
            this.rbAllViewsOneSheetOption.Name = "rbAllViewsOneSheetOption";
            this.rbAllViewsOneSheetOption.Size = new System.Drawing.Size(158, 17);
            this.rbAllViewsOneSheetOption.TabIndex = 1;
            this.rbAllViewsOneSheetOption.Text = "Dump all views to one sheet";
            this.rbAllViewsOneSheetOption.UseVisualStyleBackColor = true;
            // 
            // rbEachViewSheetOption
            // 
            this.rbEachViewSheetOption.AutoSize = true;
            this.rbEachViewSheetOption.Checked = true;
            this.rbEachViewSheetOption.Location = new System.Drawing.Point(10, 26);
            this.rbEachViewSheetOption.Name = "rbEachViewSheetOption";
            this.rbEachViewSheetOption.Size = new System.Drawing.Size(190, 17);
            this.rbEachViewSheetOption.TabIndex = 0;
            this.rbEachViewSheetOption.TabStop = true;
            this.rbEachViewSheetOption.Text = "Dump each view to seperate sheet";
            this.rbEachViewSheetOption.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 409);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(444, 23);
            this.progressBar1.TabIndex = 15;
            this.progressBar1.Visible = false;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(543, 404);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(72, 28);
            this.button7.TabIndex = 16;
            this.button7.Text = "&Close";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // AnalyticDumpDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 444);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkedListBoxSections);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBoxDest);
            this.Controls.Add(this.listBoxSrc);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnalyticDumpDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analytic views dump";
            this.Load += new System.EventHandler(this.CalculationDumpWizard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CalculationDumpWizard_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBoxSrc;
        private System.Windows.Forms.ListBox listBoxDest;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckedListBox checkedListBoxSections;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAllViewsOneSheetOption;
        private System.Windows.Forms.RadioButton rbEachViewSheetOption;
        private System.Windows.Forms.CheckBox checkBoxViewName;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button7;
    }
}