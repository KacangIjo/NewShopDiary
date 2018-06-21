//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Vision.Barcodes;
using Android.Gms.Vision;
using Android.Graphics;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;
using static Android.Gms.Vision.Detector;
using Android.Util;
using System;

namespace ShopDiaryProjectV1
{
    [Activity(Label = "AddActivity")]
    public class ItemAddActivity : Activity
    {
        private ImageButton mBtnBack;

        private Button mBtnWithoutScan;
        private TextView mTextScannedBarcode;
        SurfaceView mCamPreview;
        BarcodeDetector mbarcodeDetector;
        CameraSource mcameraSource;
        public static string scannedBarcode = "-";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //base.OnCreate(savedInstanceState);
            //Window.RequestFeature(WindowFeatures.NoTitle);
            //SetContentView(Resource.Layout.PageAdd);

            //mBtnWithoutScan = FindViewById<Button>(Resource.Id.buttonAddWithoutScan1);
            //mBtnBack = FindViewById<ImageButton>(Resource.Id.btnAddBack1);
            //mCamPreview = FindViewById<SurfaceView>(Resource.Id.cameraPreview);
            //mTextScannedBarcode = FindViewById<TextView>(Resource.Id.textScannedBarcode);


            //Button flashButton;
            //View zxingOverlay;

            //flashButton = this.FindViewById<Button>(Resource.Id.buttonScanCustomView);
            //flashButton.Click += async delegate
            //{

            //    //Tell our scanner we want to use a custom overlay instead of the default
            //    scanner.UseCustomOverlay = true;

            //    //Inflate our custom overlay from a resource layout
            //    zxingOverlay = LayoutInflater.FromContext(this).Inflate(Resource.Layout.ZxingOverlay, null);

            //    //Find the button from our resource layout and wire up the click event
            //    flashButton = zxingOverlay.FindViewById<Button>(Resource.Id.buttonZxingFlash);
            //    flashButton.Click += (sender, e) => scanner.ToggleTorch();

            //    //Set our custom overlay
            //    scanner.CustomOverlay = zxingOverlay;

            //    //Start scanning!
            //    var result = await scanner.Scan();

            //    HandleScanResult(result);
            //};

            //mBtnWithoutScan.Click += (object sender, EventArgs args) =>
            //{
            //    Intent nextActivity = new Intent(this, typeof(ItemAddFormActivity));
            //    StartActivity(nextActivity);
            //};
            //mBtnBack.Click += (object sender, EventArgs args) =>
            //{
            //    Intent nextActivity = new Intent(this, typeof(PageMainActivity));
            //    StartActivity(nextActivity);
            //};

            //void HandleScanResult(ZXing.Result result)
            //{
            //    string msg = "";

            //    if (result != null && !string.IsNullOrEmpty(result.Text))
            //        msg = "Found Barcode: " + result.Text;
            //    else
            //        msg = "Scanning Canceled!";

            //    this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
            //}



        }
    }
}