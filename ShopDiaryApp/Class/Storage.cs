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

    public class Storage
    {
        
        private string _name;
        private string _area;
        private string _description;
        public int ID { get; set; }
        public List<Inventory> _inventories { get; set; }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Storage()
        {
        }
        public Storage(string name, string area, string description)
        {

        }
    }
}
