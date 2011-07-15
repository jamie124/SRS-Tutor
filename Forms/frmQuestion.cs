using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TutorClient
{
    public partial class frmQuestionDesigner : Form
    {
        QuestionManager mQuestionManager;
        bool mModifying;
        string mQuestionName;

        public frmQuestionDesigner(QuestionManager prQuestionManager, bool prModifying, string prQuestionName)
        {
            InitializeComponent();
            mQuestionManager = prQuestionManager;
            mModifying = prModifying;
            mQuestionName = prQuestionName;
        }
 
        private void frmQuestionDesigner_Load(object sender, EventArgs e)
        {
            cbQuestionTypes.SelectedIndex = 0;
            gbTrueFalse.Hide();

            if (mModifying)
            {
                // Display the question data
                question iSelectedQuestion = mQuestionManager.GetQuestionByName(mQuestionName);

                cbQuestionTypes.Enabled = false;
                txtQuestion.Enabled = false;
                btnCreate.Text = "Modify Question";

                txtQuestion.Text = iSelectedQuestion.Question;

                // Warning, dodgy setting of combo box values, seems too hardcoded
                switch (iSelectedQuestion.QuestionType)
                {
                    case "MC":     // Multi-choice
                        cbQuestionTypes.Text = "Multi-Choice";
                        txtField1.Text = iSelectedQuestion.PossibleAnswers[0];
                        txtField2.Text = iSelectedQuestion.PossibleAnswers[1];
                        txtField3.Text = iSelectedQuestion.PossibleAnswers[2];
                        txtField4.Text = iSelectedQuestion.PossibleAnswers[3];

                        txtAnswer.Text = iSelectedQuestion.Answer;
                        break;
                    case "SA":     // Short Answer
                        cbQuestionTypes.Text = "Short Answer";
                        break;
                    case "TF":     // True/False
                        cbQuestionTypes.Text = "True/False";
                        txtAnswer.Text = iSelectedQuestion.Answer;
                        break;
                    case "MA":     // Matching
                        break;
                }
            }
            else
            {
                cbQuestionTypes.Enabled = true;
                txtQuestion.Enabled = true;
                btnCreate.Text = "Create Question";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            question iNewQuestion = new question();
            bool iAddQuestion = true;

            // Do some error checking
            // nested ifs :(
            if (txtQuestion.Text.Length > 44)
                if (MessageBox.Show("The question title entered is longer than 44 chars, this may make the question hard to read in the question window.", 
                    "Warning: Long Question", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    if (mModifying != false)
                    {
                        if (!mQuestionManager.IsQuestionInDictionary(txtQuestion.Text))
                        {
                            iAddQuestion = false;
                        }
                        else
                        {
                            MessageBox.Show("The question supplied is already in use, please provide a different one", "Question in use");
                        }
                    }
                }
            

            // If no problems have been found, add the question
            if (iAddQuestion == true)
            {

                    iNewQuestion.Question = txtQuestion.Text;

                    switch (cbQuestionTypes.SelectedIndex)
                    {
                        case 0:     // Multi-choice
                            iNewQuestion.QuestionType = "MC";
                            iNewQuestion.PossibleAnswers = new string[4];

                            iNewQuestion.PossibleAnswers[0] = txtField1.Text;
                            iNewQuestion.PossibleAnswers[1] = txtField2.Text;
                            iNewQuestion.PossibleAnswers[2] = txtField3.Text;
                            iNewQuestion.PossibleAnswers[3] = txtField4.Text;

                            iNewQuestion.Answer = txtAnswer.Text;
                            break;
                        case 1:     // Short Answer
                            iNewQuestion.QuestionType = "SA";
                            iNewQuestion.PossibleAnswers = new string[1];
                            iNewQuestion.PossibleAnswers[0] = "NA"; // Not applicable for this question
                            break;
                        case 2:     // True/False
                            iNewQuestion.QuestionType = "TF";
                            iNewQuestion.Answer = txtTFAnswer.Text;
                            iNewQuestion.PossibleAnswers = new string[2];
                            iNewQuestion.PossibleAnswers[0] = "True";
                            iNewQuestion.PossibleAnswers[1] = "False";
                            break;
                        case 3:     // Matching
                            iNewQuestion.QuestionType = "MA";
                            iNewQuestion.PossibleAnswers = new string[1];
                            iNewQuestion.PossibleAnswers[0] = "NA";
                            break;
                    }

                    // Add the question to the queue to be sent
                    mQuestionManager.AddQuestionToQueue(iNewQuestion);
                    mQuestionManager.NewQuestionDataAvailable = true;
                    Close();
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbQuestionTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbQuestionTypes.SelectedIndex)
            {
                case 0:     // Multi-Choice                    
                    if (gbTrueFalse.Visible == true)
                        gbTrueFalse.Hide();
                    if (gbMultiChoice.Visible == false)
                        gbMultiChoice.Show();

                    this.Height = 373;
                    btnCreate.Location = new Point(51,305);
                    btnClose.Location = new Point(155, 305);
                    break;
                case 1:     // Short Answer
                    if (gbTrueFalse.Visible == true)
                        gbTrueFalse.Hide();
                    if (gbMultiChoice.Visible == true)
                        gbMultiChoice.Hide();
                    this.Height = 179;

                    btnCreate.Location = new Point(51, 107);
                    btnClose.Location = new Point(155, 107);
                    break;
                case 2:     // True/False
                    if (gbMultiChoice.Visible == true)
                        gbMultiChoice.Hide();
                    if (gbTrueFalse.Visible == false)
                        gbTrueFalse.Show();
                    this.Height = 256;
                    btnCreate.Location = new Point(51, 184);
                    btnClose.Location = new Point(155, 184);
                    break;
                case 3:     // Matching
                    if (gbTrueFalse.Visible == true)
                        gbTrueFalse.Hide();
                    if (gbMultiChoice.Visible == true)
                        gbMultiChoice.Hide();
                    this.Height = 179;
                    btnCreate.Location = new Point(51, 107);
                    btnClose.Location = new Point(155, 107);
                    break;
            } 
        }
    }
}
