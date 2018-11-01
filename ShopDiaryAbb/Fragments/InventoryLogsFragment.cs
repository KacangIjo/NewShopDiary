using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.Class;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;
using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ShopDiaryAbb.Fragments
{
    public class InventoryLogsFragment : Fragment
    {
        #region field
        private User mUser = LoginPageActivity.StaticUserClass;
        public List<InventoryViewModel> mInventories;
        public List<InventoryViewModel> mFilteredInventories;

        private InventoryLogsRecyclerAdapter mInventoryLogsAdapter;
        private RecyclerView mListViewInventoryLogs;
        
        private readonly InventoryLogDataService mInventoryLogDataService;
        private readonly InventoryDataService mInventoryDataService;

        private FragmentTransaction mFragmentTransaction;

        private Android.Support.V7.Widget.SearchView mSearchView;


        #endregion
        public InventoryLogsFragment()
        {
            mInventoryLogDataService = new InventoryLogDataService();
            mInventoryDataService = new InventoryDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public static InventoryLogsFragment NewInstance()
        {
            var frag2 = new InventoryLogsFragment { Arguments = new Bundle() };
            return frag2;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.ManageInvetoriesLogLayout, container, false);

            mListViewInventoryLogs = view.FindViewById<RecyclerView>(Resource.Id.recyclerInventoryLogs);
            mListViewInventoryLogs.SetLayoutManager(new LinearLayoutManager(Activity));
            LoadInventoriesData();
          
            return view;
        }

    
        private async void LoadInventoriesData()
        {
            List<InventoryViewModel> mInventories = LoginPageActivity.mGlobalInventories;
            List<InventorylogViewModel> mInventoryLogs = await mInventoryLogDataService.GetAll();
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
                this.mInventoryLogsAdapter = new InventoryLogsRecyclerAdapter(mFilteredInventories,mInventoryLogs, this.Activity);
                this.mListViewInventoryLogs.SetAdapter(this.mInventoryLogsAdapter);
            }
        }
       

    }
}