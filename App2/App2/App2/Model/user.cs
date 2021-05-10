using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App2.Model
{
    class user
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
