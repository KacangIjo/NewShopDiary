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
using ShopDiaryApp.Services;
using ShopDiaryProject.Domain.Models;

namespace ShopDiaryApp.Fragments
{
    public class StorageAddFragment : Android.Support.V4.App.Fragment
    {
        private EditText mStorageName;
        private EditText mStorageArea;
        private EditText mStorageDescription;
        private Button mButtonAdd;
        private Button mButtonCancel;
        private ProgressBar mProgressBar;

        StorageDataService mStorageDataService;

        private FragmentTransaction mFragmentTransaction;
        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;

        public StorageAddFragment()
        {
            mStorageDataService = new StorageDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }



        public static StorageAddFragment NewInstance()
        {
            var frag2 = new StorageAddFragment { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ManageStorageAdd,container,false);
            mButtonAdd = view.FindViewById<Button>(Resource.Id.buttonAddStorage);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonCancelLocation);
            mStorageName = view.FindViewById<EditText>(Resource.Id.editTextAddLocationName);
            mStorageArea = view.FindViewById<EditText>(Resource.Id.editTextAddStorageArea);
            mStorageDescription = view.FindViewById<EditText>(Resource.Id.editTextAddStorageDescription);
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarAddStorage);
            mButtonAdd.Click += MButtonAdd_Click;
            return view;
        }

        private void MButtonAdd_Click(object sender, EventArgs e)
        {
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;
            Storage newStorage = new Storage()
            {
                Name = mStorageName.Text,
                Area=mStorageArea.Text,
                Description = mStorageDescription.Text,
                CreatedUserId = mAuthorizedId.ToString(),
                AddedUserId = mAuthorizedId.ToString()

            };

            new Thread(new ThreadStart(delegate
            {
                UpgradeProgress();
                var isAdded = mStorageDataService.Add(newStorage);

                if (isAdded)
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Storage Added", ToastLength.Long).Show());
                    mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
                    ReplaceFragment(new StoragesFragment(), "Manage Storages");
                }
                else
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed", ToastLength.Long).Show());
                }
            })).Start();

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
