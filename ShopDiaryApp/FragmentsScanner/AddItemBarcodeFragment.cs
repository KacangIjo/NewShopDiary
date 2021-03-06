using System;
using System.Collections.Generic;
using System.Linq;
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
using ShopDiaryApp.Adapter;
using ShopDiaryApp.Fragments;

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
        public List<String> mProductAdapter = new List<string>();

        public static string scannedBarcode = "-";
        

        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly CategoryDataService mCategoryDataService;
        private readonly StorageDataService mStorageDataService;

        MobileBarcodeScanner scanner;
    
        
        private FragmentTransaction mFragmentTransaction;
        private Spinner mSpinnerStorages;
        private Spinner mSpinnerCategories;
        private ImageButton mAddtoInventory;
        private EditText mExpDateChoose;
        private ImageButton mScan;
        private DateTime DateTemp;
        private TextView mBarcode;
        private AutoCompleteTextView mName;
        private EditText mPrice;
        private bool isBarcodeFound;
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
            isBarcodeFound = false;
        }

        public static AddItemBarcodeFragment NewInstance()
        {
            var frag2 = new AddItemBarcodeFragment { Arguments = new Bundle() };
            return frag2;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AddItemFormLayout, container, false);

            mBarcode = view.FindViewById<TextView>(Resource.Id.textViewAddBarcodeId);
            mName = view.FindViewById<AutoCompleteTextView>(Resource.Id.editTextAddName);
            
            mPrice = view.FindViewById<EditText>(Resource.Id.editTextAddItemPrice);
            mSpinnerCategories = view.FindViewById<Spinner>(Resource.Id.spinnerAddItemCategory);
            mSpinnerStorages = view.FindViewById<Spinner>(Resource.Id.spinnerAddItemStorage);
            mExpDateChoose = view.FindViewById<EditText>(Resource.Id.editTextAddItemExpDate);
            mAddtoInventory = view.FindViewById<ImageButton>(Resource.Id.buttonAddAddToInventory);
            mScan = view.FindViewById<ImageButton>(Resource.Id.imageButtonAddScan2);
            mBarcode.Text = "-";
            mPrice.Text = "0";
            LoadItemData();
            mExpDateChoose.Click += (object sender, EventArgs args) =>
            {
                //ngeluarin dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogDatePickerActivity DatePickerDialog = new DialogDatePickerActivity();
                DatePickerDialog.Show(transaction, "dialogue fragment");
                DatePickerDialog.OnPickDateComplete += DatePickerDialogue_OnComplete;

            };

            mAddtoInventory.Click += (object sender, EventArgs args) =>
            {
                if (mBarcode.Text == "-")
                {
                   for(int i=0;i<mProducts.Count();i++)
                    {
                        if(mName.Text== mProducts[i].Name)
                        {
                            mProduct.Id = mProducts[i].Id;
                            AddInventoryData();
                        }
                        else
                        {
                            mProduct.Id = new Guid();
                            AddProductData();
                            AddInventoryData();
                        }
                    }
                }
                else if (mBarcode.Text != "-")
                {
                    if (isBarcodeFound)
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


        private void AddProductData()
        {
            Product product = new Product()
            {
                Name = mName.Text,
                BarcodeId = mBarcode.Text,
                CategoryId = mCategory.Id,
                AddedUserId=LoginPageActivity.StaticUserClass.ID.ToString()
            };
            new Thread(new ThreadStart(delegate
            {
                var isProductAdded = mProductDataService.Add(product);
                if (isProductAdded){
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Product Added", ToastLength.Long).Show());
                }
                else{
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed to add, please check again form's field", ToastLength.Long).Show());
                }
            })).Start();
        }
        private void AddInventoryData()
        {
            //for (int i = 0; mProducts.Count > i; i++){
            //    if (mName.Text == mProducts[i].Name){
            //        mProduct.Id = mProducts[i].Id;
            //        mProduct.Name = mProducts[i].Name;
            //    }
            //}
            Inventory newInventory = new Inventory(){
                ExpirationDate = DateTemp,
                StorageId = mStorage.Id,
                ItemName = mName.Text,
                Price = decimal.Parse(mPrice.Text),
                ProductId = mProduct.Id,
                AddedUserId = LoginPageActivity.StaticUserClass.ID.ToString()
            };
            new Thread(new ThreadStart(delegate{
                var isInventoryAdded = mInventoryDataService.Add(newInventory);
                if (isInventoryAdded){
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Inventory Added", ToastLength.Long).Show());
                    ReplaceFragment(new HomeFragment(), "Home");
                }
                else{
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed to add, please check again form's field", ToastLength.Long).Show());
                }

            })).Start();
        }

        private async void LoadItemData(){
            //Spinner Adapter Category
            this.mCategories = await mCategoryDataService.GetAll();
            var adapterCategories = new SpinnerCategoryAdapter(this.Activity, mCategories);
            mSpinnerCategories.Adapter = adapterCategories;

            mSpinnerCategories.ItemSelected += SpinnerCategory_ItemSelected;
            mSpinnerCategories.SetSelection(0);
        
            //Spinner Adapter Storage
            List<StorageViewModel> tempStorages = new List<StorageViewModel>();
            tempStorages = await mStorageDataService.GetAll();
            for (int i = 0; i<tempStorages.Count(); i++)
            {
                if (tempStorages[i].LocationId == MainActivity.StaticActiveLocationClass.Id)
                {
                    mStorages.Add(tempStorages[i]);
                }
            }
            var adapterStorages = new SpinnerStorageAdapter(this.Activity, mStorages);
            mSpinnerStorages.Adapter = adapterStorages;

            mSpinnerStorages.ItemSelected += SpinnerStorage_ItemSelected;
            mSpinnerStorages.SetSelection(0);

            //Get data product
            this.mProducts = new List<ProductViewModel>();
            this.mProducts = await mProductDataService.GetAll();
            this.mProducts.Count();
            List<ProductViewModel> tempProduct = new List<ProductViewModel>();
            tempProduct = await mProductDataService.GetAll();
            for (int i = 0; tempProduct.Count() > i; i++)
            {
                mProductAdapter.Add(tempProduct[i].Name);
            }
            var adapter = new ArrayAdapter<String>(this.Activity, Resource.Layout.support_simple_spinner_dropdown_item, mProductAdapter);
            mName.Adapter = adapter;
        }
        private void SpinnerStorage_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            mStorage = mStorages[e.Position];

            string toast = string.Format("{0} selected", mStorage.Name);
            Toast.MakeText(this.Activity, toast, ToastLength.Long).Show();
            //LoadRecyclerAdapter(mStorage, mCategory);

        }
        private void SpinnerCategory_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            mCategory = mCategories[e.Position];

            string toast = string.Format("{0} selected", mCategory.Name);
            Toast.MakeText(this.Activity, toast, ToastLength.Long).Show();
            //LoadRecyclerAdapter(mStorage, mCategory);

        }

        private void DatePickerDialogue_OnComplete(object sender, OnDatePickedEventArgs e)
        {
            DateTemp = e.Date;
            mExpDateChoose.Text = DateTemp.ToString();
            Toast.MakeText(this.Activity, "Expired Date Added", ToastLength.Short).Show();
        }

        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }

    }
}