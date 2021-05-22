using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App2.Repositories
{
    interface IUserService
    {
        Task<bool> IsUserExists(string uname);
        Task<bool> RegisterUser(string uname, string passwd);
        Task<bool> LoginUser(string uname, string passwd);

    }
}
