using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDna.Integration.CustomUI;
using System.Runtime.InteropServices;
using ExcelDna.Integration;
using HANA_XML_Dumper.ExcelAddIn;

namespace HANA_XML_Dumper
{
    [ComVisible(true)]
    public class MyRibbon : ExcelRibbon
    {
        public void OnButtonPressed(IRibbonControl control)
        {
            //MessageBox.Show("Hello from control " + control.Id);
            CTPManager.ShowCTP();
        }
        public static void ShowDumpCalc()
        {
            CalculationDumpDialog calculationDumpDialog = new CalculationDumpDialog();
            calculationDumpDialog.ShowDialog();
        }

        public static void ShowDumpAnalytic()
        {
            AnalyticDumpDialog analyticDumpDialog = new AnalyticDumpDialog();
            analyticDumpDialog.ShowDialog();
        }
    }
    public static class MyFunctions
    {
        [ExcelFunction(Description = "My first .NET function")]
        public static string SayHello(string name)
        {
            return "Hello " + name;
        }
    }

}
