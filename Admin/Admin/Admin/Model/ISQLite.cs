using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Model
{
   public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
