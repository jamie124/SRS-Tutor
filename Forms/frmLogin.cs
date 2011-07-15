using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TutorClient
{
    public partial class frmLogin : Form
    {
        Network mNetwork;
        XmlHandler mXmlHandler;
        frmAddServer mAddServerForm;
        Thread mConnectionResponseThread;
        Settings mSettings;
        string mIPAddress;                  // Currently selected IP address

        #region Delegates
        
        delegate string GetServerValueCallback();
        delegate void SetConnectionStatusCallback(string prText);
        delegate void CloseWindowCallback();

        delegate void HideLoginWindowCallback();
        #endregion

        #region Callbacks

        // Doesn't work properly
        private string GetServerSelectedItem()
        {
            if (this.cbServers.InvokeRequired)
            {
                GetServerValueCallback c = new GetServerValueCallback(GetServerSelectedItem);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(c, new object[] { });
                    }
                    catch (System.Exception ex)
                    {
                    }
                }
            }
            else
            {
                return this.cbServers.Text;
            }
            return "";
        }

        private void SetConnectionStatus(string prText)
        {
            if (this.lblConnectionStatus.InvokeRequired)
            {
                SetConnectionStatusCallback r = new SetConnectionStatusCallback(SetConnectionStatus);
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
                this.lblConnectionStatus.Text = prText;
            }
        }

        private void CloseWindow()
        {
            if (this.InvokeRequired)
            {
                CloseWindowCallback r = new CloseWindowCallback(CloseWindow);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(r, new object[] { });
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                this.Close();
            }
        }

        private void HideWindow()
        {
            if (this.InvokeRequired)
            {
                HideLoginWindowCallback r = new HideLoginWindowCallback(HideWindow);
                if (!this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(r, new object[] { });
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                this.Hide();
            }
        }
        #endregion

        public frmLogin(Network prNetwork, out Settings prSettings)
        {
            mSettings = new Settings();
            mNetwork = prNetwork;
            mXmlHandler = new XmlHandler();

            mAddServerForm = new frmAddServer(mSettings);

            InitializeComponent();

            // Attempt to load settings from file
            if (!mXmlHandler.LoadUserSettings("tutorSettings.xml", out mSettings))
            {
                // If none were found create a new settings file
                mSettings.UserName = txtUsername.Text;
                
                mXmlHandler.SaveUserSettings(mSettings); 
            }

            txtUsername.Text = mSettings.UserName;
           
            foreach (string iIPAddress in mSettings.ServerIPs)
            {
                cbServers.Items.Add(iIPAddress);
            }

            prSettings = mSettings;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mNetwork.AuthReceived = true;
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            mNetwork.ConnectionResponse = "";
            mNetwork.AuthReceived = false;

            // Set the IP address if it isn't already
            mIPAddress = cbServers.Text;

            StartConnectionThread();
        }

        private void StartConnecting()
        {
            int iConnectionTime = 0;    // Time spent connecting

            this.SetConnectionStatus("Connecting...");

            mNetwork.ShouldStopThread = false;

            // Encrypt the password and send the login data
            if (mNetwork.ConnectToServer(txtUsername.Text, mXmlHandler.Encrypt(txtPassword.Text, "P@ssword1"), mIPAddress, 8000))
            {
                //AttemptLogin(mSettings.UserName, mSettings.ServerIP, Convert.ToInt32(mSettings.ServerPort));

                while (!mNetwork.AuthReceived)
                {
                    // Wait for connection confirmation
                    if (mNetwork.ConnectionResponse == "TUTORCONNECTED")
                    {
                        mNetwork.AuthReceived = true;
                        this.CloseWindow();
                    }
                    else if (mNetwork.ConnectionResponse == "USERNAMETAKEN")
                    {
                        this.SetConnectionStatus("The username chosen is already in use.");
                        mNetwork.AuthReceived = true;
                        mNetwork.ConnectionResponse = "";
                        //mNetwork.StopConnections();     // Disconnect the logged in user
                    }
                    else if (mNetwork.ConnectionResponse == "STUDENTCONNECTED")
                    {
                        this.SetConnectionStatus("Username is not flagged as a tutor");
                        mNetwork.AuthReceived = true;
                        mNetwork.ConnectionResponse = "";
                        mNetwork.StopConnections();     // Disconnect the logged in user
                    }
                    else if (mNetwork.ConnectionResponse == "INCORRECTPASS")
                    {
                        this.SetConnectionStatus("Incorrect password");
                        mNetwork.AuthReceived = true;
                        mNetwork.ConnectionResponse = "";
                        mNetwork.StopConnections();     // Disconnect the logged in user
                    }  
                    // If too much time has been spent waiting assume connection has failed
                    else if (iConnectionTime > 100)
                    {
                        this.SetConnectionStatus("Please try again.");
                        mNetwork.AuthReceived = true;
                        mNetwork.ConnectionResponse = "";
                        mNetwork.StopConnections();     // Disconnect the logged in user
                    }
                    else
                    {
                        mNetwork.AuthReceived = false;
                        mNetwork.ConnectionResponse = "";
                        iConnectionTime++;
                    }
                    Thread.Sleep(1);
                }
            }
        }

        private void StartConnectionThread()
        {
            mConnectionResponseThread = new Thread(new ThreadStart(StartConnecting));
            mConnectionResponseThread.Start();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            mIPAddress = "";
            if (cbServers.Items.Count > 0)
            {
                cbServers.SelectedIndex = 0;
            }
        }

        private void btnAddServer_Click(object sender, EventArgs e)
        {
            mAddServerForm.ShowDialog();
            if (mAddServerForm.ServerIP != null)
                cbServers.Items.Add(mAddServerForm.ServerIP);
        }

        private void frmLogin_Paint(object sender, PaintEventArgs e)
        {
            // This is probably a dodgy place to put this, however couldn't find anything better
            if (mNetwork.UserWasDisconnected == true)
            {
                lblConnectionStatus.Text = "You have been disconnected due to server or network issues.";
                mNetwork.UserWasDisconnected = false;
            }
        }

        private void cbServers_SelectedValueChanged(object sender, EventArgs e)
        {
            mIPAddress = cbServers.Text;
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            int iCurrIP = 0;

            mSettings.UserName = txtUsername.Text;
            mSettings.ServerIPs = new string[cbServers.Items.Count];

            // Get the IPs from the combobox
            foreach (string iIPAddress in cbServers.Items)
            {
                mSettings.ServerIPs[iCurrIP] = iIPAddress;
                iCurrIP++;
            }

            mXmlHandler.SaveUserSettings(mSettings);
        }
    }
}
