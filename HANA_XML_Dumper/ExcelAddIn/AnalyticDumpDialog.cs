using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HANA_XML_Dumper.Data;
using ExcelDna.Integration;
using Microsoft.Office.Interop;
using HANA_XML_Dumper.Dumpers;
using System.Xml;
using HANA_XML_Dumper.Members;

namespace HANA_XML_Dumper.ExcelAddIn
{
    public partial class AnalyticDumpDialog : Form
    {
        Dictionary<string, Type> typeContainer;

        public AnalyticDumpDialog()
        {
            InitializeComponent();
            checkedListBoxSections.Items.Add("Semantics", true);
            checkedListBoxSections.Items.Add("Data foundation", true);
            checkedListBoxSections.Items.Add("Variables", true);

            srcFiles = new Dictionary<string, FilesData>();
            typeContainer = new Dictionary<string, Type>();
            typeContainer.Add("Semantics", typeof(AN_SemanticsDumper));
            typeContainer.Add("Data foundation", typeof(AN_DataFoundationMember));
            typeContainer.Add("Variables", typeof(AN_VariableDumper));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(listBoxDest, listBoxSrc);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MoveAllListBoxItems(listBoxDest, listBoxSrc);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MoveAllListBoxItems(listBoxSrc, listBoxDest);
        }

        private void MoveListBoxItems(System.Windows.Forms.ListBox source, System.Windows.Forms.ListBox destination)
        {
            ListBox.SelectedObjectCollection sourceItems = source.SelectedItems;
            foreach (var item in sourceItems)
            {
                destination.Items.Add(item);
            }
            while (source.SelectedItems.Count > 0)
            {
                source.Items.Remove(source.SelectedItems[0]);
            }
        }

        private void MoveAllListBoxItems(System.Windows.Forms.ListBox source, System.Windows.Forms.ListBox destination)
        {
            destination.Items.AddRange(source.Items);
            source.Items.Clear();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(listBoxSrc, listBoxDest);
        }

        private void listBoxDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBoxSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public string Folder
        {
            get { return textBoxFolder.Text; }
            set { textBoxFolder.Text = value; }
        }

        Dictionary<string, FilesData> srcFiles;

        private void FolderChanged()
        {
            listBoxSrc.Items.Clear();
            string[] views = Directory.GetFiles(this.Folder, "*.analyticview", SearchOption.AllDirectories);
            foreach (string file in views)
            {
                FilesData filesData = null;
                if (srcFiles.ContainsKey(file))
                {
                    filesData = srcFiles[file];
                }
                else
                {
                    filesData = new FilesData(file);
                    srcFiles.Add(file, filesData);
                }
                if (!listBoxDest.Items.Contains(filesData))
                    listBoxSrc.Items.Add(filesData);

            }
        }

        private void BrowseFolder()
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Open a folder which contains the HANA Developer Mode output";
                folderBrowserDialog.ShowNewFolderButton = false;
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                folderBrowserDialog.SelectedPath = this.Folder;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Folder = folderBrowserDialog.SelectedPath;
                    FolderChanged();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CalculationDumpWizard_Load(object sender, EventArgs e)
        {

        }

        private void textBoxFolder_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void CalculationDumpWizard_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ChangeSelection(ListBox listBox, bool selected)
        {
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                listBox.SetSelected(i, selected);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeSelection(listBoxSrc, true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChangeSelection(listBoxSrc, false);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ChangeSelection(listBoxDest, true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangeSelection(listBoxDest, false);
        }


        //private void PrepareSheets()

        private void Dump()
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void listBoxSrc_DoubleClick(object sender, EventArgs e)
        {
            MoveListBoxItems(listBoxSrc, listBoxDest);
        }

        private void listBoxDest_DoubleClick(object sender, EventArgs e)
        {
            MoveListBoxItems(listBoxDest, listBoxSrc);
        }

        private void textBoxFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.Folder = textBoxFolder.Text;
                FolderChanged();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var excelApp = (Microsoft.Office.Interop.Excel.Application)ExcelDnaUtil.Application;
            try
            {
                excelApp.Application.ScreenUpdating = false;
                excelApp.Application.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual;
                BackgroundWorker backgroundWorker = sender as BackgroundWorker;
                var excelSheet = excelApp.ActiveWorkbook.ActiveSheet;
                int rowCounter = excelApp.ActiveCell.Row;
                int colCounter = 1;
                double maxProgress = checkedListBoxSections.CheckedItems.Count+1 * listBoxDest.Items.Count+1;
                int sectionsQty = 1;
                int filesQty = 1;
                double currentProgress = 0;
                foreach (var item in checkedListBoxSections.CheckedItems)
                {

                    var dumper = (IDumper)Activator.CreateInstance(typeContainer[item.ToString()], new object[] { null });
                    var firstmember = (IMember)Activator.CreateInstance(dumper.MemberType);
                    excelSheet.Cells(rowCounter++, colCounter).Value = item.ToString();
                    if (checkBoxViewName.Checked)
                        excelSheet.Cells(rowCounter, colCounter++).Value = "View";
                    foreach (string field in firstmember.ToArray(Enums.MemberToStringOption.Header))
                    {

                        excelSheet.Cells(rowCounter, colCounter++).Value = field;
                    }
                    rowCounter++;


                    foreach (FilesData filesData in listBoxDest.Items)
                    {

                        using (XmlTextReader reader = new XmlTextReader(filesData.FullPath))
                        {

                            string viewName = Path.GetFileNameWithoutExtension(filesData.FullPath);
                            var HANAXMLDumper = (IDumper)Activator.CreateInstance(typeContainer[item.ToString()], new object[] { reader });
                            List<IMember> viewMembers = HANAXMLDumper.DumpHANAXML(viewName);


                            foreach (IMember member in viewMembers)
                            {
                                colCounter = 1;
                                if (checkBoxViewName.Checked)
                                    excelSheet.Cells(rowCounter, colCounter++).Value = viewName;
                                foreach (string field in member.ToArray(Enums.MemberToStringOption.Body))
                                {
                                    excelSheet.Cells(rowCounter, colCounter++).Value = field;
                                }
                                rowCounter++;
                            }



                        }
                        //rowCounter++;
                        filesQty++;
                        currentProgress = filesQty * sectionsQty;
                        backgroundWorker.ReportProgress(Convert.ToInt32(currentProgress / maxProgress * 100));
                    }
                    sectionsQty++;
                }

                excelApp.ActiveWorkbook.ActiveSheet.Columns("A:X").AutoFit();
            }
         /*   catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error occured: {0}", ex.Message), "Dump error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }*/
            finally
            {
                excelApp.Application.ScreenUpdating = true;
                excelApp.Application.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationAutomatic;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = Math.Min(e.ProgressPercentage,100);

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
