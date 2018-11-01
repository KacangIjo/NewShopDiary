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
    public class CategoryEditFragment : Fragment
    {
        private TextView mCategoryTitle;
        private EditText mCategoryName;
        private EditText mCategoryDescription;
        private ImageButton mButtonSave;
        private ImageButton mButtonDelete;
        private ProgressBar mProgressBar;

        LocationDataService mLocationDataService;
        private FragmentTransaction mFragmentTransaction;

        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;

        public CategoryEditFragment()
        {
            mLocationDataService = new LocationDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            

        }

        public static CategoryEditFragment NewInstance()
        {
            var frag2 = new CategoryEditFragment { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.CategoryDetail,container,false);
            //mCategoryTitle = view.FindViewById<TextView>(Resource.Id.textviewCategoryDetailTitle);
            //mCategoryName = view.FindViewById<EditText>(Resource.Id.editTextCategoryDetailName);
            mCategoryDescription = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailDescription);
            mCategoryTitle.Text = MainActivity.StaticLocationClass.Name.ToString();
            mCategoryName.Text = MainActivity.StaticLocationClass.Name.ToString();
            mCategoryDescription.Text = MainActivity.StaticLocationClass.Description.ToString();
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarAddLocation);
            mButtonSave.Click += (object sender, EventArgs args) =>
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