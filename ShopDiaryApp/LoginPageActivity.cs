﻿using System;
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
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;

namespace ShopDiaryApp
{
    [Activity(Label = "LoginPageActivity",MainLauncher =true)]
    public class LoginPageActivity : Activity
    {
        public static Guid mAuthorizedUserId;
        

        private Button mBtnLogIn;
        private Button mBtnSignUp;
        private EditText mEmail;
        private EditText mPassword;
        private ProgressBar mProgressBar;
        int progressvalue = 0;

        private readonly AccountDataService mAccountDataService;
        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly StorageDataService mStorageDataService;
        private readonly LocationDataService mLocationDataService;
        private readonly CategoryDataService mCategoryDataService;
        private readonly ConsumeDataService mConsumeDataService;
        private readonly UserLocationDataService mUserLocationDataService;
        private readonly UserDataDataService mUserDataDataService;

        public static Class.User StaticUserClass = new Class.User();
        public static Class.Location StaticLocationClass = new Class.Location();
        public static Class.Storage StaticStorageClass = new Class.Storage();
        public static Class.UserLocation StaticUserLocationClass = new Class.UserLocation();

        public static List<InventoryViewModel> mGlobalInventories=new List<InventoryViewModel>();
        public static List<ProductViewModel> mGlobalProducts=new List<ProductViewModel>();
        public static List<StorageViewModel> mGlobalStorages=new List<StorageViewModel>();
        public static List<LocationViewModel> mGlobalLocations = new List<LocationViewModel>();
        public static List<CategoryViewModel> mGlobalCategories = new List<CategoryViewModel>();
        public static List<UserLocationViewModel> mGlobalUserLocs = new List<UserLocationViewModel>();
        public static List<UserDataViewModel> mGlobalUserDatas = new List<UserDataViewModel>();
        public LoginPageActivity()
        {
            mAccountDataService = new AccountDataService();
            mInventoryDataService = new InventoryDataService();
            mProductDataService = new ProductDataService();
            mStorageDataService = new StorageDataService();
            mLocationDataService = new LocationDataService();
            mCategoryDataService = new CategoryDataService();
            mConsumeDataService = new ConsumeDataService();
            mUserDataDataService = new UserDataDataService();
            mUserLocationDataService = new UserLocationDataService();
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
            mEmail.Text = "priambododo@gmail.com";
            DateTime test = System.DateTime.Now;

            // Button Function
            mBtnLogIn.Click += (object sender, EventArgs e) =>
            {
                mProgressBar.Visibility = Android.Views.ViewStates.Visible;
                new Thread(new ThreadStart(async delegate
                {
                    
                    var intent = new Intent(this, typeof(MainActivity));
                    var user = mEmail.Text;
                    var pass = mPassword.Text;

                    var isLogin = await mAccountDataService.Login(user, pass);
                    var UserInfo = await mAccountDataService.GetUserInfo();
                    var temp = UserInfo.ID;
                    StaticUserClass.ID = Guid.Parse(temp);
                    StaticUserClass.Username = UserInfo.Email;
                    LoadData();
                    //intent.PutExtra("AuthorizedUserId", UserInfo.ID.ToString());
                    UpgradeProgress();
                    if (isLogin)
                    {
                        
                        RunOnUiThread(() => mProgressBar.Visibility = Android.Views.ViewStates.Invisible);
                        this.StartActivity(intent);
                    }
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
        private async void LoadData()
        {
            
            mGlobalLocations = await mLocationDataService.GetAll();
            mGlobalProducts = await mProductDataService.GetAll();
            mGlobalStorages = await mStorageDataService.GetAll();
            mGlobalCategories = await mCategoryDataService.GetAll();
            mGlobalUserLocs = await mUserLocationDataService.GetAll();
            mGlobalUserDatas = await mUserDataDataService.GetAll();
            mGlobalInventories = new List<InventoryViewModel>();

            List<InventoryViewModel> tempInventories = await mInventoryDataService.GetAll();
            for (int i = 0; i < tempInventories.Count(); i++)
            {
                for (int j = 0; j < mGlobalProducts.Count(); j++)
                {
                    
                    if (tempInventories[i].ProductId == mGlobalProducts[j].Id)
                    {
                        tempInventories[i].ItemName = mGlobalProducts[j].Name;
                        mGlobalInventories.Add(tempInventories[i]);
                    }
                }
            }
        }
        private void UpgradeProgress()
        {
            while (progressvalue < 100)
            {
                progressvalue += 10;
                mProgressBar.Progress = progressvalue;
                Thread.Sleep(300);
            }
           
        }
        
    }
}