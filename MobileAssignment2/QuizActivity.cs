﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileAssignment2.DataAccess;

namespace MobileAssignment2
{
    [Activity(Label = "Quiz")]
    public class QuizActivity : Activity
    {
        string Category;
        #region FormItems
        MediaPlayer wrongAnswerSound;
        List<Quiz> Quizlist;
        List<Quiz> ChosenList;
        RadioButton rbAnswer1;
        RadioButton rbAnswer2;
        RadioButton rbAnswer3;
        RadioGroup radioAnswerGroup;
        TextView txtQuestion;
        Button btnSubmitAnswer;
        #endregion
        int correctCount = 0, questionCount = 1, questionstoask=5;
        Quiz CurrentQuestion=new Quiz();
        Quiz Question = new Quiz();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuizLayout);
            DBStore dBStore = new DBStore();
            Category = Intent.GetStringExtra("Category");
            Enum.TryParse(Category, out QuizCategory category);
            //Pull Data from database
            List<Quiz> CompleteQuizlist = dBStore.GetQuizList();
            ChosenList = CurrentQuestion.GetQuizCategory(CompleteQuizlist, category);
            ChosenList = CurrentQuestion.PullXRandomQuestions(5, ChosenList);
            Question = ChosenList[0];
            txtQuestion = FindViewById<TextView>(Resource.Id.txtQuestion);
            rbAnswer1 = FindViewById<RadioButton>(Resource.Id.radioAnswer1);
            rbAnswer2 = FindViewById<RadioButton>(Resource.Id.radioAnswer2);
            rbAnswer3 = FindViewById<RadioButton>(Resource.Id.radioAnswer3);
            btnSubmitAnswer = FindViewById<Button>(Resource.Id.btnSubmitAnswer);
            radioAnswerGroup = FindViewById<RadioGroup>(Resource.Id.radioAnswerGroup);
            wrongAnswerSound = MediaPlayer.Create(this, Resource.Raw.WrongAnswer);
            txtQuestion.Text = Question.Question;
            /*rbAnswer1.Text = ChosenList[0].RightAnswer;
            rbAnswer2.Text = ChosenList[0].WrongAnswer1;
            rbAnswer3.Text= ChosenList[0].WrongAnswer2;*/
            RandomiseButtons(Question);
            btnSubmitAnswer.Click += BtnSubmitAnswer_Click;
            //lbltest = FindViewById<TextView>(Resource.Id.lblTest);
        }

        private void BtnSubmitAnswer_Click(object sender, EventArgs e)
        {
            if (rbAnswer1.Checked == true || rbAnswer2.Checked == true || rbAnswer3.Checked == true)
            {
                RadioButton SelectedButton = FindViewById<RadioButton>(radioAnswerGroup.CheckedRadioButtonId);
                if (CurrentQuestion.IsAnswerCorrect(SelectedButton.Text, Question))
                {
                    Toast ansSelect = Toast.MakeText(this, "Correct!", ToastLength.Short);
                    ansSelect.Show();
                    correctCount++;
                    wrongAnswerSound.Start();
                }
                else
                {
                    Toast ansSelect = Toast.MakeText(this, "You are wrong", ToastLength.Short);
                    ansSelect.Show();
                    wrongAnswerSound.Start();
                }
                NextQuestion();
                /*wrongAnswerSound.Start();
                Toast ansSelect = Toast.MakeText(this, "Test", ToastLength.Long);
                ansSelect.Show();*/
            }
            else
            {
                Toast ansSelect = Toast.MakeText(this, "Please select an Answer", ToastLength.Short);
                ansSelect.Show();
            }
        }

        private void NextQuestion()
        {
            if (questionCount <= questionstoask)
            {
                radioAnswerGroup.ClearCheck();
                questionCount++;
                Question = ChosenList[questionCount - 1];
                RandomiseButtons(Question);
                txtQuestion.Text = Question.Question;
            }
            else
            {
                FinishQuiz();
            }

        }

        private void FinishQuiz()
        {
            Finish();
        }

        private void RandomiseButtons(Quiz Question)
        {//Function randomise Where
            Random rnd = new Random();
            int layout = rnd.Next(1, 7);
            //Define possible layouts
            #region DefineLayouts
            switch (layout) {
                case 1:
                    rbAnswer1.Text = Question.RightAnswer;
                    rbAnswer2.Text = Question.WrongAnswer1;
                    rbAnswer3.Text = Question.WrongAnswer2;
                    break;
                case 2:
                    rbAnswer1.Text = Question.RightAnswer;
                    rbAnswer2.Text = Question.WrongAnswer2;
                    rbAnswer3.Text = Question.WrongAnswer1;
                    break;
                case 3:
                    rbAnswer1.Text = Question.WrongAnswer1;
                    rbAnswer2.Text = Question.RightAnswer;
                    rbAnswer3.Text = Question.WrongAnswer2;
                    break;
                case 4:
                    rbAnswer1.Text = Question.WrongAnswer2;
                    rbAnswer2.Text = Question.RightAnswer;
                    rbAnswer3.Text = Question.WrongAnswer1;
                    break;
                case 5:
                    rbAnswer1.Text = Question.WrongAnswer1;
                    rbAnswer2.Text = Question.WrongAnswer2;
                    rbAnswer3.Text = Question.RightAnswer;
                    break;
                case 6:
                    rbAnswer1.Text = Question.WrongAnswer2;
                    rbAnswer2.Text = Question.WrongAnswer1;
                    rbAnswer3.Text = Question.RightAnswer;
                    break;
            }
            #endregion
        }
       
    }
}