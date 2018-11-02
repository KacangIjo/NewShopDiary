using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;

namespace ShopDiaryAbb.Fragments
{
    public class LocationAddFragment : Android.Support.V4.App.Fragment
    {
        private EditText mLocationName;
        private EditText mLocationAddress;
        private EditText mLocationDescription;
        private Button mButtonAdd;
        private Button mButtonCancel;
        private ProgressBar mProgressBar;

        LocationDataService mLocationDataService;

        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;

        public LocationAddFragment()
        {
            mLocationDataService = new LocationDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
        }

        public static LocationAddFragment NewInstance()
        {
            var frag2 = new LocationAddFragment { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ManageLocationAdd,container,false);
            mButtonAdd = view.FindViewById<Button>(Resource.Id.buttonAddLocation);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonCancelLocation);
            mLocationName = view.FindViewById<EditText>(Resource.Id.editTextAddLocationName);
            mLocationAddress = view.FindViewById<EditText>(Resource.Id.editTextAddLocationAddress);
            mLocationDescription = view.FindViewById<EditText>(Resource.Id.editTextAddLocationDescription);
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarAddLocation);
            mButtonAdd.Click += MButtonAdd_Click;
            return view;
        }

        private void MButtonAdd_Click(object sender, EventArgs e)
        {
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;
            LocationViewModel newLoc = new LocationViewModel()
            {
                Name = mLocationName.Text,
                Address=mLocationAddress.Text,
                Description = mLocationDescription.Text,
                CreatedUserId = mAuthorizedId.ToString(),
                AddedUserId = mAuthorizedId.ToString()
            };
            new Thread(new ThreadStart(delegate
            {
               
                var isAdded = mLocationDataService.Add(newLoc.ToModel());

                if (isAdded)
                {
                    LoadData();
                    UpgradeProgress();
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Location Added", ToastLength.Long).Show());
                    mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
                }
                else
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed", ToastLength.Long).Show());
                }
            })).Start();
           

        }

        private async void LoadData()
        {
            LoginPageActivity.mGlobalLocations.Clear();
            LoginPageActivity.mGlobalLocations = await mLocationDataService.GetAll();
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
    }
}