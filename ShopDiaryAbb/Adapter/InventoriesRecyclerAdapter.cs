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
    public class InventoriesRecyclerAdapter : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        private readonly List<InventoryViewModel> mInventories;
        private int mSelectedPosition = -1;

        public InventoriesRecyclerAdapter(List<InventoryViewModel> inventories, Activity activity)
        {
       
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
                if (holder is ViewHolder vh)
                {
                    var inv = this.mInventories[position];
                    vh.ItemName.Text = inv.ItemName.ToString();
                    vh.Stock.Text = inv.Stock.ToString();
                    vh.ExpiredDate.Text = inv.ExpirationDate.ToString("yyyy-MM-dd");
                    if (inv.ExpirationDate < DateTime.Now)
                    {
                        vh.ItemStatus.SetBackgroundColor(Android.Graphics.Color.Red);
                        vh.ItemStatus.Text = "Expired";
                    }
                    if (inv.ExpirationDate > DateTime.Now)
                    {
                        vh.ItemStatus.SetBackgroundColor(Android.Graphics.Color.Green);
                        vh.ItemStatus.Text = "Good";
                    }
                    vh.ItemView.Selected = (mSelectedPosition == position);
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterHomeInventories, parent, false);
            var vh = new ViewHolder(v, this.OnClick);
            return vh;
        }

        public class ViewHolder : RecyclerView.ViewHolder
        {
            public ViewHolder(IntPtr javaReference, JniHandleOwnership transfer)
                : base(javaReference, transfer)
            {
            }

            public ViewHolder(View itemView, Action<int> listener): base(itemView)
            {
                ItemName = itemView.FindViewById<TextView>(Resource.Id.HomeInventoriesAdapterItemName);
                Stock = itemView.FindViewById<TextView>(Resource.Id.HomeInventoriesAdapterStock);
                ExpiredDate = itemView.FindViewById<TextView>(Resource.Id.HomeInventoriesAdapterExpiredDate);
                ItemStatus = itemView.FindViewById<TextView>(Resource.Id.HomeInventoriesAdapterExpStatus);
                
                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView ItemName { get; }
            public TextView Stock { get; }
            public TextView ExpiredDate { get; }
            public TextView ItemStatus { get; }

        }

    }
}