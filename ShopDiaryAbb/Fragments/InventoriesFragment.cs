using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.Class;
using ShopDiaryAbb.DialogFragments;
using ShopDiaryAbb.Models;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ShopDiaryAbb.Fragments
{
    public class InventoriesFragment : Fragment
    {
        #region field
        private User mUser = LoginPageActivity.StaticUserClass;
        public List<InventoryViewModel> mInventories;
        public List<InventoryViewModel> mFilteredInventories;
        public static InventoryViewModel mSelectedInventoryClass;
        private int QuantityTemp;

        private InventoriesRecyclerAdapter mInventoriesAdapter;
        private RecyclerView mListViewInventories;
        
        private readonly InventoryLogDataService mInventoryLogDataService;
        private readonly InventoryDataService mInventoryDataService;

        private FragmentTransaction mFragmentTransaction;
      
        private ProgressBar mProgressBar;
        private ImageButton mButtonHistory;
        private ImageButton mButtonAdd;
      
        private int mSelectedInventory = -1;
        #endregion
        public InventoriesFragment()
        {
            mInventoryLogDataService = new InventoryLogDataService();
            mInventoryDataService = new InventoryDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public static InventoriesFragment NewInstance()
        {
            var frag2 = new InventoriesFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;

            View view = inflater.Inflate(Resource.Layout.ManageInventoriesLayout, container, false);
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarManageInventories);
            mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
            mButtonHistory = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageInventoriesLog);
            mButtonAdd= view.FindViewById<ImageButton>(Resource.Id.imageButtonManageInventoriesAdd);
            mListViewInventories = view.FindViewById<RecyclerView>(Resource.Id.recylerInventories);
            mListViewInventories.SetLayoutManager(new LinearLayoutManager(Activity));
            LoadInventoriesData();
            mButtonHistory.Click += OnHistoryClicked;
            return view;
        }

       

        private void LoadInventoriesData()
        {
            List<InventoryViewModel> mInventories = LoginPageActivity.mGlobalInventories;

            StorageViewModel SelectedStorage = LoginPageActivity.StaticStorageClass;     

            mFilteredInventories = new List<InventoryViewModel>();
            for (int i = 0; i < mInventories.Count; i++)
            {
                if (mInventories[i].StorageId == SelectedStorage.Id)
                {
                    mFilteredInventories.Add(mInventories[i]);
                }
            }

            if (mFilteredInventories != null)
            {
               
                this.mInventoriesAdapter = new InventoriesRecyclerAdapter(mFilteredInventories, this.Activity);
                this.mInventoriesAdapter.ItemClick += OnInventoryClicked;
                this.Activity.RunOnUiThread(() => mInventoriesAdapter.NotifyDataSetChanged());
                this.Activity.RunOnUiThread(() => this.mListViewInventories.SetAdapter(this.mInventoriesAdapter));

            }
        }
        #region clicked function
        private void OnHistoryClicked(object sender, EventArgs e)
        {
            ReplaceFragment(new InventoryLogsFragment(), "Inventory Logs");
        }
        private void OnInventoryClicked(object sender, int e)
        {
            mSelectedInventoryClass = mFilteredInventories[e];
            LoginPageActivity.StaticInventoryClass = mSelectedInventoryClass;

            //ShowOptionsDialog
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            DialogInventoryOptions InventoriesOptionsDialog = new DialogInventoryOptions();
            InventoriesOptionsDialog.Show(transaction, "dialogue fragment");
            InventoriesOptionsDialog.OnInventoryDialogPicked += InventoryOptions_OnComplete;
        }

        private void InventoryOptions_OnComplete(object sender, OnInventoryDialogPicked e)
        {
           
            var menu = e.MenuItem;
            if (menu == 1)
            {
                if (mSelectedInventoryClass.Id != null)
                {
                    mProgressBar.Visibility = Android.Views.ViewStates.Visible;
                    Inventorylog newInventoryLog = new Inventorylog()
                    {
                        InventoryId = mSelectedInventoryClass.Id,
                        Description = mSelectedInventoryClass.ItemName + " Used By " + mUser.Username,
                        LogDate = DateTime.Now,
                        CreatedUserId = mUser.ID.ToString(),
                        AddedUserId = mUser.ID.ToString()
                    };
                    new Thread(new ThreadStart(delegate
                    {
                        var isAdded = mInventoryLogDataService.Add(newInventoryLog);
                        var isDeleted = mInventoryDataService.Delete(mSelectedInventoryClass.Id);
                        if (isDeleted)
                        {
                            UpdateInventories();
                            LoadInventoriesData();
                            UpgradeProgress();
                            this.Activity.RunOnUiThread(() => mInventoriesAdapter.NotifyDataSetChanged());
                            this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Item Removed", ToastLength.Long).Show());
                            
                            ReplaceFragment(new InventoriesFragment(), "Manage Inventories");
                            mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
                        }
                        else
                        {
                            this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed", ToastLength.Long).Show());
                        }
                    })).Start();
                }
            }
            else if (menu == 2)
            {
                if (mSelectedInventoryClass.Id != null)
                {
                    mProgressBar.Visibility = Android.Views.ViewStates.Visible;
                    Inventorylog newInventoryLog = new Inventorylog()
                    {

                        InventoryId = mSelectedInventoryClass.Id,
                        Description = mSelectedInventoryClass.ItemName + " Throwed By " + mUser.Username,
                        LogDate = DateTime.Now,
                        CreatedUserId = mUser.ID.ToString(),
                        AddedUserId = mUser.ID.ToString()
                    };
                    new Thread(new ThreadStart(delegate
                    {

                        var isAdded = mInventoryLogDataService.Add(newInventoryLog);
                        var isDeleted = mInventoryDataService.Delete(mSelectedInventoryClass.Id);
                        if (isDeleted)
                        {
                            UpdateInventories();
                            LoadInventoriesData();
                            UpgradeProgress();
                            this.Activity.RunOnUiThread(() => mInventoriesAdapter.NotifyDataSetChanged());
                            this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Item Removed", ToastLength.Long).Show());
                            ReplaceFragment(new InventoriesFragment(), "Manage Inventories");
                            mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
                        }
                        else
                        {
                            this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed", ToastLength.Long).Show());
                        }
                    })).Start();
                }
                
            }
            else if (menu == 3)
            {
                
                if (mSelectedInventoryClass.Id != null)
                {
                    mProgressBar.Visibility = Android.Views.ViewStates.Visible;
                    Inventorylog newInventoryLog = new Inventorylog()
                    {
                        InventoryId = mSelectedInventoryClass.Id,
                        Description = mSelectedInventoryClass.ItemName + " Removed By " + mUser.Username,
                        LogDate = DateTime.Now,
                        CreatedUserId = mUser.ID.ToString(),
                        AddedUserId = mUser.ID.ToString()
                    };
                    new Thread(new ThreadStart(delegate
                    {

                        var isAdded = mInventoryLogDataService.Add(newInventoryLog);
                        var isDeleted = mInventoryDataService.Delete(mSelectedInventoryClass.Id);
                        if (isDeleted)
                        {
                            UpdateInventories();
                            LoadInventoriesData();
                            UpgradeProgress();
                            this.Activity.RunOnUiThread(() => mInventoriesAdapter.NotifyDataSetChanged());
                            this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Item Removed", ToastLength.Long).Show());
                            mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
                        }
                        else
                        {
                            this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed", ToastLength.Long).Show());
                        }
                    })).Start();
                }
            }
            else
            {

            }
            
           
        }
        private async void UpdateInventories()
        {
            InventoryDataService mInventoryDataService = new InventoryDataService();
            ProductDataService mProductDataService = new ProductDataService();
            LoginPageActivity.mGlobalProducts = await mProductDataService.GetAll();
            LoginPageActivity.mGlobalInventories = new List<InventoryViewModel>();

            List<InventoryViewModel> tempInventories = await mInventoryDataService.GetAll();
            for (int i = 0; i < tempInventories.Count(); i++)
            {
                for (int j = 0; j < LoginPageActivity.mGlobalProducts.Count(); j++)
                {
                    if (tempInventories[i].ProductId == LoginPageActivity.mGlobalProducts[j].Id)
                    {
                        tempInventories[i].ItemName = LoginPageActivity.mGlobalProducts[j].Name;
                        LoginPageActivity.mGlobalInventories.Add(tempInventories[i]);
                    }
                    if (LoginPageActivity.mGlobalProducts[j].AddedUserId == LoginPageActivity.StaticUserClass.ID.ToString())
                    {
                        LoginPageActivity.mGlobalProductsByUser.Add(LoginPageActivity.mGlobalProducts[j]);
                    }
                }
            }
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
        private void NumberPickerDialogue_OnComplete(object sender, OnNumberPickedEventArgs e)
        {
            QuantityTemp = e.Quantity;
        }
        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.main_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
        #endregion


    }
}