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
   public class OnCompleteCategoryDetailEventArgs:EventArgs
    {
        private string _name;


        private string _description;


        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public OnCompleteCategoryDetailEventArgs(string name,string description):base()
        {
            Name = name;
            Description = description;
        }

    }
   
    class DialogCategoryDetail:DialogFragment
    {
        private EditText mEditTextName;
        private EditText mEditTextArea;
        private EditText mEditTextBlock;
        private EditText mEditTextDescription;
        private Button mButtonCancel;
        private Button mButtonOk;
        private CategoryViewModel mCategory = new CategoryViewModel();
        public event EventHandler<OnCompleteCategoryDetailEventArgs> OnCompleteCategoryDetail;
        public override  View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogCategoryDetail, container, false);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonCategoryDetailCancel);
            mButtonOk = view.FindViewById<Button>(Resource.Id.buttonCategoryDetailEdit);
            mEditTextName = view.FindViewById<EditText>(Resource.Id.editTextCategoryDetailName);
            mEditTextDescription = view.FindViewById<EditText>(Resource.Id.editTextCategoryDetailDescription);
            mEditTextName.Text = mCategory.Name;

            mEditTextDescription.Text = mCategory.Description;
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
            OnCompleteCategoryDetail.Invoke(this, new OnCompleteCategoryDetailEventArgs(mEditTextName.Text,mEditTextDescription.Text));
            this.Dismiss();
        }
     

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            //Dialog.Window.Attributes.WindowAnimation = Resource.Style.dialogueAnimation; //set animasi
        }
        public DialogCategoryDetail(CategoryViewModel cat)
        {
            mCategory = cat;
        }
    }
}