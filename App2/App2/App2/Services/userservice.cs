using App2.Model;
using Firebase.Database;
using Firebase.Database.Query;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Services
{

    class userservice 
    {
        /*
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
    */
        FirebaseClient client;

        public userservice()
        {
            client = new FirebaseClient("https://bellaria-10f58-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<bool> IsUserExists(string uname)
        {
            var user = (await client.Child("Users")
                .OnceAsync<user>()).Where(u => u.Object.username== uname).FirstOrDefault();

            return (user != null);
        }

        public async Task<bool> RegisterUser(string uname, string passwd)
        {
            if (await IsUserExists(uname) == false)
            {
                await client.Child("Users")
                    .PostAsync(new user()
                    {
                        username = uname,
                        password = passwd
                    });
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LoginUser(string uname, string passwd)
        {
            var user = (await client.Child("Users")
                .OnceAsync<user>()).Where(u => u.Object.username == uname)
                .Where(u => u.Object.password == passwd).FirstOrDefault();

            return (user != null);
        }
    }

}
