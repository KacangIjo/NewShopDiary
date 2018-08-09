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

namespace ShopDiaryApp.Class
{

    public class User
    {

        private Guid _Id;
        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }


        public Guid ID
        {
            get { return _Id; }
            set { _Id = value; }
        }


        public User()
        {
        }
        public User(Guid id)
        {
            ID = ID;
        }
        

    }
}