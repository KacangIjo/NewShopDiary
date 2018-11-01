using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;
using System;
using System.Collections.Generic;

namespace ShopDiaryAbb.Fragments
{
    public class CategoriesFragment : Fragment
    {
        private CategoryRecyclerAdapter mCategoryAdapter;
        public List<CategoryViewModel> mCategories;
        static CategoryViewModel mSelectedCategoryClass;
        private readonly CategoryDataService mCategoryDataService;

        private RecyclerView mListViewCategory;
        private int mSelectedCategory = -1;
        private FragmentTransaction mFragmentTransaction;
        private ImageButton mButtonAdd;
        private ImageButton mButtonEdit;
        private Android.Support.V7.Widget.SearchView mSearchView;

        public CategoriesFragment()
        {
            mCategoryDataService = new CategoryDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public static CategoriesFragment NewInstance()
        {
            var frag2 = new CategoriesFragment { Arguments = new Bundle() };
            return frag2;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.ManageCategoriesLayout, container, false);
            
            mListViewCategory = view.FindViewById<RecyclerView>(Resource.Id.recyclerCategories);
            mListViewCategory.SetLayoutManager(new LinearLayoutManager(Activity));
            mButtonAdd = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageCategoriesAdd);
            mButtonEdit = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageLocationEdit);
            mButtonAdd.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new CategoryAddFragment(), "Add Category");
            };
            mButtonEdit.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new CategoryEditFragment(), "Edit Category");
            };
            LoadCategoriesData();
            return view;
        }

        private async void LoadCategoriesData()
        {
            
            List<CategoryViewModel> mCategoriesByUser = await mCategoryDataService.GetAll();
            mCategories = new List<CategoryViewModel>();
            for (int i = 0; mCategoriesByUser.Count > i; i++)
            {
                if (mCategoriesByUser[i].UserId == LoginPageActivity.StaticUserClass.ID.ToString())
                {
                    mCategories.Add(mCategoriesByUser[i]);
                }
            }
            if (mCategories != null)
            {

                mCategoryAdapter = new CategoryRecyclerAdapter(mCategories, this.Activity);
                mCategoryAdapter.ItemClick += OnLocationClicked;
                mListViewCategory.SetAdapter(this.mCategoryAdapter);
            }
        }

        private void OnLocationClicked(object sender, int e)
        {
            mSelectedCategory = e;
            mSelectedCategoryClass = mCategories[e];
            //mTextSelectedLocation.Text = mLocations[e].Name;
            MainActivity.StaticLocationClass.Id = mCategories[e].Id;
            MainActivity.StaticLocationClass.Name = mCategories[e].Name;
            MainActivity.StaticLocationClass.Description = mCategories[e].Description;
        }


        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            //SearchMenu
            inflater.Inflate(Resource.Menu.nav_search, menu);
            var searchItem = menu.FindItem(Resource.Id.action_search);
            var provider = MenuItemCompat.GetActionView(searchItem);
            mSearchView = provider.JavaCast<Android.Support.V7.Widget.SearchView>();
            mSearchView.QueryTextChange += (s, e) => mCategoryAdapter.Filter.InvokeFilter(e.NewText);
            mSearchView.QueryTextSubmit += (s, e) =>
            {
                Toast.MakeText(this.Activity, "You searched: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;
            };
            //MenuItemCompat.SetOnActionExpandListener(searchItem, new SearchViewExpandListener(mCategoryAdapter));

        }

        //private class SearchViewExpandListener : Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
        //{
        //    private readonly IFilterable _adapter;

        //    public SearchViewExpandListener(IFilterable adapter)
        //    {
        //        _adapter = adapter;
        //    }

        //    public bool OnMenuItemActionCollapse(IMenuItem item)
        //    {
        //        _adapter.Filter.InvokeFilter("");
        //        return true;
        //    }

        //    public bool OnMenuItemActionExpand(IMenuItem item)
        //    {
        //        return true;
        //    }
        //}

        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
    }
}