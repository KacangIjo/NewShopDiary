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
        private readonly string mStatus;
        private readonly string mColor;
        private readonly List<InventoryViewModel> mInventories;
        

        private int mSelectedPosition = -1;

        public HomeAdapter(List<InventoryViewModel> minventories, string mstatus, string mcolor, Activity activity)
        {
            mInventories = minventories;
            mStatus = mstatus;
            mColor = mcolor;
            
            mActivity = activity;
            
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
           
            if (mInventories.Count > 0)
            {
                if (holder is ViewHolder vh)
                {
                    
                    var inv = this.mInventories[position];
                    vh.ItemName.Text = inv.ItemName;
                    vh.Status.Text = mStatus;
                    vh.Status.SetTextColor(Color.ParseColor(mColor));
                    
                    //if (position % 2 == 0)
                    //{
                        
                    //    vh.ItemName.Text = inv.ItemName;
                    //    vh.Status.Text = mStatus;
                    //    vh.Status.SetTextColor(Color.ParseColor(mColor));
                    //    vh.SelectedRow.SetBackgroundColor(Color.LightGray);
                    //}
                    //else
                    //{
                    //    vh.ItemName.Text = inv.ItemName;
                    //    vh.Status.Text = mStatus;
                    //    vh.Status.SetTextColor(Color.ParseColor(mColor));
                    //    vh.SelectedRow.SetBackgroundColor(Color.WhiteSmoke);
                    //}
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
                this.Status = itemView.FindViewById<TextView>(Resource.Id.homeAdapterStatus);
                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView ItemName { get; }
            public TextView Status { get; }
            public LinearLayout SelectedRow { get; }

        }

    }
}