using System;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using ShopDiaryAbb.Fragments;
using ShopDiaryAbb.FragmentsScanner;
using ShopDiaryAbb.Models.ViewModels;

namespace ShopDiaryAbb
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        #region Properties
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            if (!AppCenter.Configured)
            {
                Push.PushNotificationReceived += (sender, e) =>
                {
                    // Add the notification message and title to the message
                    var summary = $"Push notification received:" +
                                        $"\n\tNotification title: {e.Title}" +
                                        $"\n\tMessage: {e.Message}";

                    // If there is custom data associated with the notification,
                    // print the entries
                    if (e.CustomData != null)
                    {
                        summary += "\n\tCustom data:\n";
                        foreach (var key in e.CustomData.Keys)
                        {
                            summary += $"\t\t{key} : {e.CustomData[key]}\n";
                        }
                    }

                    // Send the notification summary to debug output
                    System.Diagnostics.Debug.WriteLine(summary);
                };
            }
            CustomProperties properties = new CustomProperties();
            properties.Set("UserId", LoginPageActivity.StaticUserClass.ID.ToString()); 
            AppCenter.SetCustomProperties(properties);
            AppCenter.SetEnabledAsync(true);
            AppCenter.GetInstallIdAsync();
            AppCenter.Start("4ced249d-df1d-4780-a8d1-23dc63ddf900", typeof(Push));
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            CreateNotificationChannel();
            Notify();

            if (savedInstanceState == null)
            {
                navigationView.SetCheckedItem(Resource.Id.nav_home_1);
                ListItemClicked(0);
            }
            

        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
           
            int id = item.ItemId;

            if (id == Resource.Id.menuHome)
            {
                SupportActionBar.Title = "Home";
                ListItemClicked(0);
            }
            else if (id == Resource.Id.menuLocations)
            {
                SupportActionBar.Title = "Manage Locations";
                ListItemClicked(1);
            }
            else if (id == Resource.Id.menuStorages)
            {
                SupportActionBar.Title = "Manage Storages";
                ListItemClicked(2);
            }
            else if (id == Resource.Id.menuAddItem)
            {
                SupportActionBar.Title = "Add Item To Inventory";
                ListItemClicked(3);
            }
            else if (id == Resource.Id.menuShoplist)
            {
                SupportActionBar.Title = "Manage Shop List";
                ListItemClicked(4);
            }
            else if (id == Resource.Id.menuCategories)
            {
                SupportActionBar.Title = "Manage Categories";
                ListItemClicked(5);
            }
            else if (id == Resource.Id.menuProducts)
            {
                SupportActionBar.Title = "Manage Products";
                ListItemClicked(6);
            }
            else if (id == Resource.Id.menuSummary)
            {
                SupportActionBar.Title = "Summary";
                ListItemClicked(7);
            }
            else if (id == Resource.Id.menuLogout)
            {
                Android.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogFragmentLogout dialogLocation = new DialogFragmentLogout();
                dialogLocation.Show(transaction, "dialogue fragment");
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }


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
                    fragment = StoragesFragment.NewInstance();
                    break;
                case 3:
                    fragment = AddItemBarcodeFragment.NewInstance();
                    break;
                case 4:
                    fragment = ShopListFragment.NewInstance();
                    break;
                case 5:
                    fragment = CategoriesFragment.NewInstance();
                    break;
                case 6:
                    fragment = ProductsFragment.NewInstance();
                    break;
                case 7:
                    fragment = SummaryFragment.NewInstance();
                    break;

            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.main_frame, fragment)
                .Commit();
        }

        public void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            string channelName = Resources.GetString(Resource.String.channel_name);
            string channelDescription = GetString(Resource.String.channel_description);
            NotificationChannel channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            NotificationManager notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public new void Notify()
        {
            var builder = new Android.Support.V4.App.NotificationCompat.Builder(this, CHANNEL_ID)
                 .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                 .SetContentTitle("Expired Items") // Set the title
                 .SetSmallIcon(Resource.Drawable.Icon) // This is the icon to display
                 .SetContentText("You have expired Items in your inventory"); // the message to display.

            // Finally, publish the notification:
            var notificationManager = Android.Support.V4.App.NotificationManagerCompat.From(this);
            notificationManager.Notify(NOTIFICATION_ID, builder.Build());
        }

    }
}

