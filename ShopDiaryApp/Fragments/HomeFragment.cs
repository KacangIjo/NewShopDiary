using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.Adapter;
using ShopDiaryApp.FragmentsScanner;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ShopDiaryApp.Fragments
{
    public class HomeFragment : Fragment
    {
        #region Layout Properties
        private HomeInventoryRecycleAdapter mInventoryAdapter;
        private RecyclerView mListViewInventory;

        public List<InventoryViewModel> mInventories;
        public List<ProductViewModel> mProducts;
        public List<StorageViewModel> mStorages;
        public List<UserLocationViewModel> mUserLoc;

        private int mSelectedInventory = -1;

        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly StorageDataService mStorageDataService;

        private TextView mExpCounter;
        private TextView mRunOutCounter;
        private TextView mStockCounter;
        private ProgressBar mProgressBar;
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



        }
        public static HomeFragment NewInstance()
        {
            var frag1 = new HomeFragment { Arguments = new Bundle() };
            return frag1;
        }
        #endregion

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.HomeLayout, container, false);
            mListViewInventory = view.FindViewById<RecyclerView>(Resource.Id.recyclerHomePage);
            mListViewInventory.SetLayoutManager(new LinearLayoutManager(Activity));
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarHome);
            mStorage = view.FindViewById<ImageButton>(Resource.Id.imageButtonHomeStorages);
            mUse = view.FindViewById<ImageButton>(Resource.Id.imageButtonUseItem);
            mAdd = view.FindViewById<ImageButton>(Resource.Id.imageButtonHomeAdd);
            mRunOut = view.FindViewById<ImageButton>(Resource.Id.imageButtonHomeRunOut);
            mShopList = view.FindViewById<ImageButton>(Resource.Id.imageButtonUseItem);
            #region button shortcut function
            mStorage.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new StoragesFragment(), "Manage Storages");
            };
            mUse.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new StoragesFragment(), "Manage Storages");
            };
            mAdd.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new AddItemBarcodeFragment(), "Add Item");
            };
            mRunOut.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new StoragesFragment(), "Manage Storages");
            };
            mShopList.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new StoragesFragment(), "Manage Storages");
            };
            #endregion
            return view;
        }

        private async void LoadInventoryData()
        {
            
            mStorages = await mStorageDataService.GetAll();
            this.mInventories = await mInventoryDataService.GetAll();
            this.mProducts = await mProductDataService.GetAll();
            UpgradeProgress();
            if (mInventories != null)
            {
                this.mInventoryAdapter = new HomeInventoryRecycleAdapter(this.mInventories, this.mProducts, this.Activity);
                this.mInventoryAdapter.ItemClick += OnInventoryClick;
                this.mListViewInventory.SetAdapter(this.mInventoryAdapter);
            }
            
        }

        private void OnInventoryClick(object sender, int e)
        {
            mSelectedInventory = e;
        }

        private void UpgradeProgress()
        {
            while (progressvalue < 100)
            {
                progressvalue += 10;
                mProgressBar.Progress = progressvalue;
                Thread.Sleep(300);
            }

        }

        public void ReplaceFragment(Fragment fragment, string tag)
        {
            FragmentTransaction mFragmentTransaction;
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }

    }
}