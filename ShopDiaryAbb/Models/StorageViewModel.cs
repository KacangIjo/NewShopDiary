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

namespace ShopDiaryAbb.Models
{
    public class StorageViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Area { get; set; }
        public string Description { get; set; }

        public StorageViewModel() { }
        public StorageViewModel(Storage store)
        {
            Id = store.Id;
            Name = store.Name;

            Area = store.Area;
            Description = store.Description;
        }
    }
}