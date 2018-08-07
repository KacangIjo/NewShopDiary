using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;

namespace ShopDiaryApp.Fragments
{
    public class LocationsFragment : Fragment
    {
        Android.Support.V7.Widget.SearchView mSearchView;
        FragmentTransaction mFragmentTransaction;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
            // Create your fragment here

        }

        public static LocationsFragment NewInstance()
        {
            var frag2 = new LocationsFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.ManageLocationsLayout, null);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            //SearchMenu
            inflater.Inflate(Resource.Menu.nav_search, menu);
            var searchItem = menu.FindItem(Resource.Id.action_search);
            var provider = MenuItemCompat.GetActionView(searchItem);
            mSearchView = provider.JavaCast<Android.Support.V7.Widget.SearchView>();
            mSearchView.QueryTextSubmit += (sender, args) =>
            {
                Toast.MakeText(this.Activity, "You searched: " + args.Query, ToastLength.Short).Show();
            };

            //ActionMenu
            var mBottomToolbar = this.Activity.FindViewById<Android.Widget.Toolbar>(Resource.Id.secondToolbar);
            mBottomToolbar.InflateMenu(Resource.Menu.nav_manageLocation);
            mBottomToolbar.MenuItemClick += (sender, e) =>
            {

                switch (e.Item.ToString())
                {
                    case "Add":
                        ReplaceFragment(new LocationAddFragment(), "addlocation");
                        break;
                    

                }

                base.OnCreateOptionsMenu(menu, inflater);

            };
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