using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShopDiaryProjectV1.Services;
using System.Threading;
using ShopDiaryProject.Domain.Models;
using Android.Support.V7.Widget;
using ShopDiaryProjectV1.Adapter;
using ShopDiaryProject.Android.Models.ViewModels;

namespace ShopDiaryProjectV1
{   
    [Activity(Label = "LocationActivity")]
    public class LocationActivity : Activity
    { 
        #region field
        private LocationsRecycleAdapter mLocationsAdapter;

        public List<LocationViewModel> mLocations;

        private RecyclerView mListViewLocations;

        private readonly LocationDataService mLocationDataService;
       

        private Button mAddButton;
        private ImageButton mBackButton;
        private Button mEditButton;
        private LocationViewModel mSelectedLocationClass;
        private Guid mAuthorizedId;
        private TextView mTextSelectedLocation;
        private int mSelectedLocation=-1;
        private ProgressDialog mProgressDialog;
        #endregion

        public LocationActivity()
        {
            mLocationDataService = new LocationDataService();
            
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.PageManagePlace);
            InitFields();
        }

        protected override void OnStart()
        {
            base.OnStart();

        }
        private void InitFields()
        {
            this.mAddButton = FindViewById<Button>(Resource.Id.buttonLocationAdd);
            this.mEditButton=FindViewById<Button>(Resource.Id.buttonLocationEdit);
            this.mBackButton = FindViewById<ImageButton>(Resource.Id.buttonLocationBack);
            this.mTextSelectedLocation = FindViewById<TextView>(Resource.Id.textViewManageLocationSelectedLocation);
            this.mListViewLocations = this.FindViewById<RecyclerView>(Resource.Id.recyclerViewLocations);
            this.mListViewLocations.SetLayoutManager(new LinearLayoutManager(this));
            mAuthorizedId = LoginActivity.StaticUserClass.ID;

            LoadLocationData();
            mBackButton.Click += (object sender, EventArgs args) =>
             {
                 Intent nextActivity = new Intent(this, typeof(PageMainActivity));
                 StartActivity(nextActivity);
             };
            mEditButton.Click += (object sender, EventArgs args) =>
            {
                if (mSelectedLocation > -1)
                {
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    DialogLocationDetail dialogLocation = new DialogLocationDetail(mSelectedLocationClass);
                    dialogLocation.Show(transaction, "dialogue fragment");
                    dialogLocation.OnCompleteLocationDetail += EditLocationDialog_OnLocationEdit;
                }
                else
                {
                    Toast.MakeText(this, "Select Location First..", ToastLength.Long).Show();
                }
                
            };
            mAddButton.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogAddLocation dialogLocation = new DialogAddLocation();
                dialogLocation.Show(transaction, "dialogue fragment");
                dialogLocation.OnAddLocationComplete += AddLocationDialog_OnLocationAdd;
            };
        }
       



        private async void LoadLocationData()
        {
            mProgressDialog = ProgressDialog.Show(this, "Please wait...", "Getting location data...", true);
            //this.mListViewLocations = this.FindViewById<RecyclerView>(Resource.Id.recyclerViewLocations);
            List<LocationViewModel> mLocationsByUser = await mLocationDataService.GetAll();
            mLocations= new List<LocationViewModel>();
            for (int i = 0; mLocationsByUser.Count > i; i++)
            {
                if (mLocationsByUser[i].AddedUserId == LoginActivity.StaticUserClass.ID.ToString())
                {
                    mLocations.Add(mLocationsByUser[i]);
                }
            }

            if (mLocations != null)
            {
               
                this.mLocationsAdapter = new LocationsRecycleAdapter(mLocations, this);
                this.mLocationsAdapter.ItemClick += OnLocationClicked;
              
                this.mListViewLocations.SetAdapter(this.mLocationsAdapter);
            }
            mProgressDialog.Hide();
        }

        private void OnLocationClicked(object sender, int e)
        {
            mSelectedLocation = e;
            mSelectedLocationClass = mLocations[e];
            mTextSelectedLocation.Text = mLocations[e].Name;
            LoginActivity.StaticLocationClass.Id = mLocations[e].Id;
            LoginActivity.StaticLocationClass.Name = mLocations[e].Name;
        }

        private void EditLocationDialog_OnLocationEdit(object sender, OnCompleteLocationDetailEventArgs e)
        {

            Location newLoc = new Location()
            {
                Name = e.Name,
                Address = e.Address,
                Description = e.Description,
                
            };
            var progressDialog = ProgressDialog.Show(this, "Please wait...", "Adding Location...", true);
            new Thread(new ThreadStart(delegate
            {

                var isAdded = mLocationDataService.Edit(mLocations[mSelectedLocation].Id,newLoc);
                RunOnUiThread(() => progressDialog.Hide());

                if (isAdded)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Location Editted", ToastLength.Long).Show());
                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Failed to Edit, please check again form's field", ToastLength.Long).Show());
                }

            })).Start();
        }
        private void AddLocationDialog_OnLocationAdd(object sender, OnAddLocationEventArgs e)
        {

            Location newLoc = new Location()
            {
                Name = e.Name,
                Address = e.Address,
                Description = e.Description,
                CreatedUserId = mAuthorizedId.ToString()
            };
            var progressDialog = ProgressDialog.Show(this, "Please wait...", "Adding Location...", true);
            new Thread(new ThreadStart(delegate
            {

                var isAdded = mLocationDataService.Add(newLoc);
                RunOnUiThread(() => progressDialog.Hide());

                if (isAdded)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Location Added", ToastLength.Long).Show());
                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Failed to add, please check again form's field", ToastLength.Long).Show());
                }

            })).Start();
        }
    }
}