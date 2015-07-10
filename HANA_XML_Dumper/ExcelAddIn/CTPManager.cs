using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration.CustomUI;
using ExcelDna.Integration;
using HANA_XML_Dumper.TaskPanes;
using System.Windows.Forms;

namespace HANA_XML_Dumper.ExcelAddIn
{

    internal static class CTPManager
    {
        static CustomTaskPane ctp;

        public static void ShowCTP()
        {
            if (ctp == null)
            {
                // Make a new one using ExcelDna.Integration.CustomUI.CustomTaskPaneFactory 
                /*
                ctp = CustomTaskPaneFactory.CreateCustomTaskPane(typeof(DumpUserControl), "My Super Task Pane");
                ctp.Visible = true;
                ctp.DockPosition = MsoCTPDockPosition.msoCTPDockPositionLeft;
                ctp.DockPositionStateChange += ctp_DockPositionStateChange;
                ctp.VisibleStateChange += ctp_VisibleStateChange;
                 */

                CalculationDumpDialog calculationDumpWizard = new CalculationDumpDialog();
                calculationDumpWizard.ShowDialog();
            }
            else
            {
                // Just show it again
                ctp.Visible = true;
            }
        }


        public static void DeleteCTP()
        {
            if (ctp != null)
            {
                // Could hide instead, by calling ctp.Visible = false;
                ctp.Delete();
                ctp = null;
            }
        }

        static void ctp_VisibleStateChange(CustomTaskPane CustomTaskPaneInst)
        {
            MessageBox.Show("Visibility changed to " + CustomTaskPaneInst.Visible);
        }

        static void ctp_DockPositionStateChange(CustomTaskPane CustomTaskPaneInst)
        {
            ((DumpUserControl)ctp.ContentControl).TheLabel = "Moved to " + CustomTaskPaneInst.DockPosition.ToString();
        }
    }
}

