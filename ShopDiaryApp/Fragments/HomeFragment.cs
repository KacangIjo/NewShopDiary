using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.Adapter;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;
using System;
using System.Collections.Generic;

namespace ShopDiaryApp.Fragments
{
    public class HomeFragment : Fragment
    {
        #region Layout Properties
        private MainAdapter mInventoryAdapter;
        private RecyclerView mListViewInventory;

        public List<InventoryViewModel> mInventories;
        public List<ProductViewModel> mProducts;
        public List<StorageViewModel> mStorages;
        public List<RoleLocationViewModel> mRoles;
        public List<UserLocationViewModel> mUserLoc;

        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly StorageDataService mStorageDataService;

        private TextView mExpCounter;
        private TextView mRunOutCounter;
        private TextView mStockCounter;
        private ImageButton mStorage;
        private ImageButton mUse;
        private ImageButton mAdd;
        private ImageButton mShopList;
        private ImageButton mRunOut;

        public event EventHandler OptionButtonWasClicked;

        private int progressvalue = 0;
        #endregion
        public HomeFragment()
        {
            mInventoryDataService = new InventoryDataService();
            mProductDataService = new ProductDataService();
            mStorageDataService = new StorageDataService();
        }



        #region Fragment Properties
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            #region field

            #endregion


            //mStorage.Click += BtnOption_Click;
            //mUse.Click += BtnOption_Click;
            //mAdd.Click += BtnOption_Click;
            //mShopList.Click += BtnOption_Click;
            //mRunOut.Click += BtnOption_Click;



        }
        public static HomeFragment NewInstance()
        {
            var frag1 = new HomeFragment { Arguments = new Bundle() };
            return frag1;
        }
        #endregion

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view= inflater.Inflate(Resource.Layout.HomeLayout, null);
            mRunOut = view.FindViewById<ImageButton>(Resource.Id.imageButtonHomeRunOut);
            mRunOut.Click += (object sender, EventArgs args) =>
            {
                
                Toast.MakeText(this.Activity, "OK", ToastLength.Long).Show();
            };

           return view;
        }
       
    }
}