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
using System.Linq;
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
        public List<LocationViewModel> mLocations;
        public List<UserLocationViewModel> mUserLoc;
        

        private int mSelectedInventory = -1;

        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly StorageDataService mStorageDataService;
        private readonly LocationDataService mLocationDataService;


        private Spinner mSpinnerActiveLocation;
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
            mLocationDataService = new LocationDataService();
            mInventoryDataService = new InventoryDataService();
            mProductDataService = new ProductDataService();
            mStorageDataService = new StorageDataService();
            mInventories = new List<InventoryViewModel>();
            mProducts = new List<ProductViewModel>();
            mStorages = new List<StorageViewModel>();
            mLocations = new List<LocationViewModel>();
            mUserLoc = new List<UserLocationViewModel>();
     
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
            mSpinnerActiveLocation = view.FindViewById<Spinner>(Resource.Id.spinnerHomeActiveLocation);
            mListViewInventory = view.FindViewById<RecyclerView>(Resource.Id.recyclerHomePage);
            mListViewInventory.SetLayoutManager(new LinearLayoutManager(Activity));
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarHome);
            mStorage = view.FindViewById<ImageButton>(Resource.Id.imageButtonHomeStorages);
            mUse = view.FindViewById<ImageButton>(Resource.Id.imageButtonUseItem);
            mAdd = view.FindViewById<ImageButton>(Resource.Id.imageButtonHomeAdd);
            mRunOut = view.FindViewById<ImageButton>(Resource.Id.imageButtonHomeRunOut);
            mShopList = view.FindViewById<ImageButton>(Resource.Id.imageButtonUseItem);
            
            LoadInventoryData();
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

        private void LoadInventoryData()
        {
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;
            //ActiveLocationData
            List<LocationViewModel> tempLocations = new List<LocationViewModel>();
            
            for (int i = 0; LoginPageActivity.mGlobalLocations.Count() > i; i++)
            {
                if (Guid.Parse(LoginPageActivity.mGlobalLocations[i].AddedUserId) == LoginPageActivity.StaticUserClass.ID)
                {
                    mLocations.Add(LoginPageActivity.mGlobalLocations[i]);
                }
            }
            var adapterLocation = new SpinnerLocationAdapter (this.Activity, mLocations);
            mSpinnerActiveLocation.Adapter = adapterLocation;
            mSpinnerActiveLocation.ItemSelected += SpinnerLocation_ItemSelected;
            

            //Inventories Data

            if (LoginPageActivity.mGlobalInventories!= null)
            {
                this.mInventoryAdapter = new HomeInventoryRecycleAdapter(LoginPageActivity.mGlobalInventories,LoginPageActivity.mGlobalProducts, this.Activity);
                this.mInventoryAdapter.ItemClick += OnInventoryClick;
                this.mListViewInventory.SetAdapter(this.mInventoryAdapter);
            }
            UpgradeProgress();
            mProgressBar.Visibility = Android.Views.ViewStates.Invisible;

        }
        private void SpinnerLocation_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            MainActivity.StaticActiveLocationClass = mLocations[e.Position];

            string toast = string.Format("{0} selected", MainActivity.StaticActiveLocationClass.Name);
            Toast.MakeText(this.Activity, toast, ToastLength.Long).Show();
            //LoadRecyclerAdapter(mStorage, mCategory);



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