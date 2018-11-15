using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ShopDiaryAbb.Models.ViewModels;

namespace ShopDiaryAbb.Adapter
{
    public class HomeStorageAdapter : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        
        private readonly List<InventoryViewModel> mInventories;
        private readonly List<StorageViewModel> mStorages;

        private int mSelectedPosition = -1;

        public HomeStorageAdapter(List<StorageViewModel> storages, Activity activity)
        {
            mStorages = storages.ToList();   
            mActivity = activity;
        }

        public override int ItemCount => this.mStorages.Count;

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
            if (holder is ViewHolder vh)
            {
                var storage = this.mStorages[position];
                vh.ItemName.Text = storage.Name;
                vh.ItemView.Selected = (mSelectedPosition == position);
            }
        }
        

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterHomeStorage, parent, false);
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
                this.ItemName = itemView.FindViewById<TextView>(Resource.Id.HomeStorageAdapterItemName);
                this.ExpiredItem = itemView.FindViewById<TextView>(Resource.Id.HomeStorageAdapterWarningCounter);
                this.WarningItem = itemView.FindViewById<TextView>(Resource.Id.HomeStorageAdapterWarningCounter);
                this.GoodItem = itemView.FindViewById<TextView>(Resource.Id.HomeStorageAdapterGoodCounter);


                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView ItemName { get; }
            public TextView ExpiredItem { get; }
            public TextView WarningItem { get; }
            public TextView GoodItem { get; }

        }

    }
}