﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MobileAssignment2.Resources.layout
{
    public class QuizResultsFrg : DialogFragment
    {
        TextView lblScoreDetails;
        Button btnCloseResults;
        string category;
        int score;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var resultDisplayFrg = inflater.Inflate(Resource.Layout.QuizResultsFrg, container, false);
            score = Arguments.GetInt("quizScore", 56);
            category = Arguments.GetString("quizCategory", "OOPS");
            
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            lblScoreDetails = resultDisplayFrg.FindViewById<TextView>(Resource.Id.lblScoreDetails);
            lblScoreDetails.Text = $"You have scored {score} points in {category}";
            

            btnCloseResults = resultDisplayFrg.FindViewById<Button>(Resource.Id.btnCloseResults);
            btnCloseResults.Click += (object sender, EventArgs e) => { Dismiss(); };
            

            return resultDisplayFrg;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            Dialog.Window.Attributes.WindowAnimations=Resource.s
        }
    }
}