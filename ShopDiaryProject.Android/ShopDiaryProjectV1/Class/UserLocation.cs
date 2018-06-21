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

    public class UserLocation
    {
        
        
        private string _description;
        public Guid ID { get; set; }

        public Guid RoleLocationId { get; set; }
        public Guid UserId { get; set; }
        public Guid LocationId { get; set; }


        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }



        public UserLocation()
        {
        }
        public UserLocation(Guid id, Guid rolelocationid, Guid userid, Guid locationid, string area, string description)
        {

        }
    }
}
