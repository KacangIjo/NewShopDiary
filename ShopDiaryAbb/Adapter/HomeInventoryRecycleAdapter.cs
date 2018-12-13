using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ShopDiaryAbb.Models.ViewModels;
using Android.Graphics;

namespace ShopDiaryAbb.Adapter
{
    public class HomeInventoryRecycleAdapter : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        private readonly ProductViewModel mSelectedProduct;
        private readonly List<InventoryViewModel> mInventories;
        private int mSelectedPosition = -1;

        public HomeInventoryRecycleAdapter(List<InventoryViewModel> inventories, Activity activity)
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

            if (holder is ViewHolder vh)
            {
                var inv = this.mInventories[position];
                vh.ItemName.Text = inv.ItemName.ToString();
                if (inv.ExpirationDate >= DateTime.Today)
                {
                    vh.ItemStatus.Text = inv.ExpirationDate.ToString();
                    vh.ItemStatus.SetTextColor(Color.Red);
                }
                else 
                {
                    vh.ItemStatus.Text = inv.ExpirationDate.ToString();
                    vh.ItemStatus.SetTextColor(Color.Orange);
                }

                vh.ItemView.Selected = (mSelectedPosition == position);
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

            public ViewHolder(View itemView, Action<int> listener)
                : base(itemView)
            {
                this.ItemName = itemView.FindViewById<TextView>(Resource.Id.HomeInventoriesAdapterItemName);
                this.ItemStatus = itemView.FindViewById<TextView>(Resource.Id.HomeInventoriesAdapterExpStatus);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView ItemName { get; }
            public TextView ItemStatus { get; }

        }

    }
}