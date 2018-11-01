using System;
using System.Collections.Generic;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ShopDiaryAbb.Models.ViewModels;
using Java.Lang;
using System.Linq;
using ShopDiaryAbb.Fragments;

namespace ShopDiaryAbb.Adapter
{
    public class CategoryRecyclerAdapter : RecyclerView.Adapter, IFilterable
    {
        private readonly Activity mActivity;
        private List<CategoryViewModel> mCategories;
        private List<CategoryViewModel> mFilteredCategories;
        private int mSelectedPosition = -1;

        public Filter Filter { get; private set; }

        public CategoryRecyclerAdapter(List<CategoryViewModel> categories, Activity activity)
        {
            this.mCategories = categories;
            this.mActivity = activity;
            Filter = new CategoryFilter(this);
        }

        public override int ItemCount => this.mCategories.Count;
        

        public event EventHandler<int> ItemClick;

        private void OnClick(int position)
        {
            this.ItemClick?.Invoke(this, position);
            NotifyItemChanged(position);
            mSelectedPosition = position;
            NotifyItemChanged(position);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (this.mCategories.Count > 0)
            {
                var vh = holder as ViewHolder;
                if (vh != null)
                {
                    var cat = this.mCategories[position];
                    vh.ItemName.Text = cat.Name.ToString();
                    vh.ItemDescription.Text = cat.Description.ToString();
                    vh.ItemView.Selected = (mSelectedPosition == position);
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterCategories, parent, false);
            var vh = new ViewHolder(v, this.OnClick);
            return vh;
        }

        public class ViewHolder : RecyclerView.ViewHolder
        {
            public ViewHolder(IntPtr javaReference, JniHandleOwnership transfer)
                : base(javaReference, transfer)
            {
            }

            public ViewHolder(View itemView, Action<int> listener)
                : base(itemView)
            {
                this.ItemName = itemView.FindViewById<TextView>(Resource.Id.textviewCategoriesAdapterName);
                this.ItemDescription = itemView.FindViewById<TextView>(Resource.Id.textviewCategoriesAdapterDescription);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView ItemName { get; }
            public TextView ItemDescription { get; }

        }
        private class CategoryFilter : Filter
        {
            private readonly CategoryRecyclerAdapter mCategoryAdapter;
            public CategoryFilter(CategoryRecyclerAdapter adapter)
            {
                mCategoryAdapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<CategoryViewModel>();
                if (mCategoryAdapter.mCategories == null)
                    mCategoryAdapter.mCategories = mCategoryAdapter.mFilteredCategories;

                if (constraint == null) return returnObj;

                if (mCategoryAdapter.mCategories != null && mCategoryAdapter.mCategories.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        mCategoryAdapter.mCategories.Where(
                            chemical => chemical.Name.ToLower().Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    mCategoryAdapter.mFilteredCategories = values.ToArray<Java.Lang.Object>()
                        .Select(r => r.ToNetObject<CategoryViewModel>()).ToList();

                mCategoryAdapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }

         
        }

    }
}