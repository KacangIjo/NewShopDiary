using System;
using System.Collections.Generic;
using System.Threading;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.API.Models.ViewModels;
using ShopDiaryApp.Services;
using ShopDiaryProject.Domain.Models;

namespace ShopDiaryApp.Fragments
{
    public class LocationAddUserFragment : Fragment
    {
        private readonly UserLocationDataService mUserLocationDataService;
        private readonly UserDataDataService mUserDataDataService;
        List<UserLocationViewModel> mUserLocations;
        UserLocationViewModel mSelectedUserLocation;
        List<UserDataViewModel> mUserDatas;
        UserDataViewModel mSelectedUserData;

        private EditText mEmail;
        private EditText mName;
        private EditText mDescription;
        private CheckBox mCreate;
        private CheckBox mUpdate;
        private CheckBox mRead;
        private CheckBox mDelete;
        public  int mCreateValue;
        public  int mUpdateValue;
        public  int mReadValue;
        public  int mDeleteValue;
        private Button mButtonAdd;
        private Button mButtonCancel;

        private FragmentTransaction mFragmentTransaction;

        public LocationAddUserFragment()
        {
            mUserLocationDataService = new UserLocationDataService();
            mUserDataDataService = new UserDataDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public static LocationAddUserFragment NewInstance()
        {
            var frag2 = new LocationAddUserFragment { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ManageLocationAddUser, container, false);
            mEmail = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailAddUserName);
            mName = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailAddUserName);
            mDescription = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailAddUserDescription);
            mCreate = view.FindViewById<CheckBox>(Resource.Id.checkBoxCreate);
            mDelete = view.FindViewById<CheckBox>(Resource.Id.checkBoxDelete);
            mUpdate = view.FindViewById<CheckBox>(Resource.Id.checkBoxUpdate);
            mRead = view.FindViewById<CheckBox>(Resource.Id.checkBoxRead);
            mButtonAdd = view.FindViewById<Button>(Resource.Id.buttonLocationDetailAddUserAdd);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonLocationDetailAddUserCancel);
            mCreate.Click += (o, e) => {
                if (mCreate.Checked)
                    mCreateValue = 1;
                else
                    mCreateValue = 0;
            };
            mDelete.Click += (o, e) => {
                if (mDelete.Checked)
                    mDeleteValue = 1;
                else
                    mDeleteValue = 0;
            };
            mUpdate.Click += (o, e) => {
                if (mUpdate.Checked)
                    mUpdateValue = 1;
                else
                    mUpdateValue = 0;
            };
            mRead.Click += (o, e) => {
                if (mRead.Checked)
                    mReadValue = 1;
                else
                    mReadValue = 0;
            };
            mButtonAdd.Click += (object sender, EventArgs args) =>
            {
                for (int i = 0; i < LoginPageActivity.mGlobalUserDatas.Count;i++)
                {
                    if(mEmail.Text==LoginPageActivity.mGlobalUserDatas[i].Email)
                    {
                        UserLocation newLoc = new UserLocation()
                        {

                            RegisteredUser = LoginPageActivity.mGlobalUserDatas[i].UserId,
                            Description = mDescription.Text,
                            Create = mCreateValue,
                            Update = mUpdateValue,
                            Read = mReadValue,
                            Delete = mDeleteValue,
                            AddedUserId = LoginPageActivity.StaticUserClass.ID.ToString(),
                            LocationId = LocationsFragment.mSelectedLocationClass.Id
                        };
                        new Thread(new ThreadStart(delegate
                        {
                            var isRoleAdded = mUserLocationDataService.Add(newLoc);
                            if (isRoleAdded)
                            {
                                this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "User Added", ToastLength.Long).Show());
                            }
                            else
                            {
                                this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed to add, please check again form's field", ToastLength.Long).Show());
                            }

                        })).Start();
                        ReplaceFragment(new LocationEditFragment(), "Location Detail");
                    }
                    else
                    {
                       this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Email not Found", ToastLength.Long).Show());
                    }
                }
              
            };
            mButtonCancel.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new LocationEditFragment(), "Location Detail");
            };

            return view;
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