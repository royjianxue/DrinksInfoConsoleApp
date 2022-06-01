using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;


namespace DrinksInfo
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            

            var drink = await Program.GetDrinkCategories();

            TableVisualization.ShowTable(drink);


            foreach (var item in drink)
            {
                Console.WriteLine(item.StrCategory);
            }

        }

        public static async Task<List<Category>> GetDrinkCategories()
        {
            string url = "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list";

            var httpResponseMessage = await client.GetAsync(url);

            string jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

            var category = JsonConvert.DeserializeObject<CategoryList>(jsonResponse);

            return category.Drinks;

        }
    
 

            
    }
}


