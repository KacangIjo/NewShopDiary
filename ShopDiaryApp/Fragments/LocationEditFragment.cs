using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.Services;
using ShopDiaryProject.Domain.Models;
using System;
using System.Threading;

namespace ShopDiaryApp.Fragments
{
    public class LocationEditFragment : Fragment
    {
        private TextView mLocationTitle;
        private EditText mLocationName;
        private EditText mLocationAddress;
        private EditText mLocationDescription;
        private ImageButton mButtonAddUser;
        private ImageButton mButtonDelete;
        private ImageButton mButtonSave;
        private ProgressBar mProgressBar;

        LocationDataService mLocationDataService;
        private FragmentTransaction mFragmentTransaction;

        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;

        public LocationEditFragment()
        {
            mLocationDataService = new LocationDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public static LocationAddFragment NewInstance()
        {
            var frag2 = new LocationAddFragment { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.LocationDetail,container,false);
            mLocationTitle = view.FindViewById<TextView>(Resource.Id.textviewLocationDetailTitle);
            mButtonAddUser = view.FindViewById<ImageButton>(Resource.Id.imageButtonLocationDetailAddUser);
            mButtonSave = view.FindViewById<ImageButton>(Resource.Id.imageButtonLocationDetailSave);
            mButtonDelete = view.FindViewById<ImageButton>(Resource.Id.imageButtonLocationDetailDelete);
            mLocationName = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailName);
            mLocationAddress = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailAddress);
            mLocationDescription = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailDescription);
            mLocationTitle.Text = MainActivity.StaticLocationClass.Name.ToString();
            mLocationName.Text = MainActivity.StaticLocationClass.Name.ToString();
            mLocationAddress.Text = MainActivity.StaticLocationClass.Address.ToString();
            mLocationDescription.Text = MainActivity.StaticLocationClass.Description.ToString();
            mProgressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBarAddLocation);

            mButtonAddUser.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new LocationAddUserFragment(), "Add User");
            };
            mButtonSave.Click += (object sender, EventArgs args) =>
            {
                Location newLoc = new Location()
                {
                    Name = mLocationName.Text,
                    Address = mLocationAddress.Text,
                    Description = mLocationDescription.Text,

                };
                new Thread(new ThreadStart(delegate
                {
                    var isEdited = mLocationDataService.Edit(LocationsFragment.mSelectedLocationClass.Id, newLoc);
         
                    if (isEdited)
                    {
                        
                        this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Location Editted", ToastLength.Long).Show());
                    }
                    else
                    {
                        this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Failed to Edit, please check again form's field", ToastLength.Long).Show());
                    }

                })).Start();
                ReplaceFragment(new LocationsFragment(), "Manage Locations");
            };
            mButtonDelete.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new LocationsFragment(), "Manage Locations");
            };
            return view;
        }

        

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            //SearchMenu
            inflater.Inflate(Resource.Menu.nav_standart, menu);
            
        }

        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
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


    }
}