using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;

namespace YSchool
{
    [Activity(Label = "YSchool", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            btnRegister.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(RegisterActivity));
            };

            btnLogin.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(LoginActivity));
            };


        }

        //public override void OnBackPressed()
        //{
        //    //Include the code here
        //    return;
        //}
    }
}

