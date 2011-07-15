namespace TutorClient
{
    partial class frmQuestionDesigner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cbQuestionTypes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.gbMultiChoice = new System.Windows.Forms.GroupBox();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtField4 = new System.Windows.Forms.TextBox();
            this.txtField3 = new System.Windows.Forms.TextBox();
            this.txtField2 = new System.Windows.Forms.TextBox();
            this.txtField1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTFAnswer = new System.Windows.Forms.TextBox();
            this.gbTrueFalse = new System.Windows.Forms.GroupBox();
            this.gbMultiChoice.SuspendLayout();
            this.gbTrueFalse.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Question Type:";
            // 
            // cbQuestionTypes
            // 
            this.cbQuestionTypes.FormattingEnabled = true;
            this.cbQuestionTypes.Items.AddRange(new object[] {
            "Multi-Choice",
            "Short Answer",
            "True/False",
            "Matching"});
            this.cbQuestionTypes.Location = new System.Drawing.Point(97, 19);
            this.cbQuestionTypes.Name = "cbQuestionTypes";
            this.cbQuestionTypes.Size = new System.Drawing.Size(201, 21);
            this.cbQuestionTypes.TabIndex = 1;
            this.cbQuestionTypes.SelectedIndexChanged += new System.EventHandler(this.cbQuestionTypes_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Question:";
            // 
            // txtQuestion
            // 
            this.txtQuestion.Location = new System.Drawing.Point(97, 54);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(201, 49);
            this.txtQuestion.TabIndex = 3;
            this.txtQuestion.Text = "How many black squares are there on a chess board?";
            // 
            // gbMultiChoice
            // 
            this.gbMultiChoice.Controls.Add(this.txtAnswer);
            this.gbMultiChoice.Controls.Add(this.label8);
            this.gbMultiChoice.Controls.Add(this.label7);
            this.gbMultiChoice.Controls.Add(this.txtField4);
            this.gbMultiChoice.Controls.Add(this.txtField3);
            this.gbMultiChoice.Controls.Add(this.txtField2);
            this.gbMultiChoice.Controls.Add(this.txtField1);
            this.gbMultiChoice.Controls.Add(this.label6);
            this.gbMultiChoice.Controls.Add(this.label5);
            this.gbMultiChoice.Controls.Add(this.label4);
            this.gbMultiChoice.Controls.Add(this.label3);
            this.gbMultiChoice.Location = new System.Drawing.Point(15, 109);
            this.gbMultiChoice.Name = "gbMultiChoice";
            this.gbMultiChoice.Size = new System.Drawing.Size(283, 188);
            this.gbMultiChoice.TabIndex = 4;
            this.gbMultiChoice.TabStop = false;
            this.gbMultiChoice.Text = "Multi-Choice Options";
            // 
            // txtAnswer
            // 
            this.txtAnswer.Location = new System.Drawing.Point(53, 134);
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(224, 20);
            this.txtAnswer.TabIndex = 10;
            this.txtAnswer.Text = "32";
            this.txtAnswer.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Answer:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(171, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "At least two fields must be entered.";
            // 
            // txtField4
            // 
            this.txtField4.Location = new System.Drawing.Point(53, 108);
            this.txtField4.Name = "txtField4";
            this.txtField4.Size = new System.Drawing.Size(224, 20);
            this.txtField4.TabIndex = 7;
            this.txtField4.Text = "50";
            // 
            // txtField3
            // 
            this.txtField3.Location = new System.Drawing.Point(53, 82);
            this.txtField3.Name = "txtField3";
            this.txtField3.Size = new System.Drawing.Size(224, 20);
            this.txtField3.TabIndex = 6;
            this.txtField3.Text = "48";
            // 
            // txtField2
            // 
            this.txtField2.Location = new System.Drawing.Point(53, 56);
            this.txtField2.Name = "txtField2";
            this.txtField2.Size = new System.Drawing.Size(224, 20);
            this.txtField2.TabIndex = 5;
            this.txtField2.Text = "40";
            // 
            // txtField1
            // 
            this.txtField1.Location = new System.Drawing.Point(53, 29);
            this.txtField1.Name = "txtField1";
            this.txtField1.Size = new System.Drawing.Size(224, 20);
            this.txtField1.TabIndex = 4;
            this.txtField1.Text = "32";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Field 4:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Field 3:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Field 2:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Field 1:";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(51, 305);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(98, 23);
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "Create Question";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(155, 305);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Answer:";
            // 
            // txtTFAnswer
            // 
            this.txtTFAnswer.Location = new System.Drawing.Point(53, 29);
            this.txtTFAnswer.Name = "txtTFAnswer";
            this.txtTFAnswer.Size = new System.Drawing.Size(224, 20);
            this.txtTFAnswer.TabIndex = 10;
            this.txtTFAnswer.Text = "True";
            // 
            // gbTrueFalse
            // 
            this.gbTrueFalse.Controls.Add(this.txtTFAnswer);
            this.gbTrueFalse.Controls.Add(this.label9);
            this.gbTrueFalse.Location = new System.Drawing.Point(15, 109);
            this.gbTrueFalse.Name = "gbTrueFalse";
            this.gbTrueFalse.Size = new System.Drawing.Size(283, 72);
            this.gbTrueFalse.TabIndex = 7;
            this.gbTrueFalse.TabStop = false;
            this.gbTrueFalse.Text = "True/False Options";
            // 
            // frmQuestionDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 335);
            this.Controls.Add(this.gbTrueFalse);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.gbMultiChoice);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbQuestionTypes);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(322, 373);
            this.MinimumSize = new System.Drawing.Size(322, 179);
            this.Name = "frmQuestionDesigner";
            this.Text = "Question Designer";
            this.Load += new System.EventHandler(this.frmQuestionDesigner_Load);
            this.gbMultiChoice.ResumeLayout(false);
            this.gbMultiChoice.PerformLayout();
            this.gbTrueFalse.ResumeLayout(false);
            this.gbTrueFalse.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbQuestionTypes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.GroupBox gbMultiChoice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtField4;
        private System.Windows.Forms.TextBox txtField3;
        private System.Windows.Forms.TextBox txtField2;
        private System.Windows.Forms.TextBox txtField1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTFAnswer;
        private System.Windows.Forms.GroupBox gbTrueFalse;
    }
}