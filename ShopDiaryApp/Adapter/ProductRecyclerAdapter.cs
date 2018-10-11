using System;
using System.Collections.Generic;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ShopDiaryApp.Models.ViewModels;
using Java.Lang;
using System.Linq;
using ShopDiaryApp.Fragments;

namespace ShopDiaryApp.Adapter
{
    public class ProductRecyclerAdapter : RecyclerView.Adapter, IFilterable
    {
        private readonly Activity mActivity;
        private List<ProductViewModel> mProducts;
        private List<ProductViewModel> mFilteredProducts;
        private int mSelectedPosition = -1;

        public Filter Filter { get; private set; }

        public ProductRecyclerAdapter(List<ProductViewModel> products, Activity activity)
        {
            this.mProducts = products;
            this.mActivity = activity;
            Filter = new ProductFilter(this);
        }

        public override int ItemCount => this.mProducts.Count;
        

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
            if (this.mProducts.Count > 0)
            {
                var vh = holder as ViewHolder;
                if (vh != null)
                {
                    var cat = this.mProducts[position];
                    vh.ItemName.Text = cat.Name.ToString();
                    vh.ItemDescription.Text = cat.BarcodeId.ToString();
                    vh.ItemView.Selected = (mSelectedPosition == position);
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterProducts, parent, false);
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
                this.ItemName = itemView.FindViewById<TextView>(Resource.Id.textviewProductsAdapterName);
                this.ItemDescription = itemView.FindViewById<TextView>(Resource.Id.textviewProductsAdapterBarcodeId);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView ItemName { get; }
            public TextView ItemDescription { get; }

        }
        private class ProductFilter : Filter
        {
            private readonly ProductRecyclerAdapter mProductRecyclerAdapter;
            public ProductFilter(ProductRecyclerAdapter adapter)
            {
                mProductRecyclerAdapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<ProductViewModel>();
                if (mProductRecyclerAdapter.mProducts == null)
                    mProductRecyclerAdapter.mProducts = mProductRecyclerAdapter.mFilteredProducts;

                if (constraint == null) return returnObj;

                if (mProductRecyclerAdapter.mProducts != null && mProductRecyclerAdapter.mProducts.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        mProductRecyclerAdapter.mProducts.Where(
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
                    mProductRecyclerAdapter.mFilteredProducts = values.ToArray<Java.Lang.Object>()
                        .Select(r => r.ToNetObject<ProductViewModel>()).ToList();

                mProductRecyclerAdapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }

         
        }

    }
}