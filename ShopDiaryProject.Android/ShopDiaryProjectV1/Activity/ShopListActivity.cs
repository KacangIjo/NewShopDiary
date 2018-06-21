//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;

//using ShopDiaryProjectV1.Adapter;
//using Android.Support.V7.Widget;
//using ShopDiaryProjectV1.Services;
//using System.Threading;
//using ShopDiaryProject.Domain.Models;
//using ShopDiaryProject.Android.Models.ViewModels;

//namespace ShopDiaryProjectV1
//{
//    [Activity(Label = "StorageActivity")]
//    public class ShopListActivity : Activity
//    {
//        private CategoryRecyclerAdapter mCategoryAdapter;

//        //public List<ShopListViewModel> mShopLists;

//        private RecyclerView mListViewCategory;

//        //private readonly WishlistDataService mShopListDataService;


//        private Button mAddButton;
//        private Button mEditButton;
//        private ImageButton mBackButton;
//        //private ShopListViewModel mSelectedShopListClass;
//        private TextView mTextSelectedCategory;
//        private int mSelectedShopList = -1;
//        private ProgressDialog mProgressDialog;


//        public ShopListActivity()
//        {
//            mShopListDataService = new WishlistDataService();

//        }
//        protected override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);
//            Window.RequestFeature(WindowFeatures.NoTitle);
//            SetContentView(Resource.Layout.PageCategories);
//            InitFields();
//        }

//        protected override void OnStart()
//        {
//            base.OnStart();
        
//        }
//        private void InitFields()
//        {
//            this.mAddButton = FindViewById<Button>(Resource.Id.buttonCategoriesAdd);
//            this.mEditButton = FindViewById<Button>(Resource.Id.buttonCategoriesEdit);
//            this.mBackButton = FindViewById<ImageButton>(Resource.Id.buttonCategoriesBack);
//            this.mTextSelectedCategory = FindViewById<TextView>(Resource.Id.textViewSelectedCategory);
//            this.mListViewCategory = FindViewById<RecyclerView>(Resource.Id.recyclerViewCategories);
//            this.mListViewCategory.SetLayoutManager(new LinearLayoutManager(this));

//            LoadStoragesData();
//            mEditButton.Click += (object sender, EventArgs args) =>
//            {
//                if (mSelectedShopList > -1)
//                {
//                    //FragmentTransaction transaction = FragmentManager.BeginTransaction();
//                    //DialogCategoryDetail dialogCategory = new DialogCategoryDetail(mSelectedShopListClass);
//                    //dialogCategory.Show(transaction, "dialogue fragment");
//                    //dialogCategory.OnCompleteCategoryDetail += EditCategoryDialog_OnCategoryEdit;
//                }
//                else
//                {
//                    Toast.MakeText(this, "Select Storage First..", ToastLength.Long).Show();
//                }

//            };
//            mAddButton.Click += (object sender, EventArgs args) =>
//            {
//                FragmentTransaction transaction = FragmentManager.BeginTransaction();
//                DialogAddStorage dialogStorage = new DialogAddStorage();
//                dialogStorage.Show(transaction, "dialogue fragment");
//                dialogStorage.OnAddStorageComplete += AddStorageDialog_OnStorageAdd;
//            };
//            mBackButton.Click += (object sender, EventArgs args) =>
//            {
//                FragmentTransaction transaction = FragmentManager.BeginTransaction();
//                DialogAddStorage dialogStorage = new DialogAddStorage();
//                dialogStorage.Show(transaction, "dialogue fragment");
//                dialogStorage.OnAddStorageComplete += AddStorageDialog_OnStorageAdd;
//            };
//        }




//        private async void LoadStoragesData()
//        {
//            mProgressDialog = ProgressDialog.Show(this, "Please wait...", "Getting Storage data...", true);
//            this.mShopLists = new List<ShopListViewModel>();
//            this.mShopLists = await mShopListDataService.GetAll();
//            if (mShopLists != null)
//            {
//                //this.mCategoryAdapter = new CategoryRecyclerAdapter(this.m, this);
//                //this.mCategoryAdapter.ItemClick += OnStorageClicked;

//                //this.mListViewCategory.SetAdapter(this.mCategoryAdapter);
//            }
//            mProgressDialog.Hide();
//        }

//        private void OnStorageClicked(object sender, int e)
//        {
//            mSelectedShopList = e;
//            mSelectedShopListClass = mShopLists[e];
            
//        }

//        private void EditCategoryDialog_OnCategoryEdit(object sender, OnCompleteCategoryDetailEventArgs e)
//        {

//            Wishlist EditedCategory = new Wishlist()
//            {
             
//            };
//            var progressDialog = ProgressDialog.Show(this, "Please wait...", "Editing Category...", true);
//            new Thread(new ThreadStart(delegate
//            {

//                var isAdded = mShopListDataService.Edit(mShopLists[mSelectedShopList].Id, EditedCategory);
//                RunOnUiThread(() => progressDialog.Hide());

//                if (isAdded)
//                {
//                    RunOnUiThread(() => Toast.MakeText(this, "Category Editted", ToastLength.Long).Show());
//                }
//                else
//                {
//                    RunOnUiThread(() => Toast.MakeText(this, "Failed to Edit, please check again form's field", ToastLength.Long).Show());
//                }

//            })).Start();
//        }
//        private void AddStorageDialog_OnStorageAdd(object sender, OnAddStorageEventArgs e)
//        {

//            Wishlist newLoc = new Wishlist()
//            {
//                Description = e.Description,
               
//            };
//            var progressDialog = ProgressDialog.Show(this, "Please wait...", "Adding Storage...", true);
//            new Thread(new ThreadStart(delegate
//            {

//                var isAdded = mShopListDataService.Add(newLoc);
//                var temp=newLoc.Id.ToString();
//                RunOnUiThread(() => progressDialog.Hide());

//                if (isAdded)
//                {
//                    RunOnUiThread(() => Toast.MakeText(this, "Storage Added", ToastLength.Long).Show());
//                }
//                else
//                {
//                    RunOnUiThread(() => Toast.MakeText(this, "Failed to add, please check again form's field", ToastLength.Long).Show());
//                }

//            })).Start();
//        }


//    }
   
//}