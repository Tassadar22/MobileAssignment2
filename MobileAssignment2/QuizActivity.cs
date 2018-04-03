using System;
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
using MobileAssignment2.Resources.layout;

namespace MobileAssignment2
{
    [Activity(Label = "Quiz")]
    public class QuizActivity : Activity,IDialogInterfaceOnDismissListener
    {
        string Category;
        List<Quiz> ChosenList;
        #region LayoutItems
        MediaPlayer wrongAnswerSound;
        MediaPlayer rightAnswerSound;
        MediaPlayer finishQuizSound;    
        RadioButton rbAnswer1;
        RadioButton rbAnswer2;
        RadioButton rbAnswer3;
        RadioGroup radioAnswerGroup;
        TextView txtQuestion;
        TextView lblQuestionCount;
        TextView lblScore;
        Button btnSubmitAnswer;
        #endregion
        //Integers to keep track of number of questions correctly answered, the question number which the user is on and the number of questions to ask
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
            #region LayoutWireUP
            txtQuestion = FindViewById<TextView>(Resource.Id.txtQuestion);
            rbAnswer1 = FindViewById<RadioButton>(Resource.Id.radioAnswer1);
            rbAnswer2 = FindViewById<RadioButton>(Resource.Id.radioAnswer2);
            rbAnswer3 = FindViewById<RadioButton>(Resource.Id.radioAnswer3);
            btnSubmitAnswer = FindViewById<Button>(Resource.Id.btnSubmitAnswer);
            radioAnswerGroup = FindViewById<RadioGroup>(Resource.Id.radioAnswerGroup);
            wrongAnswerSound = MediaPlayer.Create(this, Resource.Raw.WrongAnswer);
            rightAnswerSound = MediaPlayer.Create(this, Resource.Raw.RightAnswer);
            finishQuizSound = MediaPlayer.Create(this, Resource.Raw.FinishQuiz);
            lblQuestionCount = FindViewById<TextView>(Resource.Id.lblQuestionCount);
            lblScore = FindViewById<TextView>(Resource.Id.lblScore);
            #endregion
            txtQuestion.Text = Question.Question;
            RandomiseButtons(Question);
            btnSubmitAnswer.Click += BtnSubmitAnswer_Click;
        }
        private void BtnSubmitAnswer_Click(object sender, EventArgs e)
        {//Prohibit user from continuing one radio button is selected
            if (rbAnswer1.Checked == true || rbAnswer2.Checked == true || rbAnswer3.Checked == true)
            {//Find which radio button has been selected
                RadioButton SelectedButton = FindViewById<RadioButton>(radioAnswerGroup.CheckedRadioButtonId);
                if (CurrentQuestion.IsAnswerCorrect(SelectedButton.Text, Question))
                {//Correct Answer has been selected Display message and play sound
                    Toast ansSelect = Toast.MakeText(this, "Correct!", ToastLength.Short);
                    ansSelect.Show();
                    correctCount++;
                    rightAnswerSound.Start();
                }
                else
                {
                    Toast ansSelect = Toast.MakeText(this, "You are wrong", ToastLength.Short);
                    ansSelect.Show();
                    wrongAnswerSound.Start();
                }
                NextQuestion();//Regardless of answer selected, fill field with new question details
            }
            else
            {//if no radio button is selected inform user
                Toast ansSelect = Toast.MakeText(this, "Please select an Answer", ToastLength.Short);
                ansSelect.Show();
            }
        }
        private void NextQuestion()
        {//If the user has not yet completed the quiz show the next answer
            if (questionCount < questionstoask)
            {
                radioAnswerGroup.ClearCheck();
                questionCount++;
                Question = ChosenList[questionCount - 1];
                RandomiseButtons(Question);
                txtQuestion.Text = Question.Question;
                lblQuestionCount.Text = $"Question: {questionCount}/{questionstoask}";
                lblScore.Text = $"Score: {correctCount}";
            }//Otherwise call function end quiz
            else FinishQuiz();
        }
        private void FinishQuiz()
        {//Play sound on completion and display results fragment
            finishQuizSound.Start();
            FragmentTransaction finishtxn = FragmentManager.BeginTransaction();
            var quizData = new Bundle();
            quizData.PutInt("quizScore",correctCount); 
            //quizData.PutString("quizCategory", Category);
            QuizResultsFrg quizresults = new QuizResultsFrg() { Arguments = quizData };
            quizresults.Show(finishtxn, "Quiz Results");
        }
        void IDialogInterfaceOnDismissListener.OnDismiss(IDialogInterface dialog)
        {
            Finish();
        }
        
        private void RandomiseButtons(Quiz Question)
        {//Correct Answers within class objects are stored as first question asked, this function randomises the layout
            Random rnd = new Random();
            int layout = rnd.Next(1, 7);
            //Define possible layouts
            #region DefineLayouts
            switch (layout) {
                case 1:
                    rbAnswer1.Text = Question.RightAnswer; rbAnswer2.Text = Question.WrongAnswer1; rbAnswer3.Text = Question.WrongAnswer2;
                    break;
                case 2:
                    rbAnswer1.Text = Question.RightAnswer;rbAnswer2.Text = Question.WrongAnswer2;rbAnswer3.Text = Question.WrongAnswer1;
                    break;
                case 3:
                    rbAnswer1.Text = Question.WrongAnswer1;rbAnswer2.Text = Question.RightAnswer;rbAnswer3.Text = Question.WrongAnswer2;
                    break;
                case 4:
                    rbAnswer1.Text = Question.WrongAnswer2; rbAnswer2.Text = Question.RightAnswer; rbAnswer3.Text = Question.WrongAnswer1;
                    break;
                case 5:
                    rbAnswer1.Text = Question.WrongAnswer1; rbAnswer2.Text = Question.WrongAnswer2; rbAnswer3.Text = Question.RightAnswer;
                    break;
                case 6:
                    rbAnswer1.Text = Question.WrongAnswer2; rbAnswer2.Text = Question.WrongAnswer1; rbAnswer3.Text = Question.RightAnswer;
                    break;
            }
            #endregion
        }
       
    }
}