using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Runtime;
using MobileAssignment2.DataAccess;
using System.Collections.Generic;

namespace MobileAssignment2
{
    [Activity(Label = "Brass Tacks", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button btnCatSelector;
        Button btnStartQuiz;
        Button btnQuestionList;
        string chosenCategory;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            btnCatSelector = FindViewById<Button>(Resource.Id.btnCatSelector);
            btnStartQuiz = FindViewById<Button>(Resource.Id.btnStartQuiz);
            btnQuestionList = FindViewById<Button>(Resource.Id.btnQuestionList);
            DBStore dBStore = new DBStore();
            List<Quiz> quizlist = dBStore.GetQuizList();
            Quiz quiz = quizlist[0];
            btnCatSelector.Click += BtnCatSelector_Click;
            btnStartQuiz.Click += BtnStartQuiz_Click;
            btnQuestionList.Click += BtnQuestionList_Click;
        }

        private void BtnQuestionList_Click(object sender, System.EventArgs e)
        {
            Intent QuestionListIntent = new Intent(this, typeof(QuestionListActivity));
            StartActivity(QuestionListIntent);
        }

        private void BtnStartQuiz_Click(object sender, System.EventArgs e)
        {
            //If Category is not chosen return this message
            if (!string.IsNullOrEmpty(chosenCategory))
            {
                Intent MainQuiz = new Intent(this, typeof(QuizActivity));
                MainQuiz.PutExtra("Category", chosenCategory);
                StartActivity(MainQuiz);
            }
            else
            {
                Toast toastMsg = Toast.MakeText(this, "Please select a Quiz category", ToastLength.Long);
                toastMsg.Show();
            }
        }

        private void BtnCatSelector_Click(object sender, System.EventArgs e)
        {
            Intent QuizCategory = new Intent(this, typeof(CategorySelecterActivity));
            StartActivityForResult(QuizCategory, 100);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if(requestCode ==100&&resultCode==Result.Ok)
            {
                chosenCategory = data.GetStringExtra("ChosenCategory");
            }
        }

    }
}

