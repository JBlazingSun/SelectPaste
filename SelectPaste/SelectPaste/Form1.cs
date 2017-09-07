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
        static Jyh jyh = Jyh.GetInstance();
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
            ReloadList(ReadValue(filePath));
        }

        private void SelectPaste_From_Load(object sender, EventArgs e)
        {
            var regInfo=jyh.GetRegister(FilePath, FilePath);
            if (regInfo.Count>0 && File.Exists(regInfo.First(r => r.Key==FilePath).Value))
            {
                txtboxFilePath.Text = regInfo.First(r => r.Key == FilePath).Value;
                ReloadList(ReadValue(txtboxFilePath.Text));
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
                WriteValue(txtboxValue.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                throw;
            }
            ReloadList(ReadValue(txtboxFilePath.Text));
            txtboxValue.Text = "";
        }
        public List<string> ReadValue(string filePath)
        {
            var enStrList=jyh.ReadTxt(filePath);
            var deStrList=new List<string>();
            foreach (var VARIABLE in enStrList)
            {
                if (VARIABLE!="")
                {
                    deStrList.Add(jyh.DESDecrypt(VARIABLE, password));
                }
            }
            return deStrList;
        }

        public void ReloadList(List<string> values)
        {
            listBox1.DataSource = values;
        }

        public bool WriteValue(string valueText, string passwordIV= "blazings")
        {
            var encryptStr = jyh.DESEncrypt(valueText, passwordIV);
            jyh.WriteTxtAppend(txtboxFilePath.Text, encryptStr, true);
            return true;
        }
        //删除
        public void DeleteValue(string filePath,string valueText, string passwordIV = "blazings")
        {
            var encryptStr = jyh.DESEncrypt(valueText, passwordIV);
            jyh.ModifyTxt(filePath,encryptStr,"");

        }
        //单机复制
        private void listBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(listBox1.SelectedItem.ToString());
                labState.Text = listBox1.SelectedItem.ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        //删除
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (jyh.ConfirmForm("确认删除?"))
                    {
                        var deleteStr = listBox1.SelectedItem.ToString();
                        DeleteValue(txtboxFilePath.Text, deleteStr);
                        labState.Text = "删除: " + deleteStr;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
                ReloadList(ReadValue(txtboxFilePath.Text));
            }
        }
    }
}
