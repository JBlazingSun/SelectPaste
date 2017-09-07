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
        private const int hotKeyId = 100;
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
        //窗口加载
        private void SelectPaste_From_Load(object sender, EventArgs e)
        {
            //热键
            if (!Jyh.RegisterHotKey(Handle, hotKeyId, Jyh.KeyModifiers.Alt, Keys.V))
            {
                MessageBox.Show("热键注册失败");
            }
            
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
        //窗口关闭
        private void SelectPaste_From_FormClosed(object sender, FormClosedEventArgs e)
        {
            Jyh.UnregisterHotKey(Handle, hotKeyId);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case hotKeyId:    //按下的是Shift+S
                            if (Visible)
                            {
                                HideForm();
                            }
                            else
                            {
                                ShowForm();
                            }
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void SelectPaste_From_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                Hide();
            }
        }

        public void ShowForm()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }
        public void HideForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.notifyIcon1.Visible = true;
            this.Hide();
        }
        //双击显示
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                HideForm();
            }
            else
            {
                ShowForm();
            }
        }

        private void txtboxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData==Keys.Enter)
            {
                btnAdd_Click(sender, e);
            }
        }
    }
}
