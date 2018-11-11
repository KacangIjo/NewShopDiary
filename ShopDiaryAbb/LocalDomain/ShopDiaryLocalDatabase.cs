using Android.Util;
using SQLite;

using System.Collections.Generic;
using System.Linq;
namespace ShopDiaryAbb.LocalDomain
{
    public class ShopDiaryLocalDatabase
    {
        readonly string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "UserInfoLocal.db")))
                {
                    connection.CreateTable<UserInfoLocal>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        //Add or Insert Operation  

        public bool InsertIntoTable(UserInfoLocal localuserinfo)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "UserInfoLocal.db")))
                {
                    connection.Insert(localuserinfo);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public List<UserInfoLocal> SelectTable()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "UserInfoLocal.db")))
                {
                    return connection.Table<UserInfoLocal>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

       
        //Edit Operation  

        public bool UpdateTable(UserInfoLocal localuserinfo)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "UserInfoLocal.db")))
                {
                    connection.Query<UserInfoLocal>("UPDATE UserInfoLocal set IsLogin=?, UserInfoId=? Where Id=?", localuserinfo.IsLogin, localuserinfo.UserInfoId, localuserinfo.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        //Delete Data Operation  

        public bool RemoveTable(UserInfoLocal localuserinfo)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "UserInfoLocal.db")))
                {
                    connection.Delete(localuserinfo);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        //Select Operation  

        public bool SelectTable(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "UserInfoLocal.db")))
                {
                    connection.Query<UserInfoLocal>("SELECT * FROM UserInfoLocal Where IsLogin=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

       
    }
}