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
   public class OnCompleteStorageDetailEventArgs:EventArgs
    {
        private string _name;
        private string _block;
        
        private string _description;
        private string _area;

        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }


        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Block
        {
            get { return _block; }
            set { _block = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public OnCompleteStorageDetailEventArgs(string name,string area,string block,string description):base()
        {
            Name = name;
            Area = area;
            Block = block;
            Description = description;
        }

    }
   
    class DialogStorageDetail:DialogFragment
    {
        private EditText mEditTextName;
        private EditText mEditTextArea;
        private EditText mEditTextBlock;
        private EditText mEditTextDescription;
        private Button mButtonCancel;
        private Button mButtonOk;
        private StorageViewModel mStorage=new StorageViewModel();
        public event EventHandler<OnCompleteStorageDetailEventArgs> OnCompleteStorageDetail;
        public override  View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogStorageDetail, container, false);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonStoreDetailCancel);
            mButtonOk = view.FindViewById<Button>(Resource.Id.buttonStoreDetailOk);
            mEditTextName = view.FindViewById<EditText>(Resource.Id.editTextStorageDetailName);
            mEditTextArea = view.FindViewById<EditText>(Resource.Id.editTextStorDetailArea);
            mEditTextBlock = view.FindViewById<EditText>(Resource.Id.editTextStorDetailBlock);
            mEditTextDescription = view.FindViewById<EditText>(Resource.Id.editTextStoreDetailDescription);
            mEditTextName.Text = mStorage.Name;
            mEditTextArea.Text = mStorage.Area;
            mEditTextDescription.Text = mStorage.Description;
            mButtonOk.Click += BtnOk_Click;
            mButtonCancel.Click += BtnCancel_Click;

            return view;
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
           
            this.Dismiss();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            //klik button registernya...
            OnCompleteStorageDetail.Invoke(this, new OnCompleteStorageDetailEventArgs(mEditTextName.Text,mEditTextArea.Text,mEditTextBlock.Text,mEditTextDescription.Text));
            this.Dismiss();
        }
     

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            //Dialog.Window.Attributes.WindowAnimation = Resource.Style.dialogueAnimation; //set animasi
        }
        public DialogStorageDetail(StorageViewModel storage)
        {
            mStorage = storage;
        }
    }
}