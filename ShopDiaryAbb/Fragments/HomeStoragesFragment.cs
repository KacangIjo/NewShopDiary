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
    public class HomeStoragesFragment : Fragment
    {
        private HomeStorageAdapter mStorageAdapter;
        public List<StorageViewModel> mFilteredStorage;
        public static StorageViewModel mSelectedStorageClass;


        private RecyclerView mListViewStorages;
        private int mSelectedStorage = -1;
        private FragmentTransaction mFragmentTransaction;

        private Android.Support.V7.Widget.SearchView mSearchView;

        public HomeStoragesFragment()
        {
          
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public static HomeStoragesFragment NewInstance()
        {
            var frag2 = new HomeStoragesFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.HomeStoragesLayout, container, false);
            
            mListViewStorages = view.FindViewById<RecyclerView>(Resource.Id.recyclerHomeStorage);
            mListViewStorages.SetLayoutManager(new LinearLayoutManager(Activity));
            LoadLocationData();
            return view;
        }

        private void LoadLocationData()
        {
            //List<InventoryViewModel> mInventories = LoginPageActivity.mGlobalInventories;
            //List<StorageViewModel> tempStorage = new List<StorageViewModel>();
            //LocationViewModel mSelectedLocation = LoginPageActivity.StaticActiveLocationClass;
            //List<InventoryViewModel> InventoriesByLocation = mInventories.Join(StorageByLocation, i => i.StorageId, s => s.Id, (i, s) => i).Where(i => i.ExpirationDate < DateTime.Now).ToList();
            //List<StorageViewModel> mStoragesByUser = LoginPageActivity.mGlobalStorages.Where(s => s.LocationId == mSelectedLocation.Id).ToList();
            //mStoragesByUser.Join(mInventories,i=>i.st)

            List<StorageViewModel> mStoragesByUser = HomeFragment.mStorages;
            List<InventoryViewModel> temp = HomeFragment.mInventories;
            mFilteredStorage = mStoragesByUser.Join(temp, i => i.Id, s => s.StorageId, (i, s) => i)
                 .GroupBy(i => i.Id)
                 .Select(g => g.First())
                 .ToList();


            if (mFilteredStorage != null)
            {
                this.mStorageAdapter = new HomeStorageAdapter(mFilteredStorage, this.Activity);
                this.mStorageAdapter.ItemClick += OnStorageClicked;
                this.mListViewStorages.SetAdapter(this.mStorageAdapter);
            }
        }

        private void OnStorageClicked(object sender, int e)
        {
            mSelectedStorage = e;
            mSelectedStorageClass = mFilteredStorage[e];
            ReplaceFragment(new HomeInventoriesFragment(), mSelectedStorageClass.Name.ToString());
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
            mFragmentTransaction.Replace(Resource.Id.main_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
    }
}