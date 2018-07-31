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
    public class Product
    {
        
        private string _barcodeId;
        private string _name;
        public List<Inventory> _inventories { get; set; }
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string BarcodeId
        {
            get { return _barcodeId; }
            set { _barcodeId = value; }
        }

        public Product()
        {
            this._inventories = new List<Inventory>();
        }
        public Product(string barcodeId, string name,Inventory i)
        {
            ID = ID + 1;
            _inventories = new List<Inventory>();
            BarcodeId = barcodeId;
            Name = name;
            _inventories.Add(i);
        }

    }
}