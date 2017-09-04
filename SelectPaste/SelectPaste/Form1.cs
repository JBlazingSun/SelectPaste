using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelectPaste
{
    public partial class SelectPaste_From : Form
    {
        public SelectPaste_From()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            
        }
    }

    public class MyFunc
    {
        private MyFunc()
        {
        }
        private readonly MyFunc _instance=new MyFunc();

        public static MyFunc GetInstance()
        {
            return _instance;
        }
        
    }
}
