using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using ShopDiaryAbb.Fragments;
using static Android.Widget.DatePicker;

namespace ShopDiaryAbb.DialogFragments
{
   public class OnStorageDialogPicked:EventArgs
    {
        private bool _isNeedLoadData;

        public bool IsNeedLoadData
        {
            get { return _isNeedLoadData; }
            set { _isNeedLoadData = value; }
        }

        public OnStorageDialogPicked(bool isNeedLoadData):base()
        {
            IsNeedLoadData = isNeedLoadData;
        }

    }
    class DialogStorageOptions:DialogFragment
    {
        private bool isNeedLoadData;

        FragmentTransaction mFragmentTransaction;
        private Button mButtonView;
        private Button mButtonEdit;
        private Button mButtonDelete;
        private Button mButtonCancel;
        private TextView mSelected;
        public event EventHandler<OnStorageDialogPicked> OnStorageOptionsPicked;
        public override  View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogStorageOptions, container, false);

            mButtonView = view.FindViewById<Button>(Resource.Id.buttonDialogStoragesViewInventories);
            mButtonEdit = view.FindViewById<Button>(Resource.Id.buttonDialogStoragesEdit);
            mButtonDelete = view.FindViewById<Button>(Resource.Id.buttonDialogStoragesDelete);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonDialogStorageCancel);
            mSelected= view.FindViewById<TextView>(Resource.Id.textViewDialogStorages);

            mSelected.Text = StoragesFragment.mSelectedStorageClass.Name;
            mButtonView.Click += BtnView_Click;
            mButtonEdit.Click += BtnEdit_Click;
            mButtonDelete.Click += BtnDelete_Click;
            mButtonCancel.Click += BtnCancel_Click;
            return view;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            isNeedLoadData = false;
            Dismiss();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            isNeedLoadData = true;
            Dismiss();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            isNeedLoadData = false;
            ReplaceFragment(new StorageEditFragment(), "Edit Storages");
            Dismiss();
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            isNeedLoadData = false;
            ReplaceFragment(new InventoriesFragment(), "Manage Inventories");
            Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            //Dialog.Window.Attributes.WindowAnimation = Resource.Style.dialogueAnimation; //set animasi
        }

        public void ReplaceFragment(Fragment fragment, string tag)
        {
            mFragmentTransaction = FragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.main_frame, fragment, tag);
            mFragmentTransaction.AddToBackStack(tag);
            mFragmentTransaction.CommitAllowingStateLoss();
        }
    }
}