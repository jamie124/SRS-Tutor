using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TutorClient
{
    public class IOProcessor
    {
        QuestionManager mQuestionManager;
        UserManager mUserManager;
        AnswerManager mAnswerManager;
        ChatMessage mNewChatMessage;

        public IOProcessor(UserManager prUserManager, QuestionManager prQuestionManager, AnswerManager prAnswerManager)
        {
            mUserManager = prUserManager;
            mQuestionManager = prQuestionManager;
            mAnswerManager = prAnswerManager;
        }

        public string RemoveFrontCharacters(string prString, int prNumToRemove)
        {
            int iCount = 0;
            int iStringLength = prString.Length;
            char[] iStringArray = new char[100];// prString.ToCharArray();
            //string iProcessedString;

            for (int i = 0; i < iStringLength; i++)
            {
                if (i >= prNumToRemove)
                {
                    if (prString[i] != ';')
                    {
                        iStringArray[iCount] = prString[i];
                        iCount++;
                    }
                    else 
                    {
                       // iProcessedString = new string(iStringArray);
                        iStringArray[iCount] = prString[i];
                        return new string(iStringArray);
                    }
                }
            }
            return new string(iStringArray);
        }

        public void ParseNewString(string prString, transferrableUserDetails prUser)
        {
            switch (prString[0])
            {
                case 'I':
                    ProcessInstructionString(RemoveFrontCharacters(prString, 2), prUser);
                    break;
                case 'C':
                    ProcessChatString(RemoveFrontCharacters(prString, 2), prUser);
            	    break;
                case 'A':
                    ProcessAnswerString(RemoveFrontCharacters(prString, 2), prUser);
                    break;
            }
        }

        // Interprets instructions sent from command line
        private void ProcessInstructionString(string prString, transferrableUserDetails prUser)
        {
            string iInstruction;
            string iResult = "";
            char[] iStringArray = new char[prString.Length];

            for (int i = 0; i < prString.Length; i++)
            {
                if (prString[i] != ';')
                {
                    iStringArray[i] = prString[i];
                }
            }

            iInstruction = new string(iStringArray).Replace("\0", "").ToLower();

            // TODO: Proper parsing.
            // List all questions
            if (iInstruction == "list questions" || iInstruction == "list q")
            {
                if (mQuestionManager.QuestionList.Count > 0)
                {
                    for (int q = 1; q <= mQuestionManager.QuestionList.Count; q++)
                    {
                        iResult += mQuestionManager.QuestionList[q].Question;
                        iResult = "";
                    }
                }
            }
            // List all users
            if (iInstruction == "list users" || iInstruction == "list u")
            {
                if (mUserManager.UsersOnline.Count > 0)
                {
                    for (int q = 1; q <= mUserManager.UsersOnline.Count; q++)
                    {
                        iResult += mUserManager.UsersOnline[q].Username;
                        iResult = "";
                    }
                }
                else
                {
                }
            }
        }

        private void ProcessChatString(string prString, transferrableUserDetails prUser)
        {
            string iChatString = prString.Replace("\0", "");
            char[] iProcessedString = new char[iChatString.Length];

            for (int i = 0; i < iChatString.Length; i++)
            {
                if (iChatString[i] != ';')
                {
                    iProcessedString[i] = iChatString[i];
                }
                else
                {
                    break;
                }
            }
            mNewChatMessage.Message = new string(iProcessedString);
            mNewChatMessage.SendTo = prUser.Username;

        }

        private void ProcessAnswerString(string prString, transferrableUserDetails prUser)
        {
            char[] iAnswerArray = new char[150];        // The max char size for all answers is currently 150
            char[] iQuestionIDArray = new char[5];      // allow for up to 99999 ID's
            bool iQuestionIDFound = false;
            bool iAnswerFound = false;
            int iCurrPos = 0;
            Answer iNewAnswer = new Answer();

            for (int i = 0; i < prString.Length; i++)
            {
                if (prString[i] != ';')
                {
                    if (!iQuestionIDFound)
                    {
                        if (prString[i] != '|')
                        {
                            iQuestionIDArray[iCurrPos] = prString[i];
                            iCurrPos++;
                        }
                        else
                        {
                            iQuestionIDFound = true;
                            iCurrPos = 0;
                        }
                    }
                    else
                    {
                        if (!iAnswerFound)
                        {
                            if (prString[i] != '|')
                            {
                                iAnswerArray[iCurrPos] = prString[i];
                                iCurrPos++;
                            }
                            else
                            {
                                iAnswerFound = true;
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            iNewAnswer.Username = prUser.Username;
            iNewAnswer.AnswerString = new string(iAnswerArray).Replace("\0", "");
            iNewAnswer.QuestionID = Convert.ToInt32(new string(iQuestionIDArray).Replace("\0", ""));
            mAnswerManager.AddAnswer(iNewAnswer);
        }
    }
}
