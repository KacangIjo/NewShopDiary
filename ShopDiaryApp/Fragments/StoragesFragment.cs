using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryApp.Adapter;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;
using System;
using System.Collections.Generic;

namespace ShopDiaryApp.Fragments
{
    public class StoragesFragment : Fragment
    {
        private StoragesRecycleAdapter mStoragesAdapter;
        public List<StorageViewModel> mStorages;
        static StorageViewModel mSelectedStorageClass;
        private readonly StorageDataService mStorageDataService;

        private RecyclerView mListViewStorage;
        private int mSelectedStorage = -1;
        private FragmentTransaction mFragmentTransaction;
        private ImageButton mButtonAdd;
        private ImageButton mButtonEdit;
        private Android.Support.V7.Widget.SearchView mSearchView;
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
            mButtonAdd = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageLocationAdd);
            mButtonEdit = view.FindViewById<ImageButton>(Resource.Id.imageButtonManageLocationEdit);
            mButtonAdd.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new LocationAddFragment(), "Add Location");
            };
            mButtonEdit.Click += (object sender, EventArgs args) =>
            {
                ReplaceFragment(new LocationEditFragment(), "Edit Location");
            };
            LoadStorageData();
            return view;
        }
        private async void LoadStorageData()
        {

            List<StorageViewModel> mStoragesByUser = await mStorageDataService.GetAll();
            mStorages = new List<StorageViewModel>();
            for (int i = 0; mStoragesByUser.Count > i; i++)
            {
                if (mStoragesByUser[i].AddedUserId == LoginPageActivity.StaticUserClass.ID.ToString())
                {
                    mStorages.Add(mStoragesByUser[i]);
                }
            }
            if (mStorages != null)
            {

                mStoragesAdapter = new StoragesRecycleAdapter(mStorages, this.Activity);
                mStoragesAdapter.ItemClick += OnLocationClicked;
                mListViewStorage.SetAdapter(this.mStoragesAdapter);
            }

        }
        private void OnLocationClicked(object sender, int e)
        {
            mSelectedStorage = e;
            mSelectedStorageClass = mStorages[e];
            //mTextSelectedLocation.Text = mLocations[e].Name;
            MainActivity.StaticStorageClass.Id = mStorages[e].Id;
            MainActivity.StaticStorageClass.Name = mStorages[e].Name;
            MainActivity.StaticStorageClass.Area = mStorages[e].Area;
            MainActivity.StaticStorageClass.Description = mStorages[e].Description;
        }
        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.content_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
    }
}