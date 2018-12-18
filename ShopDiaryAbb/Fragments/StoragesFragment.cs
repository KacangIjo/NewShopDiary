using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.DialogFragments;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopDiaryAbb.Fragments
{
    public class StoragesFragment : Fragment
    {
        private StoragesRecycleAdapter mStoragesAdapter;
        public List<StorageViewModel> mStorages;
        public static StorageViewModel mSelectedStorageClass;
        private readonly StorageDataService mStorageDataService;

        private RecyclerView mListViewStorage;
        private int mSelectedStorage = -1;
        private FragmentTransaction mFragmentTransaction;
        private ImageButton mButtonAdd;
        private ImageButton mButtonEdit;

        int QuantityTemp=1;
        public StoragesFragment()
        {
            mStorageDataService = new StorageDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public static StoragesFragment NewInstance()
        {
            var frag2 = new StoragesFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.ManageStoragesLayout, container, false);

            mListViewStorage = view.FindViewById<RecyclerView>(Resource.Id.recylerStorages);
            mListViewStorage.SetLayoutManager(new LinearLayoutManager(Activity));
            mButtonAdd = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageStorageAdd);
            mButtonEdit = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageStoragesEdit);
            mButtonAdd.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new StorageAddFragment(), "Add Storage");
            };
            mButtonEdit.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new StorageEditFragment(), "Edit Storage");
            };
            LoadStorageData();
            return view;
        }
        private void LoadStorageData()
        {
            mStorages = LoginPageActivity.mGlobalStorages.Where(s => s.LocationId == LoginPageActivity.StaticActiveLocationClass.Id).ToList();
            if (mStorages != null)
            {
                mStoragesAdapter = new StoragesRecycleAdapter(mStorages, this.Activity);
                mStoragesAdapter.ItemClick += OnStorageClicked;
                this.Activity.RunOnUiThread(() => mStoragesAdapter.NotifyDataSetChanged());
                this.Activity.RunOnUiThread(() => this.mListViewStorage.SetAdapter(this.mStoragesAdapter));
            }
        }

        
        private void OnStorageClicked(object sender, int e)
        {
            mSelectedStorage = e;
            mSelectedStorageClass = mStorages[e];
            //mTextSelectedLocation.Text = mLocations[e].Name;
            LoginPageActivity.StaticStorageClass = mStorages[e];
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            DialogStorageOptions StorageDialog = new DialogStorageOptions();
            StorageDialog.Show(transaction, "dialogue fragment");
            StorageDialog.OnStorageOptionsPicked += StorageOptions_OnComplete;

        }

        private void StorageOptions_OnComplete(object sender, OnStorageDialogPicked e)
        {
          
        }

        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.main_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
    }
}