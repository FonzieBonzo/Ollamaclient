namespace Ollamaclient
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
            textBoxLog = new TextBox();
            label1 = new Label();
            tbOllamaURL = new TextBox();
            cbALT = new ComboBox();
            cbModel = new ComboBox();
            tbPrompt = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            cbLog = new CheckBox();
            button1 = new Button();
            btnSave = new Button();
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
            label1.Location = new Point(12, 19);
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
            // cbALT
            // 
            cbALT.DropDownStyle = ComboBoxStyle.DropDownList;
            cbALT.FormattingEnabled = true;
            cbALT.Location = new Point(114, 48);
            cbALT.Name = "cbALT";
            cbALT.Size = new Size(674, 23);
            cbALT.TabIndex = 3;
            cbALT.SelectedIndexChanged += cbALT_SelectedIndexChanged;
            // 
            // cbModel
            // 
            cbModel.FormattingEnabled = true;
            cbModel.Location = new Point(114, 135);
            cbModel.Name = "cbModel";
            cbModel.Size = new Size(674, 23);
            cbModel.TabIndex = 4;
            // 
            // tbPrompt
            // 
            tbPrompt.Location = new Point(114, 174);
            tbPrompt.Multiline = true;
            tbPrompt.Name = "tbPrompt";
            tbPrompt.ScrollBars = ScrollBars.Vertical;
            tbPrompt.Size = new Size(675, 105);
            tbPrompt.TabIndex = 5;
            // 
            // label2
            // 
            label2.Location = new Point(12, 48);
            label2.Name = "label2";
            label2.Size = new Size(96, 15);
            label2.TabIndex = 6;
            label2.Text = "Keys :";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.Location = new Point(12, 138);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 7;
            label3.Text = "Model :";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // label4
            // 
            label4.Location = new Point(12, 177);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 8;
            label4.Text = "Prompt :";
            label4.TextAlign = ContentAlignment.TopRight;
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
            // button1
            // 
            button1.Location = new Point(739, 19);
            button1.Name = "button1";
            button1.Size = new Size(50, 23);
            button1.TabIndex = 11;
            button1.Text = "Ok";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(738, 285);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(50, 23);
            btnSave.TabIndex = 12;
            btnSave.Text = "Ok";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(832, 565);
            Controls.Add(btnSave);
            Controls.Add(button1);
            Controls.Add(cbLog);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(tbPrompt);
            Controls.Add(cbModel);
            Controls.Add(cbALT);
            Controls.Add(tbOllamaURL);
            Controls.Add(label1);
            Controls.Add(textBoxLog);
            Name = "Form1";
            Text = "OllamaClient";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxLog;
        private Label label1;
        private TextBox tbOllamaURL;
        private ComboBox cbALT;
        private ComboBox cbModel;
        private TextBox tbPrompt;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private CheckBox cbLog;
        private Button button1;
        private Button btnSave;
    }
}
