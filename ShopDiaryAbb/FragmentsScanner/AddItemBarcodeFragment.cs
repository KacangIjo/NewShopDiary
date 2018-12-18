using System;
using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Widget;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;
using ShopDiaryAbb.DialogFragments;
using System.Threading;
using Android.Views;
using Android.Support.V4.App;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.Fragments;
using ZXing.Mobile;
using ShopDiaryAbb.Models;

namespace ShopDiaryAbb.FragmentsScanner
{
    public class AddItemBarcodeFragment : Fragment
    {
        #region field
        public List<StorageViewModel> mStorages;
        public List<CategoryViewModel> mCategories;
        public List<ProductViewModel> mProducts;
        public List<CategoryViewModel> spinnerListCategories;

        public Guid tempproductid;
        public Guid mNewProductId;
        public bool mIsContainName = false;
        public InventoryViewModel mInventory;
        public ProductViewModel mProduct = new ProductViewModel();
        public ProductViewModel mProductForBarcode;
        public StorageViewModel mStorage;
        public CategoryViewModel mCategory;
        public List<String> mProductAdapter = new List<string>();

        public static string scannedBarcode = "-";

        int sel=-1;
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
        private EditText mStock;
        #endregion
        public AddItemBarcodeFragment()
        {
            mInventoryDataService = new InventoryDataService();
            mProductDataService = new ProductDataService();
            mCategoryDataService = new CategoryDataService();
            mStorageDataService = new StorageDataService();
            spinnerListCategories = new List<CategoryViewModel>();
            mProducts = LoginPageActivity.mGlobalProducts;
            mStorages = LoginPageActivity.mGlobalStorages;
            mCategories = LoginPageActivity.mGlobalCategories;
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

            mStock = view.FindViewById<EditText>(Resource.Id.editTextAddItemTotalItem);
      
            mSpinnerStorages = view.FindViewById<Spinner>(Resource.Id.spinnerAddItemStorage);
            mExpDateChoose = view.FindViewById<EditText>(Resource.Id.editTextAddItemExpDate);
            mAddtoInventory = view.FindViewById<ImageButton>(Resource.Id.buttonAddAddToInventory);
            mScan = view.FindViewById<ImageButton>(Resource.Id.imageButtonAddScan2);
            mBarcode.Text = "-";
            mStock.Text = "1";
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
                    ProductViewModel findProduct = mProducts.Find(p => p.Name == mName.Text);
                    if (findProduct!=null)
                    {
                        mProduct.Id = findProduct.Id;
                        
                        AddInventoryData();
                    }
                    else
                    {
                        mProduct.Id = new Guid();
                        AddProductData();
                        AddInventoryData();
                    }
                }
                else if (mBarcode.Text != "-")
                {
                    AddInventoryData();
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
                        mIsContainName = true;
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
                Id = new Guid(),
                Name = mName.Text,
                BarcodeId = mBarcode.Text,
                CategoryId = new Guid(),
                AddedUserId=LoginPageActivity.StaticUserClass.ID.ToString()
            };
            mNewProductId = product.Id;

            new Thread(new ThreadStart(delegate
            {
                var isProductAdded = mProductDataService.Add(product);
                if (isProductAdded)
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Product Added", ToastLength.Long).Show());
                }
                else
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed to add, please check again form's field", ToastLength.Long).Show());
                }


            })).Start();
        }
        private void AddInventoryData()
        {

            if (mBarcode.Text == "-")
            {
                AddProductData();
                tempproductid = mNewProductId;
            }
            else if (mBarcode.Text != "-")
            {
                if (mIsContainName)
                {
                    tempproductid = mProduct.Id;
                }
                else
                {
                    AddProductData();
                    tempproductid = mNewProductId;
                }
            }

            Inventory newInventory = new Inventory()
            {
                ExpirationDate = DateTemp,
                StorageId = mStorage.Id,
                ItemName = mName.Text,
                Stock = int.Parse(mStock.Text),
                ProductId = tempproductid,
                AddedUserId = LoginPageActivity.StaticUserClass.ID.ToString()
            };
            new Thread(new ThreadStart(delegate{
                if (int.Parse(mStock.Text) != 0)
                {
                    var isInventoryAdded = mInventoryDataService.Add(newInventory);
                    if (isInventoryAdded)
                    {
                        UpdateDataAsync();
                        this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Inventory Added", ToastLength.Long).Show());
                        ReplaceFragment(new HomeFragment(), "Home");
                    }
                    else
                    {
                        this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed to add, please check again form's field", ToastLength.Long).Show());
                    }
                }
                else
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "No Item Added", ToastLength.Long).Show());
                }
            })).Start();
        }

        private void LoadItemData(){

            //Spinner Adapter Storage
            List<StorageViewModel> tempStorages = mStorages.Where(s => s.LocationId == LoginPageActivity.StaticActiveLocationClass.Id).ToList();
            var adapterStorages = new SpinnerStorageAdapter(this.Activity, tempStorages);
            mSpinnerStorages.Adapter = adapterStorages;

            mSpinnerStorages.ItemSelected += SpinnerStorage_ItemSelected;
            if (mSpinnerStorages.Count!=0)
            {
                mSpinnerStorages.SetSelection(0);
            }

            //Get data product
            List<ProductViewModel> tempProduct = new List<ProductViewModel>();
            tempProduct = LoginPageActivity.mGlobalProducts;
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


        }
        #region categoryItemSelected
        //private void SpinnerCategory_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        //{
        //    Spinner spinner = (Spinner)sender;
        //    mCategory = mCategories[e.Position];

        //    string toast = string.Format("{0} selected", mCategory.Name);
        //    Toast.MakeText(this.Activity, toast, ToastLength.Long).Show();
        //    //LoadRecyclerAdapter(mStorage, mCategory);

        //}
        #endregion

        private void DatePickerDialogue_OnComplete(object sender, OnDatePickedEventArgs e)
        {
            DateTemp = e.Date;
            mExpDateChoose.Text = DateTemp.ToString("yyyy-MM-dd");
            Toast.MakeText(this.Activity, "Expired Date Added", ToastLength.Short).Show();
        }
        public async void UpdateDataAsync()
        {
            InventoryDataService mInventoryDataService = new InventoryDataService();
            ProductDataService mProductDataService = new ProductDataService();
            LoginPageActivity.mGlobalProducts = await mProductDataService.GetAll();
            LoginPageActivity.mGlobalInventories = new List<InventoryViewModel>();

            List<InventoryViewModel> tempInventories = await mInventoryDataService.GetAll();
            for (int i = 0; i < tempInventories.Count(); i++)
            {
                for (int j = 0; j < LoginPageActivity.mGlobalProducts.Count(); j++)
                {
                    if (tempInventories[i].ProductId == LoginPageActivity.mGlobalProducts[j].Id)
                    {
                        tempInventories[i].ItemName = LoginPageActivity.mGlobalProducts[j].Name;
                        LoginPageActivity.mGlobalInventories.Add(tempInventories[i]);
                    }
                    if (LoginPageActivity.mGlobalProducts[j].AddedUserId == LoginPageActivity.StaticUserClass.ID.ToString())
                    {
                        LoginPageActivity.mGlobalProductsByUser.Add(LoginPageActivity.mGlobalProducts[j]);
                    }
                }
            }
        }
        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.main_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }

    }
}