namespace WindowsFormsApplication2
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.url = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.Sel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dmtotab = new System.Windows.Forms.Button();
            this.FrozenColumns = new System.Windows.Forms.TextBox();
            this.tab1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tab2 = new System.Windows.Forms.Button();
            this.TabP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CreateT = new System.Windows.Forms.Button();
            this.tabname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabstr = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.SelQ = new System.Windows.Forms.Button();
            this.SelC = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.Sel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(268, 622);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "tab";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // url
            // 
            this.url.Location = new System.Drawing.Point(11, 622);
            this.url.Multiline = true;
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(251, 35);
            this.url.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.Sel);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tabstr);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkedListBox2);
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Controls.Add(this.url);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1529, 669);
            this.panel1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(395, 622);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 35);
            this.button2.TabIndex = 20;
            this.button2.Text = "Sel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Sel
            // 
            this.Sel.Controls.Add(this.SelC);
            this.Sel.Controls.Add(this.SelQ);
            this.Sel.Location = new System.Drawing.Point(482, 32);
            this.Sel.Name = "Sel";
            this.Sel.Size = new System.Drawing.Size(91, 584);
            this.Sel.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dmtotab);
            this.panel2.Controls.Add(this.FrozenColumns);
            this.panel2.Controls.Add(this.tab1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tab2);
            this.panel2.Controls.Add(this.TabP);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.CreateT);
            this.panel2.Controls.Add(this.tabname);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(481, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(93, 587);
            this.panel2.TabIndex = 18;
            // 
            // dmtotab
            // 
            this.dmtotab.Location = new System.Drawing.Point(0, 3);
            this.dmtotab.Name = "dmtotab";
            this.dmtotab.Size = new System.Drawing.Size(93, 60);
            this.dmtotab.TabIndex = 10;
            this.dmtotab.Text = "添加";
            this.dmtotab.UseVisualStyleBackColor = true;
            this.dmtotab.Click += new System.EventHandler(this.dmtotab_Click);
            // 
            // FrozenColumns
            // 
            this.FrozenColumns.Location = new System.Drawing.Point(-1, 400);
            this.FrozenColumns.Multiline = true;
            this.FrozenColumns.Name = "FrozenColumns";
            this.FrozenColumns.Size = new System.Drawing.Size(93, 73);
            this.FrozenColumns.TabIndex = 17;
            // 
            // tab1
            // 
            this.tab1.Location = new System.Drawing.Point(0, 69);
            this.tab1.Name = "tab1";
            this.tab1.Size = new System.Drawing.Size(93, 58);
            this.tab1.TabIndex = 3;
            this.tab1.Text = "去除";
            this.tab1.UseVisualStyleBackColor = true;
            this.tab1.Click += new System.EventHandler(this.tab_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-4, 382);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "FrozenColumns";
            // 
            // tab2
            // 
            this.tab2.Location = new System.Drawing.Point(-1, 227);
            this.tab2.Name = "tab2";
            this.tab2.Size = new System.Drawing.Size(93, 58);
            this.tab2.TabIndex = 4;
            this.tab2.Text = "添加父节点";
            this.tab2.UseVisualStyleBackColor = true;
            this.tab2.Click += new System.EventHandler(this.tab2_Click);
            // 
            // TabP
            // 
            this.TabP.Location = new System.Drawing.Point(-1, 148);
            this.TabP.Multiline = true;
            this.TabP.Name = "TabP";
            this.TabP.Size = new System.Drawing.Size(93, 73);
            this.TabP.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-1, 288);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "TableName";
            // 
            // CreateT
            // 
            this.CreateT.Location = new System.Drawing.Point(-1, 504);
            this.CreateT.Name = "CreateT";
            this.CreateT.Size = new System.Drawing.Size(93, 58);
            this.CreateT.TabIndex = 11;
            this.CreateT.Text = "CreateT";
            this.CreateT.UseVisualStyleBackColor = true;
            this.CreateT.Click += new System.EventHandler(this.CreateT_Click);
            // 
            // tabname
            // 
            this.tabname.Location = new System.Drawing.Point(-1, 306);
            this.tabname.Multiline = true;
            this.tabname.Name = "tabname";
            this.tabname.Size = new System.Drawing.Size(93, 73);
            this.tabname.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-1, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "父节点名称";
            // 
            // tabstr
            // 
            this.tabstr.Location = new System.Drawing.Point(1085, 39);
            this.tabstr.Multiline = true;
            this.tabstr.Name = "tabstr";
            this.tabstr.Size = new System.Drawing.Size(441, 587);
            this.tabstr.TabIndex = 15;
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(579, 42);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(500, 587);
            this.treeView1.TabIndex = 9;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "measure";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "dimension";
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.CheckOnClick = true;
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(11, 232);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(465, 384);
            this.checkedListBox2.TabIndex = 5;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(11, 39);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(465, 184);
            this.checkedListBox1.TabIndex = 2;
            // 
            // SelQ
            // 
            this.SelQ.Location = new System.Drawing.Point(4, 11);
            this.SelQ.Name = "SelQ";
            this.SelQ.Size = new System.Drawing.Size(75, 59);
            this.SelQ.TabIndex = 0;
            this.SelQ.Text = "去除";
            this.SelQ.UseVisualStyleBackColor = true;
            this.SelQ.Click += new System.EventHandler(this.SelQ_Click);
            // 
            // SelC
            // 
            this.SelC.Location = new System.Drawing.Point(4, 482);
            this.SelC.Name = "SelC";
            this.SelC.Size = new System.Drawing.Size(75, 58);
            this.SelC.TabIndex = 1;
            this.SelC.Text = "Creat";
            this.SelC.UseVisualStyleBackColor = true;
            this.SelC.Click += new System.EventHandler(this.SelC_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1542, 683);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Sel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox url;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button tab1;
        private System.Windows.Forms.Button tab2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.TextBox TabP;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button dmtotab;
        private System.Windows.Forms.Button CreateT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tabname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tabstr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox FrozenColumns;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel Sel;
        private System.Windows.Forms.Button SelQ;
        private System.Windows.Forms.Button SelC;
    }
}

