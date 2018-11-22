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
using ShopDiaryAbb.Models.ViewModels;

namespace ShopDiaryAbb.Adapter
{
    public class ShopItemRecycleAdapter : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        private readonly List<ShopitemViewModel> mShopItem;
        private int mSelectedPosition = -1;

        public ShopItemRecycleAdapter(List<ShopitemViewModel> mshopitem, Activity activity)
        {
            this.mShopItem = mshopitem;
            this.mActivity = activity;
        }

        public override int ItemCount => this.mShopItem.Count;

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
                ShopitemViewModel sl = this.mShopItem[position];
                //vh.ShopListName.Text = sl.Name;
                //vh.ShopListDescription.Text = sl.Description;
              
                vh.ItemView.Selected = (mSelectedPosition == position);
            }

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterShopItem, parent, false);
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
                //this.Name = itemView.FindViewById<TextView>(Resource.Id.textviewShopItemAdapter);
                //this.Desc = itemView.FindViewById<TextView>(Resource.Id.textviewadap);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView Name { get; }
            public TextView Desc { get; }

        }

    }
}