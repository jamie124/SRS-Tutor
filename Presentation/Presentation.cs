using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

// Functions for presenting data results graphically
namespace TutorClient
{
    // Results for a Multi-choice question
    public struct MultichoiceResults
    {
        private Dictionary<string, int> mAnswers;
        private question mQuestion;
        private int mTotalAnswers;

        public int TotalAnswers
        {
            get { return mTotalAnswers; }
            set { mTotalAnswers = value; }
        }

        public question Question
        {
            get { return mQuestion; }
            set { mQuestion = value; }
        }

        public Dictionary<string, int> Answers
        {
            get { return mAnswers; }
            set { mAnswers = value; }
        }

        public MultichoiceResults(Dictionary<string, int> prAnswers, int prTotalAnswers, question prQuestion)
        {
            mAnswers = prAnswers;
            mQuestion = prQuestion;
            mTotalAnswers = prTotalAnswers;
        }
    }

    // Results for a True/False question
    public struct TrueFalseResults
    {
        private int mYes;
        private int mNo;
        private string mQuestion;

        public int Yes
        {
            get { return mYes; }
            set { mYes = value; }
        }
        public int No
        {
            get { return mNo; }
            set { mNo = value; }
        }
        public string Question
        {
            get { return mQuestion; }
            set { mQuestion = value; }
        }

        public TrueFalseResults(int prYes, int prNo, string prQuestion)
        {
            mYes = prYes;
            mNo = prNo;
            mQuestion = prQuestion;
        }
    }

    public class Presentation
    {
        private int mZeroPosition;
        private int mLeftGap;
        private int mTopGap;

        // Details about the form
        private Form mPresentationForm;

        static Mutex mMutex;

        Dictionary<int, Rectangle> mAnswerPositions = new Dictionary<int, Rectangle>();   // Positions of answers

        public Presentation(Form prPresentationForm)
        {
            mLeftGap = 150;
            mTopGap = 0;
            mPresentationForm = prPresentationForm;

            mMutex = new Mutex(true);
        }

        // Get a free position
        // This function works by determining where on the screen the answer can be drawn without
        // overlapping other answers
        private Rectangle GetNewPosition(Dictionary<int, Rectangle> prPositions, string prAnswerText, Graphics g)
        {
            // Used to determine the size of the text
            Font iFont = new Font("Arial", 18);
            SizeF iAnswerSize = g.MeasureString(prAnswerText, iFont);
            Rectangle iPositionToAdd, iTempPosition;
            
            int iPosX = 50;
            int iPosY = mTopGap + 250;

            iFont.Dispose();

            if (prPositions.Count == 0)
                iPositionToAdd = new Rectangle(new Point(iPosX, iPosY), iAnswerSize.ToSize());     // If there are no existing positions just return a starting one.
            else
            {
                // Changed to just tile the answers across the screen
                iTempPosition = prPositions.Last().Value;      // Get the last position added

                // See if there's room to draw answer horizontally
                if ((iTempPosition.Right + iAnswerSize.Width) < mPresentationForm.Width)
                {
                    iPosX = iTempPosition.Right + 10;   // Move new position to be 10 px to the right of last position.
                    iPosY = iTempPosition.Y;
                }
                else
                {
                    iPosY = iTempPosition.Bottom + 10;
                }

                iPositionToAdd = new Rectangle(new Point(iPosX, iPosY), iAnswerSize.ToSize());

            }

            // Add position to used positions list
            if (mAnswerPositions.Count == 0)
                mAnswerPositions.Add(0, iPositionToAdd);
            else
            {
                if (!mAnswerPositions.ContainsValue(iPositionToAdd))
                {
                    mMutex.WaitOne();
                    mAnswerPositions.Add(mAnswerPositions.Last().Key + 1, iPositionToAdd);
                    mMutex.ReleaseMutex();
                }
            }

            return iPositionToAdd;
        }

        // This code is from http://www.jigar.net/howdoi/viewhtmlcontent139.aspx
        public static void DrawRoundedRectangle(Graphics g, Rectangle prRect, int prDegree, Pen prPen)
        {

            GraphicsPath iGraphicPath = new GraphicsPath();

            iGraphicPath.AddArc(prRect.X, prRect.Y, prDegree, prDegree, 180, 90);
            iGraphicPath.AddArc(prRect.X + prRect.Width - prDegree, prRect.Y, prDegree, prDegree, 270, 90);
            iGraphicPath.AddArc(prRect.X + prRect.Width - prDegree, prRect.Y + prRect.Height - prDegree, prDegree, prDegree, 0, 90);
            iGraphicPath.AddArc(prRect.X, prRect.Y + prRect.Height - prDegree, prDegree, prDegree, 90, 90);
            iGraphicPath.AddLine(prRect.X, prRect.Y + prRect.Height - prDegree, prRect.X, prRect.Y + prDegree / 2);

            g.DrawPath(prPen, iGraphicPath);

            iGraphicPath.Dispose();

        }

        // Displays the results from a short answer question
        public void DrawShortAnswers(Dictionary<int, Answer> prAnswers, Graphics g)
        {
            SolidBrush iBrush = new SolidBrush(Color.White);
            Font iFont = new Font("Arial", 18);
            Pen iPen = new Pen(Color.White, 1);

            Dictionary<int, Answer> iAnswers = prAnswers;                          // Local version of answers

            // Temp positions
            Rectangle iAnswerPosition;// = new Point(mLeftGap + 75, 400);

            foreach (KeyValuePair<int, Answer> iAnswer in prAnswers)
            {
                string iAnswerText = "\b" + "Anonymous" + "\b\n" + iAnswer.Value.AnswerString;

                if (mAnswerPositions.ContainsKey(iAnswer.Key))
                    iAnswerPosition = mAnswerPositions[iAnswer.Key];
                else
                    iAnswerPosition = GetNewPosition(mAnswerPositions, iAnswerText, g);  // Get a new position to draw at

                DrawRoundedRectangle(g, iAnswerPosition, 30, iPen);
                g.DrawString(iAnswerText, iFont, iBrush, iAnswerPosition.X, iAnswerPosition.Y);

            }

            iBrush.Dispose();
            iFont.Dispose();
            iPen.Dispose();
        }

        // Draws a graph for displaying yes/no or true/false results
        public void DrawResultsTF(TrueFalseResults prResults, Graphics g)
        {
            int iTotalResults = prResults.Yes + prResults.No;
            int iIntervals = 0;
            int iIntervalSize = 1;

            // Start and end positions for scale
            int iScaleStartingPos = mPresentationForm.Height - 125;
            int iCurrentScalePos = iScaleStartingPos;
            int iScaleEndPos = mTopGap + 200;
            int iScaleHeight = iScaleStartingPos - iScaleEndPos;    // Get the height of the scale

            int iIntervalGapSize;

            float iCurrentScaleNumber = 0.0f;

            // Bar heights
            int iBarYesHeight = 0;
            int iBarNoHeight = 0;

            SolidBrush iBrush = new SolidBrush(Color.White);
            Font iFont = new Font("Arial", 30);
            Pen iPen = new Pen(Color.White, 5);

            // Draw the scale
            // The scales kind of annoying, at low result numbers it doesn't seem to be very accurate
            // thought it doesn't seem too bad at larger numbers
            if (iTotalResults > 8)
            {
                iIntervalSize = iTotalResults / 2;

                iIntervals = 8;
            }
            else
            {
                iIntervalSize = 1;
                iIntervals = iTotalResults;
            }

            for (int i = 0; i <= iIntervals; i++)
            {
                if (iIntervals > 1)
                    iIntervalGapSize = iScaleHeight / iIntervals;  // Work out the size of the gap between intervals
                else if (iIntervals == 1)
                    iIntervalGapSize = iScaleHeight / 2;           // Hacky to get 1 result working     
                else
                    iIntervalGapSize = iScaleHeight / 8;

                if (iTotalResults <= 8)
                {
                    iCurrentScaleNumber = i * (8 / 8);
                }
                else
                {
                    float iTemp = (float)System.Math.Round((float)iTotalResults / 8, 1);
                    iCurrentScaleNumber = (float)i * iTemp;

                    if (iCurrentScaleNumber > 0)
                    {
                        iCurrentScaleNumber = (float)System.Math.Ceiling(iCurrentScaleNumber);
                    }
                }

                // Work out where to draw the scale number
                if (iTotalResults == 1)
                {
                    if (i == 0)
                        iCurrentScalePos = iScaleStartingPos;
                    else
                        iCurrentScalePos = iScaleStartingPos - (iIntervalGapSize * 2);      // This is for when there is only 1 result
                }
                else
                {
                    if (i >= 1)
                        iCurrentScalePos = iScaleStartingPos - (iIntervalGapSize * i);
                    else if (i == 0)
                        iCurrentScalePos = iScaleStartingPos;   // If printing the 0 marker
                }

                mZeroPosition = Convert.ToInt32(mPresentationForm.Height - 125);

                if (iCurrentScaleNumber < 10)
                {
                    g.DrawString("     " + iCurrentScaleNumber.ToString() + "-", iFont, iBrush, (mPresentationForm.Width / 2) - 350, iCurrentScalePos);
                }
                else if (iCurrentScaleNumber >= 100)
                {
                    g.DrawString(" " + iCurrentScaleNumber.ToString() + "-", iFont, iBrush, (mPresentationForm.Width / 2) - 350, iCurrentScalePos);
                }
                else
                {
                    g.DrawString("   " + iCurrentScaleNumber.ToString() + "-", iFont, iBrush, (mPresentationForm.Width / 2) - 350, iCurrentScalePos);
                }
                // Need to get numbers < 8 working properly
            }

            // Work out the heights of each bar

            if (iTotalResults > 0)
            {
                float iResultsGap = 0;
                // Test implementation
                if (prResults.Yes > 0)
                {
                    float iTemp = (float)System.Math.Round((float)iTotalResults / (float)prResults.Yes, 1);
                    iResultsGap = (float)System.Math.Round((float)iTotalResults / (float)prResults.Yes, 1, MidpointRounding.ToEven);
                    iBarYesHeight = (int)System.Math.Round(iScaleHeight / iResultsGap, MidpointRounding.ToEven);
                }
                if (prResults.No > 0)
                {
                    iResultsGap = (float)System.Math.Round((float)iTotalResults / (float)prResults.No, 1, MidpointRounding.ToEven);
                    iBarNoHeight = (int)System.Math.Round(iScaleHeight / iResultsGap, MidpointRounding.ToEven);
                }
                //iBarYesHeight = (prResults.Yes * (iScaleHeight / iTotalResults));
                //iBarNoHeight = (prResults.No * (iScaleHeight / iTotalResults));
            }

            // Draw the bars
            // Yes bar
            iBrush = new SolidBrush(Color.Green);
            g.FillRectangle(iBrush, (mPresentationForm.Width / 2) - 225, (mPresentationForm.Height - 125 - iBarYesHeight) + 25, 250, iBarYesHeight);
            // No bar
            iBrush = new SolidBrush(Color.Red);
            g.FillRectangle(iBrush, (mPresentationForm.Width / 2) + 75, (mPresentationForm.Height - 125 - iBarNoHeight) + 25, 250, iBarNoHeight);

            // Draw the labels
            DrawTFGraphLabels(g, prResults);
            
            // Free up memory
            iPen.Dispose();
            iBrush.Dispose();
            iFont.Dispose();
        }

        // Draws a graph for displaying yes/no or true/false results
        public void DrawResultsMC(MultichoiceResults prResults, Graphics g)
        {
            int iTotalResults = prResults.TotalAnswers;
            int iIntervals = 0;
            int iIntervalSize = 1;

            // Start and end positions for scale
            int iScaleStartingPos = mPresentationForm.Height - 125;
            int iCurrentScalePos = iScaleStartingPos;
            int iScaleEndPos = mTopGap + 200;
            int iScaleHeight = iScaleStartingPos - iScaleEndPos;    // Get the height of the scale

            int iIntervalGapSize;

            float iCurrentScaleNumber = 0.0f;

            // Bar heights
            int iBarYesHeight = 0;
            int iBarNoHeight = 0;
            int[] iBarHeights = new int[4];

            SolidBrush iBrush = new SolidBrush(Color.White);
            Font iFont = new Font("Arial", 30);
            Pen iPen = new Pen(Color.White, 5);

            // Draw the scale
            // The scales kind of annoying, at low result numbers it doesn't seem to be very accurate
            // thought it doesn't seem too bad at larger numbers
            if (iTotalResults > 8)
            {
                iIntervalSize = iTotalResults / 2;

                iIntervals = 8;
            }
            else
            {
                iIntervalSize = 1;
                iIntervals = iTotalResults;
            }

            for (int i = 0; i <= iIntervals; i++)
            {
                if (iIntervals > 1)
                    iIntervalGapSize = iScaleHeight / iIntervals;  // Work out the size of the gap between intervals
                else if (iIntervals == 1)
                    iIntervalGapSize = iScaleHeight / 2;           // Hacky to get 1 result working     
                else
                    iIntervalGapSize = iScaleHeight / 8;

                if (iTotalResults <= 8)
                {
                    iCurrentScaleNumber = i * (8 / 8);
                }
                else
                {
                    float iTemp = (float)System.Math.Round((float)iTotalResults / 8, 1);
                    iCurrentScaleNumber = (float)i * iTemp;

                    if (iCurrentScaleNumber > 0)
                    {
                        iCurrentScaleNumber = (float)System.Math.Ceiling(iCurrentScaleNumber);
                    }
                }

                // Work out where to draw the scale number
                if (iTotalResults == 1)
                {
                    if (i == 0)
                        iCurrentScalePos = iScaleStartingPos;
                    else
                        iCurrentScalePos = iScaleStartingPos - (iIntervalGapSize * 2);      // This is for when there is only 1 result
                }
                else
                {
                    if (i >= 1)
                        iCurrentScalePos = iScaleStartingPos - (iIntervalGapSize * i);
                    else if (i == 0)
                        iCurrentScalePos = iScaleStartingPos;   // If printing the 0 marker
                }

                mZeroPosition = Convert.ToInt32(mPresentationForm.Height - 125);

                if (iCurrentScaleNumber < 10)
                {
                    g.DrawString("     " + iCurrentScaleNumber.ToString() + "-", iFont, iBrush, (mPresentationForm.Width / 2) - 350, iCurrentScalePos);
                }
                else if (iCurrentScaleNumber >= 100)
                {
                    g.DrawString(" " + iCurrentScaleNumber.ToString() + "-", iFont, iBrush, (mPresentationForm.Width / 2) - 350, iCurrentScalePos);
                }
                else
                {
                    g.DrawString("   " + iCurrentScaleNumber.ToString() + "-", iFont, iBrush, (mPresentationForm.Width / 2) - 350, iCurrentScalePos);
                }
                // Need to get numbers < 8 working properly
            }

            // Work out the heights of each bar
            if (iTotalResults > 0)
            {
                float iResultsGap = 0;
                int i = 0;

                if (prResults.Question.Question != null)
                {
                    foreach (KeyValuePair<string, int> iAnswer in prResults.Answers)
                    {
                        if (prResults.Answers[iAnswer.Key] > 0)
                        {
                            float iTemp = (float)System.Math.Round((float)iTotalResults / (float)prResults.Answers[iAnswer.Key], 1);
                            iResultsGap = (float)System.Math.Round((float)iTotalResults / (float)prResults.Answers[iAnswer.Key], 1, MidpointRounding.ToEven);
                            iBarHeights[i] = (int)System.Math.Round(iScaleHeight / iResultsGap, MidpointRounding.ToEven);
                            i++;
                        }
                    }
                }
            }

            // Draw the bars
            // First bar
            iBrush = new SolidBrush(Color.Green);
            g.FillRectangle(iBrush, (mPresentationForm.Width / 2) - 225, (mPresentationForm.Height - 125 - iBarHeights[0]) + 25, 100, iBarHeights[0]);
            // Second bar
            iBrush = new SolidBrush(Color.Red);
            g.FillRectangle(iBrush, (mPresentationForm.Width / 2) - 75, (mPresentationForm.Height - 125 - iBarHeights[1]) + 25, 100, iBarHeights[1]);
            // Third bar
            iBrush = new SolidBrush(Color.Blue);
            g.FillRectangle(iBrush, (mPresentationForm.Width / 2) + 75, (mPresentationForm.Height - 125 - iBarHeights[2]) + 25, 100, iBarHeights[2]);
            // Forth bar
            iBrush = new SolidBrush(Color.Yellow);
            g.FillRectangle(iBrush, (mPresentationForm.Width / 2) + 225, (mPresentationForm.Height - 125 - iBarHeights[3]) + 25, 100, iBarHeights[3]);

            // Draw the labels
            DrawMCGraphLabels(g, prResults);

            // Free up memory
            iPen.Dispose();
            iBrush.Dispose();
            iFont.Dispose();
        }

        // Draw the questions title
        public void DrawQuestionTitle(Graphics g, string prQuestion)
        {
            Font iFont = new Font("Sylfaen", 28);
            SizeF iTitleSize = g.MeasureString(prQuestion, iFont);
            int iPosition = (mPresentationForm.Width / 2) - (int)(iTitleSize.Width / 2);
            g.DrawString(prQuestion, iFont, Brushes.White, iPosition, mTopGap + 150);

            iFont.Dispose();
        }

        // Draw the time remaining to answer the question
        public void DrawTimeRemaining(Graphics g, string prTimeRemaining)
        {
            Font iFont = new Font("Sylfaen", 28);
            g.DrawString(prTimeRemaining, iFont, Brushes.Yellow, 25, mTopGap + 100);

            iFont.Dispose();
        }

        // Text rotation code taken from http://www.c-sharpcorner.com/Blogs/BlogDetail.aspx?BlogId=580
        private void DrawTFGraphLabels(Graphics g, TrueFalseResults prResults)
        {
            Font iFont = new Font("Sylfaen", 20);
           
            string iLeftLabel = "Number of Responses";
            SizeF iLabelSize = g.MeasureString(iLeftLabel, iFont);
            Pen iPen = new Pen(Color.White);

            int iTextHeightPos = mPresentationForm.Height - 100;

            // Draw and rotate left label
            Bitmap iStringMap = new Bitmap((int)iLabelSize.Height + 1, (int)iLabelSize.Width + 1);
            Graphics iImageBitmap = Graphics.FromImage(iStringMap);
            iImageBitmap.SmoothingMode = SmoothingMode.AntiAlias;

            iImageBitmap.TranslateTransform(0, iLabelSize.Width);
            iImageBitmap.RotateTransform(-90);
            iImageBitmap.DrawString(iLeftLabel, iFont, Brushes.White, new PointF(0 , 0), new StringFormat());
            g.DrawImage(iStringMap, (mPresentationForm.Width / 2) - 340, (float)mTopGap + 250);

            // Draw bottom labels
            g.DrawString("True", iFont, Brushes.White, (mPresentationForm.Width / 2) - 225, iTextHeightPos);
            g.DrawString("False", iFont, Brushes.White, (mPresentationForm.Width / 2) + 75, iTextHeightPos);
            //g.DrawString(prResults.Yes.ToString(), iFont, Brushes.LightBlue, mLeftGap + 280, iTextHeightPos + 40);
            //g.DrawString(prResults.No.ToString(), iFont, Brushes.LightBlue, mLeftGap + 600, iTextHeightPos + 40);
            g.DrawString(prResults.Yes.ToString(), iFont, Brushes.LightBlue, (mPresentationForm.Width / 2) - 225, iTextHeightPos + 40);
            g.DrawString(prResults.No.ToString(), iFont, Brushes.LightBlue, (mPresentationForm.Width / 2) + 75, iTextHeightPos + 40);


            // Draw Left and bottom lines
            // Left
            g.DrawLine(iPen, new Point((mPresentationForm.Width / 2) - 250, mTopGap + 200), new Point((mPresentationForm.Width / 2) - 250, mZeroPosition + 25));
            // Bottom
            g.DrawLine(iPen, new Point((mPresentationForm.Width / 2) - 250, mZeroPosition + 25), new Point(mLeftGap + 870, mZeroPosition + 25));

            iImageBitmap.Dispose();
            iPen.Dispose();
            iFont.Dispose();
            iStringMap.Dispose();
        }

        // Text rotation code taken from http://www.c-sharpcorner.com/Blogs/BlogDetail.aspx?BlogId=580
        private void DrawMCGraphLabels(Graphics g, MultichoiceResults prResults)
        {
            Font iFont = new Font("Sylfaen", 18);
            string iLeftLabel = "Number of Responses";
            SizeF iLabelSize = g.MeasureString(iLeftLabel, iFont);
            Pen iPen = new Pen(Color.White);

            int iTextHeightPos = mPresentationForm.Height - 100;

            // Draw and rotate left label
            Bitmap iStringMap = new Bitmap((int)iLabelSize.Height + 1, (int)iLabelSize.Width + 1);
            Graphics iImageBitmap = Graphics.FromImage(iStringMap);
            iImageBitmap.SmoothingMode = SmoothingMode.AntiAlias;

            iImageBitmap.TranslateTransform(0, iLabelSize.Width);
            iImageBitmap.RotateTransform(-90);
            iImageBitmap.DrawString(iLeftLabel, iFont, Brushes.White, new PointF(0, 0), new StringFormat());
            g.DrawImage(iStringMap, (mPresentationForm.Width / 2) - 340, (float)mTopGap + 250);

            if (prResults.Question.Question != null)
            {
                // Loop through results dictionary and draw the answer and it's count
                int iXPos = 225;
                foreach (KeyValuePair<string, int> iAnswer in prResults.Answers)
                {
                    // If the question is too long, shortify it
                    if (iAnswer.Key.Length > 4)
                        g.DrawString(ShortifyQuestion(iAnswer.Key), iFont, Brushes.White, (mPresentationForm.Width / 2) - iXPos, iTextHeightPos);
                    else
                        g.DrawString(iAnswer.Key, iFont, Brushes.White, (mPresentationForm.Width / 2) - iXPos, iTextHeightPos);
                    // Display total just below answer
                    g.DrawString(iAnswer.Value.ToString(), iFont, Brushes.LightBlue,
                        (mPresentationForm.Width / 2) - iXPos, iTextHeightPos + 40);
                    iXPos -= 150;   // Move text 150 pixels to the right
                }
            }

            // Draw Left and bottom lines
            // Left
            g.DrawLine(iPen, new Point((mPresentationForm.Width / 2) - 250, mTopGap + 200), new Point((mPresentationForm.Width / 2) - 250, mZeroPosition + 25));
            // Bottom
            g.DrawLine(iPen, new Point((mPresentationForm.Width / 2) - 250, mZeroPosition + 25), new Point(mLeftGap + 870, mZeroPosition + 25));

            iImageBitmap.Dispose();
            iPen.Dispose();
            iFont.Dispose();
            iStringMap.Dispose();
        }

        // Takes the first letter of each word a returns a combined string containing them
        private string ShortifyQuestion(string prQuestion)
        {
            int iNumberOfWords = 0;
            int iWhitespace = 0;
            int iPosInArray = 0;    // Current position in shorterned question array
            
            // Get number of words
            Regex iRegex = new Regex(" ", RegexOptions.IgnoreCase);
            iWhitespace = iRegex.Match(prQuestion).Captures.Count;
            iNumberOfWords = iWhitespace + 1;   // Number of words should be whitespace chars + 1

            string[] iQuestionArray = prQuestion.Split(' ');
            char[] iShorternedQuestion = new char[iNumberOfWords];

            foreach (string iQuestion in iQuestionArray)
            {
                // Get the first character of the word and add it to the array
                iShorternedQuestion[iPosInArray] = iQuestion.ToArray()[0];
                iPosInArray++;
            }

            return new string(iShorternedQuestion);
        }
    }
}
