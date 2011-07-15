using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace TutorClient
{
    public partial class frmAddServer : Form
    {
        Settings mSettings;

        private string mServerIP;

        public string ServerIP
        {
            get { return mServerIP; }
            set { mServerIP = value; }
        }

        public frmAddServer(Settings prSettings)
        {
            mSettings = prSettings;

            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Determines if the string is a valid IP
        // Source: http://www.dreamincode.net/code/snippet1379.htm
        private bool IsValidIP(string prAddress)
        {
            IPAddress iIP;
            bool iValid = false;
            if (string.IsNullOrEmpty(prAddress))
            {
                iValid = false;
            }
            else
            {
                iValid = IPAddress.TryParse(prAddress, out iIP);
            }
            return iValid;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Make sure the IP is valid
            if (IsValidIP(txtServerIP.Text) && txtServerIP.Text.Length >= 8)
            {
                mServerIP = txtServerIP.Text;
                Close();
            }
            else
            {
                MessageBox.Show("The IP address entered is not in IP v4 format", "Incorrect IP");
            }
        }

    }
}
