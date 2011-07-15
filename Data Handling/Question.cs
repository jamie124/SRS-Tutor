using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TutorClient
{
    class Question
    {
        private int mQuestionID;
        private string mQuestionString;
        private string mFullQuestionString;
        private string mQuestionType;
        private string mAnswer;

        private string[] mPossibleAnswers = new string[4];
        private bool mInstantAnswer;
        public int QuestionID
        {
            get { return mQuestionID; }
            set { mQuestionID = value; }
        }
        public string QuestionString
        {
            get { return mQuestionString; }
            set { mQuestionString = value; }
        }
        public string FullQuestionString
        {
            get { return mFullQuestionString; }
            set { mFullQuestionString = value; }
        }
        public string QuestionType
        {
            get { return mQuestionType; }
            set { mQuestionType = value; }
        }
        public string Answer
        {
            get { return mAnswer; }
            set { mAnswer = value; }
        }
        public string[] PossibleAnswers
        {
            get { return mPossibleAnswers; }
            set { mPossibleAnswers = value; }
        }
        public bool InstantAnswer
        {
            get { return mInstantAnswer; }
            set { mInstantAnswer = value; }
        }

        //public string GetPossibleAnswer(int prIndex)
        //{
        //    return mPossibleAnswers[]
        //}

        // Remove X chars from the start of the string
        public string RemoveFrontCharacters(string prString, int prNumToRemove)
        {
            int iCount = 0;
            int iStringLength = prString.Length;
            char[] iStringArray = new char[100];// prString.ToCharArray();

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
                        iStringArray[iCount] = prString[i];
                        return new string(iStringArray);
                    }
                }
            }
            return new string(iStringArray);
        }

        // Parse a question and split it into the various sections
        public void ProcessQuestion(string prQuestion)
        {
            bool iQuestionFound = false;
            bool iQuestionTypeFound = false;
            char[] iQuestionIDArray = new char[5];
            char[] iQuestionStringArray = new char[50];
            char[] iQuestionTypeArray = new char[2];
            char[,] iPossibleAnswersArray = new char[4, 20];
            char[] iAnswerArray = new char[20];

            int iCurrPos = 0;
            int iSection = 0;
            int iCurrAnswer = 0;

            for (int i = 0; i < prQuestion.Length; i++)
            {
                if (prQuestion[i] != ';')
                {
                    switch (iSection)
                    {
                        case 0:     // Question ID
                            if (prQuestion[i] != '|')
                            {
                                iQuestionIDArray[iCurrPos] = prQuestion[i];
                                iCurrPos++;
                            }
                            else
                            {
                                iCurrPos = 0;
                                iSection++;
                            }
                            break;
                        case 1:     // Question Type
                            if (prQuestion[i] != '|')
                            {
                                iQuestionTypeArray[iCurrPos] = prQuestion[i];
                                iCurrPos++;
                            }
                            else
                            {
                                iSection++;
                                iCurrPos = 0;
                            }
                            break;
                        case 2:     // Question
                            if (prQuestion[i] != '|')
                            {
                                iQuestionStringArray[iCurrPos] = prQuestion[i];
                                iCurrPos++;
                            }
                            else
                            {
                                iSection++;
                                iCurrPos = 0;
                            }
                            break;
                        case 3:     // Possible answers
                            if (prQuestion[i] != '|')
                            {
                                if (prQuestion[i] != ',')
                                {
                                    iPossibleAnswersArray[iCurrAnswer, iCurrPos] = prQuestion[i];
                                    iCurrPos++;
                                }
                                else
                                {
                                    iCurrAnswer++;
                                    iCurrPos = 0;
                                }
                            }
                            else
                            {
                                iSection++;
                                iCurrPos = 0;
                            }
                            break;
                    }
                }
                else
                {
                    break;
                }
            }

            mQuestionID = Convert.ToInt32(new string(iQuestionIDArray).Replace("\0", ""));
            mQuestionType = new string(iQuestionTypeArray);
            mQuestionString = new string(iQuestionStringArray).Replace("\0", "");
            mFullQuestionString = prQuestion;

            string[] iPossibleAnswers = new string[4];
            char[] iTempAnswer = new char[20];
            int iCurrChar = 0;
            for (int x = 0; x <= iCurrAnswer; x++)
            {
               // string mPossibleAnswers[] = new string[]{"", "", "", ""};
                while (iPossibleAnswersArray[x, iCurrChar] != '\0')
                {
                    iTempAnswer[iCurrChar] = iPossibleAnswersArray[x, iCurrChar];
                    iCurrChar++;
                }
                mPossibleAnswers[x] = new string(iTempAnswer).Replace("\0", "");
                iCurrChar = 0;
                Array.Clear(iTempAnswer, 0, iTempAnswer.Length);
            }
        }
    }
}

