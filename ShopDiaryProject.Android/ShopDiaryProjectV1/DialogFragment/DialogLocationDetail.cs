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
using Android.Locations;
using ShopDiaryProject.Android.Models.ViewModels;

namespace ShopDiaryProjectV1
{
   public class OnCompleteLocationDetailEventArgs:EventArgs
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

        public OnCompleteLocationDetailEventArgs(string name,string address,string description):base()
        {
            Name = name;
            Address = address;
            Description = description;
        }

    }
   
    class DialogLocationDetail:DialogFragment
    {
        private EditText mEditTextName;
        private EditText mEditTextAddress;
        private EditText mEditTextDescription;
        private Button mButtonCancel;
        private Button mButtonEdit;
        private LocationViewModel mLocation=new LocationViewModel();
        public event EventHandler<OnCompleteLocationDetailEventArgs> OnCompleteLocationDetail;
        public override  View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogLocationDetail, container, false);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonLocationDetailCancel);
            mButtonEdit = view.FindViewById<Button>(Resource.Id.buttonLocDetailEdit);
            mEditTextName = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailName);
            mEditTextAddress = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailAddress);
            mEditTextDescription = view.FindViewById<EditText>(Resource.Id.editTextLocationDetailDescription);
            mEditTextName.Text = mLocation.Name;
            mEditTextAddress.Text = mLocation.Address;
            mEditTextDescription.Text = mLocation.Description;
            mButtonEdit.Click += BtnEdit_Click;
        
           
            return view;
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
           
            this.Dismiss();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            //klik button registernya...
            OnCompleteLocationDetail.Invoke(this, new OnCompleteLocationDetailEventArgs(mEditTextName.Text,mEditTextAddress.Text,mEditTextDescription.Text));
            this.Dismiss();
        }
     

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            //Dialog.Window.Attributes.WindowAnimation = Resource.Style.dialogueAnimation; //set animasi
        }
        public DialogLocationDetail(LocationViewModel location)
        {
            mLocation = location;
        }
    }
}