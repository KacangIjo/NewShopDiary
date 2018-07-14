//using System;
//using System.Collections.Generic;

//using Android.App;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.Support.V7.Widget;
//using ShopDiaryApp.Models.ViewModels;

//namespace ShopDiaryApp.Adapter
//{
//    public class CategoryRecyclerAdapter : RecyclerView.Adapter
//    {
//        private readonly Activity mActivity;
//        private readonly List<CategoryViewModel> mCategories;
//        private int mSelectedPosition = -1;

//        public CategoryRecyclerAdapter(List<CategoryViewModel> categories, Activity activity)
//        {
//            this.mCategories = categories;
//            this.mActivity = activity;
//        }

//        public override int ItemCount => this.mCategories.Count;

//        public event EventHandler<int> ItemClick;

//        private void OnClick(int position)
//        {
//            this.ItemClick?.Invoke(this, position);
//            NotifyItemChanged(position);
//            mSelectedPosition = position;
//            NotifyItemChanged(position);
//        }

//        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
//        {
//            if (this.mCategories.Count > 0)
//            {
//                var vh = holder as ViewHolder;
//                if (vh != null)
//                {
//                    var cat = this.mCategories[position];
//                    vh.ItemName.Text = cat.Name.ToString();
//                    vh.ItemDescription.Text = cat.Description.ToString();
//                    vh.ItemView.Selected = (mSelectedPosition == position);
//                }
//            }
//        }

//        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
//        {
//            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AdapterCategories, parent, false);
//            var vh = new ViewHolder(v, this.OnClick);
//            return vh;
//        }
        
//        public class ViewHolder : RecyclerView.ViewHolder
//        {
//            public ViewHolder(IntPtr javaReference, JniHandleOwnership transfer)
//                : base(javaReference, transfer)
//            {
//            }

//            public ViewHolder(View itemView, Action<int> listener)
//                : base(itemView)
//            {
//                this.ItemName = itemView.FindViewById<TextView>(Resource.Id.textViewAdapterCategoryName);
//                this.ItemDescription = itemView.FindViewById<TextView>(Resource.Id.textViewAdapterCategoryDescription);

//                itemView.Click += (sender, e) => listener(this.LayoutPosition);
//            }

//            public TextView ItemName { get; }
//            public TextView ItemDescription { get; }

//        }

//    }
//}