using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace ShopDiaryApp.Fragments
{
    public class InventoriesFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static InventoriesFragment NewInstance()
        {
            var frag2 = new InventoriesFragment { Arguments = new Bundle() };
            return frag2;
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu=true;
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.ManageLocationsLayout, null);
        }

      

    }
}