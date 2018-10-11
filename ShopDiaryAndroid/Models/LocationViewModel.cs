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
using ShopDiaryProject.Domain.Models;

namespace ShopDiaryAndroid.Models
{
    public class LocationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public LocationViewModel() { }
        public LocationViewModel(Location location)
        {
            Id = location.Id;
            Name = location.Name;
            Address = location.Address;
            Description = location.Description;
        }
    }
}