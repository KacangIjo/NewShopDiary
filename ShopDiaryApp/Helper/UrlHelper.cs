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

namespace ShopDiaryApp.Helper
{
    public class UrlHelper
    {
        private const string Domain_Url = "http://shopdiary.funcraftstudio.com/";
        private const string Base_Url = Domain_Url + "api/";

        #region Rests
        public const string Locations_Url = Base_Url + "Locations";
        public const string Storages_Url = Base_Url + "Storages";
        public const string Inventories_Url = Base_Url + "Inventories";
        public const string Inventorylogs_Url = Base_Url + "Inventorylogs";
        public const string Categories_Url = Base_Url + "Categories";
        public const string Products_Url = Base_Url + "Products";
        public const string ShopList_Url = Base_Url + "ShopList";
        public const string Consumes_Url = Base_Url + "Consumes";
        public const string Purchases_Url = Base_Url + "Purchases";
        public const string Shopitem_Url = Base_Url + "Shopitems";
        public const string Shoplists_Url = Base_Url + "Shoplists";
        public const string Userlocations_Url = Base_Url + "UserLocations";
        public const string Rolelocations_Url = Base_Url + "RoleLocations";
        public const string UserDatas_Url = Base_Url + "UserDatas";
        #endregion

        #region User
        public const string Account_Url = Base_Url + "Account";
        public const string Account_Login = Domain_Url + "Token";
        
        #endregion
    }
}