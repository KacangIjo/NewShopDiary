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
using SQLite;

namespace ShopDiaryAbb.LocalDomain
{
    public class UserInfoLocal
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int IsLogin { get; set; }
        public string UserInfoId { get; set; }
    }
}