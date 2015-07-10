using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HANA_XML_Dumper.TaskPanes
{
    [ComVisible(true)]
    public partial class DumpUserControl : UserControl
    {
        public DumpUserControl()
        {
            InitializeComponent();
        }

        public string TheLabel
        {
            get { return thelabel.Text; }
            set { thelabel.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("123");
        }
    }
}
