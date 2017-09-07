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
        private string password = "blazings";
        private string iv = "iv";
        public SelectPaste_From()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            var filePath = jyh.OpenFile("*.txt");
            FileInfo fi = new FileInfo(filePath);
            if (fi.Extension != ".txt")
            {
                MessageBox.Show("请选择txt文件");
                return;
            }
            var signal=jyh.WriteRegister(FilePath, FilePath, FilePath, filePath);
            if (!signal)
            {
                MessageBox.Show("写入注册表失败");
                return;
            }
            txtboxFilePath.Text = filePath;
            ReloadList(valueList);

        }

        private void SelectPaste_From_Load(object sender, EventArgs e)
        {
            var regInfo=jyh.GetRegister(FilePath, FilePath);
            if (regInfo.Count>0 && File.Exists(regInfo.First(r => r.Key==FilePath).Value))
            {
                txtboxFilePath.Text = regInfo.First(r => r.Key == FilePath).Value;
                var valueList = fc.ReadAllValue(section, txtboxFilePath.Text, jyh);
                ReloadList(valueList);
            }
            else
            {
                SelectFile_Click(sender, e);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtboxValue.Text=="")
            {
                return;
            }
            try
            {
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                throw;
            }
            
        }
        public void ReloadList(List<string> values)
        {
            listBox1.DataSource = values;
        }

        public bool WriteValue(string valueText, string passwordIV= "blazings")
        {
            var encryptStr = jyh.DESEncrypt(valueText, passwordIV);

            return true;
        }
    }
}
