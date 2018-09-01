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

        public static List<InventoryViewModel> mInventories;
        public static List<ProductViewModel> mProducts;
        public static List<StorageViewModel> mStorages;
        public static List<UserLocationViewModel> mUserLoc;

        public static LocationViewModel StaticLocationClass = new LocationViewModel();
        private TextView mUsernameInfo;


        // drawer
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        IMenuItem previousItem;

        #endregion

        public MainActivity()
        {
            
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);
            
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
            #region BOTTOM TOOLBAR
            ////bottomToolbar
            //var bottomToolbar = FindViewById<Android.Widget.Toolbar>(Resource.Id.secondToolbar);
            //bottomToolbar.InflateMenu(Resource.Menu.HomeShortcutMenu);
            //bottomToolbar.MenuItemClick += (sender, e) => {
            //    var trans = SupportFragmentManager.BeginTransaction();
            //    var aweu = e.Item.TitleFormatted.ToString();
            //    switch (e.Item.ToString())
            //    {
            //        case "Storage":
            //            trans.Replace(Resource.Id.content_frame, new StoragesFragment(), "Manage Storages");
            //            trans.Commit();
            //            break;
            //        case "Use":
            //            trans.Replace(Resource.Id.content_frame, new LocationsFragment(), "Manage Locations");
            //            trans.Commit();
            //            break;
            //        case "Add":
            //            trans.Replace(Resource.Id.content_frame, new LocationsFragment(), "Manage Locations");
            //            trans.Commit();
            //            break;
            //        case "RunOut":
            //            trans.Replace(Resource.Id.content_frame, new LocationsFragment(), "Manage Locations");
            //            trans.Commit();
            //            break;
            //        case "ShopList":
            //            trans.Replace(Resource.Id.content_frame, new LocationsFragment(), "Manage Locations");
            //            trans.Commit();
            //            break;
                       
            //    }
            //};

           
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
                    fragment = StoragesFragment.NewInstance();
                    break;
                case 3:
                    fragment = StoragesFragment.NewInstance();
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



        
    }

}

