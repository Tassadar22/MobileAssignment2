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
    [Activity(Label = "Select Category")]
    public class CategorySelecterActivity : Activity  
    {
        Button btnGeo;
        Button btnHis;
        Button btnGen;
        Button btnAll;
        Button btnReturntoMainMenu;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CategorySelector);
            btnGeo = FindViewById<Button>(Resource.Id.btnGeo);
            btnHis = FindViewById<Button>(Resource.Id.btnHis);
            btnGen = FindViewById<Button>(Resource.Id.btnGen);
            btnAll = FindViewById<Button>(Resource.Id.btnAll);
            btnReturntoMainMenu = FindViewById<Button>(Resource.Id.btnReturntoMainMenu);

            btnReturntoMainMenu.Click += BtnReturntoMainMenu_Click;
            btnGeo.Click += BtnGeo_Click;
            btnHis.Click += BtnHis_Click;
            btnGen.Click += BtnGen_Click;
            btnAll.Click += BtnAll_Click;
        }

        private void BtnAll_Click(object sender, EventArgs e)
        {
            SetCategoryandReturn(QuizCategory.All);
        }

        private void BtnGen_Click(object sender, EventArgs e)
        {
            SetCategoryandReturn(QuizCategory.General_Knowledge);
        }

        private void BtnHis_Click(object sender, EventArgs e)
        {
            SetCategoryandReturn(QuizCategory.History);
        }

        private void BtnGeo_Click(object sender, EventArgs e)
        {
            SetCategoryandReturn(QuizCategory.Geography);
        }
        
        private void SetCategoryandReturn(QuizCategory category)
        {
            //Return to main menu with chosen category and result code to reflect this
            Intent returnMainResult = new Intent();
            returnMainResult.PutExtra("ChosenCategory", category.ToString());
            SetResult(Result.Ok, returnMainResult);
            Finish();
        }

        private void BtnReturntoMainMenu_Click(object sender, EventArgs e)
        {
            //Return to main menu unchanged and warn 
            var returnWarningMsg = new AlertDialog.Builder(this);
            returnWarningMsg.SetTitle("Error");
            returnWarningMsg.SetMessage("Are you sure you want to return to the Main Menu?, all changes will be lost");
            returnWarningMsg.SetPositiveButton("Yes",(senderAlert, args)=> { Finish(); });
            returnWarningMsg.SetNegativeButton("No", (senderAlert, args) => {});
            returnWarningMsg.Show();
        }
        
    }
}