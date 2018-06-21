using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShopDiaryProjectV1.Adapter;
using Android.Support.V7.Widget;
using ShopDiaryProject.Android.Models.ViewModels;
using ShopDiaryProjectV1.Services;
using ShopDiaryProject.Domain.Models;
using System.Threading;
using Android.Text;


namespace ShopDiaryProjectV1
{
    [Activity(Label = "UseActivity")]
    public class UseActivity : Activity
    {
        #region field
        private InventoryRecycleAdapter mInventoryAdapter;
        private InventoryRecycleAdapterByStorage mInventoryAdapterByStorage;

        public List<InventoryViewModel> mInventories;
        public List<ProductViewModel> mProducts;
        public List<StorageViewModel> mStorages;
        public List<CategoryViewModel> mCategories;
        public List<InventoryViewModel> mTempInventories = new List<InventoryViewModel>(); 
        public List<InventoryViewModel> inventoriesForSearch = new List<InventoryViewModel>();

        public InventoryViewModel mInventory;
        public ProductViewModel mProduct;
        public StorageViewModel mStorage;
        public CategoryViewModel mCategory;

        private RecyclerView mListViewInventory;

        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly CategoryDataService mCategoryDataService;
        private readonly StorageDataService mStorageDataService;
        private readonly ConsumeDataService mConsumeDataService;

        private EditText mSearchBox;
        private Button mButtonSearch;
        private Spinner mSpinnerStorages;
        private ImageButton mButtonBack;
        private Button mButtonUse;
        private Button mButtonDelete;
        private TextView mTextSelectedItem;
        private int mSelectedItem = -1;
        private ProgressDialog mProgressDialog;
        #endregion

        public UseActivity()
        {
            mInventoryDataService = new InventoryDataService();
            mProductDataService = new ProductDataService();
            mCategoryDataService = new CategoryDataService();
            mStorageDataService = new StorageDataService();
            mConsumeDataService = new ConsumeDataService();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.PageUse);
            InitFields();
        }

        protected override void OnStart()
        {
            base.OnStart();
 
        }
        private void InitFields()
        {
            this.mButtonUse = FindViewById<Button>(Resource.Id.buttonUseUse);
            this.mButtonDelete = FindViewById<Button>(Resource.Id.buttonUseDelete);
            this.mTextSelectedItem = FindViewById<TextView>(Resource.Id.textViewUseSelectedItem);
            this.mListViewInventory = this.FindViewById<RecyclerView>(Resource.Id.recyclerViewUse);
            this.mListViewInventory.SetLayoutManager(new LinearLayoutManager(this));
            this.mSpinnerStorages = FindViewById<Spinner>(Resource.Id.spinnerUseStorageSelected);
            this.mButtonBack = FindViewById<ImageButton>(Resource.Id.btnUseBack);
            this.mButtonSearch = FindViewById<Button>(Resource.Id.buttonSearchUse);
            this.mSearchBox = FindViewById<EditText>(Resource.Id.searchBoxUse);

            LoadAdapterData();
            LoadItemData();
            mButtonUse.Click += (object sender, EventArgs args) =>
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Confirm Use");
                alert.SetMessage("Use your item?");
                alert.SetPositiveButton("Use It", (senderAlert, eventargs) => 
                {
                    Consume newLoc = new Consume()
                    {
                        DateConsumed = System.DateTime.Now,
                        InventoryId=mInventory.Id

                    };
                    var progressDialog = ProgressDialog.Show(this, "Please wait...", "Consuming...", true);
                    new Thread(new ThreadStart(delegate
                    {

                        var isAdded = mConsumeDataService.Add(newLoc);
                        RunOnUiThread(() => progressDialog.Hide());

                        if (isAdded)
                        {
                            RunOnUiThread(() => Toast.MakeText(this, "Consumed!", ToastLength.Short).Show());
                        }
                        else
                        {
                            RunOnUiThread(() => Toast.MakeText(this, "Failed to consume, please check your connection", ToastLength.Long).Show());
                        }

                    })).Start();

                   
                });

                alert.SetNegativeButton("Cancel", (senderAlert, eventargs) => {
                    Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            };
            mButtonDelete.Click += (object sender, EventArgs args) =>
            {
                var progressDialog = ProgressDialog.Show(this, "Please wait...", "Consuming...", true);
                new Thread(new ThreadStart(delegate
                {  
                    var isDeleted = mInventoryDataService.Delete(mInventory.Id);
                    RunOnUiThread(() => progressDialog.Hide());

                    if (isDeleted)
                    {
                        RunOnUiThread(() => Toast.MakeText(this, "Consumed!", ToastLength.Short).Show());
                    }
                    else
                    {
                        RunOnUiThread(() => Toast.MakeText(this, "Failed to consume, please check your connection", ToastLength.Long).Show());
                    }

                })).Start();
            };
            mButtonBack.Click += (object sender, EventArgs args) =>
            {
                    Intent nextActivity = new Intent(this, typeof(PageMainActivity));
                    StartActivity(nextActivity);
            };
            mButtonSearch.Click += (object sender, EventArgs args) =>
             {
                 inventoriesForSearch.Clear();
                 SearchItem();
             };
            
        }
        private void SearchItem()
        {
            for (int i = 0; i < mTempInventories.Count(); i++)
            {
                if (mTempInventories[i].ItemName.StartsWith(mSearchBox.Text))
                {
                    inventoriesForSearch.Add(mTempInventories[i]);
                }
            }
            this.mInventoryAdapterByStorage = new InventoryRecycleAdapterByStorage(this.mStorage.Id, this.inventoriesForSearch, this.mProducts, this);
            this.mInventoryAdapterByStorage.ItemClick += OnInventorySearchClick;
            this.mListViewInventory.SetAdapter(this.mInventoryAdapterByStorage);
        }

        private void OnInventorySearchClick(object sender, int e)
        {
            mSelectedItem = e;
            mTextSelectedItem.Text = inventoriesForSearch[e].ItemName;
            mInventory = inventoriesForSearch[e];
        }

        private async void LoadItemData()
        {
            mProgressDialog = ProgressDialog.Show(this, "Please wait...", "Getting data...", true);

            //Spinner Adapter Storage
            this.mStorages = await mStorageDataService.GetAll();
            var adapterStorages = new SpinnerStorageAdapter(this, mStorages);
            mSpinnerStorages.Adapter = adapterStorages;
            mSpinnerStorages.ItemSelected += SpinnerStorage_ItemSelected;

            
            mProgressDialog.Hide();
        }
        private async void LoadAdapterData()
        {
            this.mInventories = await mInventoryDataService.GetAll();
            this.mProducts = await mProductDataService.GetAll();
            mProducts.Count();
            //for(int i=0;i< mInventoriesTemp.Count();i++)
            //{
            //    if(mInventoriesTemp[i].IsDeleted && mInventoriesTemp[i].IsConsumed==false)
            //    {
            //        mInventories.Add(mInventoriesTemp[i]);
            //    }
            //}

            this.mInventories = mInventories.OrderBy(e => e.ItemName).ToList();
        }


        private void LoadRecyclerAdapter(StorageViewModel store,CategoryViewModel cat)
        {

            //int i = 0;
            //while (mInventories[i] != null)
            //{
            //    if (store!=null && mInventories[i].StorageId==store.Id)
            //    {
            //        int productCounter = -1;
            //        if (cat!=null && mProducts[productCounter].CategoryId==cat.Id)
            //        {
            //            this.mInventoryAdapter = new InventoryRecycleAdapter(this.mInventories, this.mProducts, this);
            //            this.mInventoryAdapter.ItemClick += OnInventoryClick;
            //            this.mListViewInventory.SetAdapter(this.mInventoryAdapter);
            //        }
            //        productCounter++;
            //    }
            //    else
            //    {
            //        this.mInventoryAdapter = new InventoryRecycleAdapter(this.mInventories, this.mProducts, this);
            //        this.mInventoryAdapter.ItemClick += OnInventoryClick;
            //        this.mListViewInventory.SetAdapter(this.mInventoryAdapter);
            //    }
            //    i++;
            //}
        }

        private void OnInventoryClick(object sender, int e)
        {
            mSelectedItem = e;
            //mProducts.Count();
            //for(int i=0; i > mProducts.Count();i++)
            //{
            //    if(mProducts[i].Id == mInventories[mSelectedItem].ProductId)
            //    {
            //        mTextSelectedItem.Text = mProducts[i].Name;
            //    }
            //}
            mTextSelectedItem.Text = mTempInventories[e].ItemName;
            mInventory = mTempInventories[e];

        }

        private void SpinnerStorage_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            mTempInventories.Clear();
            Spinner spinner = (Spinner)sender;
            mStorage = mStorages[e.Position];

            for (int i = 0; i < mInventories.Count(); i++)
            {
                if (mStorage.Id == mInventories[i].StorageId)
                {
                    mTempInventories.Add(mInventories[i]);
                }
            }
            string toast = string.Format("{0} selected", mStorage.Name);
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            this.mInventoryAdapterByStorage = new InventoryRecycleAdapterByStorage(this.mStorages[e.Position].Id, this.mTempInventories, this.mProducts, this);
            this.mInventoryAdapterByStorage.ItemClick += OnInventoryClick;
            this.mListViewInventory.SetAdapter(this.mInventoryAdapterByStorage);
        }
  

        
    }
}