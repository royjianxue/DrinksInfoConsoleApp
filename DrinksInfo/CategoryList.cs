using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrinksInfo
{
    // CategoryList myDeserializedClass = JsonConvert.DeserializeObject<CategoryList>(myJsonResponse);
    public class CategoryList
    {
        [JsonProperty("drinks")]
        public List<Category> Drinks { get; set; }
    }

    public class Category
    {
        [JsonProperty("strCategory")]
        public string StrCategory { get; set; }
    }
}
