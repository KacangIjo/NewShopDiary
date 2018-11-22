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
    public class ShopListsRecylceAdapter : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        private readonly List<ShoplistViewModel> mShopList;
        private int mSelectedPosition = -1;

        public ShopListsRecylceAdapter(List<ShoplistViewModel> mshoplist, Activity activity)
        {
            this.mShopList = mshoplist;
            this.mActivity = activity;
        }

        public override int ItemCount => this.mShopList.Count;

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
                var storage = this.mShopList[position];
                //vh.StorageName.Text = storage.Name;
                //vh.StorageArea.Text = storage.Area;
                //vh.StorageDescription.Text = storage.Description;
                vh.ItemView.Selected = (mSelectedPosition == position);
            }

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterShopList, parent, false);
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
                this.ShopListName = itemView.FindViewById<TextView>(Resource.Id.textviewShopListDetailTitle);
                this.ShopListDescription = itemView.FindViewById<TextView>(Resource.Id.textviewShoplistAdapterDescription);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView ShopListName { get; }
            public TextView ShopListDescription { get; }

        }

    }
}