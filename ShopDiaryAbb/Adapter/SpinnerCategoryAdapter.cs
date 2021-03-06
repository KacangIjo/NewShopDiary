﻿using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Models.ViewModels;

namespace ShopDiaryAbb.Adapter
{
    public class SpinnerCategoryAdapter : BaseAdapter
    {
        readonly Activity mActivity;
        private List<CategoryViewModel> mCategories;
        public SpinnerCategoryAdapter(Activity activity, List<CategoryViewModel> categories)
        {
            mActivity = activity;
            mCategories = categories;
        }
        public override int Count
        {
            get { return mCategories.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = mCategories[position];
            var view = (convertView ?? mActivity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem,
                parent,
                false));
            var name = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            name.Text = item.Name;
            return view;
        }

        public CategoryViewModel GetItemAtPosition(int position)
        {
            return mCategories[position];
        }
    }
}