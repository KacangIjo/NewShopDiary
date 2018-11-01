using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ShopDiaryAbb.Fragments
{
    public class StorageEditFragment : Fragment
    {
        private TextView mStorageTitle;
        private EditText mStorageName;
        private EditText mStorageArea;
        private EditText mStorageDescription;
        private ImageButton mButtonSave;
        private ImageButton mButtonDelete;
        private ProgressBar mProgressBar;

        StorageDataService mStorageDataService;
        private FragmentTransaction mFragmentTransaction;

        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;

        public StorageEditFragment()
        {
            mStorageDataService = new StorageDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


        }

        public static StorageEditFragment NewInstance()
        {
            var frag2 = new StorageEditFragment { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.CategoryDetail,container,false);
            mStorageTitle = view.FindViewById<TextView>(Resource.Id.textviewStorageDetailTitle);
            mStorageName = view.FindViewById<EditText>(Resource.Id.editTextStoragesDetailName);
            mStorageArea = view.FindViewById<EditText>(Resource.Id.editTextStoragesDetailArea);
            mStorageDescription = view.FindViewById<EditText>(Resource.Id.editTextStoragesDetailDescription);
            //Set Text Collumn
            mStorageTitle.Text = MainActivity.StaticStorageClass.Name.ToString();
            mStorageName.Text = MainActivity.StaticStorageClass.Name.ToString();
            mStorageArea.Text = MainActivity.StaticStorageClass.Area.ToString();
            mStorageDescription.Text = MainActivity.StaticStorageClass.Description.ToString();
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarAddLocation);
            mButtonSave.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new StoragesFragment(), "Add User");
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
