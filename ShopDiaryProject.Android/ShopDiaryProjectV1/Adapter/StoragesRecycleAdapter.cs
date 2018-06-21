using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.Android.Models.ViewModels;

namespace ShopDiaryProjectV1.Adapter
{
    public class StoragesRecycleAdapter : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        private readonly List<StorageViewModel> mStorages;
        private int mSelectedPosition = -1;

        public StoragesRecycleAdapter(List<StorageViewModel> storages, Activity activity)
        {
            this.mStorages = storages;
            this.mActivity = activity;
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
            if (this.mStorages.Count > 0)
            {
                var vh = holder as ViewHolder;
                if (vh != null)
                {
                    var storage = this.mStorages[position];
                    vh.StorageName.Text = storage.Name;
                    vh.StorageArea.Text = storage.Area;
                    vh.StorageDescription.Text = storage.Description;
                    vh.ItemView.Selected = (mSelectedPosition == position);
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterStorages, parent, false);
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
                this.StorageName = itemView.FindViewById<TextView>(Resource.Id.textViewAdapterStorageName);
                this.StorageArea = itemView.FindViewById<TextView>(Resource.Id.textViewAdapterStorageArea);
                this.StorageDescription = itemView.FindViewById<TextView>(Resource.Id.textViewAdapterStorageDescription);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView StorageName { get; }
            public TextView StorageArea { get; }
            public TextView StorageDescription { get; }

        }

    }
}