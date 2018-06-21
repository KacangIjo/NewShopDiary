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

namespace ShopDiaryProjectV1
{
   public class OnAddLocationEventArgs:EventArgs
    {
        private string _name;
        private string _address;
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public OnAddLocationEventArgs(string name,string address,string description):base()
        {
            Name = name;
            Address = address;
            Description = description;
        }

    }
    class DialogAddLocation:DialogFragment
    {
        private EditText mEditTextName;
        private EditText mEditTextAddress;
        private EditText mEditTextDescription;
        private Button mButtonOK;
        private Button mButtonCancel;
        public event EventHandler<OnAddLocationEventArgs> OnAddLocationComplete;
        public override  View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogAddLocation, container, false);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonAddLocationCancel);
            mButtonOK = view.FindViewById<Button>(Resource.Id.buttonAddLocationAdd);
            mEditTextName = view.FindViewById<EditText>(Resource.Id.editTextAddLocationName);
            mEditTextAddress = view.FindViewById<EditText>(Resource.Id.editTextAddLocationAddress);
            mEditTextDescription = view.FindViewById<EditText>(Resource.Id.editTextAddLocationDesc);
            mButtonOK.Click += BtnAddLocation_CLick;
            return view;
        }

        private void BtnAddLocation_CLick(object sender, EventArgs e)
        {
            //klik button registernya...
            OnAddLocationComplete.Invoke(this, new OnAddLocationEventArgs(mEditTextName.Text,mEditTextAddress.Text,mEditTextDescription.Text));
            this.Dismiss();
        }
     
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
           
        }
    }
}