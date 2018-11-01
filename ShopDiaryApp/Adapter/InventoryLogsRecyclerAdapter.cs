using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ShopDiaryApp.Models.ViewModels;

namespace ShopDiaryApp.Adapter
{
    public class InventoryLogsRecyclerAdapter : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        private readonly List<InventoryViewModel> mInventories;
        private readonly List<InventorylogViewModel> mInventoryLogs;
        private int mSelectedPosition = -1;

        public InventoryLogsRecyclerAdapter(List<InventoryViewModel> inventories, List<InventorylogViewModel> inventorylogs, Activity activity)
        {
            mInventoryLogs = inventorylogs;
            mInventories = inventories;
            mActivity = activity;
        }

        public override int ItemCount => this.mInventoryLogs.Count;

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
            if (this.mInventoryLogs.Count > 0)
            {
                var vh = holder as ViewHolder;
                if (vh != null)
                {
                    var inv = this.mInventoryLogs[position];
                            vh.Description.Text = inv.Description;
                            vh.Date.Text = inv.LogDate.ToString();
                        
                    vh.ItemView.Selected = (mSelectedPosition == position);
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterInventoriesLog, parent, false);
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
               
                this.Description = itemView.FindViewById<TextView>(Resource.Id.textviewInventoryLogsDescription);
                this.Date = itemView.FindViewById<TextView>(Resource.Id.textviewInventoryLogsDate);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

          
            public TextView Description { get; }
            public TextView Date { get; }

        }

    }
}