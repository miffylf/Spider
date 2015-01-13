using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace spider
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Hashtable tb = new Hashtable();
            Anaysis asd = new Anaysis();
            asd.GetHerf(listBox1,tb, "www.baidu.com");
        }
    }
}
