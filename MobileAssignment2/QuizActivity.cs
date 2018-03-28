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
        List<Quiz> GeoList;
        TextView lbltest;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuizLayout);
            DBStore dBStore = new DBStore();
            Category = Intent.GetStringExtra("Category");
            //Category = data.GetStringExtra("ChosenCategory");
            //Pull Data from database
            List<Quiz> Quizlist = dBStore.GetQuizList();
            lbltest = FindViewById<TextView>(Resource.Id.lblTest);
        }
        private List<Quiz> GetCategory(List<Quiz> UnsortedList, QuizCategory category)
        {
            List<Quiz> Categorical=new List<Quiz>();
            foreach(Quiz quiz in UnsortedList)
            {
                if(quiz.Category==category)
                {
                    Categorical.Add(quiz);
                }
            }
            return Categorical;
        }

        private List<Quiz> PullRandomQuestion(int amountToTake, List<Quiz> inputList)
        {//Function to return n quiz questions randomly from an inputted list
            int listsize = Inputlist.Count;
            List<int> indicesToGet = new List<int>();
            
            Random rnd = new Random();
            for(int i =0;i<=listsize;i++)
            {
                if (inputList.Count == 0)
                {
                    indicesToGet.Add(rnd.Next(1, amountToTake));
                }else if()
            }

            return Inputlist;
        }

    }
}