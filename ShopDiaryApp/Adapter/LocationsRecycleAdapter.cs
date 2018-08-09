using System;
using System.Collections.Generic;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ShopDiaryApp.Models.ViewModels;

namespace ShopDiaryApp.Adapter
{
    public class LocationsRecycleAdapter : RecyclerView.Adapter
    {
        private readonly Activity mActivity;
        private readonly List<LocationViewModel> mLocations;
        private int mSelectedPosition = -1;

        public LocationsRecycleAdapter(List<LocationViewModel> locations, Activity activity)
        {
            this.mLocations = locations;
            this.mActivity = activity;
        }

        public override int ItemCount => this.mLocations.Count;

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
            if (this.mLocations.Count > 0)
            {
                var vh = holder as ViewHolder;
                if (vh != null)
                {

                    var location = this.mLocations[position];
                    vh.LocationName.Text = location.Name;
                    vh.LocationAddress.Text = location.Address;
                    vh.LocationDescription.Text = location.Description;
                    vh.ItemView.Selected = (mSelectedPosition == position);



                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterLocations, parent, false);
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
                this.LocationName = itemView.FindViewById<TextView>(Resource.Id.textviewLocationsAdapterName);
                this.LocationAddress = itemView.FindViewById<TextView>(Resource.Id.textviewLocationAdapterAddress);
                this.LocationDescription = itemView.FindViewById<TextView>(Resource.Id.textviewLocationAdapterDescription);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView LocationName { get; }
            public TextView LocationAddress { get; }
            public TextView LocationDescription { get; }

        }

    }
}