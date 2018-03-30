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
    [Activity(Label = "QuestionListActivity")]
    public class QuestionListActivity : Activity
    {
        List<Quiz> Quizlist = new List<Quiz>();
        ListView lbQuestionList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuestionList);
            lbQuestionList = FindViewById<ListView>(Resource.Id.lvQuestionList);
            PopulateList();
            lbQuestionList.Adapter = new QuizAdapter(this, Quizlist);

            lbQuestionList.ItemClick += LbQuestionList_ItemClick;
        }

        private void LbQuestionList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int questionClicked = e.Position;
            
            //Ask user if they want return to list, open google with questions or return to the main menu
            var listGoogleMenu = new AlertDialog.Builder(this);
            listGoogleMenu.SetTitle("Error");
            listGoogleMenu.SetMessage("Would you like to learn more about this question?");
            listGoogleMenu.SetPositiveButton("Yes", (senderAlert, args) => { ShowDetails(questionClicked); });
            listGoogleMenu.SetNegativeButton("Finish", (senderAlert, args) => { Finish(); }); 
            listGoogleMenu.SetNeutralButton("Go back to List", (senderAlert, args) => { }); // do nothing and return to list
            listGoogleMenu.Show();
            
            
        }

        private void PopulateList()
        {
            DBStore quizDB = new DBStore();
            Quizlist = quizDB.GetQuizList();
        }
        private void ShowDetails(int position)
        {
         
            string searchTerm = Quizlist[position].GoogleSearchItem;
            Intent googleIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(@"http://www.google.ie/#q=" + searchTerm));
            googleIntent.AddFlags(ActivityFlags.NewTask);
            StartActivity(googleIntent);
        }
    }
}