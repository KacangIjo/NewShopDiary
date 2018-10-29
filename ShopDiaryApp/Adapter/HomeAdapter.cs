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
    public class HomeAdapter : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        private readonly List<ProductViewModel> mProducts;
        private readonly List<InventoryViewModel> mInventories;
        private readonly LocationViewModel mSelectedLocation;
        private readonly List<StorageViewModel> mStorages;
        private int mSelectedPosition = -1;

        public HomeAdapter(List<InventoryViewModel> inventories, List<ProductViewModel> products,List<StorageViewModel> storages,LocationViewModel selectedlocation, Activity activity)
        {
            this.mProducts = products;
            this.mInventories = inventories;
            this.mSelectedLocation = selectedlocation;
            this.mStorages = storages;
            this.mActivity = activity;
        }

        public override int ItemCount => this.mProducts.Count;

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
            if (this.mProducts.Count > 0)
            {
                int ExpCounter = 0;
                var vh = holder as ViewHolder;
                if (vh != null)
                {
                    var inv = this.mProducts[position];

                    vh.ItemName.Text = inv.Name;
                    for (int i = 0; i < mInventories.Count(); i++)
                    {

                        if (mInventories[i].ProductId == inv.Id)
                        {
                            if (mInventories[i].ExpirationDate == DateTime.Now)
                            {
                                vh.ExpiredItem.Text = (ExpCounter + 1).ToString();
                            }
                            else
                            {
                                vh.WarningItem.Text = (ExpCounter + 1).ToString();
                                vh.GoodItem.Text = (ExpCounter + 1).ToString();
                            }
                        }
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