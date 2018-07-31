using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Services;
using System.Collections.Generic;

namespace ShopDiaryApp.Fragments
{
    public class HomeFragment : Fragment
    {
        private MainAdapter mInventoryAdapter;

        public List<InventoryViewModel> mInventories;
        public List<ProductViewModel> mProducts;
        public List<StorageViewModel> mStorages;
        public List<RoleLocationViewModel> mRoles;
        public List<UserLocationViewModel> mUserLoc;

        private readonly InventoryDataService mInventoryDataService;
        private readonly ProductDataService mProductDataService;
        private readonly StorageDataService mStorageDataService;

        public HomeFragment()
        {
            mInventoryDataService = new InventoryDataService();
            mProductDataService = new ProductDataService();
            mStorageDataService = new StorageDataService();
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static HomeFragment NewInstance()
        {
            var frag1 = new HomeFragment { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.HomeLayout, null);
        }
    }
}