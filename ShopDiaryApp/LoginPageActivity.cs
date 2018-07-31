using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.Services;

namespace ShopDiaryApp
{
    [Activity(Label = "LoginPageActivity",MainLauncher =true)]
    public class LoginPageActivity : Activity
    {
        public static Guid mAuthorizedUserId;
        private readonly AccountDataService mAccountDataService;

        private Button mBtnLogIn;
        private Button mBtnSignUp;
        private EditText mEmail;
        private EditText mPassword;
        private ProgressBar mProgressBar;

        public static Class.User StaticUserClass = new Class.User();
        public static Class.Location StaticLocationClass = new Class.Location();
        public static Class.Storage StaticStorageClass = new Class.Storage();
        public static Class.UserLocation StaticUserLocationClass = new Class.UserLocation();

        public LoginPageActivity()
        {
            mAccountDataService = new AccountDataService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.LoginPageStart);

            // Create your application here
            mBtnLogIn = FindViewById<Button>(Resource.Id.buttonLoginPageLogin);
            mBtnSignUp = FindViewById<Button>(Resource.Id.buttonLoginPageSignUp);
            mEmail = FindViewById<EditText>(Resource.Id.textInputEditTextLoginPageEmail);
            mPassword = FindViewById<EditText>(Resource.Id.editTextLoginPagePassword);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            mPassword.Text = "Ganteng@123";
            mEmail.Text = "balabalarebus@gmail.com";
            DateTime test = System.DateTime.Now;

            // Button Function
            mBtnLogIn.Click += (object sender, EventArgs e) =>
            {

                new Thread(new ThreadStart(async delegate
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    var user = mEmail.Text;
                    var pass = mPassword.Text;


                    var isLogin = await mAccountDataService.Login(user, pass);
                    var UserInfo = await mAccountDataService.GetUserInfo();
                    var temp = UserInfo.ID;
                    StaticUserClass.ID = Guid.Parse(temp);
                    //intent.PutExtra("AuthorizedUserId", UserInfo.ID.ToString());

                    RunOnUiThread(() => mProgressBar.Progress = 100);

                    if (isLogin)
                        this.StartActivity(intent);
                    else
                        RunOnUiThread(() => Toast.MakeText(this, "Failed to login in, please check again your username & password", ToastLength.Long).Show());
                })).Start();
            };

            mBtnSignUp.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(SignupPageActivity));
                this.StartActivity(intent);
            };
        }

        
    }
}