﻿namespace SelectPaste
{
    partial class SelectPaste_From
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtboxValue = new System.Windows.Forms.TextBox();
            this.txtboxFilePath = new System.Windows.Forms.TextBox();
            this.SelectFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] {
            "11",
            "22",
            "33",
            "44",
            "55"});
            this.listBox1.Location = new System.Drawing.Point(12, 50);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(353, 316);
            this.listBox1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(290, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // txtboxValue
            // 
            this.txtboxValue.Location = new System.Drawing.Point(12, 12);
            this.txtboxValue.Name = "txtboxValue";
            this.txtboxValue.Size = new System.Drawing.Size(272, 21);
            this.txtboxValue.TabIndex = 2;
            // 
            // txtboxFilePath
            // 
            this.txtboxFilePath.Location = new System.Drawing.Point(12, 383);
            this.txtboxFilePath.Name = "txtboxFilePath";
            this.txtboxFilePath.Size = new System.Drawing.Size(272, 21);
            this.txtboxFilePath.TabIndex = 3;
            // 
            // SelectFile
            // 
            this.SelectFile.Location = new System.Drawing.Point(290, 381);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(75, 23);
            this.SelectFile.TabIndex = 4;
            this.SelectFile.Text = "浏览";
            this.SelectFile.UseVisualStyleBackColor = true;
            // 
            // SelectPaste_From
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 427);
            this.Controls.Add(this.SelectFile);
            this.Controls.Add(this.txtboxFilePath);
            this.Controls.Add(this.txtboxValue);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listBox1);
            this.Name = "SelectPaste_From";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectPaste";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtboxValue;
        private System.Windows.Forms.TextBox txtboxFilePath;
        private System.Windows.Forms.Button SelectFile;
    }
}

