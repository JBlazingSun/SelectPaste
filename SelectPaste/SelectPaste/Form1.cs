using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpCommonDll;
using Newtonsoft.Json;
using SelectPaste.Service;
using Timer = System.Windows.Forms.Timer;

namespace SelectPaste
{
    public partial class SelectPaste_From : Form
    {
        private string FilePath = "FilePath";
        static Jyh jyh = Jyh.GetInstance();
        private const int hotKeyId = 100;
        private static string passwdPath;
        public SelectPaste_From()
        {
            InitializeComponent();

        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            passwdPath = jyh.OpenFile("*.txt");
            FileInfo fi = new FileInfo(passwdPath);
            if (fi.Extension != ".txt")
            {
                MessageBox.Show("请选择txt文件");
                return;
            }
            var signal=jyh.WriteRegister(FilePath, FilePath, FilePath, passwdPath);
            if (!signal)
            {
                MessageBox.Show("写入注册表失败");
                return;
            }
            txtboxFilePath.Text = passwdPath;
            ReloadList(ReadValue(passwdPath));

        }
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        //运行线程
        public void DoWork(object sender, DoWorkEventArgs eventArgs)
        {
            while (true)
            {
                backgroundWorker1.ReportProgress(0, sender);
                Thread.Sleep(15*1000);
            }
        }
        //统一处理业务
        private void ProgressChanged(object o, ProgressChangedEventArgs eventArgs)
        {
            var setTime = new SetTime();
            var unixBeijingTime = setTime.GetUnixBeijingTime();
            setTime.SetTimeFunc(unixBeijingTime);
        }

        //窗口加载
        private void SelectPaste_From_Load(object sender, EventArgs e)
        {

            //同步时间
            //            Task.Factory.StartNew(TimeTask.TimeTaskThread);
            
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += DoWork;
            backgroundWorker1.ProgressChanged += ProgressChanged;

            backgroundWorker1.RunWorkerAsync();

            //热键
            if (!Jyh.RegisterHotKey(Handle, hotKeyId,  Jyh.KeyModifiers.Shift, Keys.V))
            {
                MessageBox.Show("热键注册失败");
            }
            
            var regInfo=jyh.GetRegister(FilePath, FilePath);
            if (regInfo.Count>0 && File.Exists(regInfo.First(r => r.Key==FilePath).Value))
            {
                txtboxFilePath.Text = regInfo.First(r => r.Key == FilePath).Value;
                passwdPath = txtboxFilePath.Text;
                ReloadList(ReadValue(txtboxFilePath.Text));
            }
            else
            {
                SelectFile_Click(sender, e);
            }
            HideForm();
        }
        //添加一个
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxValue.Text))
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
            var StrList=File.ReadAllText(filePath);
            var deStrList = JsonConvert.DeserializeObject<List<string>>(StrList);
            if (deStrList == null)
            {
                deStrList = new List<string>();
            }
            return deStrList;
        }
        //重载列表
        public void ReloadList(List<string> values)
        {
            listBox1.Items.Clear();
            if (values!=null)
            {
                foreach (var value in values)
                {
                    listBox1.Items.Add(value);
                }
            }
        }
        //写入文件
        public void WriteValue(string valueText)
        {
            var passwdList = ReadValue(passwdPath);
            if (passwdList.Contains(valueText))
            {
                return;
            }
            passwdList.Add(valueText);
            var toJson = JsonConvert.SerializeObject(passwdList);
            jyh.WriteTxt(passwdPath,toJson);
        }
        //删除
        public void DeleteValue(string filePath,string valueText, string passwordIV = "blazings")
        {
            var passwdList = ReadValue(passwdPath);
            for (int i = passwdList.Count - 1; i >= 0; i--)
            {
                if (valueText.Equals(passwdList[i]))
                {
                    passwdList.RemoveAt(i);
                }
            }
            var toJson = JsonConvert.SerializeObject(passwdList);
            jyh.WriteTxt(passwdPath, toJson);
        }
        //单击复制
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
                        case hotKeyId:    
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

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouseE = e;
            //右键
            if (mouseE.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show();
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
