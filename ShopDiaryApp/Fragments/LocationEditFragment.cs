using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.Adapter;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ShopDiaryApp.Fragments
{
    public class LocationEditFragment : Fragment
    {
        private TextView mLocationTitle;
        private EditText mLocationName;
        private EditText mLocationAddress;
        private EditText mLocationDescription;
        private ImageButton mButtonAddUser;
        private ImageButton mButtonDeleteUser;
        private ImageButton mButtonSave;
        private ProgressBar mProgressBar;

        LocationDataService mLocationDataService;
        private FragmentTransaction mFragmentTransaction;

        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;

        public LocationEditFragment()
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
            View view = inflater.Inflate(Resource.Layout.LocationDetail,container,false);
            mLocationTitle = view.FindViewById<TextView>(Resource.Id.textviewLocationDetailTitle);
            mButtonAddUser = view.FindViewById<ImageButton>(Resource.Id.imageButtonLocationDetailAddUser);
            mButtonDeleteUser = view.FindViewById<ImageButton>(Resource.Id.imageButtonLocationDetailAddUser);
            mLocationName = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailName);
            mLocationAddress = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailAddress);
            mLocationDescription = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailDescription);
            mLocationTitle.Text = MainActivity.StaticLocationClass.Name.ToString();
            mLocationName.Text = MainActivity.StaticLocationClass.Name.ToString();
            mLocationAddress.Text = MainActivity.StaticLocationClass.Address.ToString();
            mLocationDescription.Text = MainActivity.StaticLocationClass.Description.ToString();
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarAddLocation);
            mButtonAddUser.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new LocationAddUserFragment(), "Add User");
            };
            return view;
        }

        

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            //SearchMenu
            inflater.Inflate(Resource.Menu.nav_standart, menu);
            
        }

        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
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