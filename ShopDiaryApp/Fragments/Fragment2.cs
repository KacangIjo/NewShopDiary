using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;

namespace ShopDiaryApp.Fragments
{
    public class Fragment2 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.fragment2);
            // Create your fragment here
        }

        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    //MenuInflater.Inflate(Resource.Menu.nav_menu,menu);

        //    //var searchItem = menu.FindItem(Resource.Id.action_search);

        //    //var searchView = searchItem.ActionView.JavaCast<Android.Widget.SearchView>();
        //    //return base.OnCreateOptionsMenu(menu);
        //}


       
    }
}