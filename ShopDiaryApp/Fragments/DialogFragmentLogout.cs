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

namespace ShopDiaryApp.Fragments
{
    public class DialogFragmentLogout : DialogFragment
    {
        public static DialogFragmentLogout NewInstance(Bundle bundle)
        {
            DialogFragmentLogout fragment = new DialogFragmentLogout();
            fragment.Arguments = bundle;
            return fragment;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
            alert.SetTitle("Logout");
            alert.SetMessage("Are You Sure Want To LogOut?");
            alert.SetPositiveButton("Logout", (senderAlert, args) => {
                Toast.MakeText(Activity, "Deleted!", ToastLength.Short).Show();
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(Activity, "Cancelled!", ToastLength.Short).Show();
            });

            return alert.Create();
        }
    }
}