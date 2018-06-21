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

using ShopDiaryProjectV1.Adapter;
using Android.Support.V7.Widget;
using ShopDiaryProjectV1.Services;
using System.Threading;
using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.Android.Models.ViewModels;

namespace ShopDiaryProjectV1
{
    [Activity(Label = "StorageActivity")]
    public class StoragesActivity : Activity
    {
        private StoragesRecycleAdapter mStoragesAdapter;

        public List<StorageViewModel> mStorages;

        private RecyclerView mListViewStorage;

        private readonly StorageDataService mStorageDataService;


        private Button mAddButton;
        private Button mEditButton;
        private Button mDeleteButton;
        private StorageViewModel mSelectedStorageClass;
        private TextView mTextSelectedStorage;
        private int mSelectedStorage = -1;
        private ProgressDialog mProgressDialog;


        public StoragesActivity()
        {
            mStorageDataService = new StorageDataService();

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.PageStorages);
            InitFields();
        }

        protected override void OnStart()
        {
            base.OnStart();
        
        }
        private void InitFields()
        {
            this.mAddButton = FindViewById<Button>(Resource.Id.buttonStorageAdd);
            this.mEditButton = FindViewById<Button>(Resource.Id.buttonStorageEdit);
            this.mDeleteButton = FindViewById<Button>(Resource.Id.buttonStorageDelete);
            this.mTextSelectedStorage = FindViewById<TextView>(Resource.Id.textViewSelectedStorage);
            this.mListViewStorage = FindViewById<RecyclerView>(Resource.Id.recyclerViewStorages);
            this.mListViewStorage.SetLayoutManager(new LinearLayoutManager(this));

            LoadStoragesData();
            mDeleteButton.Click += (object sender, EventArgs args) =>
            {
                var progressDialog = ProgressDialog.Show(this, "Please wait...", "Deleting...", true);
                new Thread(new ThreadStart(delegate
                {
                    if (mSelectedStorage > -1)
                    {
                        var isDeleted = mStorageDataService.Delete(mSelectedStorageClass.Id);
                        RunOnUiThread(() => progressDialog.Hide());

                        if (isDeleted)
                        {
                            RunOnUiThread(() => Toast.MakeText(this, "Deleted!", ToastLength.Short).Show());
                        }
                        else
                        {
                            RunOnUiThread(() => Toast.MakeText(this, "Failed to delete, please check your connection", ToastLength.Long).Show());
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Select Storage First..", ToastLength.Long).Show();
                    }
                   

                })).Start();
            };

            mEditButton.Click += (object sender, EventArgs args) =>
            {
                if (mSelectedStorage > -1)
                {
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    DialogStorageDetail dialogStorage = new DialogStorageDetail(mSelectedStorageClass);
                    dialogStorage.Show(transaction, "dialogue fragment");
                    dialogStorage.OnCompleteStorageDetail += EditStorageDialog_OnStorageEdit;
                }
                else
                {
                    Toast.MakeText(this, "Select Storage First..", ToastLength.Long).Show();
                }

            };
            mAddButton.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogAddStorage dialogStorage = new DialogAddStorage();
                dialogStorage.Show(transaction, "dialogue fragment");
                dialogStorage.OnAddStorageComplete += AddStorageDialog_OnStorageAdd;
            };
        }

        private void MDeleteButton_Click(object sender, EventArgs e)
        {
            
        }

        private async void LoadStoragesData()
        {
            mProgressDialog = ProgressDialog.Show(this, "Please wait...", "Getting Storage data...", true);
            List<StorageViewModel>mStoragesByLocation=new List<StorageViewModel>();
            mStoragesByLocation = await mStorageDataService.GetAll();
            this.mStorages = new List<StorageViewModel>();
           
            for (int i = 0; mStoragesByLocation.Count > i; i++)
            {
                if (mStoragesByLocation[i].LocationId == LoginActivity.StaticLocationClass.Id)
                {
                    mStorages.Add(mStoragesByLocation[i]);
                }
            }
            if (mStorages != null)
            {
                this.mStoragesAdapter = new StoragesRecycleAdapter(this.mStorages, this);
                this.mStoragesAdapter.ItemClick += OnStorageClicked;

                this.mListViewStorage.SetAdapter(this.mStoragesAdapter);
            }
            mProgressDialog.Hide();
        }

        private void OnStorageClicked(object sender, int e)
        {
            mSelectedStorage = e;
            mSelectedStorageClass = mStorages[e];
            mTextSelectedStorage.Text = mStorages[e].Name;
            
        }

        private void EditStorageDialog_OnStorageEdit(object sender, OnCompleteStorageDetailEventArgs e)
        {

            Storage EditedStorage = new Storage()
            {
                Name = e.Name,
                Area=e.Area,
                Description = e.Description,
            };
            var progressDialog = ProgressDialog.Show(this, "Please wait...", "Editing Storage...", true);
            new Thread(new ThreadStart(delegate
            {

                var isAdded = mStorageDataService.Edit(mStorages[mSelectedStorage].Id, EditedStorage);
                RunOnUiThread(() => progressDialog.Hide());

                if (isAdded)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Storage Editted", ToastLength.Long).Show());
                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Failed to Edit, please check again form's field", ToastLength.Long).Show());
                }

            })).Start();
        }
        private void AddStorageDialog_OnStorageAdd(object sender, OnAddStorageEventArgs e)
        {

            Storage newLoc = new Storage()
            {
                Name = e.Name,
                Area = e.Area,
                Description = e.Description,
             
                LocationId = LoginActivity.StaticLocationClass.Id
            };
            var progressDialog = ProgressDialog.Show(this, "Please wait...", "Adding Storage...", true);
            new Thread(new ThreadStart(delegate
            {

                var isAdded = mStorageDataService.Add(newLoc);
                var temp=newLoc.Id.ToString();
                RunOnUiThread(() => progressDialog.Hide());

                if (isAdded)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Storage Added", ToastLength.Long).Show());
                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Failed to add, please check again form's field", ToastLength.Long).Show());
                }

            })).Start();
        }


    }
   
}