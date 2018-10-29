using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.Adapter;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;
using System.Collections.Generic;
namespace ShopDiaryApp.Fragments
{
    public class InventoriesFragment : Fragment
    {
        #region field
        private InventoriesRecyclerAdapter mInventoriesAdapter;
        public List<InventoryViewModel> mInventories;
        static InventoryViewModel mSelectedInventoryClass;
        private readonly InventoryDataService mInventoryDataService;

        private RecyclerView mListViewInventories;
        
        private FragmentTransaction mFragmentTransaction;
        private Button mButtonUse;
        private Button mButtonThrow;
        private Button mButtonRemove;
        private Android.Support.V7.Widget.SearchView mSearchView;

        private int mSelectedInventory = -1;
        #endregion
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
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

            mListViewInventories = view.FindViewById<RecyclerView>(Resource.Id.recylerInventories);
            mListViewInventories.SetLayoutManager(new LinearLayoutManager(Activity));
            LoadInventoriesData();
            return view;
        }
        private void LoadInventoriesData()
        {
            List<InventoryViewModel> mInventories = LoginPageActivity.mGlobalInventories;

            StorageViewModel SelectedStorage = MainActivity.StaticStorageClass;     

            List<InventoryViewModel> mFilteredInventories = new List<InventoryViewModel>();
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
        private void OnInventoryClicked(object sender, int e)
        {
            
            mSelectedInventoryClass = mInventories[e];
            //mTextSelectedLocation.Text = mLocations[e].Name;
   

        }


    }
}