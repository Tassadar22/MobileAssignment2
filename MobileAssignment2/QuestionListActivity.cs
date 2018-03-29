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
            string question = Quizlist[questionClicked].Question;
            Intent googleIntent = new Intent(Intent.ActionView,Android.Net.Uri.Parse(@"http://www.google.ie/#q=" + question));
            googleIntent.AddFlags(ActivityFlags.NewTask);
            StartActivity(googleIntent);
        }

        private void PopulateList()
        {
            DBStore quizDB = new DBStore();
            Quizlist = quizDB.GetQuizList();
        }
    }
}