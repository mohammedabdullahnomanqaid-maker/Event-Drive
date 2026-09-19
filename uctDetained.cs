using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Publisher_Subscriber_Design_Pattern
{
    public partial class uctDetained : UserControl
    {
        public uctDetained()
        {
            InitializeComponent();
            label1.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
