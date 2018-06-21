
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
using ShopDiaryProjectV1.Class;

namespace ShopDiaryProjectV1
{
    public class OnRegisterEventArgs : EventArgs
    {
        private string mEmail;
        private string mPassword;
        private string mConfirmPassword;

        public string ConfirmPassword
        {
            get { return mConfirmPassword; }
            set { mConfirmPassword = value; }
        }


        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }


        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        public OnRegisterEventArgs(string email, string password, string confirmpassword) : base()
        {
            Email = email;

            Password = password;

            ConfirmPassword = confirmpassword;
        }
    }
    class dialogueRegister : DialogFragment
    {
        private EditText mtxtBoxEmail;
        private EditText mtxtBoxUsername;
        private EditText mtxtBoxPassword;
        private EditText mtxtBoxConfirmPassword;
        private Button mbtnRegister;
        //private Button mbtnRegister;
        public event EventHandler<OnRegisterEventArgs> OnRegisterComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogPageRegister, container, false);
            mtxtBoxEmail = view.FindViewById<EditText>(Resource.Id.editTextRegEmail);
            mtxtBoxUsername = view.FindViewById<EditText>(Resource.Id.editTextRegUser);
            mtxtBoxPassword = view.FindViewById<EditText>(Resource.Id.editText1);
            mtxtBoxConfirmPassword = view.FindViewById<EditText>(Resource.Id.editText2);
            mbtnRegister = view.FindViewById<Button>(Resource.Id.buttonRegisterOK);
            mbtnRegister.Click += BtnRegister_Click;
            return view;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            OnRegisterComplete.Invoke(this, new OnRegisterEventArgs(mtxtBoxEmail.Text, mtxtBoxPassword.Text, mtxtBoxConfirmPassword.Text));
            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
           
        }



    }
}
