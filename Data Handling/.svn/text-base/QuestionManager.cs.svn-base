using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TutorClient
{
    [Serializable()]
    public class question
    {
        private int mQuestionID;
        private string mQuestion;
        private string mQuestionType;
        private string[] mPossibleAnswers;
        private string mAnswer;

        public int QuestionID
        {
            get { return mQuestionID; }
            set { mQuestionID = value; }
        }
        public string Question
        {
            get { return mQuestion; }
            set { mQuestion = value; }
        }
        public string QuestionType
        {
            get { return mQuestionType; }
            set { mQuestionType = value; }
        }
        public string[] PossibleAnswers
        {
            get { return mPossibleAnswers; }
            set { mPossibleAnswers = value; }
        }
        public string Answer
        {
            get { return mAnswer; }
            set { mAnswer = value; }
        }
    }

    public class QuestionManager
    {
        private Dictionary<int, question> mQuestionList;    // Local copy of server questions
        private Queue<question> mQuestionsToSend;           // Questions to send to server

        private bool mNewQuestionDataAvailable;

        public bool NewQuestionDataAvailable
        {
            get { return mNewQuestionDataAvailable; }
            set { mNewQuestionDataAvailable = value; }
        }

        public Queue<question> QuestionsToSend
        {
            get { return mQuestionsToSend; }
            set { mQuestionsToSend = value; }
        }

        public Dictionary<int, question> QuestionList
        {
            get { return mQuestionList; }
            set { mQuestionList = value; }
        }

        private int mLastQuestionAddedToList;

        public QuestionManager()
        {
            mQuestionList = new Dictionary<int, question>();
            mQuestionsToSend = new Queue<question>();

            mLastQuestionAddedToList = 0;
        }

        // Insert the question number into the string
        public string InsertQuestionNumber(string prQuestionString, int prQuestionNum)
        {
            return prQuestionString.Insert(0, prQuestionNum.ToString() + "|");
        }

        // Add a question to the queue
        public void AddQuestionToQueue(question prQuestion)
        {
            mQuestionsToSend.Enqueue(prQuestion);
        }

        public int GetNewQuestionID()
        {
            int iID = 0;

            iID = mQuestionList[mQuestionList.Last().Key].QuestionID + 1;
            return iID;
        }

        public bool IsListEmpty()
        {
            if (mQuestionList.Count == 0)
                return true;
            else
                return false;
        }

        // Check if the question name is in use
        public bool IsQuestionInDictionary(string prQuestionName)
        {
            question iQuestion = GetQuestionByName(prQuestionName);

            if (iQuestion != null)
            {
                if (mQuestionList.ContainsValue(iQuestion))
                {
                    return true;
                }
            }
            return false;
        }

        // Get question by name
        public question GetQuestionByName(string prQuestionName)
        {
            foreach (KeyValuePair<int, question> iQuestion in mQuestionList)
            {
                if (iQuestion.Value.Question == prQuestionName)
                {
                    return iQuestion.Value;
                }
            }
            // Question not found
            return null;
        }

        // Get question by id
        public question GetQuestionByID(int prQuestionID)
        {
            foreach (KeyValuePair<int, question> iQuestion in mQuestionList)
            {
                if (iQuestion.Value.QuestionID == prQuestionID)
                {
                    return iQuestion.Value;
                }
            }
            // Question not found
            return null;
        }

        public string GetLastQuestionAdded()
        {
            mLastQuestionAddedToList++;
            int i = mLastQuestionAddedToList;

            string iQuestionType = "";
          
            switch (mQuestionList[i].QuestionType)
            {
            case "MC":
                iQuestionType = "Multi-Choice";
            	break;
            case "SA":
                iQuestionType = "Short Answer";
                break;
            case "TF":
                iQuestionType = "True/False";
                break;
            case "MA":
                iQuestionType = "Matching";
                break;
            }

            return mQuestionList[i].Question + " - " + iQuestionType;
        }

        public bool IsNewQuestionAvailable()
        {
            if (mQuestionList.Count > mLastQuestionAddedToList)
                return true;
            else
                return false;
        }

        // Get the requested question string
        // Possibly unneeded
        public string GetQuestionStringByID(int prQuestionID)
        {
            return mQuestionList[prQuestionID].Question;
        }

        // Get the index for the requested question name
        public int GetQuestionIndexByQuestionName(string prQuestionName)
        {
            int iIndex = 0;
            foreach (KeyValuePair<int, question> iQuestion in mQuestionList)
            {
                if (iQuestion.Value.Question == prQuestionName)
                    return iQuestion.Key;
                else
                    iIndex++;
            }
            return -1;  // If none found
        }
    }
}
