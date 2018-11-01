using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.Adapter;
using ShopDiaryApp.Class;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;
using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ShopDiaryApp.Fragments
{
    public class InventoriesFragment : Fragment
    {
        #region field
        private User mUser = LoginPageActivity.StaticUserClass;
        public List<InventoryViewModel> mInventories;
        public List<InventoryViewModel> mFilteredInventories;
        static InventoryViewModel mSelectedInventoryClass;

        private InventoriesRecyclerAdapter mInventoriesAdapter;
        private RecyclerView mListViewInventories;
        
        private readonly InventoryLogDataService mInventoryLogDataService;
        private readonly InventoryDataService mInventoryDataService;

        private FragmentTransaction mFragmentTransaction;
        private Button mButtonUse;
        private Button mButtonThrow;
        private Button mButtonRemove;
        private ProgressBar mProgressBar;
        private Android.Support.V7.Widget.SearchView mSearchView;

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
            mButtonUse= view.FindViewById<Button>(Resource.Id.buttonManageInventoriesUse);
            mButtonThrow = view.FindViewById<Button>(Resource.Id.buttonManageInventoriesThrow);
            mButtonRemove = view.FindViewById<Button>(Resource.Id.buttonManageInventoriesRemove);
            var mButtonHistory = view.FindViewById<Button>(Resource.Id.buttonManageInventoriesLogs);
            mListViewInventories = view.FindViewById<RecyclerView>(Resource.Id.recylerInventories);
            mListViewInventories.SetLayoutManager(new LinearLayoutManager(Activity));
            LoadInventoriesData();
            mButtonUse.Click += OnUseClicked;
            mButtonThrow.Click += OnThrowClicked;
            mButtonRemove.Click += OnRemoveClicked;
            mButtonHistory.Click += OnHistoryClicked;
            return view;
        }

       

        private void LoadInventoriesData()
        {
            List<InventoryViewModel> mInventories = LoginPageActivity.mGlobalInventories;

            StorageViewModel SelectedStorage = MainActivity.StaticStorageClass;     

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
                this.mListViewInventories.SetAdapter(this.mInventoriesAdapter);
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
            MainActivity.StaticInventoryClass = mSelectedInventoryClass;
        }
        private void OnRemoveClicked(object sender, EventArgs e)
        {
            if (mSelectedInventoryClass.Id != null)
            {
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
                        UpgradeProgress();
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
        private void OnThrowClicked(object sender, EventArgs e)
        {
            if (mSelectedInventoryClass.Id != null)
            {
                if (mSelectedInventoryClass.Id != null)
                {
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
                            UpgradeProgress();
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
        }
        private void OnUseClicked(object sender, EventArgs e)
        {
            if (mSelectedInventoryClass.Id != null)
            {
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
                        UpgradeProgress();
                        
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
        private async void UpdateInventories()
        {
            LoginPageActivity.mGlobalInventories.Clear();
            List<ProductViewModel> mProducts = LoginPageActivity.mGlobalProducts;
            List<InventoryViewModel> tempInventories = await mInventoryDataService.GetAll();
            for (int i = 0; i < tempInventories.Count(); i++)
            {
                for (int j = 0; j < mProducts.Count(); j++)
                {

                    if (tempInventories[i].ProductId == mProducts[j].Id)
                    {
                        tempInventories[i].ItemName = mProducts[j].Name;
                        LoginPageActivity.mGlobalInventories.Add(tempInventories[i]);
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
        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
        #endregion


    }
}