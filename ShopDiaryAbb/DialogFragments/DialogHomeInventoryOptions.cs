using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
   public class OnHomeInventoryDialogPicked : EventArgs
    {
        public bool IsNeedLoadData { get; set; }
        public int MenuItem { get; set; }

        public OnHomeInventoryDialogPicked(bool isNeedLoadData,int menuItem):base()
        {
            IsNeedLoadData = isNeedLoadData;
            MenuItem = menuItem;
        }

    }
    class DialogHomeInventoryOptions : DialogFragment
    {
        private bool isNeedLoadData;
        private int menuItem;

        FragmentTransaction mFragmentTransaction;
        private Button mButtonUse;
        private Button mButtonThrow;
        private Button mButtonRemove;
        private Button mButtonCancel;
        private TextView mSelected;
        public event EventHandler<OnHomeInventoryDialogPicked> OnHomeInventoryDialogPicked;
        public override  View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogInventoryOptions, container, false);

            mButtonUse = view.FindViewById<Button>(Resource.Id.buttonDialogInventoryUse);
            mButtonThrow = view.FindViewById<Button>(Resource.Id.buttonDialogInventoryThrow);
            mButtonRemove = view.FindViewById<Button>(Resource.Id.buttonDialogInventoryRemove);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonDialogInventoryCancel);
            mSelected = view.FindViewById<TextView>(Resource.Id.textViewDialogInventoriess);

            mSelected.Text = HomeInventoriesFragment.mSelectedInventoryClass.ItemName;
            mButtonUse.Click += BtnUse_Click;
            mButtonThrow.Click += BtnThrow_Click;
            mButtonRemove.Click += BtnDelete_Click;
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
            menuItem = 3;
            OnHomeInventoryDialogPicked.Invoke(this, new OnHomeInventoryDialogPicked(isNeedLoadData, menuItem));
            Dismiss();
        }

        private void BtnThrow_Click(object sender, EventArgs e)
        {
            isNeedLoadData = true;
            menuItem = 2;
            OnHomeInventoryDialogPicked.Invoke(this, new OnHomeInventoryDialogPicked(isNeedLoadData, menuItem));
            Dismiss();
        }

        private void BtnUse_Click(object sender, EventArgs e)
        {
            isNeedLoadData = true;
            menuItem = 1;
            OnHomeInventoryDialogPicked.Invoke(this, new OnHomeInventoryDialogPicked(isNeedLoadData,menuItem));
            Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
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