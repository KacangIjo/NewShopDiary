using Android.OS;
using Android.Runtime;
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

namespace ShopDiaryAbb.Fragments
{
    public class LocationsFragment : Fragment
    {
        private LocationsRecycleAdapter mLocationsAdapter;
        
        public static LocationViewModel mSelectedLocationClass;
        private readonly LocationDataService mLocationDataService;
        public List<LocationViewModel> mFilteredLocation;
        public List<LocationViewModel> mLocations;
        private RecyclerView mListViewLocations;
        private int mSelectedLocation = -1;
        private FragmentTransaction mFragmentTransaction;
        private ImageButton mButtonAdd;
        private ImageButton mButtonEdit;

        public LocationsFragment()
        {
            mLocationDataService = new LocationDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
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

        private void LoadLocationData()
        {

            List<LocationViewModel> mLocationsByUser = LoginPageActivity.mGlobalLocations;
            List<UserLocationViewModel> mSharedLocation = LoginPageActivity.mGlobalUserLocs;
            mLocations = new List<LocationViewModel>();
            //mLocations = mLocationsByUser.Where(s => s.AddedUserId == LoginPageActivity.StaticUserClass.ID.ToString()).ToList();
            for (int i = 0; mLocationsByUser.Count > i; i++)
            {
                if (mLocationsByUser[i].AddedUserId == LoginPageActivity.StaticUserClass.ID.ToString()) 
                {
                    mLocations.Add(mLocationsByUser[i]);
                }
                else
                {

                    for (int j = 0; j < mSharedLocation.Count; j++)
                    {
                        if (mSharedLocation[j].RegisteredUser == LoginPageActivity.StaticUserClass.ID)
                        {
                            mLocationsByUser[i].IsSharedLocation = true;
                            mLocations.Add(mLocationsByUser[i]);

                        }
                    }

                }
            }
            
            mFilteredLocation = mLocations.GroupBy(s => s.Id).Select(group => group.First()).ToList();
            var test = mLocations.Count;
            if (mLocations != null)
            {
                mLocationsAdapter = new LocationsRecycleAdapter(mFilteredLocation, this.Activity);
                mLocationsAdapter.ItemClick += OnLocationClicked;
                this.Activity.RunOnUiThread(() => mLocationsAdapter.NotifyDataSetChanged());
                this.Activity.RunOnUiThread(() => this.mListViewLocations.SetAdapter(this.mLocationsAdapter));
            }
        }

        private void OnLocationClicked(object sender, int e)
        {
            mSelectedLocation = e;
            mSelectedLocationClass = mLocations[e];
            LoginPageActivity.StaticActiveLocationClass = mLocations[e];
            LoginPageActivity.StaticLocationClass = mLocations[e];

            //ShowOptionsDialog
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            DialogLocationOptions LocationOptionsDialog = new DialogLocationOptions();
            LocationOptionsDialog.Show(transaction, "dialogue fragment");
            LocationOptionsDialog.OnLocationOptionsPicked += LocationOptions_OnComplete;
        }

        private void LocationOptions_OnComplete(object sender, OnLocationOptionsPicked e)
        {
            var IsLoadData = e.IsNeedLoadData;
            if (IsLoadData == true)
            {

            }
           
            Toast.MakeText(this.Activity, "Expired Date Added", ToastLength.Short).Show();
        }

        public override void OnCreateOptionsMenu(IMenu menu,MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.nav_search, menu);
            var searchItem = menu.FindItem(Resource.Id.action_search);
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