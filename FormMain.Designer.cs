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
            cbLog = new CheckBox();
            label1 = new Label();
            tbOllamaURL = new TextBox();
            btnSaveURL = new Button();
            cbALT = new ComboBox();
            cbModel = new ComboBox();
            tbPrompt = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnSave = new Button();
            rag_indexs = new Label();
            cbRag_index = new ComboBox();
            label6 = new Label();
            cbResult = new CheckBox();
            panel1 = new Panel();
            tbAsk = new TextBox();
            btnAsk = new Button();
            label5 = new Label();
            labelProcessing = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxLog
            // 
            textBoxLog.Location = new Point(113, 449);
            textBoxLog.Margin = new Padding(10);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(727, 196);
            textBoxLog.TabIndex = 0;
            // 
            // cbLog
            // 
            cbLog.AutoSize = true;
            cbLog.CheckAlign = ContentAlignment.MiddleRight;
            cbLog.Location = new Point(39, 451);
            cbLog.Name = "cbLog";
            cbLog.Size = new Size(46, 19);
            cbLog.TabIndex = 10;
            cbLog.Text = "Log";
            cbLog.UseVisualStyleBackColor = true;
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
            // cbModel
            // 
            cbModel.FormattingEnabled = true;
            cbModel.Location = new Point(97, 63);
            cbModel.Name = "cbModel";
            cbModel.Size = new Size(674, 23);
            cbModel.TabIndex = 14;
            // 
            // tbPrompt
            // 
            tbPrompt.Location = new Point(96, 131);
            tbPrompt.Multiline = true;
            tbPrompt.Name = "tbPrompt";
            tbPrompt.ScrollBars = ScrollBars.Vertical;
            tbPrompt.Size = new Size(675, 62);
            tbPrompt.TabIndex = 15;
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
            // label3
            // 
            label3.Location = new Point(-5, 67);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 17;
            label3.Text = "Model :";
            label3.TextAlign = ContentAlignment.TopRight;
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
            // btnSave
            // 
            btnSave.Location = new Point(722, 206);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(50, 23);
            btnSave.TabIndex = 19;
            btnSave.Text = "Ok";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
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
            // cbRag_index
            // 
            cbRag_index.FormattingEnabled = true;
            cbRag_index.Location = new Point(97, 95);
            cbRag_index.Name = "cbRag_index";
            cbRag_index.Size = new Size(674, 23);
            cbRag_index.TabIndex = 21;
            // 
            // label6
            // 
            label6.Location = new Point(-5, 205);
            label6.Name = "label6";
            label6.Size = new Size(96, 15);
            label6.TabIndex = 22;
            label6.Text = "Result 2 Log";
            label6.TextAlign = ContentAlignment.TopRight;
            // 
            // cbResult
            // 
            cbResult.AutoSize = true;
            cbResult.Location = new Point(97, 206);
            cbResult.Name = "cbResult";
            cbResult.Size = new Size(15, 14);
            cbResult.TabIndex = 23;
            cbResult.UseVisualStyleBackColor = true;
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
            panel1.Size = new Size(824, 247);
            panel1.TabIndex = 13;
            // 
            // tbAsk
            // 
            tbAsk.Location = new Point(113, 318);
            tbAsk.Margin = new Padding(10);
            tbAsk.Multiline = true;
            tbAsk.Name = "tbAsk";
            tbAsk.ScrollBars = ScrollBars.Vertical;
            tbAsk.Size = new Size(563, 87);
            tbAsk.TabIndex = 14;
            // 
            // btnAsk
            // 
            btnAsk.Location = new Point(689, 385);
            btnAsk.Name = "btnAsk";
            btnAsk.Size = new Size(62, 23);
            btnAsk.TabIndex = 15;
            btnAsk.Text = "Ask";
            btnAsk.UseVisualStyleBackColor = true;
            btnAsk.Click += btnAsk_Click;
            // 
            // label5
            // 
            label5.Location = new Point(4, 321);
            label5.Name = "label5";
            label5.Size = new Size(96, 15);
            label5.TabIndex = 18;
            label5.Text = "Question :";
            label5.TextAlign = ContentAlignment.TopRight;
            // 
            // labelProcessing
            // 
            labelProcessing.AutoSize = true;
            labelProcessing.Location = new Point(757, 389);
            labelProcessing.Name = "labelProcessing";
            labelProcessing.Size = new Size(70, 15);
            labelProcessing.TabIndex = 19;
            labelProcessing.Text = "Processing..";
            labelProcessing.Visible = false;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(874, 663);
            Controls.Add(labelProcessing);
            Controls.Add(label5);
            Controls.Add(btnAsk);
            Controls.Add(tbAsk);
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
            SizeChanged += FormMain_SizeChanged;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxLog;
        private Label label5;
        private CheckBox cbLog;
        private Label label1;
        private TextBox tbOllamaURL;
        private Button btnSaveURL;
        private ComboBox cbALT;
        private ComboBox cbModel;
        private TextBox tbPrompt;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnSave;
        private Label rag_indexs;
        private ComboBox cbRag_index;
        private Label label6;
        private CheckBox cbResult;
        private Panel panel1;
        private TextBox tbAsk;
        private Button btnAsk;
        private Label labelProcessing;
    }
}
