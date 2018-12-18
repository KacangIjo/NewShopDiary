using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.DialogFragments;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopDiaryAbb.Fragments
{
    public class ShopListFragment : Fragment
    {
        private ShopListsRecylceAdapter mShopListAdapter;
        public List<StorageViewModel> mStorages;
        public static ShoplistViewModel mSelectedShopList=new ShoplistViewModel();
        private readonly StorageDataService mStorageDataService;

        private RecyclerView mListViewShopList;
        private int mSelected = -1;
        private FragmentTransaction mFragmentTransaction;
        private Button mButtonAdd;
        private Button mButtonView;
        private Button mButtonEdit;

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
            mListViewShopList= view.FindViewById<RecyclerView>(Resource.Id.recylerShopLists);
            mListViewShopList.SetLayoutManager(new LinearLayoutManager(this.Activity));
            mButtonAdd = view.FindViewById<Button>(Resource.Id.buttonShopListAdd);
            mButtonView = view.FindViewById<Button>(Resource.Id.buttonShopListView);
            mButtonEdit = view.FindViewById<Button>(Resource.Id.buttonManageShopListEdit);
            LoadData();
            mButtonAdd.Click += (object sender, EventArgs args) => {
                ReplaceFragment(new ShopListAddFragement(), "Add Shop List");
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
                mShopListAdapter.ItemClick += OnShopListClicked;
                this.Activity.RunOnUiThread(() => mShopListAdapter.NotifyDataSetChanged());
                this.Activity.RunOnUiThread(() => this.mListViewShopList.SetAdapter(this.mShopListAdapter));
            }

        }
        private void OnShopListClicked(object sender, int e)
        {
            mSelected = e;
            mSelectedShopList = mShopLists[e];

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            DialogShopListOptions DialogShopList = new DialogShopListOptions();
            DialogShopList.Show(transaction, "dialogue fragment");
            DialogShopList.OnShopListOptionPicked += ShopListOptions_OnComplete;
        }

        private void ShopListOptions_OnComplete(object sender, OnShopListOptionPicked e)
        {
            var IsLoadData = e.IsNeedLoadData;
            if (IsLoadData == true)
            {
                LoadData();
            }
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