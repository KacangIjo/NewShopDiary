using Newtonsoft.Json;
using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Services.CategoryService
{
    public class WishlistService
    {
        private string url = "api/wishlists";
        private HttpClient client = new HttpClient();
        public async Task<List<Wishlist>> GetWishlists()
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WishlistOutputDto>(jsonString).Data;
            }
            else
                return new List<Wishlist>();
        }
    }

}
