using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Net.Http;
using Android.Views;
using Android.Widget;

using ShopDiaryProjectV1.Services;
using ShopDiaryProject.Android.Models.ViewModels;

namespace ShopDiaryProjectV1
{

    [Activity(Label = "LoginActivity",MainLauncher=true)]
    public class LoginActivity : Activity
    {
        public static Guid mAuthorizedUserId;
        private readonly AccountDataService mAccountDataService;

        private Button mBtnRegister;
        private Button mBtnLogin;
        private EditText mEmail;
        private EditText mPassword;
        public static Class.User StaticUserClass = new Class.User();
        public static Class.Location StaticLocationClass = new Class.Location();
        public static Class.Storage StaticStorageClass = new Class.Storage();
        public static Class.UserLocation StaticUserLocationClass = new Class.UserLocation();
        
        public LoginActivity()
        {
            mAccountDataService = new AccountDataService();
        }

      

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.PageLogin);
            //Create your application here

            mBtnRegister = FindViewById<Button>(Resource.Id.buttonMainRegister);
            mBtnLogin = FindViewById<Button>(Resource.Id.buttonMainLogin);
            mEmail = FindViewById<EditText>(Resource.Id.textBoxUsername);
            mPassword = FindViewById<EditText>(Resource.Id.textBoxMainPassword);
            mPassword.Text = "Ganteng@123";
            mEmail.Text = "balabalarebus@gmail.com";
            DateTime test = System.DateTime.Now;

            

            mBtnLogin.Click += (object sender, EventArgs e) =>
            {
                var progressDialog = ProgressDialog.Show(this, "Please wait...", "Getting user data...", true);
                new Thread(new ThreadStart(async delegate
                {
                    var intent = new Intent(this, typeof(PageMainActivity));
                    var user = mEmail.Text;
                    var pass = mPassword.Text;


                    var isLogin = await mAccountDataService.Login(user, pass);
                    var UserInfo = await mAccountDataService.GetUserInfo();
                    var temp = UserInfo.ID;
                    StaticUserClass.ID = Guid.Parse(temp);
                    //intent.PutExtra("AuthorizedUserId", UserInfo.ID.ToString());
                    
                    RunOnUiThread(() => progressDialog.Hide());

                    if (isLogin)
                        this.StartActivity(intent);
                    else
                        RunOnUiThread(() => Toast.MakeText(this, "Failed to login in, please check again your username & password", ToastLength.Long).Show());
                })).Start();
            };

            mBtnRegister.Click += (object sender, EventArgs args) =>
            {
                //ngeluarin dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialogueRegister registerDialogue = new dialogueRegister();
                registerDialogue.Show(transaction, "dialogue fragment");
                registerDialogue.OnRegisterComplete += RegisterDialogue_OnRegisterComplete;

            };
        }
        private void RegisterDialogue_OnRegisterComplete(object sender, OnRegisterEventArgs e)
        {
            var user = e.Email;
            var pass = e.Password;
            var confirmpass = e.ConfirmPassword;
            mEmail.Text = e.Email;
            var progressDialog = ProgressDialog.Show(this, "Please wait...", "Registering...", true);
            new Thread(new ThreadStart(delegate
            {

                var isRegistered = mAccountDataService.Register(user, pass, confirmpass);
                RunOnUiThread(() => progressDialog.Hide());

                if (isRegistered)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Register Completed", ToastLength.Long).Show());
                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Failed to register, please check again your username & password", ToastLength.Long).Show());
                }

            })).Start();
        }


    }
}