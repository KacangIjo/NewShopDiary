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
            var bottomToolbar = FindViewById<Android.Widget.Toolbar>(Resource.Id.secondToolbar);
            bottomToolbar.Title = "Editing";
            bottomToolbar.InflateMenu(Resource.Menu.HomeShortcutMenu);
            bottomToolbar.MenuItemClick += (sender, e) => {
                Toast.MakeText(this, "Bottom toolbar tapped: " + e.Item.TitleFormatted, ToastLength.Short).Show();
            };
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
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(Resource.Menu.nav_search, menu);

        //    return base.OnCreateOptionsMenu(menu);
        //}

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
              
            }
            return base.OnOptionsItemSelected(item);
        }
        #endregion

        private void InitFields()
        {
            
        }

        
    }

}

