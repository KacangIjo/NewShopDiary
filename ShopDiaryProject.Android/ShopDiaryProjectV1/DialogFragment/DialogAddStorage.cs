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
   public class OnAddStorageEventArgs:EventArgs
    {
        private string _name;
        private string _area;
        private string _block;
        private string _description;

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

        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public OnAddStorageEventArgs(string name,string area,string block,string description):base()
        {
            Name = name;
            Area = area;
            Block = block;
            Description = description;
        }

    }
    class DialogAddStorage:DialogFragment
    {
        private EditText mEditTextName;
        private EditText mEditTextArea;
        private EditText mEditTextBlock;
        private EditText mEditTextDescription;
        private Button mButtonOK;
        private Button mButtonCancel;
        public event EventHandler<OnAddStorageEventArgs> OnAddStorageComplete;
        public override  View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogAddStorage, container, false);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonAddStorageCancel);
            mButtonOK = view.FindViewById<Button>(Resource.Id.buttonAddStorageOK);
            mEditTextName = view.FindViewById<EditText>(Resource.Id.editTextAddStorageName);
            mEditTextArea = view.FindViewById<EditText>(Resource.Id.editTextAddStorageArea);
            mEditTextBlock = view.FindViewById<EditText>(Resource.Id.editTextAddStorageBlock);
            mEditTextDescription = view.FindViewById<EditText>(Resource.Id.editTextAddStorageDesc);
            mButtonOK.Click += BtnOk_Click;
            
            return view;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            OnAddStorageComplete.Invoke(this, new OnAddStorageEventArgs(mEditTextName.Text,mEditTextArea.Text,mEditTextBlock.Text,mEditTextDescription.Text));
            this.Dismiss();
        }
 
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            //Dialog.Window.Attributes.WindowAnimation = Resource.Style.dialogueAnimation; //set animasi
        }
    }
}