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
   public class OnShopItemOptionPicked : EventArgs
    {
        public bool IsNeedLoadData { get; set; }
        public int MenuItem { get; set; }

        public OnShopItemOptionPicked(bool isNeedLoadData, int menuItem) : base()
        {
            IsNeedLoadData = isNeedLoadData;
            MenuItem = menuItem;
        }

    }
    class DialogShopItemOptions: DialogFragment
    {
        private bool isNeedLoadData;

        FragmentTransaction mFragmentTransaction;
     
        private Button mButtonRemove;
        private Button mButtonCancel;
        private TextView mSelected;
        private int menuItem;

        public int OnHomeInventoryDialogPicked { get; internal set; }

        public event EventHandler<OnShopItemOptionPicked> OnShopItemOptionPicked;
        public override  View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogShopItemOptions, container, false);
            
            mButtonRemove = view.FindViewById<Button>(Resource.Id.buttonDialogShopItemsRemove);
            mButtonCancel = view.FindViewById<Button>(Resource.Id.buttonDialogShopItemsCancel);
            mSelected = view.FindViewById<TextView>(Resource.Id.textViewDialogShopItem);

            mSelected.Text = ShopItemsFragment.mSelectedShopItem.ItemName;

            mButtonRemove.Click += BtnRemove_Click;
            mButtonCancel.Click += BtnCancel_Click;
            return view;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            isNeedLoadData = false;
            Dismiss();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            isNeedLoadData = true;
            menuItem = 1;
            OnShopItemOptionPicked.Invoke(this, new OnShopItemOptionPicked(isNeedLoadData, menuItem));
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