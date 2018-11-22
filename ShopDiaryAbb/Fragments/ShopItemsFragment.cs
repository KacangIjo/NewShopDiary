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
        private ShopListsRecylceAdapter mShopListAdapter;
        public List<StorageViewModel> mStorages;
        public static ShoplistViewModel mSelectedShopList;
        private readonly StorageDataService mStorageDataService;

        private RecyclerView mListViewShopList;
        private int mSelected = -1;
        private FragmentTransaction mFragmentTransaction;
        private ImageButton mButtonAdd;
        private ImageButton mButtonView;
        private ImageButton mButtonEdit;

        ShoplistDataService shoplistDataService;
        public List<ShoplistViewModel> mShopLists;
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
            mButtonAdd = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageShopListAdd);
            mButtonView = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageShopListView);
            mButtonEdit = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageShopListEdit);
            LoadData();
            mButtonAdd.Click += (object sender, EventArgs args) => {
                ReplaceFragment(new ShopListAddFragement(), "Add Shop List");
            };
            mButtonView.Click += (object sender, EventArgs args) => {
                ReplaceFragment(new InventoriesFragment(), "Manage Shop Item");
            };
            mButtonEdit.Click += (object sender, EventArgs args) => {
                ReplaceFragment(new InventoriesFragment(), "Manage Shop Item");
            };



            return view;
        }
        private void LoadData()
        {
            var temp = LoginPageActivity.mGlobalShopList;
            mShopLists = LoginPageActivity.mGlobalShopList.Where(s => s.LocationId == LoginPageActivity.StaticActiveLocationClass.Id.ToString()).ToList();
            if (mShopLists != null)
            {

                mShopListAdapter = new ShopListsRecylceAdapter(mShopLists, this.Activity);
                mShopListAdapter.ItemClick += OnStorageClicked;
                mListViewShopList.SetAdapter(this.mShopListAdapter);
            }

        }
        private void OnStorageClicked(object sender, int e)
        {
            mSelected = e;
            mSelectedShopList = mShopLists[mSelected];
            
         

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