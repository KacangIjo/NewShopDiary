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
using Android.Support.V4.App;
using ShopDiaryProjectV1.Services;
using ShopDiaryProjectV1.Adapter;
using Android.Support.V7.Widget;
using System.Threading;
using ShopDiaryProject.Android.Models.ViewModels;

namespace ShopDiaryProjectV1
{
    [Activity(Label = "ActivityPageMain")]
    public class PageMainActivity : Activity
    {
#region field
        private static readonly int ButtonClickNotification = 9999;

        private  MainAdapter mInventoryAdapter;

        public List<InventoryViewModel> mInventories;
        public List<ProductViewModel> mProducts;
        public List<StorageViewModel> mStorages;
        public List<RoleLocationViewModel> mRoles;
        public List<UserLocationViewModel> mUserLoc;


        private RecyclerView mListViewInventory;

        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly StorageDataService mStorageDataService;

        private DateTime dateNow;
        private TextView mExpTotal;
        private TextView mRunOutTotal;
        private TextView mStockTotal;
        private TextView mExpCounter;
        private TextView mRunOutCounter;
        private TextView mStockCounter;
        private TextView mCurrentLocation;
        private ImageButton mBtnThreeStrips;
        private ImageButton mStoragesMenu;
        private ImageButton mUse;
        private ImageButton mAdd;
        private ImageButton mShopList;
        private ImageButton mRunOut;
        private Button btnSend;
        private int mSelectedInventory;

        private ProgressDialog mProgressDialog;
        #endregion
        public PageMainActivity()
        {
            
            mInventoryDataService = new InventoryDataService();
            mProductDataService = new ProductDataService();
            mStorageDataService = new StorageDataService();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.PageMainStart);
            
            InitFields();
            
            if (LoginActivity.StaticLocationClass.Name != null)
            {
                int i;
                mStorages.Count();
                mCurrentLocation.Text = LoginActivity.StaticLocationClass.Name;
                for (i = 0;mStorages.Count()>i; i++)
                {
                    int mCounter = 0;
                    if (mStorages[i].Id == LoginActivity.StaticLocationClass.Id)
                    {
                        for (int j = 0;mInventories.Count>j; j++)
                        {
                            if (mInventories[j].Id == mStorages[i].Id)
                            {
                                mCounter++;
                                mStockCounter.Text = mCounter.ToString();
                            }
                       
                        }

                    }
            
                }
                mExpCounter.Text = "0";

                mRunOutCounter.Text = "0";
            }
            #region shortcut menu
            mRunOut.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(UseActivity));
                StartActivity(nextActivity);
            };
            mShopList.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(DrawerMenuActivity));
                StartActivity(nextActivity);
            };
            mAdd.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(ItemAddFormActivity));
                StartActivity(nextActivity);
            };
            mUse.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(UseActivity));
                StartActivity(nextActivity);
            };
            mStoragesMenu.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(StoragesActivity));
                StartActivity(nextActivity);
            };

            btnSend.Click += (s, e) =>
            {
                Bundle valuesSend = new Bundle();
                valuesSend.PutString("sendContent", "This is content awdawda");
                Intent newIntent = new Intent(this, typeof(UseActivity));
                newIntent.PutExtras(valuesSend);

                Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
                stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(UseActivity)));
                stackBuilder.AddNextIntent(newIntent);
                PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
                NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                .SetAutoCancel(true)
                .SetContentIntent(resultPendingIntent)
                .SetContentTitle("You have new Notification")
                .SetSmallIcon(Resource.Drawable.logoshopdiary)
                .SetContentText("You have expired items");

                NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                notificationManager.Notify(ButtonClickNotification, builder.Build());

            };

            mBtnThreeStrips.Click += (object sender, EventArgs args) =>
            {
                Intent nextActivity = new Intent(this, typeof(DrawerMenuActivity));
                StartActivity(nextActivity);
            };
            #endregion
        }
        protected override void OnStart()
        {
            base.OnStart();
       
        }

        private void InitFields()
        {
            this.mProducts = new List<ProductViewModel>();
            this.mInventories = new List<InventoryViewModel>();
            this.mStorages = new List<StorageViewModel>();
            btnSend = FindViewById<Button>(Resource.Id.buttonSendNotif);
            mRunOutTotal = FindViewById<TextView>(Resource.Id.textMainRunOut);
            mStockTotal = FindViewById<TextView>(Resource.Id.textMainStockCount);
            mExpTotal = FindViewById<TextView>(Resource.Id.textMainExpCount);
            mRunOutCounter = FindViewById<TextView>(Resource.Id.textMainRunOutCount);
            mStockCounter = FindViewById<TextView>(Resource.Id.textMainStockCount);
            mExpCounter = FindViewById<TextView>(Resource.Id.textMainExpCount);
            mCurrentLocation = FindViewById<TextView>(Resource.Id.textViewCurrentLocationMainPage);
            mBtnThreeStrips = FindViewById<ImageButton>(Resource.Id.btnMainMenu);
            mStoragesMenu = FindViewById<ImageButton>(Resource.Id.btnMainStorage);
            mUse = FindViewById<ImageButton>(Resource.Id.btnMainUse);
            mAdd = FindViewById<ImageButton>(Resource.Id.btnMainAdd);
            mShopList = FindViewById<ImageButton>(Resource.Id.btnMainKart);
            mRunOut = FindViewById<ImageButton>(Resource.Id.btnMainRunOut);
            mStockCounter.Text = "25";
            mExpCounter.Text = "3";
            mRunOutCounter.Text = "-";
            mListViewInventory = this.FindViewById<RecyclerView>(Resource.Id.recyclerViewInventoryMainPage);
            this.mListViewInventory.SetLayoutManager(new LinearLayoutManager(this));
            LoadInventoryData();



        }

        private async void LoadInventoryData()
        {
            mProgressDialog = ProgressDialog.Show(this, "Please wait...", "Getting data...", true);
            mStorages = await mStorageDataService.GetAll();
            this.mInventories = await mInventoryDataService.GetAll();
            this.mProducts = await mProductDataService.GetAll();
            if (mInventories != null)
            {
                this.mInventoryAdapter = new MainAdapter(this.mInventories,this.mProducts, this);
                this.mInventoryAdapter.ItemClick += OnInventoryClick;
                this.mListViewInventory.SetAdapter(this.mInventoryAdapter);
            }
            mProgressDialog.Hide();
        }

        private void OnInventoryClick(object sender, int e)
        {
            mSelectedInventory = e;
        }
    }
}