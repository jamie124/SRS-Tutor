namespace TutorClient
{
    partial class frmMain
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
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Tutors");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Students");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Multi-Choice");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Short Answer");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("True/False");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Matching");
            this.gbUsers = new System.Windows.Forms.GroupBox();
            this.tvUsers = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionDetailsTS = new System.Windows.Forms.ToolStripMenuItem();
            this.gbQuestions = new System.Windows.Forms.GroupBox();
            this.btnModifyQuestion = new System.Windows.Forms.Button();
            this.btnDeleteQuestion = new System.Windows.Forms.Button();
            this.btnAddQuestion = new System.Windows.Forms.Button();
            this.tvQuestions = new System.Windows.Forms.TreeView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsLblConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTime = new System.Windows.Forms.ComboBox();
            this.btnAssignToSelected = new System.Windows.Forms.Button();
            this.btnAssignToAll = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblResponsesReceived = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbDisplayMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbChat = new System.Windows.Forms.GroupBox();
            this.gbPrivateMessages = new System.Windows.Forms.GroupBox();
            this.rtbPrivateMessages = new System.Windows.Forms.RichTextBox();
            this.gbAllMessages = new System.Windows.Forms.GroupBox();
            this.rtbAllMessages = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.gbUsers.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gbQuestions.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbChat.SuspendLayout();
            this.gbPrivateMessages.SuspendLayout();
            this.gbAllMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUsers
            // 
            this.gbUsers.Controls.Add(this.tvUsers);
            this.gbUsers.Location = new System.Drawing.Point(12, 27);
            this.gbUsers.Name = "gbUsers";
            this.gbUsers.Size = new System.Drawing.Size(201, 402);
            this.gbUsers.TabIndex = 0;
            this.gbUsers.TabStop = false;
            this.gbUsers.Text = "Users";
            // 
            // tvUsers
            // 
            this.tvUsers.Location = new System.Drawing.Point(6, 19);
            this.tvUsers.Name = "tvUsers";
            treeNode13.Name = "nodeTutors";
            treeNode13.Text = "Tutors";
            treeNode14.Name = "nodeStudents";
            treeNode14.Text = "Students";
            this.tvUsers.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            this.tvUsers.Size = new System.Drawing.Size(189, 377);
            this.tvUsers.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(757, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoutToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(109, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionDetailsTS});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // connectionDetailsTS
            // 
            this.connectionDetailsTS.Name = "connectionDetailsTS";
            this.connectionDetailsTS.Size = new System.Drawing.Size(174, 22);
            this.connectionDetailsTS.Text = "Connection Details";
            // 
            // gbQuestions
            // 
            this.gbQuestions.Controls.Add(this.btnModifyQuestion);
            this.gbQuestions.Controls.Add(this.btnDeleteQuestion);
            this.gbQuestions.Controls.Add(this.btnAddQuestion);
            this.gbQuestions.Controls.Add(this.tvQuestions);
            this.gbQuestions.Location = new System.Drawing.Point(444, 27);
            this.gbQuestions.Name = "gbQuestions";
            this.gbQuestions.Size = new System.Drawing.Size(301, 396);
            this.gbQuestions.TabIndex = 2;
            this.gbQuestions.TabStop = false;
            this.gbQuestions.Text = "Questions";
            // 
            // btnModifyQuestion
            // 
            this.btnModifyQuestion.Enabled = false;
            this.btnModifyQuestion.Location = new System.Drawing.Point(204, 367);
            this.btnModifyQuestion.Name = "btnModifyQuestion";
            this.btnModifyQuestion.Size = new System.Drawing.Size(92, 23);
            this.btnModifyQuestion.TabIndex = 3;
            this.btnModifyQuestion.Text = "Modify Question";
            this.btnModifyQuestion.UseVisualStyleBackColor = true;
            this.btnModifyQuestion.Click += new System.EventHandler(this.btnModifyQuestion_Click);
            // 
            // btnDeleteQuestion
            // 
            this.btnDeleteQuestion.Enabled = false;
            this.btnDeleteQuestion.Location = new System.Drawing.Point(106, 367);
            this.btnDeleteQuestion.Name = "btnDeleteQuestion";
            this.btnDeleteQuestion.Size = new System.Drawing.Size(92, 23);
            this.btnDeleteQuestion.TabIndex = 2;
            this.btnDeleteQuestion.Text = "Delete Question";
            this.btnDeleteQuestion.UseVisualStyleBackColor = true;
            this.btnDeleteQuestion.Click += new System.EventHandler(this.btnDeleteQuestion_Click);
            // 
            // btnAddQuestion
            // 
            this.btnAddQuestion.Enabled = false;
            this.btnAddQuestion.Location = new System.Drawing.Point(8, 367);
            this.btnAddQuestion.Name = "btnAddQuestion";
            this.btnAddQuestion.Size = new System.Drawing.Size(92, 23);
            this.btnAddQuestion.TabIndex = 1;
            this.btnAddQuestion.Text = "Add Question";
            this.btnAddQuestion.UseVisualStyleBackColor = true;
            this.btnAddQuestion.Click += new System.EventHandler(this.btnAddQuestion_Click);
            // 
            // tvQuestions
            // 
            this.tvQuestions.Location = new System.Drawing.Point(6, 19);
            this.tvQuestions.Name = "tvQuestions";
            treeNode15.Name = "nodeMultiChoice";
            treeNode15.Text = "Multi-Choice";
            treeNode16.Name = "nodeShortAnswer";
            treeNode16.Text = "Short Answer";
            treeNode17.Name = "nodeTrueFalse";
            treeNode17.Text = "True/False";
            treeNode18.Name = "nodeMatching";
            treeNode18.Text = "Matching";
            this.tvQuestions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18});
            this.tvQuestions.Size = new System.Drawing.Size(288, 342);
            this.tvQuestions.TabIndex = 0;
            this.tvQuestions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvQuestions_AfterSelect);
            this.tvQuestions.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvQuestions_MouseClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLblConnectionStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 711);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(757, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsLblConnectionStatus
            // 
            this.tsLblConnectionStatus.Name = "tsLblConnectionStatus";
            this.tsLblConnectionStatus.Size = new System.Drawing.Size(88, 17);
            this.tsLblConnectionStatus.Text = "Not Connected";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cbTime);
            this.groupBox3.Controls.Add(this.btnAssignToSelected);
            this.groupBox3.Controls.Add(this.btnAssignToAll);
            this.groupBox3.Location = new System.Drawing.Point(219, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(219, 97);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Question Assignment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time (Seconds)";
            // 
            // cbTime
            // 
            this.cbTime.FormattingEnabled = true;
            this.cbTime.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "100",
            "200",
            "500",
            "1000"});
            this.cbTime.Location = new System.Drawing.Point(111, 49);
            this.cbTime.Name = "cbTime";
            this.cbTime.Size = new System.Drawing.Size(105, 21);
            this.cbTime.TabIndex = 2;
            this.cbTime.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnAssignToSelected
            // 
            this.btnAssignToSelected.Enabled = false;
            this.btnAssignToSelected.Location = new System.Drawing.Point(7, 49);
            this.btnAssignToSelected.Name = "btnAssignToSelected";
            this.btnAssignToSelected.Size = new System.Drawing.Size(95, 23);
            this.btnAssignToSelected.TabIndex = 1;
            this.btnAssignToSelected.Text = "Assign To Selected";
            this.btnAssignToSelected.UseVisualStyleBackColor = true;
            // 
            // btnAssignToAll
            // 
            this.btnAssignToAll.Enabled = false;
            this.btnAssignToAll.Location = new System.Drawing.Point(7, 20);
            this.btnAssignToAll.Name = "btnAssignToAll";
            this.btnAssignToAll.Size = new System.Drawing.Size(95, 23);
            this.btnAssignToAll.TabIndex = 0;
            this.btnAssignToAll.Text = "Assign To All";
            this.btnAssignToAll.UseVisualStyleBackColor = true;
            this.btnAssignToAll.Click += new System.EventHandler(this.btnAssignToAll_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblResponsesReceived);
            this.groupBox4.Controls.Add(this.lblQuestion);
            this.groupBox4.Controls.Add(this.lblTimeRemaining);
            this.groupBox4.Location = new System.Drawing.Point(219, 130);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(219, 111);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Question Responses";
            // 
            // lblResponsesReceived
            // 
            this.lblResponsesReceived.AutoSize = true;
            this.lblResponsesReceived.Location = new System.Drawing.Point(6, 65);
            this.lblResponsesReceived.Name = "lblResponsesReceived";
            this.lblResponsesReceived.Size = new System.Drawing.Size(0, 13);
            this.lblResponsesReceived.TabIndex = 2;
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Location = new System.Drawing.Point(6, 20);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(0, 13);
            this.lblQuestion.TabIndex = 1;
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.AutoSize = true;
            this.lblTimeRemaining.Location = new System.Drawing.Point(6, 42);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(0, 13);
            this.lblTimeRemaining.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbDisplayMode);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(219, 247);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(219, 176);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Display Controls";
            // 
            // cbDisplayMode
            // 
            this.cbDisplayMode.FormattingEnabled = true;
            this.cbDisplayMode.Items.AddRange(new object[] {
            "No Projector",
            "To Data Projector"});
            this.cbDisplayMode.Location = new System.Drawing.Point(56, 23);
            this.cbDisplayMode.Name = "cbDisplayMode";
            this.cbDisplayMode.Size = new System.Drawing.Size(156, 21);
            this.cbDisplayMode.TabIndex = 1;
            this.cbDisplayMode.SelectedIndexChanged += new System.EventHandler(this.cbDisplayMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Display:";
            // 
            // gbChat
            // 
            this.gbChat.Controls.Add(this.gbPrivateMessages);
            this.gbChat.Controls.Add(this.gbAllMessages);
            this.gbChat.Controls.Add(this.btnSend);
            this.gbChat.Controls.Add(this.txtMessage);
            this.gbChat.Location = new System.Drawing.Point(12, 435);
            this.gbChat.Name = "gbChat";
            this.gbChat.Size = new System.Drawing.Size(733, 273);
            this.gbChat.TabIndex = 7;
            this.gbChat.TabStop = false;
            this.gbChat.Text = "Chat";
            // 
            // gbPrivateMessages
            // 
            this.gbPrivateMessages.Controls.Add(this.rtbPrivateMessages);
            this.gbPrivateMessages.Location = new System.Drawing.Point(367, 19);
            this.gbPrivateMessages.Name = "gbPrivateMessages";
            this.gbPrivateMessages.Size = new System.Drawing.Size(365, 222);
            this.gbPrivateMessages.TabIndex = 5;
            this.gbPrivateMessages.TabStop = false;
            this.gbPrivateMessages.Text = "Private Messages";
            // 
            // rtbPrivateMessages
            // 
            this.rtbPrivateMessages.Location = new System.Drawing.Point(6, 19);
            this.rtbPrivateMessages.Name = "rtbPrivateMessages";
            this.rtbPrivateMessages.ReadOnly = true;
            this.rtbPrivateMessages.Size = new System.Drawing.Size(353, 197);
            this.rtbPrivateMessages.TabIndex = 0;
            this.rtbPrivateMessages.Text = "";
            this.rtbPrivateMessages.TextChanged += new System.EventHandler(this.rtbPrivateMessages_TextChanged);
            // 
            // gbAllMessages
            // 
            this.gbAllMessages.Controls.Add(this.rtbAllMessages);
            this.gbAllMessages.Location = new System.Drawing.Point(0, 19);
            this.gbAllMessages.Name = "gbAllMessages";
            this.gbAllMessages.Size = new System.Drawing.Size(365, 222);
            this.gbAllMessages.TabIndex = 4;
            this.gbAllMessages.TabStop = false;
            this.gbAllMessages.Text = "All Messages";
            // 
            // rtbAllMessages
            // 
            this.rtbAllMessages.Location = new System.Drawing.Point(6, 19);
            this.rtbAllMessages.Name = "rtbAllMessages";
            this.rtbAllMessages.ReadOnly = true;
            this.rtbAllMessages.Size = new System.Drawing.Size(353, 197);
            this.rtbAllMessages.TabIndex = 0;
            this.rtbAllMessages.Text = "";
            this.rtbAllMessages.TextChanged += new System.EventHandler(this.rtbAllMessages_TextChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(651, 245);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(6, 247);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(639, 20);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.Text = "<Type message here>";
            this.txtMessage.Enter += new System.EventHandler(this.txtMessage_Enter);
            this.txtMessage.Leave += new System.EventHandler(this.txtMessage_Leave);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 733);
            this.Controls.Add(this.gbChat);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbQuestions);
            this.Controls.Add(this.gbUsers);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(773, 758);
            this.Name = "frmMain";
            this.Text = "Tutor Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.gbUsers.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbQuestions.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbChat.ResumeLayout(false);
            this.gbChat.PerformLayout();
            this.gbPrivateMessages.ResumeLayout(false);
            this.gbAllMessages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUsers;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TreeView tvUsers;
        private System.Windows.Forms.GroupBox gbQuestions;
        private System.Windows.Forms.TreeView tvQuestions;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnModifyQuestion;
        private System.Windows.Forms.Button btnDeleteQuestion;
        private System.Windows.Forms.Button btnAddQuestion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAssignToSelected;
        private System.Windows.Forms.Button btnAssignToAll;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDisplayMode;
        private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.ToolStripStatusLabel tsLblConnectionStatus;
        private System.Windows.Forms.Label lblResponsesReceived;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionDetailsTS;
        private System.Windows.Forms.GroupBox gbChat;
        private System.Windows.Forms.GroupBox gbPrivateMessages;
        private System.Windows.Forms.RichTextBox rtbPrivateMessages;
        private System.Windows.Forms.GroupBox gbAllMessages;
        private System.Windows.Forms.RichTextBox rtbAllMessages;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ComboBox cbTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}

