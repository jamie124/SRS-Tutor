using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TutorClient
{
    // Currently the answer is only tied to the user by the username
    // further developments may require more data.
    public class Answer
    {
        private int mQuestionID;
        private string mAnswer;
        private string mUsername;

        public string Username
        {
            get { return mUsername; }
            set { mUsername = value; }
        }

        public int QuestionID
        {
            get { return mQuestionID; }
            set { mQuestionID = value; }
        }

        public string AnswerString
        {
            get { return mAnswer; }
            set { mAnswer = value; }
        }
    }

    public class AnswerManager
    {
        Dictionary<int, Answer> mAnswerList = new Dictionary<int, Answer>();

        public Dictionary<int, Answer> AnswerList
        {
            get { return mAnswerList; }
            set { mAnswerList = value; }
        }

        QuestionManager mQuestionManager;

        // Variables for handling the current question
        private int mTotalTimeToAnswer;
        private int mCurrentTimeElapsed;
        private question mCurrentQuestion;
        private int mNumberOfStudentsSentQuestion;
        public question CurrentQuestion
        {
            get { return mCurrentQuestion; }
            set { mCurrentQuestion = value; }
        }
        public int TotalTimeToAnswer
        {
            get { return mTotalTimeToAnswer; }
            set { mTotalTimeToAnswer = value; }
        }
        public int CurrentTimeElapsed
        {
            get { return mCurrentTimeElapsed; }
            set { mCurrentTimeElapsed = value; }
        }

        public int NumberOfStudentsSentQuestion
        {
            get { return mNumberOfStudentsSentQuestion; }
            set { mNumberOfStudentsSentQuestion = value; }
        }
        public AnswerManager(QuestionManager prQuestionManager)
        {
            mQuestionManager = prQuestionManager;
        }

        public void AddAnswer(Answer prAnswer)
        {
            if (mAnswerList.Count == 0)
                mAnswerList.Add(0, prAnswer);
            else
                mAnswerList.Add(mAnswerList.Last().Key + 1, prAnswer);
        }

        // Return the results for a true/false question
        public TrueFalseResults CalculateTFResults()
        {
            int iYes = 0;
            int iNo = 0;
            string iQuestion = "";

            foreach (KeyValuePair<int, Answer> iAnswer in mAnswerList)
            {
                // Get the question string
                if (iQuestion == "")
                {
                    iQuestion = mQuestionManager.GetQuestionStringByID(iAnswer.Value.QuestionID);
                }

                // Get answer results
                if (iAnswer.Value.AnswerString == "True")
                    iYes++;
                else
                    iNo++;
            }
            return new TrueFalseResults(iYes, iNo, iQuestion);
        }

        // Return the results for a multi-choice question
        public MultichoiceResults CalculateMCResults()
        {
            Dictionary<string, int> iResults = new Dictionary<string, int>();
            int iTotalAnswers = 0;
            question iQuestion = new question();

            foreach (KeyValuePair<int, Answer> iAnswer in mAnswerList)
            {
                // Get the question string
                if (iQuestion.Question == null)
                {
                    iQuestion = mQuestionManager.GetQuestionByID(iAnswer.Value.QuestionID);
                }
                if (iResults.ContainsKey(iAnswer.Value.AnswerString))
                {
                    int iNewAnswer = iResults[iAnswer.Value.AnswerString] + 1;
                    iResults.Remove(iAnswer.Value.AnswerString);
                    iResults.Add(iAnswer.Value.AnswerString, iNewAnswer);
                }
                else
                {
                    iResults.Add(iAnswer.Value.AnswerString, 1);
                }
                iTotalAnswers++;
            }
            return new MultichoiceResults(iResults, iTotalAnswers, iQuestion);
        }
    }
}
