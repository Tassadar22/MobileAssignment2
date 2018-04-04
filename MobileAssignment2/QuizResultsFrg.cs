using System;
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
        int score;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override void OnDismiss(IDialogInterface dialog)
        {//Dismiss device after completion
            base.OnDismiss(dialog);
            Activity activity = this.Activity;
            ((IDialogInterfaceOnDismissListener)activity).OnDismiss(dialog);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var resultDisplayFrg = inflater.Inflate(Resource.Layout.QuizResultsFrg, container, false);
            score = Arguments.GetInt("quizScore", 56); //Impossibly high value entered for debugging purposes, maximum attainable score is 5
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            lblScoreDetails = resultDisplayFrg.FindViewById<TextView>(Resource.Id.lblScoreDetails);
            string resultdetails;
            if (score==1)
            {
                resultdetails = $"You have scored 1 point";
            }
            else resultdetails = $"You have scored {score} points";
            lblScoreDetails.Text = resultdetails; //Display results
            
            //Restrict user from using background click feature so only on button press will fragment end
            this.Dialog.SetCanceledOnTouchOutside(false);
            
            btnCloseResults = resultDisplayFrg.FindViewById<Button>(Resource.Id.btnCloseResults);
            btnCloseResults.Click += (object sender, EventArgs e) => 
            {
                Dismiss();
            };
            return resultDisplayFrg;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {//Implement animation
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.quizAnimation;

        }
        
    }
}