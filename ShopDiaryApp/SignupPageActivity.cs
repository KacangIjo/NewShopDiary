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

namespace ShopDiaryApp
{
    [Activity(Label = "LoginPageActivity",MainLauncher =false)]
    public class SignupPageActivity : Activity
    {
        private Button mButtonSignUp;
        private Button mButtonSignUpGoogle;
        private EditText mName;
        private EditText mEmail;
        private EditText mPassword;
        private EditText mConfirmPassword;

        public SignupPageActivity()
        {

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.LoginPageStart);

            // Create your application here
            mButtonSignUp = FindViewById<Button>(Resource.Id.buttonSignUpSignUp);
            mButtonSignUpGoogle = FindViewById<Button>(Resource.Id.buttonSignUpGoogle);
            mName = FindViewById<EditText>(Resource.Id.editTextSignUpPageName);
            mEmail = FindViewById<EditText>(Resource.Id.editTextSignUpEmail);
            mPassword = FindViewById<EditText>(Resource.Id.editTextSignUpPassword);
            mConfirmPassword = FindViewById<EditText>(Resource.Id.editTextSignUpConfirmPassword);

            // Button Function
            mButtonSignUp.Click += (object sender, EventArgs e) =>
            {

                var intent = new Intent(this, typeof(SignupPageActivity));
                this.StartActivity(intent);


            };
            mButtonSignUpGoogle.Click += (object sender, EventArgs e) =>
            {

                var intent = new Intent(this, typeof(SignupPageActivity));
                this.StartActivity(intent);


            };
        }

        
    }
}