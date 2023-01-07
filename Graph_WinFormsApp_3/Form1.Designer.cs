namespace Graph_WinFormsApp_3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_DBEnable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82, 922);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 59);
            this.button1.TabIndex = 1;
            this.button1.Text = "그래프그리기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1279, 922);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(173, 59);
            this.button2.TabIndex = 2;
            this.button2.Text = "그래프정지";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // formsPlot1
            // 
            this.formsPlot1.AutoScroll = true;
            this.formsPlot1.Location = new System.Drawing.Point(34, 12);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(1441, 891);
            this.formsPlot1.TabIndex = 3;
            this.formsPlot1.Load += new System.EventHandler(this.formsPlot1_Load);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(659, 943);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(55, 35);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "10";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(519, 943);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "그래프 범위";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.Location = new System.Drawing.Point(770, 942);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 39);
            this.button3.TabIndex = 6;
            this.button3.Text = "적용";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(720, 946);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 30);
            this.label2.TabIndex = 7;
            this.label2.Text = "[s]";
            // 
            // cb_DBEnable
            // 
            this.cb_DBEnable.AutoSize = true;
            this.cb_DBEnable.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cb_DBEnable.Location = new System.Drawing.Point(958, 939);
            this.cb_DBEnable.Name = "cb_DBEnable";
            this.cb_DBEnable.Size = new System.Drawing.Size(136, 34);
            this.cb_DBEnable.TabIndex = 10;
            this.cb_DBEnable.Text = "DB_Enable";
            this.cb_DBEnable.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1510, 1014);
            this.Controls.Add(this.cb_DBEnable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.formsPlot1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "BeckHoff ADS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button button1;
        private Button button2;
        public ScottPlot.FormsPlot formsPlot1;
        private TextBox textBox1;
        private Label label1;
        private Button button3;
        private Label label2;
        private CheckBox cb_DBEnable;
        //private DevExpress.XtraEditors.CheckButton checkButton1;
        // private DevExpress.XtraEditors.CheckButton checkButton2;
    }
}