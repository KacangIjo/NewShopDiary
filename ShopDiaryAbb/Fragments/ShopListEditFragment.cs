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
    public class ShopListEditFragment : Android.Support.V4.App.Fragment
    {
        private EditText mShopListName;
        private EditText mShopListDescription;
        private EditText mStoreLocation;

        private Button mButtonSave;
        private Button mButtonDelete;
        private Button mButtonCancel;

        ShoplistDataService shoplistDataService;
        public List<ShoplistViewModel> mShopLists;
        private Guid mSelectedLocationId;

        private FragmentTransaction mFragmentTransaction;
        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;

        public ShopListEditFragment()
        {
            shoplistDataService = new ShoplistDataService();
            mShopLists = new List<ShoplistViewModel>();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }



        public static ShopListEditFragment NewInstance()
        {
            var frag2 = new ShopListEditFragment { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ShopListDetail,container,false);
            mButtonSave = view.FindViewById<Button>(Resource.Id.buttonShopListEditSave);
            mButtonDelete = view.FindViewById<Button>(Resource.Id.buttonShopListEditDelete);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonShopListEditCancel);
            mShopListName = view.FindViewById<EditText>(Resource.Id.editTextShopListDetailName);
            mShopListDescription = view.FindViewById<EditText>(Resource.Id.editTextShopListDetailDescription);
            mShopListName.Text = ShopListFragment.mSelectedShopList.Name;
            mShopListDescription.Text = ShopListFragment.mSelectedShopList.Description;
            mButtonSave.Click += MButtonSave_Click;
            mButtonDelete.Click += MButtonDelete_Click;
            mButtonCancel.Click += MButtonCancel_Click;
            return view;
        }

        private void MButtonCancel_Click(object sender, EventArgs e)
        {
            ReplaceFragment(new ShopListFragment(), "Manage ShopList");
        }
        private void MButtonDelete_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(async delegate
            {

                var isAdded = shoplistDataService.Delete(ShopListFragment.mSelectedShopList.Id);
                if (isAdded)
                {
                    LoginPageActivity.mGlobalShopList = await shoplistDataService.GetAll();
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Success", ToastLength.Long).Show());

                    ReplaceFragment(new ShopListFragment(), "Manage ShopLists");
                }
                else
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed", ToastLength.Long).Show());
                }
            })).Start();
        }

        private void MButtonSave_Click(object sender, EventArgs e)
        {
          
            ShoplistViewModel newShopList = new ShoplistViewModel()
            {
                Name = mShopListName.Text,
                Description = mShopListDescription.Text,
                LocationId = LoginPageActivity.StaticActiveLocationClass.Id.ToString(),
                AddedUserId = LoginPageActivity.StaticUserClass.ID.ToString()

            };

            new Thread(new ThreadStart(async delegate
            {
             
                var isAdded = shoplistDataService.Edit(ShopListFragment.mSelectedShopList.Id, newShopList.ToModel());

                if (isAdded)
                {
                    LoginPageActivity.mGlobalShopList = await shoplistDataService.GetAll();
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Success", ToastLength.Long).Show());
                   
                    ReplaceFragment(new ShopListFragment(), "Manage ShopLists");
                }
                else
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed", ToastLength.Long).Show());
                }
            })).Start();

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
