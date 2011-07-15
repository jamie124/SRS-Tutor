using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Timers;

namespace TutorClient
{
    public partial class frmProjector : Form
    {
        AnswerManager mAnswerManager;
        Presentation mPresentation;

        System.Timers.Timer mTimer;

        private int mResponsesStartRow = 50;
        private int mResponseStartingColumn = 20;


        private delegate void refresh_delegate(object sender, ElapsedEventArgs e);

        public frmProjector(AnswerManager prAnswerManager)
        {
            InitializeComponent();
            mAnswerManager = prAnswerManager;

            mPresentation = new Presentation(this);

            mTimer = new System.Timers.Timer(1000);
            mTimer.Elapsed += new ElapsedEventHandler(UpdateForm);
            mTimer.Enabled = true;

        }

        // Position the form on the projector display
        private void MoveToProjector()
        {
            if (Screen.AllScreens.Length > 1)
            {
                Screen iProjectorDisplay = Screen.AllScreens[1];

                this.Top = iProjectorDisplay.Bounds.Top;
                this.Left = iProjectorDisplay.Bounds.Left;
            }
            else
            {
                Screen iSingleDisplay = Screen.AllScreens[0];
                this.Top = iSingleDisplay.Bounds.Top;
                this.Left = iSingleDisplay.Bounds.Left + 100;

            }

        }

        private void UpdateForm(object sender, ElapsedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                refresh_delegate d = new refresh_delegate(UpdateForm);
                try
                {
                    this.Invoke(d, new object[] { sender, e });
                }
                catch (Exception)
                {
                   
                }
            }
            else
            {
                this.Refresh();
            }
        }

        private void frmProjector_Load(object sender, EventArgs e)
        {
            if (Screen.AllScreens.Length > 1)
            {
                Screen iDisplay = Screen.AllScreens[1];
                this.Width = iDisplay.Bounds.Width;
                this.Height = iDisplay.Bounds.Height;
            }
            else
            {
                Screen iDisplay = Screen.AllScreens[0];
                this.Width = iDisplay.Bounds.Width;
                this.Height = 600;
            }

            MoveToProjector();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Bitmap iDrawing = null;
            question iTempQuestion = mAnswerManager.CurrentQuestion;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            this.BackColor = Color.Black;

            // Start double buffering
            iDrawing = new Bitmap(this.Width, this.Height, e.Graphics);
            g = Graphics.FromImage(iDrawing);

            Font fnt = new Font("Arial", 30);

            g.DrawString("Student Responses", fnt, new SolidBrush(Color.White), (this.Width / 2 - 200), 30);

            // Draw the question
            if (iTempQuestion != null)
            {
                mPresentation.DrawQuestionTitle(g, "Question: " + iTempQuestion.Question);

                // Display remaining time
                int iTimeRemaining = mAnswerManager.TotalTimeToAnswer - mAnswerManager.CurrentTimeElapsed;

                if (mAnswerManager.AnswerList.Count == mAnswerManager.NumberOfStudentsSentQuestion)
                {
                    mPresentation.DrawTimeRemaining(g, "All responses received!");
                }
                else
                {
                    if (iTimeRemaining >= 0)
                        mPresentation.DrawTimeRemaining(g, "Time Remaining: " + iTimeRemaining.ToString() + " seconds");
                    else if (iTimeRemaining == 0)
                        mPresentation.DrawTimeRemaining(g, "Times Up!");
                }

                // Change display based on question type
                switch (iTempQuestion.QuestionType)
                {
                    case "MC":
                        mPresentation.DrawResultsMC(mAnswerManager.CalculateMCResults(), g);
                        break;
                    case "TF":
                        // Draw the results of a Yes/No question
                        mPresentation.DrawResultsTF(mAnswerManager.CalculateTFResults(), g);
                        break;
                    case "SA":
                        // Draw results for a short answer question
                        mPresentation.DrawShortAnswers(mAnswerManager.AnswerList, g);
                        break;
                    case "MA":
                        break;
                }
            }
            //else
               // mPresentation.DrawQuestionTitle(g, "No Question Set");

            //DrawMousePos(g);

            //DisplayResponses(g, fnt);

            // Draw the image
            e.Graphics.DrawImageUnscaled(iDrawing, 0, 0);
            iDrawing.Dispose();

            g.Dispose();
        }
        
        // Draw the mouse position, to help with designing the drawing stuff
        private void DrawMousePos(Graphics g)
        {
            Font iFont = new Font("Arial", 18);

            // Get the width of the main screen
            Screen iMainScreen = Screen.AllScreens[0];
            int iPrimaryScreenWidth = iMainScreen.WorkingArea.Width;

            g.DrawString("X: " + (Cursor.Position.X - iPrimaryScreenWidth).ToString() + " Y: " + Cursor.Position.Y.ToString(), iFont, new SolidBrush(Color.White), 10, 20);
        }

        private void DisplayResponses(Graphics g, Font prFont)
        {
            int iNextLine = 0;
            int iNextColumn = 0;
            if (mAnswerManager.AnswerList.Count > 0)
            {
                foreach (KeyValuePair<int, Answer> iAnswer in mAnswerManager.AnswerList)
                {
                    g.DrawString(iAnswer.Value.Username + " answered " + iAnswer.Value.AnswerString, prFont, 
                        new SolidBrush(Color.White), mResponseStartingColumn + iNextColumn, mResponsesStartRow + iNextLine);
                    
                    if ((mResponsesStartRow + iNextLine) < 1000)
                    {
                        iNextLine += 20;
                    }
                    else
                    {
                        iNextLine = 0;
                        iNextColumn += 300;
                    }
                }
            }
        }
    }
}
