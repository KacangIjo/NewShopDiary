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

namespace ShopDiaryProjectV1.Class
{
    public class Inventory
    {
        
        private DateTime _expDate;
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime ExpDate
        {
            get { return _expDate; }
            set { _expDate = value; }
        }
        public Inventory()
        {

        }
        public Inventory( DateTime expdate)
        {
            Id = Id + 1;
            ExpDate = expdate;
        }

    }
}