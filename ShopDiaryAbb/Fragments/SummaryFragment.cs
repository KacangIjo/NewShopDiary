using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Adapter;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Services;
using System;
using System.Collections.Generic;

namespace ShopDiaryAbb.Fragments
{
    public class SummaryFragment : Fragment
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
        public SummaryFragment()
        {
            mStorageDataService = new StorageDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public static SummaryFragment NewInstance()
        {
            var frag2 = new SummaryFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.SummaryLayout, container, false);

         
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
                mStoragesAdapter.ItemClick += OnStorageClicked;
                mListViewStorage.SetAdapter(this.mStoragesAdapter);
            }

        }
        private void OnStorageClicked(object sender, int e)
        {
            mSelectedStorage = e;
            mSelectedStorageClass = mStorages[e];
            //mTextSelectedLocation.Text = mLocations[e].Name;
            LoginPageActivity.StaticStorageClass = mStorages[e];
            ReplaceFragment(new InventoriesFragment(), mStorages[e].Name.ToString());

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