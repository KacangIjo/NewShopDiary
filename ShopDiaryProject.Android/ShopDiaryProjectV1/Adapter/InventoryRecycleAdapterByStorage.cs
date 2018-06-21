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
using ShopDiaryProject.Android.Models.ViewModels;

namespace ShopDiaryProjectV1.Adapter
{
    public class InventoryRecycleAdapterByStorage : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        private readonly List<ProductViewModel> mProducts;
        private readonly List<InventoryViewModel> mInventories;
        private readonly Guid StorageId;
        private int mSelectedPosition = -1;

        public InventoryRecycleAdapterByStorage(Guid storageId,List<InventoryViewModel> inventories,List<ProductViewModel> products, Activity activity)
        {
            this.StorageId = storageId;
            this.mProducts = products;
            this.mInventories = inventories;
            this.mActivity = activity;
        }

        public override int ItemCount => this.mInventories.Count;

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
            if (this.mInventories.Count > 0)
            {
                var vh = holder as ViewHolder;
                if (vh != null)
                {
                    for(int j=0;mInventories.Count()>j;j++)
                    {
                       
                        if (mInventories[j].StorageId==StorageId)
                        {
                            var inv = this.mInventories[position];
                            vh.ItemName.Text = inv.ItemName.ToString();
                            vh.ItemExpDate.Text = inv.ExpirationDate.ToString();
                            vh.ItemView.Selected = (mSelectedPosition == position);
                        }

                    }
                   
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterItemStatusMain, parent, false);
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
                this.ItemName = itemView.FindViewById<TextView>(Resource.Id.textViewMainListName);
                this.ItemExpDate = itemView.FindViewById<TextView>(Resource.Id.textViewMainListExpDate);
                this.ItemQuantity = itemView.FindViewById<TextView>(Resource.Id.textViewMainListStatus);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView ItemName { get; }
            public TextView ItemExpDate { get; }
            public TextView ItemQuantity { get; }

        }

    }
}