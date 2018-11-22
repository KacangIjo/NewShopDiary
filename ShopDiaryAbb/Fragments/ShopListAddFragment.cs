using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;

namespace ShopDiaryAbb.Fragments
{
    public class ShopListAddFragement : Android.Support.V4.App.Fragment
    {
        private EditText mShopListName;
        private EditText mShopListDescription;
        private EditText mStoreLocation;
        private Spinner mSpinnerLocation;
        private Button mButtonAdd;
        private Button mButtonCancel;
        private ProgressBar mProgressBar;

        ShoplistDataService shoplistDataService;
        public List<ShoplistViewModel> mShopLists;
        private Guid mSelectedLocationId;

        private FragmentTransaction mFragmentTransaction;
        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;

        public ShopListAddFragement()
        {
            shoplistDataService = new ShoplistDataService();
            mShopLists = new List<ShoplistViewModel>();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }



        public static ShopListAddFragement NewInstance()
        {
            var frag2 = new ShopListAddFragement { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ManageShopListAdd,container,false);
            mButtonAdd = view.FindViewById<Button>(Resource.Id.buttonAddShopList);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonCancelShopList);
            mShopListName = view.FindViewById<EditText>(Resource.Id.editTextAddStorageName);
            mShopListDescription = view.FindViewById<EditText>(Resource.Id.editTextAddShopListDescription);
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarAddShopList);
            mButtonAdd.Click += MButtonAdd_Click;
            mButtonCancel.Click += MButtonCancel_Click;
            LoadInventoryData();
            return view;
        }

        private void MButtonCancel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MButtonAdd_Click(object sender, EventArgs e)
        {
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;
            ShoplistViewModel newShopList = new ShoplistViewModel()
            {
                Name = mShopListName.Text,
                Description = mShopListDescription.Text,
                LocationId = LoginPageActivity.StaticActiveLocationClass.Id.ToString(),
                AddedUserId = LoginPageActivity.StaticUserClass.ID.ToString()

            };

            new Thread(new ThreadStart(async delegate
            {
                UpgradeProgress();
                var isAdded = shoplistDataService.Add(newShopList.ToModel());

                if (isAdded)
                {
                    LoginPageActivity.mGlobalShopList = await shoplistDataService.GetAll();
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "ShopList Added", ToastLength.Long).Show());
                    mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
                    ReplaceFragment(new ShopListFragment(), "Manage ShopLists");
                }
                else
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed", ToastLength.Long).Show());
                }
            })).Start();

        }
        private void LoadInventoryData()
        {
            //mProgressBar.Visibility = Android.Views.ViewStates.Visible;
            //mShopLists = LoginPageActivity.mGlobalShopList.Where(s => s.LocationId == LoginPageActivity.StaticActiveLocationClass.Id.ToString()).ToList();
             

            //var adapterLocation = new SpinnerLocationAdapter(this.Activity, mShopLists);
            //mSpinnerLocation.Adapter = adapterLocation;
            //mSpinnerLocation.ItemSelected += SpinnerLocation_ItemSelected;
        }

        private void SpinnerLocation_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
           mSelectedLocationId = mShopLists[e.Position].Id;
        }

        private void UpgradeProgress()
        {
            int progressvalue = 0;
            while (progressvalue < 100)
            {
                progressvalue += 10;
                mProgressBar.Progress = progressvalue;
                Thread.Sleep(300);
            }

        }
        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
    }
}
