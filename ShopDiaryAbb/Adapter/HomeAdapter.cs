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
    public class HomeAdapter : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        private readonly List<InventoryViewModel> mInventories;
      
        private int mSelectedPosition = -1;

        public HomeAdapter(List<InventoryViewModel> minventory, Activity activity)
        {
            this.mInventories = minventory;
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
                int ExpCounter = 0;
                if (holder is ViewHolder vh)
                {
                    
                    var inv = this.mInventories[position];
                    if (position % 2 == 0)
                    {
                        
                        vh.ItemName.Text = inv.ItemName;
                        vh.ExpiredItem.Text = (ExpCounter + 1).ToString();
                        vh.WarningItem.Text = (ExpCounter + 1).ToString();
                        vh.GoodItem.Text = (ExpCounter + 1).ToString();
                        vh.SelectedRow.SetBackgroundColor(Color.LightGray);
                    }
                    else
                    {
                        vh.ItemName.Text = inv.ItemName;
                        vh.ExpiredItem.Text = (ExpCounter + 1).ToString();
                        vh.WarningItem.Text = (ExpCounter + 1).ToString();
                        vh.GoodItem.Text = (ExpCounter + 1).ToString();
                        vh.SelectedRow.SetBackgroundColor(Color.WhiteSmoke);
                    }
                    vh.ItemView.Selected = (mSelectedPosition == position);
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterHome, parent, false);
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
                this.SelectedRow = itemView.FindViewById<LinearLayout>(Resource.Id.homeAdapterLayout);
                this.ItemName = itemView.FindViewById<TextView>(Resource.Id.homeAdapterItemName);
                this.ExpiredItem = itemView.FindViewById<TextView>(Resource.Id.homeAdapterExpiredCounter);
                this.WarningItem = itemView.FindViewById<TextView>(Resource.Id.HomeAdapterWarningCounter);
                this.GoodItem = itemView.FindViewById<TextView>(Resource.Id.HomeAdapterGoodCounter);
                //this.SelectedRow = itemView.FindViewById<LinearLayout>(Resource.Id.homeAdapterLayoutParent);
                //this.SelectedRow.Click += (sender, e) => SelectedRow.Selected = true;
                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView ItemName { get; }
            public TextView ExpiredItem { get; }
            public TextView WarningItem { get; }
            public TextView GoodItem { get; }
            public LinearLayout SelectedRow { get; }

        }

    }
}