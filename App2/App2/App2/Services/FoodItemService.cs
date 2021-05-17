using Firebase.Database;
using Firebase.Database.Query;
using System;
using App2.Model;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace App2.Services
{
    class FoodItemService
    {
        FirebaseClient client;
        public FoodItemService()
        {
            client = new FirebaseClient("https://bellaria-10f58-default-rtdb.europe-west1.firebasedatabase.app/");
        }
        public async Task<List<FoodItem>> GetFoodItemsAsync()
        {
            var products = (await client.Child("FoodItems")
                .OnceAsync<FoodItem>()).Select(f => new FoodItem
                {
                    CategoryID=f.Object.CategoryID,
                    Description=f.Object.Description,
                    HomeSelected= f.Object.HomeSelected,
                    ImageUrl= f.Object.ImageUrl,
                    Name= f.Object.Name,
                    Price= f.Object.Price,
                    ProductID = f.Object.ProductID,
                    Rating=f.Object.Rating,
                    RatingDetail= f.Object.RatingDetail
                }).ToList();
            return products;
        }
        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByCategoryAsync(int CategoryID)
        {
            var foodItem = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(p => p.CategoryID == CategoryID).ToList();
            foreach(var item in items)
            {
                foodItem.Add(item);
            }
            return foodItem;
        }
        public async Task<ObservableCollection<FoodItem>> GetLatestFoodItem()
        {
            var LatestFoodItem = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).OrderByDescending(f => f.ProductID).Take(3);
            foreach(var item in items)
            {
                LatestFoodItem.Add(item);
            }
            return LatestFoodItem;
        }
    }
}
