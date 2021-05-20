using System;
using System.IO;
using App2.Model;
using SQLite;
using Xamarin.Forms;
[assembly: Dependency(typeof(App2.Droid.SQLite))]
namespace App2.Droid
{
    class SQLite: ISQLite
    {
    public SQLiteConnection GetConnection()
        {
            var sqliteFileName = "MyDatabase.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFileName);
            var cn = new SQLiteConnection(path);
            return cn;
        }
    }
}