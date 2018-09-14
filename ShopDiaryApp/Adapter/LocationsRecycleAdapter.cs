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
    public class LocationsRecycleAdapter : RecyclerView.Adapter,IFilterable
    {
        private readonly Activity mActivity;
        private List<LocationViewModel> mLocations;
        private List<LocationViewModel> mFilteredLocations;
        private int mSelectedPosition = -1;

        public LocationsRecycleAdapter(List<LocationViewModel> locations, Activity activity)
        {
            this.mLocations = locations;
            this.mFilteredLocations = locations;
            this.mActivity = activity;
            Filter = new LocationFilter(this);
        }

        public override int ItemCount => this.mLocations.Count;

        public Filter Filter { get; private set; }

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
            if (this.mFilteredLocations.Count > 0)
            {
                var vh = holder as ViewHolder;
                if (vh != null)
                {

                    var location = this.mFilteredLocations[position];
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
        private class LocationFilter : Filter
        {
            private readonly LocationsRecycleAdapter mLocationAdapter;
            public LocationFilter(LocationsRecycleAdapter adapter)
            {
                mLocationAdapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<LocationViewModel>();
                if (mLocationAdapter.mLocations == null)
                    mLocationAdapter.mLocations = mLocationAdapter.mFilteredLocations;

                if (constraint == null) return returnObj;

                if (mLocationAdapter.mLocations != null && mLocationAdapter.mLocations.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        mLocationAdapter.mLocations.Where(
                            location => location.Name.ToLower().Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;
            }

            

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    mLocationAdapter.mFilteredLocations = values.ToArray<Java.Lang.Object>()
                        .Select(r => r.ToNetObject<LocationViewModel>()).ToList();

                mLocationAdapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }


        }

    }
}