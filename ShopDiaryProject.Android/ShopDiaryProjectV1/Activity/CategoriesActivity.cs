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
using ShopDiaryProjectV1.Services;
using System.Threading;
using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.Android.Models.ViewModels;

namespace ShopDiaryProjectV1
{
    [Activity(Label = "StorageActivity")]
    public class CategoriesActivity : Activity
    {
        private CategoryRecyclerAdapter mCategoryAdapter;

        public List<CategoryViewModel> mCategories;

        private RecyclerView mListViewCategory;

        private readonly CategoryDataService mCategoryDataService;


        private Button mAddButton;
        private Button mEditButton;
        private ImageButton mBackButton;
        private CategoryViewModel mSelectedCategoryClass;
        private TextView mTextSelectedCategory;
        private int mSelectedCategory = -1;
        private ProgressDialog mProgressDialog;


        public CategoriesActivity()
        {
            mCategoryDataService = new CategoryDataService();

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.PageCategories);
            InitFields();
        }

        protected override void OnStart()
        {
            base.OnStart();
        
        }
        private void InitFields()
        {
            this.mAddButton = FindViewById<Button>(Resource.Id.buttonCategoriesAdd);
            this.mEditButton = FindViewById<Button>(Resource.Id.buttonCategoriesEdit);
            this.mBackButton = FindViewById<ImageButton>(Resource.Id.buttonCategoriesBack);
            this.mTextSelectedCategory = FindViewById<TextView>(Resource.Id.textViewSelectedCategory);
            this.mListViewCategory = FindViewById<RecyclerView>(Resource.Id.recyclerViewCategories);
            this.mListViewCategory.SetLayoutManager(new LinearLayoutManager(this));

            LoadStoragesData();
            mEditButton.Click += (object sender, EventArgs args) =>
            {
                if (mSelectedCategory > -1)
                {
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    DialogCategoryDetail dialogCategory = new DialogCategoryDetail(mSelectedCategoryClass);
                    dialogCategory.Show(transaction, "dialogue fragment");
                    dialogCategory.OnCompleteCategoryDetail += EditCategoryDialog_OnCategoryEdit;
                }
                else
                {
                    Toast.MakeText(this, "Select Storage First..", ToastLength.Long).Show();
                }

            };
            mAddButton.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogAddStorage dialogStorage = new DialogAddStorage();
                dialogStorage.Show(transaction, "dialogue fragment");
                dialogStorage.OnAddStorageComplete += AddStorageDialog_OnStorageAdd;
            };
            mBackButton.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogAddStorage dialogStorage = new DialogAddStorage();
                dialogStorage.Show(transaction, "dialogue fragment");
                dialogStorage.OnAddStorageComplete += AddStorageDialog_OnStorageAdd;
            };
        }




        private async void LoadStoragesData()
        {
            mProgressDialog = ProgressDialog.Show(this, "Please wait...", "Getting Storage data...", true);
            this.mCategories = new List<CategoryViewModel>();
            this.mCategories = await mCategoryDataService.GetAll();
            this.mCategories.OrderBy(o => o.Name).ToList();
            
            if (mCategories != null)
            {
                this.mCategoryAdapter = new CategoryRecyclerAdapter(this.mCategories, this);
                this.mCategoryAdapter.ItemClick += OnStorageClicked;

                this.mListViewCategory.SetAdapter(this.mCategoryAdapter);
            }
            mProgressDialog.Hide();
        }

        private void OnStorageClicked(object sender, int e)
        {
            mSelectedCategory = e;
            mSelectedCategoryClass = mCategories[e];
            mTextSelectedCategory.Text = mCategories[e].Name;
            
        }

        private void EditCategoryDialog_OnCategoryEdit(object sender, OnCompleteCategoryDetailEventArgs e)
        {

            Category EditedCategory = new Category()
            {
                Name = e.Name,
                Description = e.Description,
            };
            var progressDialog = ProgressDialog.Show(this, "Please wait...", "Editing Category...", true);
            new Thread(new ThreadStart(delegate
            {

                var isAdded = mCategoryDataService.Edit(mCategories[mSelectedCategory].Id, EditedCategory);
                RunOnUiThread(() => progressDialog.Hide());

                if (isAdded)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Category Editted", ToastLength.Long).Show());
                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Failed to Edit, please check again form's field", ToastLength.Long).Show());
                }

            })).Start();
        }
        private void AddStorageDialog_OnStorageAdd(object sender, OnAddStorageEventArgs e)
        {

            Category newLoc = new Category()
            {
                Name = e.Name,
                Description = e.Description,
                //LocationId = LoginActivity.mAuthorizedUserId
               
            };
            var progressDialog = ProgressDialog.Show(this, "Please wait...", "Adding Storage...", true);
            new Thread(new ThreadStart(delegate
            {

                var isAdded = mCategoryDataService.Add(newLoc);
                var temp=newLoc.Id.ToString();
                RunOnUiThread(() => progressDialog.Hide());

                if (isAdded)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Storage Added", ToastLength.Long).Show());
                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Failed to add, please check again form's field", ToastLength.Long).Show());
                }

            })).Start();
        }


    }
   
}