using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ShopDiaryApp.Fragments
{
    public class LocationAddUserFragment : Android.Support.V4.App.Fragment
    {
        private EditText mEmail;
        private EditText mName;
        private EditText mDescription;
        private CheckBox mCreate;
        private CheckBox mUpdate;
        private CheckBox mRead;
        private CheckBox mDelete;
        private Button mButtonAdd;
        private Button mButtonCancel;

        Guid mAuthorizedId = LoginPageActivity.StaticUserClass.ID;
        public LocationAddUserFragment()
        {

        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public static LocationAddUserFragment NewInstance()
        {
            var frag2 = new LocationAddUserFragment { Arguments = new Bundle() };
            return frag2;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ManageLocationAddUser, container, false);
            mEmail = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailAddUserName);
            mName = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailAddUserName);
            mDescription = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailAddUserDescription);
            mCreate = view.FindViewById<CheckBox>(Resource.Id.checkBoxCreate);
            mDelete = view.FindViewById<CheckBox>(Resource.Id.checkBoxDelete);
            mUpdate = view.FindViewById<CheckBox>(Resource.Id.checkBoxUpdate);
            mRead = view.FindViewById<CheckBox>(Resource.Id.checkBoxRead);
            mButtonAdd = view.FindViewById<Button>(Resource.Id.buttonLocationDetailAddUserAdd);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonLocationDetailAddUserCancel);
            return view;
        }
    }
}