using Android.OS;
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
    public class ShopListFragment : Fragment
    {
        private StoragesRecycleAdapter mStoragesAdapter;
        public List<StorageViewModel> mStorages;
        static StorageViewModel mSelectedStorageClass;
        private readonly StorageDataService mStorageDataService;

        private RecyclerView mListViewShopList;
        private int mSelectedStorage = -1;
        private FragmentTransaction mFragmentTransaction;
        private ImageButton mButtonAdd;
        private ImageButton mButtonView;
        private ImageButton mButtonEdit;

        ShoplistDataService shoplistDataService;
        public List<ShoplistViewModel> mShopLists;
        public ShopListFragment()
        {
            mStorageDataService = new StorageDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public static ShopListFragment NewInstance()
        {
            var frag2 = new ShopListFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.ManageShopList, container, false);
            mListViewShopList= view.FindViewById<RecyclerView>(Resource.Id.recyclerViewShopList);
            mListViewShopList.SetLayoutManager(new LinearLayoutManager(Activity));
            mButtonView = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageShopListAdd);
            mButtonView = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageShopListView);
            mButtonView = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageShopListEdit);



            return view;
        }
        private async void LoadStorageData()
        {

          
            mShopLists = LoginPageActivity.mGlobalShopList.Where(s => s.LocationId == LoginPageActivity.StaticActiveLocationClass.Id.ToString()).ToList();

           
            if (mStorages != null)
            {

                mStoragesAdapter = new StoragesRecycleAdapter(mStorages, this.Activity);
                mStoragesAdapter.ItemClick += OnStorageClicked;
                mListViewShopList.SetAdapter(this.mStoragesAdapter);
            }

        }
        private void OnStorageClicked(object sender, int e)
        {
            mSelectedStorage = e;
            mSelectedStorageClass = mStorages[e];
            //mTextSelectedLocation.Text = mLocations[e].Name;
            LoginPageActivity.StaticStorageClass = mStorages[e];
            ReplaceFragment(new InventoriesFragment(), mStorages[e].Name.ToString());

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