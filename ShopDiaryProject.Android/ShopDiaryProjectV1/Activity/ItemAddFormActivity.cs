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
using System.Threading;
using ShopDiaryProject.Android.Models.ViewModels;
using ShopDiaryProjectV1.Services;
using ShopDiaryProjectV1.Adapter;
using ShopDiaryProject.Domain.Models;
using ZXing.Mobile;

namespace ShopDiaryProjectV1
{
    [Activity(Label = "ItemAddActivity")]
    public class ItemAddFormActivity : Activity
    {
        #region field
        public List<StorageViewModel> mStorages;
        public List<CategoryViewModel> mCategories;
        public List<ProductViewModel> mProducts;

        public InventoryViewModel mInventory;
        public ProductViewModel mProduct=new ProductViewModel();
        public ProductViewModel mProductForBarcode;
        public StorageViewModel mStorage;
        public CategoryViewModel mCategory;

        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly CategoryDataService mCategoryDataService;
        private readonly StorageDataService mStorageDataService;

        MobileBarcodeScanner scanner;
       

        private Spinner mSpinnerStorages;
        private Spinner mSpinnerCategories;
        private Button mAddtoInventory;
        private Button mExpDateChoose;
        private Button mScan;
        private ImageButton mbtnBack;
        private DateTime DateTemp;
        private TextView mBarcode;
        private EditText mName;
        private EditText mPrice;
        private bool isBarcodeFound = false;
        private ProgressDialog mProgressDialog;
        #endregion
        public ItemAddFormActivity()
        {
            mInventoryDataService = new InventoryDataService();
            mProductDataService = new ProductDataService();
            mCategoryDataService = new CategoryDataService();
            mStorageDataService = new StorageDataService();
        }

        private void InitFields()
        {
            mProducts = new List<ProductViewModel>();
            mStorages = new List<StorageViewModel>();
            mCategories = new List<CategoryViewModel>();
            mBarcode = FindViewById<TextView>(Resource.Id.textViewAddBarcodeId);
            mName = FindViewById<EditText>(Resource.Id.editTextAddName);
            mPrice = FindViewById<EditText>(Resource.Id.editTextAddPrice);
            mSpinnerCategories = FindViewById<Spinner>(Resource.Id.spinnerAddItemCategory);
            mSpinnerStorages = FindViewById<Spinner>(Resource.Id.spinnerAddItemStorage);
            mExpDateChoose = FindViewById<Button>(Resource.Id.buttonAddExpDate);
            mAddtoInventory = FindViewById<Button>(Resource.Id.buttonAddAddToInventory);
            mScan = FindViewById<Button>(Resource.Id.buttonAddScan2);
            mbtnBack = FindViewById<ImageButton>(Resource.Id.btnAddBack2);
            mBarcode.Text = ItemAddActivity.scannedBarcode.ToString();
            mPrice.Text = "0";
            LoadItemData();
                        
            mAddtoInventory.Click += (object sender, EventArgs args) =>
            {
                if(mBarcode.Text=="-")
                {
                    mProduct.Id = new Guid();
                    AddProductData();
                    AddInventoryData();
                }
                else if (mBarcode.Text!="-") 
                {
                    
                   if(isBarcodeFound)
                    {
                        AddInventoryData();
                    }
                   else
                    {
                        AddProductData();
                        mProduct.Name = mName.Text;
                        mProduct.BarcodeId = mBarcode.Text;
                        
                        AddInventoryData();

                    }
                    
                   
                }
                
            };

            mExpDateChoose.Click += (object sender, EventArgs args) =>
            {
                //ngeluarin dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogDatePickerActivity DatePickerDialog = new DialogDatePickerActivity();
                DatePickerDialog.Show(transaction, "dialogue fragment");
                DatePickerDialog.OnPickDateComplete += DatePickerDialogue_OnComplete;

            };
            
            mbtnBack.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(PageMainActivity));
                StartActivity(nextActivity);
            };
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.PageAddWithoutScan);
            InitFields();
            MobileBarcodeScanner.Initialize(Application);
            scanner = new MobileBarcodeScanner();

            mScan.Click += async delegate {
                
                scanner.UseCustomOverlay = false;
                scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
                scanner.BottomText = "Wait for the barcode to automatically scan!";

                var result = await scanner.Scan();

                HandleScanResult(result);
                mBarcode.Text = result.Text;
                for(int i=0;mProducts.Count>i;i++)
                {
                    if(mBarcode.Text==mProducts[i].BarcodeId)
                    {
                        mProduct.Id = mProducts[i].Id;
                        mName.Text = mProducts[i].Name;
                        isBarcodeFound = true;
                    }
                }
            };

            void HandleScanResult(ZXing.Result result)
            {
                string msg = "";

                if (result != null && !string.IsNullOrEmpty(result.Text))
                    msg = "Found Barcode: " + result.Text;
                else
                    msg = "Scanning Canceled!";

                this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
            }

        }

        protected override void OnStart()
        {
            base.OnStart();

        }
        private void AddProductData()
        {

            mProduct.Name = mName.Text;
            mProduct.BarcodeId = mBarcode.Text;
            mProduct.CategoryId = mCategory.Id;
           
            new Thread(new ThreadStart(delegate
            {
                var isProductAdded = mProductDataService.Add(mProduct.ToModel());
                if (isProductAdded)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Product Added", ToastLength.Long).Show());
                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Failed to add, please check again form's field", ToastLength.Long).Show());
                }

            })).Start();
        }
        private void AddInventoryData()
        {
            var intent = new Intent(this, typeof(PageMainActivity));
            for(int i=0;mProducts.Count>i;i++)
            {
                if(mName.Text==mProducts[i].Name)
                {
                    mProduct.Id = mProducts[i].Id;
                    mProduct.Name = mProducts[i].Name;
                }
            }
            Inventory newInventory = new Inventory()
            {
                ExpirationDate = DateTemp,
                StorageId = mStorage.Id,
                ItemName = mName.Text,
                Price = decimal.Parse(mPrice.Text),
                ProductId = mProduct.Id
                

            };

            var progressDialog = ProgressDialog.Show(this, "Please wait...", "Adding To Inventory...", true);
            new Thread(new ThreadStart(delegate
            {

                var isInventoryAdded = mInventoryDataService.Add(newInventory);
                RunOnUiThread(() => progressDialog.Hide());

                if (isInventoryAdded)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Inventory Added", ToastLength.Long).Show());
                    this.StartActivity(intent);
                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Failed to add, please check again form's field", ToastLength.Long).Show());
                }

            })).Start();
        }
 
        private async void LoadItemData()
        {
            mProgressDialog = ProgressDialog.Show(this, "Please wait...", "Getting data...", true);

            //Spinner Adapter Category
            this.mCategories = await mCategoryDataService.GetAll();
            var adapterCategories = new SpinnerCategoryAdapter(this, mCategories);
            mSpinnerCategories.Adapter = adapterCategories;
            mSpinnerCategories.ItemSelected += SpinnerCategory_ItemSelected;

            //Spinner Adapter Storage
            List<StorageViewModel> tempStorages = new List<StorageViewModel>();
            tempStorages = await mStorageDataService.GetAll();
            for(int i=0;tempStorages.Count()>i;i++)
            {
                if(tempStorages[i].LocationId==LoginActivity.StaticLocationClass.Id)
                {
                    mStorages.Add(tempStorages[i]);
                }
            }

            var adapterStorages = new SpinnerStorageAdapter(this, mStorages);
            mSpinnerStorages.Adapter = adapterStorages;
            mSpinnerStorages.ItemSelected += SpinnerStorage_ItemSelected;

            //Get data product
            this.mProducts = new List<ProductViewModel>();
            this.mProducts = await mProductDataService.GetAll();
            this.mProducts.Count();
         
            mProgressDialog.Dismiss();

        }
        private void SpinnerStorage_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            mStorage = mStorages[e.Position];
            string toast = string.Format("{0} selected", mStorage.Name);
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            //LoadRecyclerAdapter(mStorage,mCategory);


        }
        private void SpinnerCategory_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            mCategory = mCategories[e.Position];

            string toast = string.Format("{0} selected", mCategory.Name);
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            //LoadRecyclerAdapter(mStorage, mCategory);

        }

        private void DatePickerDialogue_OnComplete(object sender, OnDatePickedEventArgs e)
        {
            DateTemp = e.Date;
            Toast.MakeText(this, "Expired Date Added", ToastLength.Short).Show();
        }

    }
}