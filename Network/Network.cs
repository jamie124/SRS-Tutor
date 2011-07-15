using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace TutorClient
{
    public class Network
    {
        const int mPort = 8000;
        //const string mIPAddress = "127.0.0.1";
        //const string mIPAddress = "192.168.1.64";
        const string mIPAddress = "192.168.1.112";
        //const string mIPAddress = "172.19.20.98";

        private bool mUserWasDisconnected;              // User was disconnected due to server/network crash
        private bool mIsConnected;
        private bool mAuthReceived;                     // Authentication response received
        private bool mLoggedIn;
        private string mConnectionResponse;
        private string mInstructionStringToSend;        // Command message to send
        private ChatMessage mChatMessageToSend;

        public bool UserWasDisconnected
        {
            get { return mUserWasDisconnected; }
            set { mUserWasDisconnected = value; }
        }

        public string InstructionStringToSend
        {
            get { return mInstructionStringToSend; }
            set { mInstructionStringToSend = value; }
        }

        public ChatMessage ChatMessageToSend
        {
            get { return mChatMessageToSend; }
            set { mChatMessageToSend = value; }
        }
        public bool IsConnected
        {
            get { return mIsConnected; }
            set { mIsConnected = value; }
        }
        public bool AuthReceived
        {
            get { return mAuthReceived; }
            set { mAuthReceived = value; }
        }
        public string ConnectionResponse
        {
            get { return mConnectionResponse; }
            set { mConnectionResponse = value; }
        }
        public bool LoggedIn
        {
            get { return mLoggedIn; }
            set { mLoggedIn = value; }
        }
        public int Port
        {
            get { return mPort; }
        }
        public Thread ServerThread
        {
            get { return mInputThread; }
            set { mInputThread = value; }
        }
        public TcpClient Client
        {
            get { return mClient; }
            set { mClient = value; }
        }

        private Object mThreadLock = new Object();

        private TcpListener mListener;

        private Thread mOutputThread;
        private Thread mInputThread;

        TcpClient mClient;

        NetworkStream mStream;

        User mTutor;
        transferrableUserDetails mNewUser;

        IOProcessor mIOProcessor;
        QuestionManager mQuestionManager;
        UserManager mUserManager;
        AnswerManager mAnswerManager;
        ChatManager mChatManager;

        // UserManager string
        string mUserManagerString = "";

        private static bool IsConnectionSuccessful = false;
        private static Exception socketexception;
        private static ManualResetEvent TimeoutObject = new ManualResetEvent(false);

        private bool mShouldStopThread;

        public bool ShouldStopThread
        {
            get { return mShouldStopThread; }
            set { mShouldStopThread = value; }
        }

        public void StopConnections()
        {
            try
            {
                mShouldStopThread = true;

                if (mInputThread != null)
                    if (mInputThread.IsAlive)
                        mInputThread.Join(1000);

                if (mOutputThread != null)
                    if (mOutputThread.IsAlive)
                        mOutputThread.Join(1000);

                if (mListener != null)
                {
                    mListener.Stop();
                    mListener = null;
                }

                if (mStream != null)
                    mStream.Close();
                mClient.Close();
            }
            catch (System.Exception ex)
            {
            }
        }

        public Network(UserManager prUserManager, QuestionManager prQuestionManager, 
            AnswerManager prAnswerManager, IOProcessor prIOProcessor, ChatManager prChatManager)
        {
            mIOProcessor = prIOProcessor;
            mQuestionManager = prQuestionManager;
            mUserManager = prUserManager;
            mAnswerManager = prAnswerManager;
            mChatManager = prChatManager;
            mTutor = new User();

        }

        // Unneeded I think?
        //// Join char arrays together
        //public static char[] combineArrays(char[] prHeader, char[] prBody)
        //{
        //    ChatMessage iTemp = new ChatMessage();
        //    int iLength = prBody.Length + prHeader.Length;
        //    int iMaxHeaderLen = iTemp.getMaxHeaderLength();
        //    int iMaxBodyLen = iTemp.getMaxBodyLength();
        //    int iCount = 0;

        //    char[] iCombined = new char[iMaxHeaderLen + iMaxBodyLen];


        //    for (int h = 0; h < prHeader.Length; h++)
        //    {
        //        iCombined[iCount] = prHeader[h];
        //        iCount++;
        //    }

        //    for (int b = 0; b < prBody.Length; b++)
        //    {
        //        iCombined[iCount] = prBody[b];
        //        iCount++;
        //    }

        //    return iCombined;
        //}

        // TODO: Implement decent connection checking
        public bool ConnectToServer(string prUsername, string prPassword, string prIPAddress, int prPort)
        {
            mLoggedIn = false;

            // Try to connect
            try
            {
                mClient = new TcpClient(prIPAddress, prPort);
            }
            catch (System.Exception ex)
            {
                mClient = null;
                return false;
            }

            if (mClient != null)
            {
                if (mClient.Connected)
                {
                    mStream = mClient.GetStream();
                    mIsConnected = true;
                }
                else
                {
                    mIsConnected = false;
                    return false;
                }

                if (mIsConnected && !mLoggedIn)
                {
                    TutorLogin(prUsername, prPassword);
                }
            }
            else 
            {
                mIsConnected = false;
                return false;
            }

            mInputThread = new Thread(new ThreadStart(Listen));
            mInputThread.Start();

            mOutputThread = new Thread(new ThreadStart(SendOutput));
            mOutputThread.Start();
            return true;
        }

        // New XML based Login system
        private void TutorLogin(string prUsername, string prPassword)
        {
            DictionarySerialiserMethods iDictionarySerialiser = new DictionarySerialiserMethods();
            transferrableUserDetails iUserDetailsToSend = new transferrableUserDetails();

            iUserDetailsToSend.Username = prUsername;
            iUserDetailsToSend.Password = prPassword;
            iUserDetailsToSend.UserRole = "Tutor";
            iUserDetailsToSend.DeviceOS = "WIN7";

            // Tutor Login
            byte[] iData = iDictionarySerialiser.ConvertUserDetailsToByteArray(iUserDetailsToSend);

            // Possibly unreliable method of testing. For now assume that if the connection 
            // is writable that the data was sent. 
            // TODO: Implement a check of some sort.
            if (mStream.CanWrite)
            {
                mStream.Write(iData, 0, iData.Length);
                mLoggedIn = true;
            }
            else
            {
                mLoggedIn = false;
            }
        }
        private bool CheckServer()
        {

                TcpClient tcpclient = new TcpClient();

                tcpclient.BeginConnect(mIPAddress, mPort,
                    new AsyncCallback(CallBackMethod), tcpclient);

                if (TimeoutObject.WaitOne(10, false))
                {
                    if (IsConnectionSuccessful)
                    {
                        return true;
                    }
                    else
                    {
                        tcpclient.Close();
                        return false;
                    }
                }
                else
                {
                    tcpclient.Close();
                    return false;
                }
        }

        private static void CallBackMethod(IAsyncResult asyncresult)
        {
            try
            {
                IsConnectionSuccessful = false;
                TcpClient tcpclient = asyncresult.AsyncState as TcpClient;

                if (tcpclient.Client != null)
                {
                    tcpclient.EndConnect(asyncresult);
                    IsConnectionSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                IsConnectionSuccessful = false;
                socketexception = ex;
            }
            finally
            {
                TimeoutObject.Set();
            }
        }

        // Listen for incoming data
        public void Listen()
        {
            Byte[] iData;
            string iInputString = "";
            DictionarySerialiserMethods iDictionarySerialiser = new DictionarySerialiserMethods();
            int iBytesRecv = 0;
            
            while (!mShouldStopThread)
            {
                try
                {
                    iData = new Byte[32768];
                    iBytesRecv = mStream.Read(iData, 0, iData.Length);
                }
                catch (System.Exception ex)
                {
                    // Server has gone offline
                    mIsConnected = false;
                    //StopConnections();
                    break;
                }

                // Server has gone offline
                if (iBytesRecv == 0)
                {
                    mIsConnected = false;
                    StopConnections();
                    break;
                }

                if (iBytesRecv > 0)
                {
                    char iContents = GetDataContents(iData);

                    if (iContents == 'u')   // User
                    {
                        mUserManager.UsersOnline = iDictionarySerialiser.ConvertByteArrayToUserDict(iData);
                        mUserManager.NewUserDataAvailable = true;
                    }
                    else if (iContents == 'q')  // Question
                    {
                        mQuestionManager.QuestionList = iDictionarySerialiser.ConvertByteArrayToQuestionDict(iData);
                        mQuestionManager.NewQuestionDataAvailable = true;
                    }
                    else if (iContents == 'a')  // Answer
                    {
                        mAnswerManager.AddAnswer(iDictionarySerialiser.ConvertStringToAnswer(new string(Encoding.ASCII.GetChars(iData)).Replace("\0", "")));
                    }
                    else if (iContents == 'c')  // Connection response
                    {
                        mConnectionResponse = new string(Encoding.ASCII.GetChars(iData)).Replace("\0", "");
                    }
                    else if (iContents == 'm')  // Chat message
                    {
                        ChatMessage iNewMessage = iDictionarySerialiser.ConvertStringToChatMessage(
                            new string(Encoding.ASCII.GetChars(iData)).Replace("\0", ""));

                        mChatManager.ChatMessages.Enqueue(iNewMessage);
                    }

                    Array.Clear(iData, 0, iData.Length);
                }
                
                // To prevent the CPU from running at 50-100% constantly sleep the thread for 10 milliseconds
                Thread.Sleep(10);
            }
        }

        private void SendOutput()
        {
            DictionarySerialiserMethods iDictionarySerialiser = new DictionarySerialiserMethods();
            
            while (!mShouldStopThread)
            {
                if (mQuestionManager.QuestionsToSend.Count > 0)
                {
                    // Pop a question off the queue
                    question iQuestion = mQuestionManager.QuestionsToSend.Dequeue();
                   
                    byte[] iQuestionArray = iDictionarySerialiser.ConvertQuestionToByteArray(iQuestion);
                    if (iQuestionArray != null)
                        mStream.Write(iQuestionArray, 0, iQuestionArray.Length);
                }

                if (mInstructionStringToSend != null)
                {
                    byte[] iByteArray = System.Text.Encoding.ASCII.GetBytes(mInstructionStringToSend);
                    mStream.Write(iByteArray, 0, iByteArray.Length);
                    mInstructionStringToSend = null;
                }

                if (mChatMessageToSend != null)
                {
                    byte[] iMessageArray = iDictionarySerialiser.ConvertChatMessageToByteArray(mChatMessageToSend);
                    if (iMessageArray != null)
                        mStream.Write(iMessageArray, 0, iMessageArray.Length);

                    Array.Clear(iMessageArray, 0, iMessageArray.Length);
                    mChatMessageToSend = null;
                }
                Thread.Sleep(10);
            }
        }

        // Work out what the data is
        private char GetDataContents(byte[] prData)
        {
            string iDataString = new string(Encoding.ASCII.GetChars(prData)).Replace("\0", "");
            char[] iDataTypeArray = new char[2];
            string iDataType;
            int iPosition = 0;

            // Loop through the first 2 chars
            for (int i = 0; i < 2; i++)
            {
                iDataTypeArray[i] = iDataString[i];
            }

            iDataType = new string(iDataTypeArray);
            // First check what the format of the data is
            // An XML data message starts with a '<' char
            if (iDataType == "<?")
            {
                if (Regex.IsMatch(iDataString, "transferrableUserDetails"))
                {
                    return 'u';
                }
                else if (Regex.IsMatch(iDataString, "SerializableKeyValuePairOfInt32questionInt32question"))
                {
                    return 'q';
                }
                else if (Regex.IsMatch(iDataString, "AnswerSent"))
                {
                    return 'a';
                }
                else if (Regex.IsMatch(iDataString, "ChatMessage"))
                {
                    return 'm';
                }
                else
                {
                    return '?';
                }
            }
            else
            {
                if (iDataString == "TUTORCONNECTED" || iDataString == "STUDENTCONNECTED"
                    || iDataString == "USERNAMETAKEN" || iDataString == "INCORRECTPASS")
                {
                    return 'c'; // Connection confirmation;
                }
                else
                {
                    return '?'; // Unknown Type
                }
                
            }
        }

    }
}
