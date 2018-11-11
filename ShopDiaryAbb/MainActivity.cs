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
using ShopDiaryAbb.Fragments;
using ShopDiaryAbb.FragmentsScanner;
using ShopDiaryAbb.Models.ViewModels;

namespace ShopDiaryAbb
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        #region Properties
        
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            
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

    }
}

