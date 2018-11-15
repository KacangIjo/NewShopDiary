using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.FragmentsScanner;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace ShopDiaryAbb.Fragments
{
    public class HomeFragment : Fragment
    {
        #region Layout Field
        private HomeAdapter mInventoryAdapter;
        private RecyclerView mListViewInventory;
        
        public List<ProductViewModel> mProducts;
        public List<LocationViewModel> mLocations;
        public List<UserLocationViewModel> mUserLoc;
        public List<InventoryViewModel> InventoriesNearlyExpired;
        public List<InventoryViewModel> InventoriesExpired;
        public List<InventoryViewModel> InventoriesGood;

        private int mSelectedInventory = -1;

        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly StorageDataService mStorageDataService;
        private readonly LocationDataService mLocationDataService;


        private Spinner mSpinnerActiveLocation;
        private TextView mExpCounter;
        private TextView mRunOutCounter;
        private TextView mStockCounter;
        private TextView mHomeRecyclerTitle;
        private LinearLayout mFilterExpCounter;
        private LinearLayout mFilterRunOutCounter;
        private LinearLayout mFilterGoodCounter;

        //private ImageButton mStorage;
        //private ImageButton mUse;
        //private ImageButton mAdd;
        //private ImageButton mShopList;
        //private ImageButton mRunOut;

        public static InventoryViewModel mHomeSelectedInventory;
        public static List<StorageViewModel> mStorages;
        public static List<InventoryViewModel> mInventories;

        public event EventHandler OptionButtonWasClicked;
        
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
            mRunOutCounter = view.FindViewById<TextView>(Resource.Id.textViewMainRunningOutCounter);
            mExpCounter = view.FindViewById<TextView>(Resource.Id.textViewMainExpiredCounter);
            mStockCounter = view.FindViewById<TextView>(Resource.Id.textViewMainGoodConditionCounter);
            mHomeRecyclerTitle= view.FindViewById<TextView>(Resource.Id.textViewHomeRecyclerTitle);
            mFilterExpCounter = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutExpiredCounter);
            mFilterRunOutCounter = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutNearlyExpiredCounter);
            mFilterGoodCounter = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutGoodCondition);
            
            LoadInventoryData();

            #region
            
            List<StorageViewModel> StorageByLocation = LoginPageActivity.mGlobalStorages.Where(s => s.LocationId == LoginPageActivity.StaticActiveLocationClass.Id).ToList();
          
           
            
            
            //mRunOutCounter.Text = InventoriesNearlyExpired.Count.ToString();
            //mStockCounter.Text = InventoriesGood.Count.ToString();
            //mExpCounter.Text = InventoriesExpired.Count.ToString();
            mFilterExpCounter.Click += (object sender, EventArgs args) =>
            {
                #region textField
                mHomeRecyclerTitle.Text = "Expired Items";
                mHomeRecyclerTitle.SetTextColor(Android.Graphics.Color.Red);
                #endregion
                List<InventoryViewModel> temp = LoginPageActivity.mGlobalInventories;
                InventoriesExpired = temp
               .Join(mStorages, i => i.StorageId, s => s.Id, (i, s) => i)
               .Where(i => i.ExpirationDate < DateTime.Now)
               .GroupBy(s => s.ProductId)
               .Select(g => g.First()).ToList();
                mInventories =InventoriesExpired;
                this.mInventoryAdapter = new HomeAdapter(InventoriesExpired,"Expired","Red", this.Activity);
                this.mInventoryAdapter.ItemClick += OnInventoryClick;
                this.mListViewInventory.SetAdapter(this.mInventoryAdapter);
            };
            mFilterRunOutCounter.Click += (object sender, EventArgs args) =>
            {
                #region textField
                mHomeRecyclerTitle.Text = "Nearly Expired Items";
                mHomeRecyclerTitle.SetTextColor(Android.Graphics.Color.Yellow);
                #endregion
                List<InventoryViewModel> temp = LoginPageActivity.mGlobalInventories;
                InventoriesNearlyExpired = temp
                .Join(mStorages, i => i.StorageId, s => s.Id, (i, s) => i)
                .Where(i => i.ExpirationDate > DateTime.Now)
                .GroupBy(s => s.ProductId)
                .Select(g => g.First()).ToList();
                mInventories = InventoriesNearlyExpired;
                this.mInventoryAdapter = new HomeAdapter(InventoriesNearlyExpired, "Nearly Expired","Yellow", this.Activity);
                this.mInventoryAdapter.ItemClick += OnInventoryClick;
                this.mListViewInventory.SetAdapter(this.mInventoryAdapter);
            };
            mFilterGoodCounter.Click += (object sender, EventArgs args) =>
            {
                #region textField
                mHomeRecyclerTitle.Text = "Good Items";
                mHomeRecyclerTitle.SetTextColor(Android.Graphics.Color.Green);
                #endregion
                List<InventoryViewModel> temp = LoginPageActivity.mGlobalInventories;
                InventoriesGood = temp.Join(mStorages, i => i.StorageId, s => s.Id, (i, s) => i)
                .Where(i => i.ExpirationDate > DateTime.Now)
                .GroupBy(s => s.ProductId)
                .Select(g => g.First())
                .ToList();
                mInventories = InventoriesGood;
                this.mInventoryAdapter = new HomeAdapter(InventoriesGood, "Good","Green", this.Activity);
                this.mInventoryAdapter.ItemClick += OnInventoryClick;
                this.mListViewInventory.SetAdapter(this.mInventoryAdapter);
            };
            #endregion



           
            return view;
        }

        public  void LoadInventoryData()
        {
            mLocations = LoginPageActivity.mGlobalLocations.Where(l => l.AddedUserId == LoginPageActivity.StaticUserClass.ID.ToString()).ToList();
            if (mLocations != null)
            {
                var adapterLocation = new SpinnerLocationAdapter(this.Activity, mLocations);
                mSpinnerActiveLocation.Adapter = adapterLocation;
                mSpinnerActiveLocation.ItemSelected += SpinnerLocation_ItemSelected;
                mSpinnerActiveLocation.SetSelection(0);
            }
            

        }
        private void SpinnerLocation_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            LoginPageActivity.StaticActiveLocationClass = mLocations[e.Position];
            mStorages = LoginPageActivity.mGlobalStorages.Where(s => s.LocationId == mLocations[e.Position].Id).ToList();
            List<InventoryViewModel> temp = LoginPageActivity.mGlobalInventories;

            List<InventoryViewModel> InventoriesByLocation = temp.Join(mStorages, i => i.StorageId, s => s.Id, (i, s) => i)
                .Where(i=> i.ExpirationDate>DateTime.Now)
                .GroupBy(s => s.ProductId)
                .Select(g=>g.First())
                .ToList();
            #region textField
            mHomeRecyclerTitle.Text = "Expired Items";
            mHomeRecyclerTitle.SetTextColor(Android.Graphics.Color.Red);
            #endregion
            mInventories = InventoriesByLocation;
            if (LoginPageActivity.mGlobalInventories != null)
            {
                this.mInventoryAdapter = new HomeAdapter(InventoriesByLocation,"Expired","Red", this.Activity);
                this.mInventoryAdapter.ItemClick += OnInventoryClick;
                this.mListViewInventory.SetAdapter(this.mInventoryAdapter);
            }
        }
        private void OnInventoryClick(object sender, int e)
        {
            mSelectedInventory = e;
            mHomeSelectedInventory = mInventories[mSelectedInventory];
            
            ReplaceFragment(new HomeStoragesFragment(), mHomeSelectedInventory.ItemName);
        }


        public void ReplaceFragment(Fragment fragment, string tag)
        {
            FragmentTransaction mFragmentTransaction;
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.main_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
       
    }
}