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
using static Android.Widget.DatePicker;

namespace ShopDiaryAbb.DialogFragments
{
   public class OnNumberPickedEventArgs:EventArgs
    {
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public OnNumberPickedEventArgs(int quantity):base()
        {
            Quantity = quantity;
        }

    }
    class DialogNumberPicker:DialogFragment
    {
        private int numberSet;
        private NumberPicker mNumberPicker;
        private Button mButtonOK;
        public event EventHandler<OnNumberPickedEventArgs> OnPickDateComplete;
        public override  View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogDatePicker, container, false);
          
            mButtonOK = view.FindViewById<Button>(Resource.Id.buttonDialogNumberPickerOk);
            mNumberPicker = view.FindViewById<NumberPicker>(Resource.Id.DialogNumberPicker);
            mNumberPicker.Value = 1;
            numberSet = mNumberPicker.Value;
            numberSet.ToString();
            mButtonOK.Click += BtnOk_Click;
            return view;
        }

     
        private void BtnOk_Click(object sender, EventArgs e)
        {
            numberSet = mNumberPicker.Value;
            OnPickDateComplete.Invoke(this, new OnNumberPickedEventArgs(numberSet));
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