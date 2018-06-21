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

namespace ShopDiaryProjectV1
{
    [Activity(Label = "DrawerMenuActivity")]
    public class DrawerMenuActivity : Activity
    {
        private ImageButton mButtonPlace;
        private ImageButton mButtonAdd;
        private ImageButton mButtonUse;
        private ImageButton mButtonStorage;
        private ImageButton mButtonShopList;
        private ImageButton mButtonSummary;
        private ImageButton mButtonRunOut;
        private ImageButton mButtonCategory;
        private ImageButton mButtonBack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.PageDrawerMenu);

            mButtonBack = FindViewById<ImageButton>(Resource.Id.btnDrawerBack);
            mButtonPlace = FindViewById<ImageButton>(Resource.Id.btnDrawerPlace);
            mButtonAdd = FindViewById<ImageButton>(Resource.Id.btnDrawerAdd);
            mButtonUse = FindViewById<ImageButton>(Resource.Id.btnDrawerUse);
            mButtonStorage = FindViewById<ImageButton>(Resource.Id.btnDrawerStorage);
            mButtonShopList = FindViewById<ImageButton>(Resource.Id.btnDrawerShopList);
            mButtonSummary = FindViewById<ImageButton>(Resource.Id.btnDrawerSummary);
            mButtonRunOut = FindViewById<ImageButton>(Resource.Id.btnDrawerRunOut);
            mButtonCategory = FindViewById<ImageButton>(Resource.Id.btnDrawerCategory);

            mButtonBack.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(PageMainActivity));
                StartActivity(nextActivity);
            };
            mButtonCategory.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(CategoriesActivity));
                StartActivity(nextActivity);
            };

            mButtonPlace.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(LocationActivity));
                StartActivity(nextActivity);
            };
            mButtonAdd.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(ItemAddFormActivity));
                StartActivity(nextActivity);
            };
            mButtonUse.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(UseActivity));
                StartActivity(nextActivity);
            };
            mButtonStorage.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(StoragesActivity));
                StartActivity(nextActivity);
            };
            mButtonShopList.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(PageMainActivity));
                StartActivity(nextActivity);
            };
            mButtonSummary.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(SummaryActivity));
                StartActivity(nextActivity);
            };
            mButtonRunOut.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(UseActivity));
                StartActivity(nextActivity);
            };
        }
    }
}