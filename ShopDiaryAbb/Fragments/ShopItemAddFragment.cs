using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;

namespace ShopDiaryAbb.Fragments
{
    public class ShopItemAddFragment : Android.Support.V4.App.Fragment
    {
        private EditText Name;
        private EditText Quantity;
        private EditText mStoreLocation;

        private Button mButtonAdd;
        private Button mButtonCancel;
        private ProgressBar mProgressBar;

        ShopItemDataService mShopItemDataService;
        public List<ShoplistViewModel> mShopLists;
        private Guid mSelectedLocationId;

        private FragmentTransaction mFragmentTransaction;
        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;

        public ShopItemAddFragment()
        {
            mShopItemDataService = new ShopItemDataService();
            mShopLists = new List<ShoplistViewModel>();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }



        public static ShopItemAddFragment NewInstance()
        {
            var frag2 = new ShopItemAddFragment { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ManageShopItemAdd,container,false);
            mButtonAdd = view.FindViewById<Button>(Resource.Id.buttonAddShopList);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonCancelShopList);
            Name = view.FindViewById<EditText>(Resource.Id.editTextAddShopItemName);
            Quantity = view.FindViewById<EditText>(Resource.Id.editTextAddShopItemQuantity);
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarAddShopList);
            mButtonAdd.Click += MButtonAdd_Click;
            mButtonCancel.Click += MButtonCancel_Click;
            return view;
        }

        private void MButtonCancel_Click(object sender, EventArgs e)
        {
            ReplaceFragment(new ShopListFragment(), "Manage ShopList");
        }

        private void MButtonAdd_Click(object sender, EventArgs e)
        {
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;
            ShopitemViewModel newShopList = new ShopitemViewModel()
            {
                ItemName = Name.Text,
                Quantity = int.Parse(Quantity.Text),
                ShoplistId = ShopListFragment.mSelectedShopList.Id,
                AddedUserId = LoginPageActivity.StaticUserClass.ID.ToString()

            };

            new Thread(new ThreadStart(async delegate
            {
                UpgradeProgress();
                var isAdded = mShopItemDataService.Add(newShopList.ToModel());

                if (isAdded)
                {
                    LoginPageActivity.mGlobalShopItem = await mShopItemDataService.GetAll();
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "ShopList Added", ToastLength.Long).Show());
                    mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
                    ReplaceFragment(new ShopListFragment(), "Manage ShopLists");
                }
                else
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed", ToastLength.Long).Show());
                }
            })).Start();

        }

        private void UpgradeProgress()
        {
            int progressvalue = 0;
            while (progressvalue < 100)
            {
                progressvalue += 10;
                mProgressBar.Progress = progressvalue;
                Thread.Sleep(300);
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
