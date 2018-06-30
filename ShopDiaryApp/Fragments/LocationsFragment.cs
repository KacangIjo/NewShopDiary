using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;

namespace ShopDiaryApp.Fragments
{
    public class LocationsFragment : Fragment
    {
        Android.Support.V7.Widget.SearchView searchView;
        
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
            inflater.Inflate(Resource.Menu.nav_search, menu);

            var searchItem = menu.FindItem(Resource.Id.action_search);

            var provider = MenuItemCompat.GetActionView(searchItem);

            searchView = provider.JavaCast<Android.Support.V7.Widget.SearchView>();

            searchView.QueryTextSubmit += (sender, args) =>
            {
                //Toast.MakeText(this, "You searched: " + args.Query, ToastLength.Short).Show();
            };


            base.OnCreateOptionsMenu(menu,inflater);
        }






    }
}