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
    public class Category
    {

        private string _name;
        private Guid _userId;

        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public List<Product> _products { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Category()
        {
            this._products = new List<Product>();
        }

        public Category(string name)
        {
            Name = name;
        }
    }
}