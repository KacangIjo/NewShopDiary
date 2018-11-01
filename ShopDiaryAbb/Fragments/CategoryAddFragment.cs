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
using ShopDiaryAbb.Services;
using ShopDiaryProject.Domain.Models;

namespace ShopDiaryAbb.Fragments
{
    public class CategoryAddFragment : Android.Support.V4.App.Fragment
    {
        private EditText mCategoryName;
        private EditText mCategoryDescription;
        private Button mButtonAdd;
        private Button mButtonCancel;
        private ProgressBar mProgressBar;

        CategoryDataService mCategoryDataService;

        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;

        public CategoryAddFragment()
        {
            mCategoryDataService = new CategoryDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
           
        }

        

        public static CategoryAddFragment NewInstance()
        {
            var frag2 = new CategoryAddFragment { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ManageCategoryAdd,container,false);
            mButtonAdd = view.FindViewById<Button>(Resource.Id.buttonAddCategory);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonCancelCategory);
            mCategoryName = view.FindViewById<EditText>(Resource.Id.editTextAddCategoryName);
            mCategoryDescription = view.FindViewById<EditText>(Resource.Id.editTextAddCategoryDescription);
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarAddCategory);
            mButtonAdd.Click += MButtonAdd_Click;
            return view;
        }

        private void MButtonAdd_Click(object sender, EventArgs e)
        {
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;
            Category newCategory = new Category()
            {
                Name = mCategoryName.Text,
                Description = mCategoryDescription.Text,
                UserId = mAuthorizedId,
                CreatedUserId = mAuthorizedId.ToString(),
                AddedUserId = mAuthorizedId.ToString()

            };

            new Thread(new ThreadStart(delegate
            {
                UpgradeProgress();
                var isAdded = mCategoryDataService.Add(newCategory);

                if (isAdded)
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Category Added", ToastLength.Long).Show());
                    mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
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
    }
}