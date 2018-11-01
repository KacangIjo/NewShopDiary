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

namespace ShopDiaryAbb.Class
{

    public class RoleLocation
    {
        
        
        private string _description;
        public Guid Id { get; set; }
        public int RoleCode { get; set; }


        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }



        public RoleLocation()
        {
        }
        public RoleLocation(Guid id, string description,int rolecode)
        {
            
        }
    }
}
