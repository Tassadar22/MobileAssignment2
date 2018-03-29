using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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
        List<Quiz> Quizlist;
        List<Quiz> ChosenList;
        RadioButton rbAnswer1;
        RadioButton rbAnswer2;
        RadioButton rbAnswer3;
        RadioGroup radioAnswer;
        TextView txtQuestion;
        Button btnSubmitAnswer;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuizLayout);
            DBStore dBStore = new DBStore();
            Category = Intent.GetStringExtra("Category");
            Enum.TryParse(Category, out QuizCategory category);
            //Pull Data from database
            List<Quiz> CompleteQuizlist = dBStore.GetQuizList();
            ChosenList = GetQuizCategory(CompleteQuizlist, category);
            //ChosenList = PullXRandomQuestions(5, ChosenList);
            Quiz Question = ChosenList[3];
            txtQuestion = FindViewById<TextView>(Resource.Id.txtQuestion);
            rbAnswer1 = FindViewById<RadioButton>(Resource.Id.radioAnswer1);
            rbAnswer2 = FindViewById<RadioButton>(Resource.Id.radioAnswer2);
            rbAnswer3 = FindViewById<RadioButton>(Resource.Id.radioAnswer3);
            btnSubmitAnswer = FindViewById<Button>(Resource.Id.btnSubmitAnswer);
            radioAnswer = FindViewById<RadioGroup>(Resource.Id.radioAnswerGroup);
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
            if (rbAnswer1.Checked==false|| rbAnswer2.Checked == false || rbAnswer3.Checked == false ||)
            {
                checkAnswer();
            }
            else Toast(this, "Please select an Answer", ToastLength.Long).show();
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
        private List<Quiz> GetQuizCategory(List<Quiz> UnsortedList, QuizCategory category)
        {
            List<Quiz> Categorical = new List<Quiz>();
            if (category == QuizCategory.All)
            {
                Categorical = UnsortedList;
            }
            else
            {
                foreach (Quiz quiz in UnsortedList)
                {
                    if (quiz.Category == category)
                    {
                        Categorical.Add(quiz);
                    }
                }
            }
            return Categorical;
        }
        private List<Quiz> PullXRandomQuestions(int amountToTake, List<Quiz> inputList)
        {//Function to return n quiz questions randomly from an inputted list
            int listsize = inputList.Count;
            List<int> indicesToGet = new List<int>();
            List<int> integerList = new List<int>();
            //Generate list of integers as large as the size of the list
            for (int i = 0; i < listsize; i++)
            {
                integerList.Add(i);
            }
            Random rnd = new Random();
            for(int i =0;i<=amountToTake;i++)
            {
                int randomindex = rnd.Next(0, integerList.Count);
                indicesToGet.Add(integerList[randomindex]);
                integerList.Remove(indicesToGet[i]);
            }
            List<Quiz> OutputList = new List<Quiz>();
            foreach(int i in indicesToGet)
            {
                OutputList.Add(inputList[i]);
            }
            return OutputList;
        }
    }
}