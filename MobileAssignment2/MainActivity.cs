using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Runtime;

namespace MobileAssignment2
{
    [Activity(Label = "Brass Tacks", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button btnCatSelector;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            btnCatSelector = FindViewById<Button>(Resource.Id.btnCatSelector);

            btnCatSelector.Click += BtnCatSelector_Click;
        }

        private void BtnCatSelector_Click(object sender, System.EventArgs e)
        {
            Intent QuizCategory = new Intent(this, typeof(CategorySelecterActivity));

            StartActivityForResult(QuizCategory, 40);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if(requestCode ==100&&resultCode==Result.Ok)
            {
                string chosenCategory = data.GetStringExtra("ChosenCategory");
                //Test toast to insure data is succesfully passed back to main screen
                Toast testToast = Toast.MakeText(this, $"New Category is"+chosenCategory, ToastLength.Long);
                testToast.Show();
            }
        }
    }
}

