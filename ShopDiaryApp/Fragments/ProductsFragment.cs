using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.Adapter;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;
using System;
using System.Collections.Generic;

namespace ShopDiaryApp.Fragments
{
    public class ProductsFragment: Fragment
    {
        private ProductRecyclerAdapter mProductAdapter;
        public List<ProductViewModel> mProducts;
        public static ProductViewModel mSelectedProductClass;
        private readonly ProductDataService mProductDataService;

        private RecyclerView mListViewStorage;
        private int mSelectedProduct = -1;
        private FragmentTransaction mFragmentTransaction;
        private ImageButton mButtonAdd;
        private ImageButton mButtonEdit;
        private Android.Support.V7.Widget.SearchView mSearchView;
        public ProductsFragment()
        {
            mProductDataService = new ProductDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public static ProductsFragment NewInstance()
        {
            var frag2 = new ProductsFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.ManageProducts, container, false);

            mListViewStorage = view.FindViewById<RecyclerView>(Resource.Id.recyclerProducts);
            mListViewStorage.SetLayoutManager(new LinearLayoutManager(Activity));
            mButtonAdd = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageProductsAdd);
            mButtonEdit = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageProductsCancel);
            mButtonAdd.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new StorageAddFragment(), "Add Storage");
            };
            mButtonEdit.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new StorageEditFragment(), "Edit Storage");
            };
            LoadStorageData();
            return view;
        }
        private async void LoadStorageData()
        {

            List<ProductViewModel> mProductsByUser = await mProductDataService.GetAll();
            mProducts = new List<ProductViewModel>();
            for (int i = 0; mProductsByUser.Count > i; i++)
            {
                    mProducts.Add(mProductsByUser[i]);
            }
            if (mProducts != null)
            {
                mProductAdapter = new ProductRecyclerAdapter(mProducts, this.Activity);
                mProductAdapter.ItemClick += OnLocationClicked;
                mListViewStorage.SetAdapter(this.mProductAdapter);
            }

        }
        private void OnLocationClicked(object sender, int e)
        {
            mSelectedProduct = e;
            mSelectedProductClass = mProducts[e];
            //mTextSelectedLocation.Text = mLocations[e].Name;
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