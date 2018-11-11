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
using System.Linq;
using System.Threading;

namespace ShopDiaryAbb.Fragments
{
    public class HomeFragment : Fragment
    {
        #region Layout Properties
        private HomeAdapter mInventoryAdapter;
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
        //private ImageButton mStorage;
        //private ImageButton mUse;
        //private ImageButton mAdd;
        //private ImageButton mShopList;
        //private ImageButton mRunOut;

        public static ProductViewModel mHomeSelectedProduct;
        public static List<InventoryViewModel> mInventoriesByProduct;

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
            mRunOutCounter = view.FindViewById<TextView>(Resource.Id.textViewMainRunningOutCounter);
            mExpCounter = view.FindViewById<TextView>(Resource.Id.textViewMainExpiredCounter);
            mStockCounter = view.FindViewById<TextView>(Resource.Id.textViewMainStockCounter);
            mRunOutCounter.Text = "6";
            mStockCounter.Text = "27";
            mExpCounter.Text = "4";
        
            
            LoadInventoryData();
            #region button shortcut function
            //mStorage.Click += (object sender, EventArgs args) =>
            //{
            //    ReplaceFragment(new StoragesFragment(), "Manage Storages");
            //};
            //mUse.Click += (object sender, EventArgs args) =>
            //{
            //    ReplaceFragment(new StoragesFragment(), "Manage Storages");
            //};
            //mAdd.Click += (object sender, EventArgs args) =>
            //{
            //    ReplaceFragment(new AddItemBarcodeFragment(), "Add Item");
            //};
            //mRunOut.Click += (object sender, EventArgs args) =>
            //{
            //    ReplaceFragment(new StoragesFragment(), "Manage Storages");
            //};
            //mShopList.Click += (object sender, EventArgs args) =>
            //{
            //    ReplaceFragment(new StoragesFragment(), "Manage Storages");
            //};
            #endregion
            return view;
        }

        public  void LoadInventoryData()
        {
            
            //ActiveLocationData
 
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
            List<StorageViewModel> StorageByLocation = LoginPageActivity.mGlobalStorages.Where(s => s.LocationId == mLocations[e.Position].Id).ToList();
            List<InventoryViewModel> mInventories = LoginPageActivity.mGlobalInventories;
            List<InventoryViewModel> InventoriesByLocation = mInventories.Join(StorageByLocation, i => i.StorageId, s => s.Id, (i, s) => i).Where(i=> i.ExpirationDate<DateTime.Now).ToList();
            List<InventoryViewModel> temp =InventoriesByLocation.GroupBy(s => s.ProductId).Select(group => group.First()).ToList();

            if (LoginPageActivity.mGlobalInventories != null)
            {
                this.mInventoryAdapter = new HomeAdapter(temp, this.Activity);
                this.mInventoryAdapter.ItemClick += OnInventoryClick;
                this.mListViewInventory.SetAdapter(this.mInventoryAdapter);
            }
        }
        private void OnInventoryClick(object sender, int e)
        {
            mSelectedInventory = e;
            mHomeSelectedProduct = LoginPageActivity.mGlobalProducts[mSelectedInventory];
            
            mInventoriesByProduct = new List<InventoryViewModel>();
            for(int i=0; i < LoginPageActivity.mGlobalInventories.Count; i++)
            {
                if (LoginPageActivity.mGlobalInventories[i].ProductId == mHomeSelectedProduct.Id)
                {
                    
                    mInventoriesByProduct.Add(LoginPageActivity.mGlobalInventories[i]);
                }
            }
            ReplaceFragment(new HomeStoragesFragment(), mHomeSelectedProduct.Name.ToString());
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