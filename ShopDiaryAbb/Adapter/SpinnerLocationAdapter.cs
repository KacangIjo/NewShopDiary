using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Models.ViewModels;

namespace ShopDiaryAbb.Adapter
{
    public class SpinnerLocationAdapter : BaseAdapter
    {
        readonly Activity mActivity;
        private List<LocationViewModel> mLocations;
        public SpinnerLocationAdapter(Activity activity, List<LocationViewModel> locations)
        {
            mActivity = activity;
            mLocations = locations;
        }
        public override int Count
        {
            get { return mLocations.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = mLocations[position];
            var view = (convertView ?? mActivity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem,
                parent,
                false));
            var name = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            name.Text = item.Name;
            return view;
        }

        public LocationViewModel GetItemAtPosition(int position)
        {
            return mLocations[position];
        }
    }
}