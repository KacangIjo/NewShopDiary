using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
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
    public class HomeInventoriesFragment : Fragment
    {
        private User mUser = LoginPageActivity.StaticUserClass;
        private InventoriesRecyclerAdapter mHomeInventoriesAdapter;
        public List<InventoryViewModel> mFilteredInventories;
        public static InventoryViewModel mSelectedInventoryClass;

        private readonly InventoryLogDataService mInventoryLogDataService;
        private readonly InventoryDataService mInventoryDataService;

        private RecyclerView mListViewStorages;
        private int mSelectedStorage = -1;
        private FragmentTransaction mFragmentTransaction;

        public HomeInventoriesFragment()
        {
            mInventoryLogDataService = new InventoryLogDataService();
            mInventoryDataService = new InventoryDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public static HomeInventoriesFragment NewInstance()
        {
            var frag2 = new HomeInventoriesFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.HomeInventoriesLayout, container, false);
            mListViewStorages = view.FindViewById<RecyclerView>(Resource.Id.recylerHomeInventories);
            mListViewStorages.SetLayoutManager(new LinearLayoutManager(Activity));
        
            LoadInventoriesData();
            return view;
        }

        private void LoadInventoriesData()
        {
            var SelectedStorage = HomeStoragesFragment.mSelectedStorageClass;
            var temp = LoginPageActivity.mGlobalInventories;
            mFilteredInventories = temp.Where(i => i.StorageId == SelectedStorage.Id && i.ProductId==HomeFragment.mHomeSelectedInventory.ProductId&&i.ExpirationDate<DateTime.Now).ToList();

            this.mHomeInventoriesAdapter = new InventoriesRecyclerAdapter(mFilteredInventories, this.Activity);
            this.mHomeInventoriesAdapter.ItemClick += OnStorageClicked;
            
            this.Activity.RunOnUiThread(() => mHomeInventoriesAdapter.NotifyDataSetChanged());
            this.Activity.RunOnUiThread(() => this.mListViewStorages.SetAdapter(this.mHomeInventoriesAdapter));
            

        }
        

        private void OnStorageClicked(object sender, int e)
        {
            mSelectedStorage = e;
            mSelectedInventoryClass = mFilteredInventories[e];
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            DialogHomeInventoryOptions InventoriesOptionsDialog = new DialogHomeInventoryOptions();
            InventoriesOptionsDialog.Show(transaction, "dialogue fragment");
            InventoriesOptionsDialog.OnHomeInventoryDialogPicked += InventoryOptions_OnComplete;

        }

        private void InventoryOptions_OnComplete(object sender, OnHomeInventoryDialogPicked e)
        {
            var menu = e.MenuItem;
            if (menu == 1)
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
                            LoadInventoriesData();

                            this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Item Removed", ToastLength.Long).Show());
                            ReplaceFragment(new InventoriesFragment(), "Manage Inventories");
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
                            this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Item Removed", ToastLength.Long).Show());
                            ReplaceFragment(new InventoriesFragment(), "Manage Inventories");
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
                            this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Item Removed", ToastLength.Long).Show());
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

        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
    }
}