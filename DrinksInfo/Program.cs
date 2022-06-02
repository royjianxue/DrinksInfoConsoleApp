using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
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
            DisplayCategoryMenu();
            ChoseDrinkCategories();
            ChoseDrinkIngredits();

        }
        static void DisplayCategoryMenu()
        {          
            var myDrinkCategories = GetDrinkCategories();

            TableVisualization.ShowTable(myDrinkCategories, "Categories Menu");

        }
        static void ChoseDrinkCategories()
        {
            bool isValidCategory = false;

            string chosenDrinkCategory = "";

            do
            {
                chosenDrinkCategory = UserInput.AskUserStringInput("Choose your drink category: ");

                isValidCategory = IsUserChoiceCategoryInTheMenu(chosenDrinkCategory);

            } while (isValidCategory == false);

            var myDrinks = GetDrinksByName(chosenDrinkCategory);

            TableVisualization.ShowTable(myDrinks, "Drinks Menu");
        }

        static void ChoseDrinkIngredits()
        {
            bool isValidCategory = false;

            string chosenDrinkCategory = "";

            do
            {
                chosenDrinkCategory = UserInput.AskUserStringInput("Choose your drink to display drink detail: ");

                isValidCategory = IsUserChoiceCategoryInTheMenu(chosenDrinkCategory, true);

            } while (isValidCategory == false);

            var myDrinks = GetDrinksDetailByID(chosenDrinkCategory);


            DrinkDetail drinks = myDrinks[0];

            List<object> prepList = new();

            string formattedName = "";

            foreach (PropertyInfo prop in drinks.GetType().GetProperties())
            {

                if (prop.Name.Contains("str"))
                {
                    formattedName = prop.Name.Substring(3);
                }

                if (!string.IsNullOrEmpty(prop.GetValue(drinks)?.ToString()))
                {
                    prepList.Add(new
                    {
                        Key = formattedName,
                        Value = prop.GetValue(drinks)
                    });
                }
            }
            TableVisualization.ShowTable(prepList, "Drink Detail Menu");
        }
 
        static bool IsUserChoiceCategoryInTheMenu(string chosenDrinkCategory, bool searchForDrinksDetail = false)
        {           

            if (searchForDrinksDetail == false)
            {
                var myDrinkCategories = GetDrinkCategories();

                foreach (var item in myDrinkCategories)
                {
                    if (item.StrCategory.ToLower() == chosenDrinkCategory.ToLower())
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                var myDrinkCategories = GetDrinksDetailByID(chosenDrinkCategory);

                foreach (var item in myDrinkCategories)
                {
                    if (item.IdDrink == chosenDrinkCategory)
                    {                     

                        return true;
                    }
                }
                return false;
            }  

        }

        static List<Category> GetDrinkCategories()
        {
            string drinkCategories = GetData("/list.php?c=list").Result;

            var categories = JsonConvert.DeserializeObject<CategoryList>(drinkCategories).Drinks;

            return categories;
        }

        static List<Drink> GetDrinksByName(string input)
        {
            string drinkList = GetData($"/filter.php?c={HttpUtility.UrlEncode(input)}").Result;

            var drinks = JsonConvert.DeserializeObject<Drinks>(drinkList).DrinkList;

            return drinks;
        }


        static List<DrinkDetail> GetDrinksDetailByID(string input)
        {
            string drinkList = GetData($"/lookup.php?i={HttpUtility.UrlEncode(input)}").Result;

            var drinks = JsonConvert.DeserializeObject<DrinksDetail>(drinkList).Drinks;

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


