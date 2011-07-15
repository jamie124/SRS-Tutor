using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Timers;

namespace TutorClient
{
    public partial class frmMain : Form
    {
        Network mNetwork;
        IOProcessor mIOProcessor;
        QuestionManager mQuestionManager;
        AnswerManager mAnswerManager;
        UserManager mUserManager;
        ChatManager mChatManager;

        Settings mSettings;

        bool mRunning;

        // Timers
        System.Timers.Timer mTimer;
        System.Timers.Timer mQuestionTimer;

        // Forms
        frmQuestionDesigner mQuestionDesigner;
        frmProjector mProjectorDisplay;
        frmLogin mLoginForm;

        // Lock
        private Object mThreadLock = new Object();

        private bool mFormHasLoaded = false;          // Prevents main form going back to login form straight away

        #region Delegates
        // Thread delegates
        delegate void EditUserTVCallback();
        delegate void SetQuestionTimeCallback(string prText);
        delegate void SetResponsesCallback(string prText);

        delegate void ToggleAssignAllCallback(bool prToggle);
        delegate void ToggleAddQuestionCallback(bool prToggle);
        delegate void ToggleDeleteQuestionCallback(bool prToggle);
        delegate void ToggleModifyQuestionCallback(bool prToggle);
        delegate void ToggleShowQuestionsCallback(bool prToggle);
        delegate void ToggleShowAnswersCallback(bool prToggle);
        delegate void ToggleShowAllCallback(bool prToggle);

        delegate void AddChatMessageAllCallback(string prText);

        delegate void ToggleMainWindowCallback(bool prToggle);
        #endregion

        // Callbacks
        #region Callbacks

        private void SetTimerText(string prText)
        {
            if (this.lblTimeRemaining.InvokeRequired)
            {
                SetQuestionTimeCallback t = new SetQuestionTimeCallback(SetTimerText);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(t, new object[] { prText });
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                this.lblTimeRemaining.Text = prText;
            }
        }

        private void SetResponsesText(string prText)
        {
            if (this.lblResponsesReceived.InvokeRequired)
            {
                SetResponsesCallback r = new SetResponsesCallback(SetResponsesText);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(r, new object[] { prText });
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                this.lblResponsesReceived.Text = prText;
            }
        }

        // Clear the user list
        private void ClearUserList()
        {
            if (this.tvUsers.InvokeRequired)
            {
                this.tvUsers.BeginInvoke(
                    new MethodInvoker(
                    delegate() { ClearUserList(); }));
            }
            else
            {
                this.tvUsers.Nodes.Clear();
                this.tvUsers.Nodes.Add("Tutors");
                this.tvUsers.Nodes.Add("Students");
            }
        }

        // Clear the question list
        private void ClearQuestionList()
        {
            if (this.tvQuestions.InvokeRequired)
            {
                this.tvQuestions.BeginInvoke(
                    new MethodInvoker(
                    delegate() { ClearQuestionList(); }));
            }
            else
            {
                this.tvQuestions.Nodes.Clear();
                this.tvQuestions.Nodes.Add("Multi-Choice");
                this.tvQuestions.Nodes.Add("Short Answer");
                this.tvQuestions.Nodes.Add("True/False");
                this.tvQuestions.Nodes.Add("Matching");

            }
        }

        // Add an item to the user list
        private void AddUserNode(transferrableUserDetails prUserDetails)
        {
            if (this.tvUsers.InvokeRequired)
            {
                this.tvUsers.BeginInvoke(
                    new MethodInvoker(
                    delegate() { AddUserNode(prUserDetails); }));
            }
            else
            {
                // Possibly change so positions aren't hardcoded
                if (prUserDetails.UserRole == "Tutor")
                    this.tvUsers.Nodes[0].Nodes.Add(prUserDetails.Username);
                else
                    this.tvUsers.Nodes[1].Nodes.Add(prUserDetails.Username);
                tvUsers.ExpandAll();
            }
        }

        // Add an item to the question list
        private void AddQuestionNode(question prQuestion)
        {
            if (this.tvQuestions.InvokeRequired)
            {
                this.tvQuestions.BeginInvoke(
                    new MethodInvoker(
                    delegate() { AddQuestionNode(prQuestion); }));
            }
            else
            {
                // Possibly change so positions aren't hardcoded
                switch (prQuestion.QuestionType)
                {
                    case "MC":
                        this.tvQuestions.Nodes[0].Nodes.Add(prQuestion.Question);
                        break;
                    case "SA":
                        this.tvQuestions.Nodes[1].Nodes.Add(prQuestion.Question);
                        break;
                    case "TF":
                        this.tvQuestions.Nodes[2].Nodes.Add(prQuestion.Question);
                        break;
                    case "MA":
                        this.tvQuestions.Nodes[3].Nodes.Add(prQuestion.Question);
                        break;
                }
                tvQuestions.ExpandAll();
            }
        }

        private void ToggleAssignAllButton(bool prToggle)
        {
            if (this.btnAssignToAll.InvokeRequired)
            {
                ToggleAssignAllCallback t = new ToggleAssignAllCallback(ToggleAssignAllButton);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(t, new object[] { prToggle });
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                this.btnAssignToAll.Enabled = prToggle;
            }
        }

        private void ToggleAddQuestion(bool prToggle)
        {
            if (this.btnAddQuestion.InvokeRequired)
            {
                ToggleAddQuestionCallback t = new ToggleAddQuestionCallback(ToggleAddQuestion);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(t, new object[] { prToggle });
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                this.btnAddQuestion.Enabled = prToggle;
            }
        }

        private void ToggleDeleteQuestion(bool prToggle)
        {
            if (this.btnDeleteQuestion.InvokeRequired)
            {
                ToggleDeleteQuestionCallback t = new ToggleDeleteQuestionCallback(ToggleDeleteQuestion);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(t, new object[] { prToggle });
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                this.btnDeleteQuestion.Enabled = prToggle;
            }
        }

        private void ToggleModifyQuestion(bool prToggle)
        {
            if (this.btnModifyQuestion.InvokeRequired)
            {
                ToggleModifyQuestionCallback t = new ToggleModifyQuestionCallback(ToggleModifyQuestion);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(t, new object[] { prToggle });
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                this.btnModifyQuestion.Enabled = prToggle;
            }
        }
      

        // Add a new chat message to the all messages box
        private void AddChatMessageAll(string prText)
        {
            if (this.rtbAllMessages.InvokeRequired)
            {
                AddChatMessageAllCallback t = new AddChatMessageAllCallback(AddChatMessageAll);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(t, new object[] { prText });
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                this.rtbAllMessages.Text += prText;
            }
        }

        // Hide the main window
        private void ToggleMainWindow(bool prToggle)
        {
            if (this.InvokeRequired)
            {
                ToggleMainWindowCallback t = new ToggleMainWindowCallback(ToggleMainWindow);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(t, new object[] { prToggle });
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                this.Visible = prToggle;
            }
        }
        #endregion

        public frmMain()
        {
            InitializeComponent();

            //mSettings = new Settings();
            mChatManager = new ChatManager();
            mUserManager = new UserManager();
            mQuestionManager = new QuestionManager();
            mAnswerManager = new AnswerManager(mQuestionManager);
            mIOProcessor = new IOProcessor(mUserManager, mQuestionManager, mAnswerManager);
            mNetwork = new Network(mUserManager, mQuestionManager, mAnswerManager, mIOProcessor, mChatManager);

         
            // Forms
            mProjectorDisplay = new frmProjector(mAnswerManager);
            mLoginForm = new frmLogin(mNetwork, out mSettings);

            CheckConnectionStatus();

            // Display login window

            mLoginForm.ShowDialog();

            StartRunning();

        }

        // Start the clients processes
        private void StartRunning()
        {
            mRunning = true;

            // Timers
            mTimer = new System.Timers.Timer(1000);
            mTimer.Elapsed += new ElapsedEventHandler(TimeElapsed);
            mTimer.Enabled = true;

            // Show the form if the client is reconnecting
            if (this.Visible == false)
            {
                this.ToggleMainWindow(true);
            }
        }

        private void TimeElapsed(object sender, ElapsedEventArgs e)
        {
            if (mRunning)
            {
                if (mUserManager.UsersOnline.Count > 0 && mUserManager.NewUserDataAvailable)
                {
                    ClearUserList();
                    foreach (KeyValuePair<int, transferrableUserDetails> iUserDetails in mUserManager.UsersOnline)
                    {
                        AddUserNode(iUserDetails.Value);
                    }
                    mUserManager.NewUserDataAvailable = false;
                }

                if (mQuestionManager.QuestionList.Count > 0 && mQuestionManager.NewQuestionDataAvailable)
                {
                    ClearQuestionList();
                    foreach (KeyValuePair<int, question> iQuestion in mQuestionManager.QuestionList)
                    {
                        AddQuestionNode(iQuestion.Value);
                    }
                    mQuestionManager.NewQuestionDataAvailable = false;
                }

                // Add chat messages
                if (mChatManager.ChatMessages.Count > 0)
                {
                    ChatMessage iChatMessage = mChatManager.ChatMessages.Dequeue();

                    if (iChatMessage != null)
                    {
                        AddChatMessageAll(iChatMessage.From + ": " + iChatMessage.Message + "\n");
                    }
                }

                CheckConnectionStatus();
            }
        }

        // Called every time the question timer ticks
        private void QuestionTimerTick(object sender, ElapsedEventArgs e)
        {
            int iCurrentTimeElapsed = mAnswerManager.CurrentTimeElapsed;
            int iTotalTimeToAnswer = mAnswerManager.TotalTimeToAnswer;

            if (mRunning)
            {
                if (iCurrentTimeElapsed <= iTotalTimeToAnswer)
                {
                    SetTimerText("Time Remaining: " + (iTotalTimeToAnswer - iCurrentTimeElapsed).ToString() + " seconds");
                    mAnswerManager.CurrentTimeElapsed++;
                }
                else
                {
                    SetTimerText("Time Remaining: Times up");
                    mNetwork.InstructionStringToSend = "I;TIMEUP;";

                    mQuestionTimer.Stop();
                    
                }

                // Stop the current question if all answers have been received
                // TODO: Change students online to students question was sent to
                if (mAnswerManager.AnswerList.Count == mUserManager.StudentsOnline)
                {
                    SetTimerText("All responses received");
                    mNetwork.InstructionStringToSend = "I;TIMEUP;";

                    mQuestionTimer.Stop();
                }

                SetResponsesText("Responses Received: " + mAnswerManager.AnswerList.Count + " of " + mUserManager.StudentsOnline);
            }
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CheckConnectionStatus()
        {
            if (mNetwork.IsConnected)
            {
                tsLblConnectionStatus.Text = "Connected";
                EnableControls();
                mFormHasLoaded = true;
            }
            else
            {
                tsLblConnectionStatus.Text = "Not Connected";
                DisableControls();

                if (mFormHasLoaded == true)
                {
                    mNetwork.UserWasDisconnected = true;
                    ShowLoginForm();
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cbDisplayMode.SelectedIndex = 0;
            cbTime.SelectedIndex = 2;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logout();
        }

        // Logout
        private void Logout()
        {
            mNetwork.StopConnections();
            if (mQuestionTimer != null)
                mQuestionTimer.Stop();
            mRunning = false;
        }

        // Reshow login form
        private void ShowLoginForm()
        {
            mFormHasLoaded = false;
            this.ToggleMainWindow(false);
            mLoginForm = new frmLogin(mNetwork, out mSettings);
            mLoginForm.ShowDialog();

            StartRunning();
        }

        private void RestartNetwork()
        {
            if (!mNetwork.IsConnected || mNetwork == null)
            {
                // Restart the network
                mNetwork = new Network(mUserManager, mQuestionManager, mAnswerManager, mIOProcessor, mChatManager);
            }
        }

        // Enable all controls
        private void EnableControls()
        {
            // Enable buttons
            this.ToggleAssignAllButton(true);
            this.ToggleAddQuestion(true);
        }

        // Disable all controls
        private void DisableControls()
        {
            // Clear the question and users lists
            this.ClearQuestionList();
            this.ClearUserList();

            // Disable buttons
            this.ToggleAssignAllButton(false);
            this.ToggleAddQuestion(false);
            this.ToggleDeleteQuestion(false);
        }

        // Tell the server to assign the selected question to all students
        private void btnAssignToAll_Click(object sender, EventArgs e)
        {
            if (tvQuestions.SelectedNode != null)
            {
                // Count the number of students the question is being sent to
                mUserManager.SetStudentsOnline();

                if (mUserManager.StudentsOnline > 0)
                {
                    // The line below is a instruction string which can be parsed by the server
                    mNetwork.InstructionStringToSend = "I;SEND,QUESTION,\"" + tvQuestions.SelectedNode.Text + "\",ALL;";

                    lblQuestion.Text = "Question: " + tvQuestions.SelectedNode.Text;

                    // Clear the previous answer list
                    if (mAnswerManager.AnswerList.Count > 0)
                    {
                        lock (mThreadLock)
                        {
                            mAnswerManager.AnswerList.Clear();
                        }
                    }

                    // Setup the question
                    mAnswerManager.TotalTimeToAnswer = Convert.ToInt32(cbTime.Text);
                    mAnswerManager.CurrentQuestion = mQuestionManager.GetQuestionByID(mQuestionManager.GetQuestionIndexByQuestionName(tvQuestions.SelectedNode.Text));
                    mAnswerManager.NumberOfStudentsSentQuestion = mUserManager.StudentsOnline;

                    // Start the timer
                    if (mQuestionTimer != null)
                    {
                        if (mQuestionTimer.Enabled == false)
                        {
                            mQuestionTimer = new System.Timers.Timer(1000);
                            mQuestionTimer.Elapsed += new ElapsedEventHandler(QuestionTimerTick);
                            mQuestionTimer.Enabled = true;
                            mAnswerManager.CurrentTimeElapsed = 0;
                        }
                        else
                        {
                            mAnswerManager.CurrentTimeElapsed = 0;
                            mQuestionTimer.Enabled = true;
                        }
                    }
                    else
                    {
                        mQuestionTimer = new System.Timers.Timer(1000);
                        mQuestionTimer.Elapsed += new ElapsedEventHandler(QuestionTimerTick);
                        mQuestionTimer.Enabled = true;
                        mAnswerManager.CurrentTimeElapsed = 0;
                    }
                }
                else
                {
                    MessageBox.Show("There are no students currently online", "No students online");
                }
            }
            else
            {
                MessageBox.Show("Please select a question", "Select Question");
            }
        }

        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            mQuestionDesigner = new frmQuestionDesigner(mQuestionManager, false, "");
            mQuestionDesigner.ShowDialog();
        }

        private void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            if (tvQuestions.SelectedNode != null)
            {
                if (MessageBox.Show("You are about to delete the question \"" + tvQuestions.SelectedNode.Text +
                    "\". Are you sure you want to do this? ", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // The line below is a instruction string which can be parsed by the server
                    mNetwork.InstructionStringToSend = "I;DELETE,\"" + tvQuestions.SelectedNode.Text + "\";";
                }
            }
            else
            {
                MessageBox.Show("Please select a question to delete", "Select Question");
            }
        }

        private void cbDisplayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDisplayMode.SelectedIndex == 1)
            {
                mProjectorDisplay.Show();
            }
            else
            {
                mProjectorDisplay.Hide();
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            // Resize chat box
            gbChat.Width = this.Width - 40;
            gbChat.Height = this.Height - gbUsers.Height - statusStrip1.Height - 75;

            // Move chat textbox and button
            txtMessage.Top = gbChat.Height - txtMessage.Height - 5;
            txtMessage.Width = gbChat.Width - btnSend.Width - 10;

            btnSend.Top = gbChat.Height - btnSend.Height - 3;
            btnSend.Left = txtMessage.Right + 1;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text != "" && txtMessage.Text != "<Type message here>")
            {
                ChatMessage iMessageToSend = new ChatMessage();
                iMessageToSend.From = mSettings.UserName;
                iMessageToSend.Message = txtMessage.Text;
                iMessageToSend.SendTo = "ALL";

                mNetwork.ChatMessageToSend = iMessageToSend;

                txtMessage.Text = "<Type message here>";
            }
        }

        private void rtbAllMessages_TextChanged(object sender, EventArgs e)
        {
            rtbAllMessages.SelectionStart = rtbAllMessages.Text.Length;
            rtbAllMessages.ScrollToCaret();
        }

        private void rtbPrivateMessages_TextChanged(object sender, EventArgs e)
        {
            rtbPrivateMessages.SelectionStart = rtbPrivateMessages.Text.Length;
            rtbPrivateMessages.ScrollToCaret();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout();
            mNetwork.UserWasDisconnected = false; 
            ShowLoginForm();
        }

        private void btnModifyQuestion_Click(object sender, EventArgs e)
        {
            mQuestionDesigner = new frmQuestionDesigner(mQuestionManager, true, 
                tvQuestions.SelectedNode.Text);
            mQuestionDesigner.ShowDialog();
        }

        private void tvQuestions_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void tvQuestions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Check that the selected item is a question
            if (mQuestionManager.IsQuestionInDictionary(tvQuestions.SelectedNode.Text))
            {
                btnModifyQuestion.Enabled = true;
                btnDeleteQuestion.Enabled = true;
            }
            else
            {
                btnModifyQuestion.Enabled = false;
                btnDeleteQuestion.Enabled = false;
            }
        }

        private void txtMessage_Enter(object sender, EventArgs e)
        {
            if (txtMessage.Text == "<Type message here>")
                txtMessage.Text = "";
        }

        private void txtMessage_Leave(object sender, EventArgs e)
        {
            if (txtMessage.Text == "")
                txtMessage.Text = "<Type message here>";
        }
    }
}
