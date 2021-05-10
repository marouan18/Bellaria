using App2.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace App2.Services
{

    class userservice
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<user>();
        }
        public async Task<bool> IsUserExists(string user)
        {
            await Init();
            var esito = await (from s in db.Table<user>()
                        where s.username == user
                        select s).FirstOrDefaultAsync();

            return (esito!=null);
        }

        public async Task<bool> Adduser(string username, string password)
        {
            await Init();
            if (await IsUserExists(username) == false)
            {
                var user = new user
                {
                    username = username,
                    password = password
                };
                var id = await db.InsertAsync(user);
                return true;
            }
            else
                return false;
        }
        public async Task<bool> LoginUser(string user, string passw) 
        {
            await Init();
            await Init();
            var esito = await (from s in db.Table<user>()
                               where s.username == user
                               where s.password==passw
                               select s).FirstOrDefaultAsync();

            return (esito != null);
        }
        public static async Task RemoveUser(int id)
        {
            await Init();
            await db.DeleteAsync(id);
        }
        public static async Task<IEnumerable<user>> GetUser()
        {
            await Init();
            var user = await db.Table<user>().ToListAsync();
            return user;
        }
    
    }

}
