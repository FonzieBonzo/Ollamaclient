namespace Ollamaclient
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            textBoxLog = new TextBox();
            label1 = new Label();
            tbOllamaURL = new TextBox();
            cbLog = new CheckBox();
            btnSaveURL = new Button();
            panel1 = new Panel();
            cbResult = new CheckBox();
            label6 = new Label();
            cbRag_index = new ComboBox();
            rag_indexs = new Label();
            btnSave = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            tbPrompt = new TextBox();
            cbModel = new ComboBox();
            cbALT = new ComboBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxLog
            // 
            textBoxLog.Location = new Point(114, 357);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(675, 196);
            textBoxLog.TabIndex = 0;
            // 
            // label1
            // 
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 1;
            label1.Text = "Ollama URL :";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // tbOllamaURL
            // 
            tbOllamaURL.Location = new Point(114, 19);
            tbOllamaURL.Name = "tbOllamaURL";
            tbOllamaURL.Size = new Size(623, 23);
            tbOllamaURL.TabIndex = 2;
            // 
            // cbLog
            // 
            cbLog.AutoSize = true;
            cbLog.CheckAlign = ContentAlignment.MiddleRight;
            cbLog.Location = new Point(62, 359);
            cbLog.Name = "cbLog";
            cbLog.Size = new Size(46, 19);
            cbLog.TabIndex = 10;
            cbLog.Text = "Log";
            cbLog.UseVisualStyleBackColor = true;
            // 
            // btnSaveURL
            // 
            btnSaveURL.Location = new Point(739, 19);
            btnSaveURL.Name = "btnSaveURL";
            btnSaveURL.Size = new Size(50, 23);
            btnSaveURL.TabIndex = 11;
            btnSaveURL.Text = "Ok";
            btnSaveURL.UseVisualStyleBackColor = true;
            btnSaveURL.Click += btnSaveURL_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(cbResult);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(cbRag_index);
            panel1.Controls.Add(rag_indexs);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(tbPrompt);
            panel1.Controls.Add(cbModel);
            panel1.Controls.Add(cbALT);
            panel1.Location = new Point(16, 58);
            panel1.Name = "panel1";
            panel1.Size = new Size(824, 259);
            panel1.TabIndex = 13;
            // 
            // cbResult
            // 
            cbResult.AutoSize = true;
            cbResult.Location = new Point(96, 230);
            cbResult.Name = "cbResult";
            cbResult.Size = new Size(15, 14);
            cbResult.TabIndex = 23;
            cbResult.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.Location = new Point(-5, 230);
            label6.Name = "label6";
            label6.Size = new Size(96, 15);
            label6.TabIndex = 22;
            label6.Text = "Result 2 Log";
            label6.TextAlign = ContentAlignment.TopRight;
            // 
            // cbRag_index
            // 
            cbRag_index.FormattingEnabled = true;
            cbRag_index.Location = new Point(97, 95);
            cbRag_index.Name = "cbRag_index";
            cbRag_index.Size = new Size(674, 23);
            cbRag_index.TabIndex = 21;
            // 
            // rag_indexs
            // 
            rag_indexs.Location = new Point(-5, 98);
            rag_indexs.Name = "rag_indexs";
            rag_indexs.Size = new Size(96, 15);
            rag_indexs.TabIndex = 20;
            rag_indexs.Text = "Rag_indexs";
            rag_indexs.TextAlign = ContentAlignment.TopRight;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(721, 227);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(50, 23);
            btnSave.TabIndex = 19;
            btnSave.Text = "Ok";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label4
            // 
            label4.Location = new Point(-5, 125);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 18;
            label4.Text = "Prompt :";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.Location = new Point(-5, 67);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 17;
            label3.Text = "Model :";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.Location = new Point(-5, 33);
            label2.Name = "label2";
            label2.Size = new Size(96, 15);
            label2.TabIndex = 16;
            label2.Text = "Shortcut keys :";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // tbPrompt
            // 
            tbPrompt.Location = new Point(96, 131);
            tbPrompt.Multiline = true;
            tbPrompt.Name = "tbPrompt";
            tbPrompt.ScrollBars = ScrollBars.Vertical;
            tbPrompt.Size = new Size(675, 83);
            tbPrompt.TabIndex = 15;
            // 
            // cbModel
            // 
            cbModel.FormattingEnabled = true;
            cbModel.Location = new Point(97, 63);
            cbModel.Name = "cbModel";
            cbModel.Size = new Size(674, 23);
            cbModel.TabIndex = 14;
            // 
            // cbALT
            // 
            cbALT.DropDownStyle = ComboBoxStyle.DropDownList;
            cbALT.FormattingEnabled = true;
            cbALT.Location = new Point(97, 30);
            cbALT.Name = "cbALT";
            cbALT.Size = new Size(674, 23);
            cbALT.TabIndex = 13;
            cbALT.SelectedIndexChanged += cbALT_SelectedIndexChanged;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(858, 565);
            Controls.Add(panel1);
            Controls.Add(btnSaveURL);
            Controls.Add(cbLog);
            Controls.Add(tbOllamaURL);
            Controls.Add(label1);
            Controls.Add(textBoxLog);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMain";
            Text = "OllamaClient";
            FormClosing += FormMain_FormClosing;
            FormClosed += Form1_FormClosed;
            Load += FormMain_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxLog;
        private Label label1;
        private TextBox tbOllamaURL;
        private Label label5;
        private CheckBox cbLog;
        private Button btnSaveURL;
        private Panel panel1;
        private Button btnSave;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox tbPrompt;
        private ComboBox cbModel;
        private ComboBox cbALT;
        private ComboBox cbRag_index;
        private Label rag_indexs;
        private Label label6;
        private CheckBox cbResult;
    }
}
