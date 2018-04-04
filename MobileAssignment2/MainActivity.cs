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
        string chosenCategory;//string to force user to select category
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            btnCatSelector = FindViewById<Button>(Resource.Id.btnCatSelector);
            btnStartQuiz = FindViewById<Button>(Resource.Id.btnStartQuiz);
            btnQuestionList = FindViewById<Button>(Resource.Id.btnQuestionList);
      
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
         //Only allow quiz to begin if user has chosen a category   
            if (!string.IsNullOrEmpty(chosenCategory))
            {
                Intent MainQuiz = new Intent(this, typeof(QuizActivity));
                MainQuiz.PutExtra("Category", chosenCategory);
                StartActivity(MainQuiz);
            }
            else
            {//If Category is not chosen return this message
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
        {//When Category has been succesfully take category chosen as string and pass and allow quiz to begin but only upon receiving correct result code
            if(requestCode ==100&&resultCode==Result.Ok)
            {
                chosenCategory = data.GetStringExtra("ChosenCategory");
            }
        }

    }
}

