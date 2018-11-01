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

            List<StorageViewModel> mStoragesByUser = LoginPageActivity.mGlobalStorages;
            List<InventoryViewModel> mInventories = HomeFragment.mInventoriesByProduct;
            List<StorageViewModel> tempStorage = new List<StorageViewModel>();
            LocationViewModel mSelectedLocation = MainActivity.StaticActiveLocationClass;

            ProductViewModel mSelectedProduct = HomeFragment.mHomeSelectedProduct;

            mFilteredStorage = new List<StorageViewModel>();
            for (int i = 0; i< mStoragesByUser.Count ; i++)
            {
                if (mStoragesByUser[i].LocationId == mSelectedLocation.Id)
                {
                    for (int j = 0; j < mInventories.Count; j++)
                    {
                        if (mStoragesByUser[i].Id == mInventories[j].StorageId)
                        {
                            tempStorage.Add(mStoragesByUser[i]);
                        }
                    }
                }
            }
            mFilteredStorage = tempStorage.GroupBy(s => s.Id).Select(group => group.First()).ToList();
                


            if (mFilteredStorage != null)
            {
                this.mStorageAdapter = new HomeStorageAdapter(mInventories, mFilteredStorage, mSelectedProduct , this.Activity);
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
            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
    }
}