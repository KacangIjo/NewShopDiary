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
    public class Location
    {
        private string _name;
        private string _address;
        private string _description;
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

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

        public Location()
        {
        }
  

    }
}