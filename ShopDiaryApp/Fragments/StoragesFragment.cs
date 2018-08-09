using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;

namespace ShopDiaryApp.Fragments
{
    public class StoragesFragment : Fragment
    {
        ViewPager pager;
       
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //var mToolBar = this.Activity.FindViewById<Toolbar>(Resource.Id.secondToolbar);
            //mToolBar.Visibility =  Android.Views.ViewStates.Invisible;
            // Create your fragment here
        }

        public static StoragesFragment NewInstance()
        {
            var frag2 = new StoragesFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu=true;
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.TabbedManageStorageLayout, null);
        }

      

    }
}