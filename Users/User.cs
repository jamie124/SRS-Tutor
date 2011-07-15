using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TutorClient
{
    class User
    {
        // The "logon" string will be in the format
        // HEADER|USERINFO|DEVICEINFO
        //
        // Examples
        // 0014JAMES.WHITWELL,WIN7;
        // 0014JAMES.WHITWELL,IPHONE;

        const int mHeaderLength = 2;
        const int mMaxBodyLength = 50;

        private string mUserName;
        private string mDevice;
        private char[] mUserData;
        private char[] mData;           // All data
        private int mBodyLength;

        public char[] getData()
        {
            return mData;
        }

        public void setData(char[] prData)
        {
            mData = prData;
        }

        public void bodyLength(int prLength)
        {
            mBodyLength = prLength;
            if (mBodyLength > mMaxBodyLength)
                mBodyLength = mMaxBodyLength;
        }

        public string UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }

        public string Device
        {
            get { return mDevice; }
            set { mDevice = value; }
        }

        public string getUserDevice()
        {
            return mUserName + mDevice;
        }

        public int getBodyLength()
        {
            return mData.Length;
        }

        public void encodeHeader()
        {
            //char[] iHeader = new char[mHeaderLength + 1];
            string iHeader = "U;";
            //iHeader = string.Format("{0:d4}", mBodyLength);
            char[] iHeaderArray = iHeader.ToCharArray();

            System.Array.Copy(iHeaderArray, 0, mData, 0, mHeaderLength);

        }
    }
}
