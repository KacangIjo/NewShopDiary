using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;

using ShopDiaryApp.Fragments;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Android.Runtime;
using Android.Widget;
using ShopDiaryApp.Services;
using ShopDiaryApp.Models.ViewModels;
using System.Collections.Generic;
using ShopDiaryApp.Adapter;
using Android.Support.V7.Widget;
using System;
using ShopDiaryApp.FragmentsScanner;
using Android.Content;

namespace ShopDiaryApp
{
    [Activity(Label = "@string/app_name", MainLauncher = false, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/Icon")]
    public class MainActivity : AppCompatActivity
    {
        #region properties
        //main
        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly StorageDataService mStorageDataService;
        private readonly LocationDataService mLocationDataService;
        private readonly CategoryDataService mCategoryDataService;
        private readonly ConsumeDataService mConsumeDataService;

        

        public static LocationViewModel StaticActiveLocationClass = new LocationViewModel();
        public static UserLocationViewModel StaticUserLocationClass = new UserLocationViewModel();
        public static LocationViewModel StaticLocationClass = new LocationViewModel();
        public static StorageViewModel StaticStorageClass = new StorageViewModel();
        public static InventoryViewModel StaticInventoryClass = new InventoryViewModel();

        private Android.Support.V4.App.FragmentTransaction mFragmentTransaction;
        private TextView mUsernameInfo;

        private bool _canClose = true;

        // drawer
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        IMenuItem previousItem;

        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";

        #endregion

        public MainActivity()
        {
            
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);
            SendNotification();
            #region toolbar and navigation bar
            #region TOP TOOLBAR
            //TopToolbar
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.Title = "Home";
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            //setup navigation view
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //mUsernameInfo = FindViewById<TextView>(Resource.Id.textDrawerUsername);
            //mUsernameInfo.Text = LoginPageActivity.StaticUserClass.Username;
            //handle navigation
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                if (previousItem != null)
                    previousItem.SetChecked(false);

                navigationView.SetCheckedItem(e.MenuItem.ItemId);

                previousItem = e.MenuItem;

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_home_1:
                        SupportActionBar.Title="Home";
                        ListItemClicked(0);
                        break;
                    case Resource.Id.nav_home_2:
                        SupportActionBar.Title = "Manage Locations";
                        ListItemClicked(1);
                        break;
                    case Resource.Id.nav_home_additems:
                        SupportActionBar.Title = "Add Items";
                        ListItemClicked(2);
                        break;
                    case Resource.Id.nav_home_storages:
                        SupportActionBar.Title = "Manage Storages";
                        ListItemClicked(3);
                        break;
                    case Resource.Id.nav_home_categories:
                        SupportActionBar.Title = "Manage Categories";
                        ListItemClicked(4);
                        break;
                    case Resource.Id.nav_home_products:
                        SupportActionBar.Title = "Manage Products";
                        ListItemClicked(5);
                        break;
                    case Resource.Id.nav_home_shoplist:
                        SupportActionBar.Title = "Manage ShopList";
                        ListItemClicked(6);
                        break;
                    case Resource.Id.nav_home_summary:
                        SupportActionBar.Title = " Summary";
                        ListItemClicked(7);
                        break;
                }


                drawerLayout.CloseDrawers();
            };
            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                navigationView.SetCheckedItem(Resource.Id.nav_home_1);
                ListItemClicked(0);
            }
            #endregion
            #endregion

        }

        #region navigation bar function and menu toolbar
        int oldPosition = -1;

      

        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            if (position == oldPosition)
                return;

            oldPosition = position;

            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment = HomeFragment.NewInstance();
                    break;
                case 1:
                    fragment = LocationsFragment.NewInstance();
                    break;
                case 2:
                    fragment = AddItemBarcodeFragment.NewInstance();
                    break;
                case 3:
                    fragment = StoragesFragment.NewInstance();
                    break;
                case 4:
                    fragment = CategoriesFragment.NewInstance();
                    break;
                case 5:
                    fragment = ProductsFragment.NewInstance();
                    break;
                case 6:
                    fragment = ShopListFragment.NewInstance();
                    break;
                case 7:
                    fragment = SummaryFragment.NewInstance();
                    break;

            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
                //case Resource.Id.homeShortcutStorage:
                //    trans.Add(Resource.Id.content_frame, new StoragesFragment(), "Manage Locations");
                //    trans.Commit();
                //    return true;
                //case Resource.Id.homeShortcutRunout:
                //    trans.Add(Resource.Id.content_frame, new LocationsFragment(), "Manage Locations");
                //    trans.Commit();
                //    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        #endregion


        public override void OnBackPressed()
        {
            if (SupportActionBar.Title == "Home")
            {
                Android.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogFragmentLogout dialogLocation = new DialogFragmentLogout();
                dialogLocation.Show(transaction, "dialogue fragment");
            }
            else
            {
                SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, HomeFragment.NewInstance())
               .Commit();
            }
        }

        public void SendNotification()
        {
           

            // When the user clicks the notification, SecondActivity will start up.
            var resultIntent = new Intent(this, typeof(MainActivity));
            

            // Construct a back stack for cross-task navigation:
           

            // Build the notification:
            var builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          
                          .SetContentTitle("ShopDiaryApp") // Set the title
                          .SetSmallIcon(Resource.Drawable.Icon) // This is the icon to display
                          .SetContentText("You Have Expired Items"); // the message to display.

            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(NOTIFICATION_ID, builder.Build());


        }


    }

}

