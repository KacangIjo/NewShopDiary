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
    public class ShopItemsFragment : Fragment
    {
        private ShopItemRecycleAdapter mShopItemAdapter;
        public static ShopitemViewModel mSelectedShopItem;
        private readonly StorageDataService mStorageDataService;

        private RecyclerView mListViewShopList;
        private int mSelected = -1;
        private FragmentTransaction mFragmentTransaction;
        private Button mButtonAdd;
        private Button mButtonRemove;


        ShoplistDataService shoplistDataService;
        public List<ShopitemViewModel> mShopItems;
        public ShopItemsFragment()
        {
            mStorageDataService = new StorageDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public static ShopItemsFragment NewInstance()
        {
            var frag2 = new ShopItemsFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.ManageShopItemLayout, container, false);
            mListViewShopList= view.FindViewById<RecyclerView>(Resource.Id.recylerShopItems);
            mListViewShopList.SetLayoutManager(new LinearLayoutManager(this.Activity));
            mButtonAdd = view.FindViewById<Button>(Resource.Id.buttonShopItemAddNew);
            mButtonRemove = view.FindViewById<Button>(Resource.Id.buttonShopItemRemove);
           
            LoadData();
            mButtonAdd.Click += (object sender, EventArgs args) => {
                ReplaceFragment(new ShopListAddFragement(), "Add Shop List");
            };
            mButtonRemove.Click += (object sender, EventArgs args) => {
                ReplaceFragment(new InventoriesFragment(), "Manage Shop Item");
            };
         


            return view;
        }
        private void LoadData()
        {
            var temp = LoginPageActivity.mGlobalShopItem;
            mShopItems = temp.Where(s => s.ShoplistID == ShopListFragment.mSelectedShopList.Id).ToList();
            if (mShopItems != null)
            {

                mShopItemAdapter = new ShopItemRecycleAdapter(mShopItems, this.Activity);
                mShopItemAdapter.ItemClick += OnStorageClicked;
                mListViewShopList.SetAdapter(this.mShopItemAdapter);
            }

        }
        private void OnStorageClicked(object sender, int e)
        {
            mSelected = e;
            mSelectedShopItem = mShopItems[mSelected];
            
         

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