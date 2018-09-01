using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.Adapter;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;
using System;
using System.Collections.Generic;

namespace ShopDiaryApp.Fragments
{
    public class LocationsFragment : Fragment
    {
        private LocationsRecycleAdapter mLocationsAdapter;
        public List<LocationViewModel> mLocations;
        static LocationViewModel mSelectedLocationClass;
        private readonly LocationDataService mLocationDataService;

        private RecyclerView mListViewLocations;
        private int mSelectedLocation = -1;
        private FragmentTransaction mFragmentTransaction;
        private ImageButton mButtonAdd;
        private ImageButton mButtonEdit;
        private Android.Support.V7.Widget.SearchView mSearchView;

        public LocationsFragment()
        {
            mLocationDataService = new LocationDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
            // Create your fragment here
         

        }

        public static LocationsFragment NewInstance()
        {
            var frag2 = new LocationsFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.ManageLocationsLayout, container, false);
            
            mListViewLocations = view.FindViewById<RecyclerView>(Resource.Id.recyclerLocations);
            mListViewLocations.SetLayoutManager(new LinearLayoutManager(Activity));
            mButtonAdd = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageLocationAdd);
            mButtonEdit = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageLocationEdit);
            mButtonAdd.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new LocationAddFragment(), "Add Location");
            };
            mButtonEdit.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new LocationEditFragment(), "Edit Location");
            };
            LoadLocationData();
            return view;
        }

        private async void LoadLocationData()
        {
            
            List<LocationViewModel> mLocationsByUser = await mLocationDataService.GetAll();
            mLocations = new List<LocationViewModel>();
            for (int i = 0; mLocationsByUser.Count > i; i++)
            {
                if (mLocationsByUser[i].AddedUserId == LoginPageActivity.StaticUserClass.ID.ToString())
                {
                    mLocations.Add(mLocationsByUser[i]);
                }
            }
            if (mLocations != null)
            {

                mLocationsAdapter = new LocationsRecycleAdapter(mLocations, this.Activity);
                mLocationsAdapter.ItemClick += OnLocationClicked;
                mListViewLocations.SetAdapter(this.mLocationsAdapter);
            }
        }

        private void OnLocationClicked(object sender, int e)
        {
            mSelectedLocation = e;
            mSelectedLocationClass = mLocations[e];
            //mTextSelectedLocation.Text = mLocations[e].Name;
            MainActivity.StaticLocationClass.Id = mLocations[e].Id;
            MainActivity.StaticLocationClass.Name = mLocations[e].Name;
            MainActivity.StaticLocationClass.Address= mLocations[e].Address;

            MainActivity.StaticLocationClass.Description = mLocations[e].Description;
        }


        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            //SearchMenu
            inflater.Inflate(Resource.Menu.nav_search, menu);
            var searchItem = menu.FindItem(Resource.Id.action_search);
            var provider = MenuItemCompat.GetActionView(searchItem);
            mSearchView = provider.JavaCast<Android.Support.V7.Widget.SearchView>();
            mSearchView.QueryTextSubmit += (sender, args) =>
            {
                Toast.MakeText(this.Activity, "You searched: " + args.Query, ToastLength.Short).Show();
            };
            #region bottom toolbar function
            ////ActionMenu
            //var mBottomToolbar = this.Activity.FindViewById<Android.Widget.Toolbar>(Resource.Id.secondToolbar);
            //mBottomToolbar.InflateMenu(Resource.Menu.nav_manageLocation);
            //mBottomToolbar.MenuItemClick += (sender, e) =>
            //{
            //    switch (e.Item.ToString())
            //    {
            //        case "Add":
            //            ReplaceFragment(new LocationAddFragment(), "addlocation");
            //            break;
            //    }
            //    base.OnCreateOptionsMenu(menu, inflater);
            //};
            #endregion
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