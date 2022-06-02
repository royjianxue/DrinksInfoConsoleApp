using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace DrinksInfo
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {

            RunninnSelection();
        }


        static void DisplayCategoryMenu()
        {          
            var myDrinkCategories = GetDrinkCategories();

            TableVisualization.ShowTable(myDrinkCategories, "Categories Menu: ");
        }


        static void RunninnSelection()
        {
            bool isValidCategory = false;

            DisplayCategoryMenu();

            string chosenDrinkCategory = "";

            do
            {
                chosenDrinkCategory = UserInput.AskUserStringInput("Choose your drink category: ");

                isValidCategory = IsUserChoiceCategoryInTheMenu(chosenDrinkCategory);

            } while (isValidCategory == false);

            var myDrinks = GetDrinks(chosenDrinkCategory);

            TableVisualization.ShowTable(myDrinks, "Drinks Menu");
        }


        static bool IsUserChoiceCategoryInTheMenu(string chosenDrinkCategory)
        {
            var myDrinkCategories = GetDrinkCategories();

            foreach (var item in myDrinkCategories)
            {
                if (item.StrCategory.ToLower() == chosenDrinkCategory.ToLower())
                {
                    var drinks = GetDrinks(chosenDrinkCategory);

                    return true;
                }

            }
            return false;
        }


        static List<Category> GetDrinkCategories()
        {
            string drinkCategories = GetData("/list.php?c=list").Result;

            var categories = JsonConvert.DeserializeObject<CategoryList>(drinkCategories).Drinks;

            return categories;
        }

        static List<Drink> GetDrinks(string input)
        {
            string drinkList = GetData($"/filter.php?c={HttpUtility.UrlEncode(input)}").Result;

            var drinks = JsonConvert.DeserializeObject<Drinks>(drinkList).DrinkList;

            return drinks;

        }

        static async Task<string> GetData(string url)
        {
            RestClient client = new("https://www.thecocktaildb.com/api/json/v1/1"); //using rest client API to read the URL

            RestRequest request = new RestRequest(url, Method.Get);

            request.AddHeader("Accept", "application/json");

            request.RequestFormat = DataFormat.Json;

            RestResponse response = await client.ExecuteGetAsync(request);

            return response.Content;
        }


    }
}


