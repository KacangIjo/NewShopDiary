using System;
using System.Collections.Generic;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ShopDiaryApp.Models.ViewModels;
using Java.Lang;
using System.Linq;
using ShopDiaryApp.Fragments;

namespace ShopDiaryApp.Adapter
{
    public class UserLocationRecyclerAdapter : RecyclerView.Adapter, IFilterable
    {
        private readonly Activity mActivity;
        private List<UserLocationViewModel> mUserLocation;
        private List<UserDataViewModel> mUserData;
        private List<UserLocationViewModel> mFilteredCategories;
        private int mSelectedPosition = -1;

        public Filter Filter { get; private set; }

        public UserLocationRecyclerAdapter(List<UserLocationViewModel> categories,List<UserDataViewModel> userdata, Activity activity)
        {
            this.mUserLocation = categories;
            this.mUserData = userdata;
            this.mActivity = activity;
        }

        public override int ItemCount => this.mUserLocation.Count;
        

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
            if (this.mUserLocation.Count > 0)
            {
                var vh = holder as ViewHolder;
                if (vh != null)
                {
                    var user = this.mUserLocation[position];
                    for(int i = 0; i < mUserData.Count(); i++)
                    {
                        if (user.RegisteredUser == mUserData[i].UserId)
                        {
                            vh.ItemName.Text = mUserData[i].Email;
                        }
                    }
                   
                    vh.ItemDescription.Text = "Create Read Update Delete";
                    vh.ItemView.Selected = (mSelectedPosition == position);
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterSharedLocations, parent, false);
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
                this.ItemName = itemView.FindViewById<TextView>(Resource.Id.textViewAdapterSharedLocationName);
                this.ItemDescription = itemView.FindViewById<TextView>(Resource.Id.textViewAdapterSharedLocationStatus);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView ItemName { get; }
            public TextView ItemDescription { get; }

        }

    }
}