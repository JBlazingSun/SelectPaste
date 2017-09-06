using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpCommonDll;

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
            var jyh = Jyh.GetInstance();
            
        }
    }

    public class MyFunc
    {
        private MyFunc()
        {
        }
        private static readonly MyFunc _instance=new MyFunc();

        public static MyFunc GetInstance()
        {
            return _instance;
        }
        
    }
}
