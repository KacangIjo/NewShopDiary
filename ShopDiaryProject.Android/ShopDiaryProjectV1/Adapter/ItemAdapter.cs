//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Java.Lang;
//using ShopDiaryProjectV1.Class;

//namespace ShopDiaryProjectV1
//{
//    public class ItemViewHolder : Java.Lang.Object
//    {
//        public TextView textName { get; set; }
//        public TextView textDate { get; set; }
//        public TextView textDescription { get; set; }
//    }
//    public class ItemAdapter : BaseAdapter
//    {
//        private Activity _activity;
//        private List<Product> _products;
//        public ItemAdapter(Activity activity, List<Product> products)
//        {
//            this._activity = activity;
//            this._products = products;
//        }
//        public override int Count
//        {
//            get
//            {
//                return _products.Count;
//            }
            
//        }

//        public override Java.Lang.Object GetItem(int position)
//        {
//            return null;
//        }

//        public override long GetItemId(int position)
//        {
//            return _products[position].ID;
//        }

//        public override View GetView(int position, View convertView, ViewGroup parent)
//        {
//            var view = convertView ?? _activity.
//                LayoutInflater.Inflate(Resource.Layout.LocationsAdapterTemplate, parent, false);
//            var textName = view.FindViewById<TextView>(Resource.Id.textItemAdapterName);
//            var Date = view.FindViewById<TextView>(Resource.Id.textAdapterItemDate);
            

//            textName.Text = _products[position].Name;
//            Date.Text = System.DateTime.Today.ToString();
//            return view;
//        }
//    }
//}