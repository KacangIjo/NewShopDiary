using System;
using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.OS;
using Android.Widget;
using ShopDiaryProject.Domain.Models;
using ZXing.Mobile;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;
using ShopDiaryApp.DialogFragments;
using System.Threading;
using Android.Views;
using Android.Support.V4.App;

namespace ShopDiaryApp.FragmentsScanner
{
    public class AddItemBarcodeFragment : Fragment
    {
        #region field
        public List<StorageViewModel> mStorages;
        public List<CategoryViewModel> mCategories;
        public List<ProductViewModel> mProducts;

        public InventoryViewModel mInventory;
        public ProductViewModel mProduct = new ProductViewModel();
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
        private ImageButton mScan;
        private DateTime DateTemp;
        private TextView mBarcode;
        private EditText mName;
        private EditText mPrice;
        private bool isBarcodeFound = false;
        #endregion
        public AddItemBarcodeFragment()
        {
            mInventoryDataService = new InventoryDataService();
            mProductDataService = new ProductDataService();
            mCategoryDataService = new CategoryDataService();
            mStorageDataService = new StorageDataService();
            mProducts = new List<ProductViewModel>();
            mStorages = new List<StorageViewModel>();
            mCategories = new List<CategoryViewModel>();
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ManageLocationAddUser, container, false);

            mBarcode = view.FindViewById<TextView>(Resource.Id.textViewAddBarcodeId);
            mName = view.FindViewById<EditText>(Resource.Id.editTextAddName);
            mPrice = view.FindViewById<EditText>(Resource.Id.editTextAddPrice);
            mSpinnerCategories = view.FindViewById<Spinner>(Resource.Id.spinnerAddItemCategory);
            mSpinnerStorages = view.FindViewById<Spinner>(Resource.Id.spinnerAddItemStorage);
            mExpDateChoose = view.FindViewById<Button>(Resource.Id.buttonAddExpDate);
            mAddtoInventory = view.FindViewById<Button>(Resource.Id.buttonAddAddToInventory);
            mScan = view.FindViewById<ImageButton>(Resource.Id.imageButtonAddScan2);
            //mBarcode.Text = ItemAddActivity.scannedBarcode.ToString();
            mPrice.Text = "0";
            MobileBarcodeScanner.Initialize(this.Activity.Application);
            scanner = new MobileBarcodeScanner();
            mScan.Click += async delegate {

                scanner.UseCustomOverlay = false;
                scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
                scanner.BottomText = "Wait for the barcode to automatically scan!";

                var result = await scanner.Scan();

                HandleScanResult(result);
                mBarcode.Text = result.Text;
                for (int i = 0; mProducts.Count > i; i++)
                {
                    if (mBarcode.Text == mProducts[i].BarcodeId)
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

                this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, msg, ToastLength.Short).Show());
            }
            return view;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.Activity.SetContentView(Resource.Layout.AddItemLayout);
            MobileBarcodeScanner.Initialize(this.Activity.Application);
            scanner = new MobileBarcodeScanner();

            mScan.Click += async delegate {

                scanner.UseCustomOverlay = false;
                scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
                scanner.BottomText = "Wait for the barcode to automatically scan!";

                var result = await scanner.Scan();

                HandleScanResult(result);
                mBarcode.Text = result.Text;
                for (int i = 0; mProducts.Count > i; i++)
                {
                    if (mBarcode.Text == mProducts[i].BarcodeId)
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
            }

        }

        private void AddProductData()
        {

            mProduct.Name = mName.Text;
            mProduct.BarcodeId = mBarcode.Text;
            mProduct.CategoryId = mCategory.Id;

            new Thread(new ThreadStart(delegate
            {
                var isProductAdded = mProductDataService.Add(mProduct.ToModel());
                //if (isProductAdded)
                //{
                //    this.Activity.RunOnUiThread(() => Toast.MakeText(this, "Product Added", ToastLength.Long).Show());
                //}
                //else
                //{
                //    RunOnUiThread(() => Toast.MakeText(this, "Failed to add, please check again form's field", ToastLength.Long).Show());
                //}

            })).Start();
        }
        //private void AddInventoryData()
        //{

        //    for (int i = 0; mProducts.Count > i; i++)
        //    {
        //        if (mName.Text == mProducts[i].Name)
        //        {
        //            mProduct.Id = mProducts[i].Id;
        //            mProduct.Name = mProducts[i].Name;
        //        }
        //    }
        //    Inventory newInventory = new Inventory()
        //    {
        //        ExpirationDate = DateTemp,
        //        StorageId = mStorage.Id,
        //        ItemName = mName.Text,
        //        Price = decimal.Parse(mPrice.Text),
        //        ProductId = mProduct.Id


        //    };

        //    var progressDialog = ProgressDialog.Show(this, "Please wait...", "Adding To Inventory...", true);
        //    new Thread(new ThreadStart(delegate
        //    {

        //        var isInventoryAdded = mInventoryDataService.Add(newInventory);
        //        RunOnUiThread(() => progressDialog.Hide());

        //        if (isInventoryAdded)
        //        {
        //            RunOnUiThread(() => Toast.MakeText(this, "Inventory Added", ToastLength.Long).Show());
        //            this.StartActivity(intent);
        //        }
        //        else
        //        {
        //            RunOnUiThread(() => Toast.MakeText(this, "Failed to add, please check again form's field", ToastLength.Long).Show());
        //        }

        //    })).Start();
        //}

        //private async void LoadItemData()
        //{
        //    mProgressDialog = ProgressDialog.Show(this, "Please wait...", "Getting data...", true);

        //    //Spinner Adapter Category
        //    this.mCategories = await mCategoryDataService.GetAll();
        //    var adapterCategories = new SpinnerCategoryAdapter(this, mCategories);
        //    mSpinnerCategories.Adapter = adapterCategories;
        //    mSpinnerCategories.ItemSelected += SpinnerCategory_ItemSelected;

        //    //Spinner Adapter Storage
        //    List<StorageViewModel> tempStorages = new List<StorageViewModel>();
        //    tempStorages = await mStorageDataService.GetAll();
        //    for (int i = 0; tempStorages.Count() > i; i++)
        //    {
        //        if (tempStorages[i].LocationId == LoginActivity.StaticLocationClass.Id)
        //        {
        //            mStorages.Add(tempStorages[i]);
        //        }
        //    }

        //    var adapterStorages = new SpinnerStorageAdapter(this, mStorages);
        //    mSpinnerStorages.Adapter = adapterStorages;
        //    mSpinnerStorages.ItemSelected += SpinnerStorage_ItemSelected;

        //    //Get data product
        //    this.mProducts = new List<ProductViewModel>();
        //    this.mProducts = await mProductDataService.GetAll();
        //    this.mProducts.Count();

        //    mProgressDialog.Dismiss();

        //}
        //private void SpinnerStorage_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        //{
        //    Spinner spinner = (Spinner)sender;
        //    mStorage = mStorages[e.Position];
        //    string toast = string.Format("{0} selected", mStorage.Name);
        //    Toast.MakeText(this, toast, ToastLength.Long).Show();
        //    //LoadRecyclerAdapter(mStorage,mCategory);


        //}
        //private void SpinnerCategory_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        //{
        //    Spinner spinner = (Spinner)sender;
        //    mCategory = mCategories[e.Position];

        //    string toast = string.Format("{0} selected", mCategory.Name);
        //    Toast.MakeText(this, toast, ToastLength.Long).Show();
        //    //LoadRecyclerAdapter(mStorage, mCategory);

        //}

        private void DatePickerDialogue_OnComplete(object sender, OnDatePickedEventArgs e)
        {
            DateTemp = e.Date;
            Toast.MakeText(this.Activity, "Expired Date Added", ToastLength.Short).Show();
        }

    }
}