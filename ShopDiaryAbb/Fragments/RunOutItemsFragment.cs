//using Android.OS;
//using Android.Runtime;
//using Android.Support.V4.App;
//using Android.Support.V4.View;
//using Android.Support.V7.Widget;
//using Android.Views;
//using Android.Widget;
//using ShopDiaryApp.Adapter;
//using ShopDiaryApp.Models.ViewModels;
//using ShopDiaryApp.Services;
//using System;
//using System.Collections.Generic;

//namespace ShopDiaryApp.Fragments
//{
//    public class RunOutItemsFragment : Fragment
//    {
//        private LocationsRecycleAdapter mLocationsAdapter;
//        public List<InventoryViewModel> mInventories;
//        static InventoryViewModel mSelectedInventoryClass;
//        private readonly InventoryDataService mInventoryDataService;

//        private RecyclerView mListViewRunOutItems;
//        private int mSelectedItems = -1;
//        private FragmentTransaction mFragmentTransaction;
//        private ImageButton mButtonAdd;
//        private ImageButton mButtonEdit;
//        private Android.Support.V7.Widget.SearchView mSearchView;

//        public RunOutItemsFragment()
//        {
//            mInventoryDataService = new InventoryDataService();
//        }
//        public override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);
//            HasOptionsMenu = true;
//            // Create your fragment here
         

//        }

//        public static RunOutItemsFragment NewInstance()
//        {
//            var frag2 = new RunOutItemsFragment { Arguments = new Bundle() };
//            return frag2;
//        }



//        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
//        {
//            HasOptionsMenu = true;
//            View view = inflater.Inflate(Resource.Layout.RunOutItemsLayout, container, false);

//            mListViewRunOutItems = view.FindViewById<RecyclerView>(Resource.Id.recyclerLocations);
//            mListViewRunOutItems.SetLayoutManager(new LinearLayoutManager(Activity));
//            mButtonAdd = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageLocationAdd);
//            mButtonEdit = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageLocationEdit);
//            mButtonAdd.Click += (object sender, EventArgs args) =>
//            {
//                ReplaceFragment(new LocationAddFragment(), "Add Location");
//            };
//            mButtonEdit.Click += (object sender, EventArgs args) =>
//            {
//                ReplaceFragment(new LocationEditFragment(), "Edit Location");
//            };
//            LoadLocationData();
//            return view;
//        }

//        private async void LoadLocationData()
//        {
            
//            List<LocationViewModel> mLocationsByUser = await mLocationDataService.GetAll();
//            mLocations = new List<LocationViewModel>();
//            for (int i = 0; mLocationsByUser.Count > i; i++)
//            {
//                if (mLocationsByUser[i].AddedUserId == LoginPageActivity.StaticUserClass.ID.ToString())
//                {
//                    mLocations.Add(mLocationsByUser[i]);
//                }
//            }
//            if (mLocations != null)
//            {

//                mLocationsAdapter = new LocationsRecycleAdapter(mLocations, this.Activity);
//                mLocationsAdapter.ItemClick += OnLocationClicked;
//                mListViewRunOutItems.SetAdapter(this.mLocationsAdapter);
//            }
//        }

//        private void OnLocationClicked(object sender, int e)
//        {
//            mSelectedLocation = e;
//            mSelectedLocationClass = mLocations[e];
//            //mTextSelectedLocation.Text = mLocations[e].Name;
//            MainActivity.StaticLocationClass.Id = mLocations[e].Id;
//            MainActivity.StaticLocationClass.Name = mLocations[e].Name;
//            MainActivity.StaticLocationClass.Address= mLocations[e].Address;
//            MainActivity.StaticLocationClass.Description = mLocations[e].Description;
//        }


//        public override void OnCreateOptionsMenu(IMenu menu,MenuInflater menuInflater)
//        {
//            //SearchMenu
//            menuInflater.Inflate(Resource.Menu.nav_search, menu);
//            var searchItem = menu.FindItem(Resource.Id.action_search);
//            var provider = MenuItemCompat.GetActionView(searchItem);
//            mSearchView = provider.JavaCast<Android.Support.V7.Widget.SearchView>();
//            mSearchView.QueryTextChange += (s, e) => mLocationsAdapter.Filter.InvokeFilter(e.NewText);
//            mSearchView.QueryTextSubmit += (s, e) =>
//            {
//                Toast.MakeText(this.Activity, "You searched: " + e.Query, ToastLength.Short).Show();
//                e.Handled = true;
//            };
//            MenuItemCompat.SetOnActionExpandListener(searchItem, new SearchViewExpandListener(mLocationsAdapter));
            

//        }
//        private class SearchViewExpandListener : Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
//        {
//            private readonly IFilterable _adapter;

//            public SearchViewExpandListener(IFilterable adapter)
//            {
//                _adapter = adapter;
//            }

//            public bool OnMenuItemActionCollapse(IMenuItem item)
//            {
//                _adapter.Filter.InvokeFilter("");
//                return true;
//            }

//            public bool OnMenuItemActionExpand(IMenuItem item)
//            {
//                return true;
//            }
//        }

//        public void ReplaceFragment(Fragment fragment, string tag)
//        {
//            mFragmentTransaction = FragmentManager.BeginTransaction();
//            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
//            mFragmentTransaction.AddToBackStack(tag);
//            mFragmentTransaction.CommitAllowingStateLoss();
//        }
//    }
//}