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
        Jyh jyh = Jyh.GetInstance();
        public SelectPaste_From()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            
        }

        private void SelectPaste_From_Load(object sender, EventArgs e)
        {

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
