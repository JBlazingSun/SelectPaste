using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpCommonDll;

namespace SelectPaste
{
    public partial class SelectPaste_From : Form
    {
        private string FilePath = "FilePath";
        Jyh jyh = Jyh.GetInstance();
        private MyFunc fc = MyFunc.GetInstance();
        public SelectPaste_From()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            var filePath = jyh.OpenFile("*.ini || ");
            var signal=jyh.WriteRegister(FilePath, FilePath, FilePath, filePath);
            if (!signal)
            {
                MessageBox.Show("写入注册表失败");
                return;
            }
            txtboxFilePath.Text = filePath;

        }

        private void SelectPaste_From_Load(object sender, EventArgs e)
        {
            var regInfo=jyh.GetRegister(FilePath, FilePath);
            if (regInfo.Count>0 && File.Exists(regInfo.First(r => r.Key==FilePath).Value))
            {
                txtboxFilePath.Text = regInfo.First(r => r.Key == FilePath).Value;
            }
            else
            {
                SelectFile_Click(sender, e);
            }
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
