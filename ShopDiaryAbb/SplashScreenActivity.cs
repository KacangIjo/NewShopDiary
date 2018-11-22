using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.LocalDomain;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;

namespace ShopDiaryAbb
{
    [Activity(Label = "LoginPageActivity",MainLauncher =true)]
    public class SplashScreenActivity : Activity
    {
        public static Guid mAuthorizedUserId;
      
        private ProgressBar mProgressBar;
        private int progressvalue = 0;

        private readonly AccountDataService mAccountDataService;
        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly StorageDataService mStorageDataService;
        private readonly LocationDataService mLocationDataService;
        private readonly CategoryDataService mCategoryDataService;
        private readonly ConsumeDataService mConsumeDataService;
        private readonly UserLocationDataService mUserLocationDataService;
        private readonly UserDataDataService mUserDataDataService;
        private readonly ShoplistDataService mShopListDataService;
        private readonly ShopItemDataService mShopItemDataService;

        List<UserInfoLocal> ListSource = new List<UserInfoLocal>();
        public static ShopDiaryLocalDatabase db= new ShopDiaryLocalDatabase();



        public SplashScreenActivity()
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
            mShopListDataService = new ShoplistDataService();
            mShopItemDataService = new ShopItemDataService();


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.SplashScreenLayout);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBarSplashScreen);
            mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
            db.CreateDatabase();
            ListSource = db.SelectTable();
            for (int i = 0; i < ListSource.Count; i++)
            {
                var pro = ListSource[i].Id;
            }
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;
            if (db.SelectTable(0))
            {
                var intent=new Intent();
                for (int i = 0; i <= ListSource.Count + 1; i++)
                {
                    if (ListSource[i].Id == 1 && ListSource[i].IsLogin==1)
                    {
                        LoginPageActivity.StaticUserClass.ID = Guid.Parse(ListSource[i].UserInfoId);
                        intent = new Intent(this, typeof(MainActivity));
                        LoadData();
                        UpgradeProgress();
                        RunOnUiThread(() => mProgressBar.Visibility = Android.Views.ViewStates.Invisible);
                        this.StartActivity(intent);
                        break;
                    }
                    else if(ListSource[i].Id == 1 && ListSource[i].IsLogin == 0)
                    {
                        intent = new Intent(this, typeof(LoginPageActivity));
                        UpgradeProgress();
                        RunOnUiThread(() => mProgressBar.Visibility = Android.Views.ViewStates.Invisible);
                        this.StartActivity(intent);
                        break;
                    }
                }
                
            }
            else
            {
                UserInfoLocal userInfoLocal = new UserInfoLocal()
                {
                    IsLogin = 0,
                    UserInfoId ="",
                };
                SplashScreenActivity.db.InsertIntoTable(userInfoLocal);
                var intent = new Intent(this, typeof(LoginPageActivity));
                UpgradeProgress();
                RunOnUiThread(() => mProgressBar.Visibility = Android.Views.ViewStates.Invisible);
                this.StartActivity(intent);
            }
            
       
            
         
        }
        private async void LoadData()
        {

            LoginPageActivity.mGlobalLocations = await mLocationDataService.GetAll();
            LoginPageActivity.mGlobalProducts = await mProductDataService.GetAll();
            LoginPageActivity.mGlobalStorages = await mStorageDataService.GetAll();
            LoginPageActivity.mGlobalCategories = await mCategoryDataService.GetAll();
            LoginPageActivity.mGlobalUserLocs = await mUserLocationDataService.GetAll();
            LoginPageActivity.mGlobalUserDatas = await mUserDataDataService.GetAll();
            LoginPageActivity.mGlobalShopList = await mShopListDataService.GetAll();
            LoginPageActivity.mGlobalShopItem = await mShopItemDataService.GetAll();
            LoginPageActivity.mGlobalInventories = new List<InventoryViewModel>();

            List<InventoryViewModel> tempInventories = await mInventoryDataService.GetAll();
            for (int i = 0; i < tempInventories.Count(); i++)
            {
                for (int j = 0; j < LoginPageActivity.mGlobalProducts.Count(); j++)
                {
                    if (tempInventories[i].ProductId == LoginPageActivity.mGlobalProducts[j].Id)
                    {
                        tempInventories[i].ItemName = LoginPageActivity.mGlobalProducts[j].Name;
                        LoginPageActivity.mGlobalInventories.Add(tempInventories[i]);
                    }
                    if (LoginPageActivity.mGlobalProducts[j].AddedUserId == LoginPageActivity.StaticUserClass.ID.ToString())
                    {
                        LoginPageActivity.mGlobalProductsByUser.Add(LoginPageActivity.mGlobalProducts[j]);
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