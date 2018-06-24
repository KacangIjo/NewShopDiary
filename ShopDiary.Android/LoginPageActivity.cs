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
    [Activity(Label = "LoginPageActivity",MainLauncher =true)]
    public class LoginPageActivity : Activity
    {
        private Button mBtnLogIn;
        private Button mBtnSignUp;
        private EditText mEmail;
        private EditText mPassword;

        public LoginPageActivity()
        {

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            //SetContentView(Resource.Layout.LoginPageStart);

            //// Create your application here
            //mBtnLogIn = FindViewById<Button>(Resource.Id.buttonLoginPageLogin);
            //mBtnSignUp = FindViewById<Button>(Resource.Id.buttonLoginPageSignUp);
            //mEmail = FindViewById<EditText>(Resource.Id.textInputEditTextLoginPageEmail);
            //mPassword = FindViewById<EditText>(Resource.Id.editTextLoginPagePassword);

            //// Button Function
            //mBtnLogIn.Click += (object sender, EventArgs e) =>
            //{
                
            //    var intent = new Intent(this, typeof(MainActivity));
            //    this.StartActivity(intent);

  
            //};
        }

        
    }
}