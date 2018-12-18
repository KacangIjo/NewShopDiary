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
using System.Threading;

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
        private Button mButtonCancel; 

        ShopItemDataService mShopItemDataService=new ShopItemDataService();
        public List<ShopitemViewModel> mShopItems=new List<ShopitemViewModel>();

        

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
            mButtonCancel= view.FindViewById<Button>(Resource.Id.buttonAddShopItemCancel);
            LoadData();
            mButtonAdd.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new ShopItemAddFragment(), "Manage Shop List");
            };
            mButtonRemove.Click += (object sender, EventArgs args) => {
               
            };
            //mButtonCancel.Click += (object sender, EventArgs args) => {
            //    ReplaceFragment(new ShopListFragment(), "Manage Shop List");
            //};



            return view;
        }
        private void LoadData()
        {
            var temp = LoginPageActivity.mGlobalShopItem;
            mShopItems = temp.Where(s => s.ShoplistId== ShopListFragment.mSelectedShopList.Id).ToList();
            if (mShopItems != null)
            {

                mShopItemAdapter = new ShopItemRecycleAdapter(mShopItems, this.Activity);
                mShopItemAdapter.ItemClick += OnStorageClicked;
                this.Activity.RunOnUiThread(() => mShopItemAdapter.NotifyDataSetChanged());
                this.Activity.RunOnUiThread(() => this.mListViewShopList.SetAdapter(this.mShopItemAdapter));
            }

        }
        private void OnStorageClicked(object sender, int e)
        {
            mSelected = e;
            mSelectedShopItem = mShopItems[mSelected];

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            DialogShopItemOptions ShopItemsDialog = new DialogShopItemOptions();
            ShopItemsDialog.Show(transaction, "dialogue fragment");
            ShopItemsDialog.OnShopItemOptionPicked += ShopItemOptions_OnComplete;

        }

        private void ShopItemOptions_OnComplete(object sender, OnShopItemOptionPicked e)
        {
            if (e.MenuItem == 1)
            {
                new Thread(new ThreadStart(async delegate
                {
                    var isAdded = mShopItemDataService.Delete(mSelectedShopItem.Id);
                    if (isAdded)
                    {
                        mShopItems.Clear();
                        LoginPageActivity.mGlobalShopItem = await mShopItemDataService.GetAll();
                        LoadData();
                        this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Removed", ToastLength.Long).Show());
                    }
                    else
                    {
                        this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed", ToastLength.Long).Show());
                    }
                })).Start();
            }
            else
                this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Canceled", ToastLength.Long).Show());
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