using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInfo
{

    public class Drink
    {
        [JsonProperty("strDrink")]
        public string StrDrink { get; set; }

        [JsonProperty("idDrink")]
        public string IdDrink { get; set; }
    }

    public class Drinks
    {
        [JsonProperty("drinks")]
        public List<Drink> DrinkList { get; set; }
    }

}
