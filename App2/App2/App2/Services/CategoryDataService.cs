using Firebase.Database;
using System;
using App2.Model;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace App2.Services
{
    class CategoryDataService
    {
        FirebaseClient client;
        public CategoryDataService()
        {
            client = new FirebaseClient("https://bellaria-10f58-default-rtdb.europe-west1.firebasedatabase.app/");
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = (await client.Child("Categories").OnceAsync<Category>()).Select(c => new Category
            {
                CategoryID = c.Object.CategoryID,
                CategoryName=c.Object.CategoryName,
                CategoryPoster=c.Object.CategoryPoster,
                ImageUrl=c.Object.ImageUrl
            }).ToList();
            return categories;
        }
    }
}
