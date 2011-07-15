using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TutorClient
{
    [Serializable()]
    // This is cut down version of the userDetails, containing only the important information
    public class transferrableUserDetails
    {
        private string mUsername;
        private string mPassword;
        private string mDeviceOS;
        private string mUserRole;

        public string Username
        {
            get { return mUsername; }
            set { mUsername = value; }
        }
        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        public string DeviceOS
        {
            get { return mDeviceOS; }
            set { mDeviceOS = value; }
        }
        public string UserRole
        {
            get { return mUserRole; }
            set { mUserRole = value; }
        }
        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {

            info.AddValue("UserName", mUsername);
            info.AddValue("DeviceOS", mDeviceOS);
            info.AddValue("UserRole", mUserRole);
        }
    }

    [Serializable()]
    public class UserManager
    {
        private Object mLock = new Object();

        private Dictionary<int, transferrableUserDetails> mUsersOnline;
        private bool mNewUserDataAvailable;

        private int mStudentsOnline;

        public int StudentsOnline
        {
            get { return mStudentsOnline; }
            set { mStudentsOnline = value; }
        }

        public bool NewUserDataAvailable
        {
            get { return mNewUserDataAvailable; }
            set { mNewUserDataAvailable = value; }
        }

        public Dictionary<int, transferrableUserDetails> UsersOnline
        {
            get { return mUsersOnline; }
            set { mUsersOnline = value; }
        }

        public UserManager()
        {
            mUsersOnline = new Dictionary<int, transferrableUserDetails>();
        }

        // Get number of users online
        public int GetNumOfUsers()
        {
            return mUsersOnline.Count;
        }

        // Gets a new free ID
        public int GetNewUserID(int prExtraIndex)
        {
            if (mUsersOnline.Count == 0)
                return 0;
            else
                return mUsersOnline.Last().Key + 1 + prExtraIndex;
        }
        
        // Remove a user 
        public void RemoveUser(string prUserName)
        {
            // Find the users key
            int iKey = 0;

            foreach (KeyValuePair<int, transferrableUserDetails> iUser in mUsersOnline)
            {
                if (iUser.Value.Username == prUserName)
                {
                    iKey = iUser.Key;
                    break;
                }
            }
            lock (this)
            {
                mUsersOnline.Remove(iKey);
            }
        }

        // Kick all connected users from server
        public void KickUsers()
        {
            // Lock thread to prevent bad things happening
            lock (this)
            {
                mUsersOnline.Clear();
            }
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("UserList", mUsersOnline);
        }

        public void SetStudentsOnline()
        {
            mStudentsOnline = 0;

            foreach (KeyValuePair<int, transferrableUserDetails> iUser in mUsersOnline)
            {
                if (iUser.Value.UserRole == "Student")
                {
                    mStudentsOnline++;
                }
            }
        }
    }
}
