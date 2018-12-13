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
using System.Linq;

namespace ShopDiaryAbb.Fragments
{
    public class HomeInventoriesFragment : Fragment
    {
        private InventoriesRecyclerAdapter mHomeInventoriesAdapter;
        public List<InventoryViewModel> mFilteredInventories;
        public static InventoryViewModel mSelectedInventoryClass;


        private RecyclerView mListViewStorages;
        private int mSelectedStorage = -1;
        private FragmentTransaction mFragmentTransaction;

        private Button mUse;
        private Button mThrow;
        private Button mRemove;

        public HomeInventoriesFragment()
        {
          
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public static HomeInventoriesFragment NewInstance()
        {
            var frag2 = new HomeInventoriesFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.HomeInventoriesLayout, container, false);
            mListViewStorages = view.FindViewById<RecyclerView>(Resource.Id.recylerHomeInventories);
            mListViewStorages.SetLayoutManager(new LinearLayoutManager(Activity));
            mUse = view.FindViewById<Button>(Resource.Id.buttonHomeInventoriesUse);
            mThrow = view.FindViewById<Button>(Resource.Id.buttonHomeInventoriesThrow);
            mRemove = view.FindViewById<Button>(Resource.Id.buttonHomeInventoriesRemove);
            LoadInventoriesData();
            return view;
        }

        private void LoadInventoriesData()
        {
            var SelectedStorage = HomeStoragesFragment.mSelectedStorageClass;
            var temp = LoginPageActivity.mGlobalInventories;
            mFilteredInventories = temp.Where(i => i.StorageId == SelectedStorage.Id && i.ProductId==HomeFragment.mHomeSelectedInventory.ProductId).ToList();

            this.mHomeInventoriesAdapter = new InventoriesRecyclerAdapter(mFilteredInventories, this.Activity);
            this.mHomeInventoriesAdapter.ItemClick += OnStorageClicked;
            this.mListViewStorages.SetAdapter(this.mHomeInventoriesAdapter);

        }
        

        private void OnStorageClicked(object sender, int e)
        {
            mSelectedStorage = e;
            mSelectedInventoryClass = mFilteredInventories[e];

           
        }


        public override void OnCreateOptionsMenu(IMenu menu,MenuInflater menuInflater)
        {
            //SearchMenu
            menuInflater.Inflate(Resource.Menu.nav_search, menu);
            var searchItem = menu.FindItem(Resource.Id.action_search);
           
            

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