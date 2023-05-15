using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalVoyage
{
    internal class Program
    {
        public static List<Recipe> Recipes = new List<Recipe>();
        static void Main(string[] args)
        {
            RequestFilter filter = new RequestFilter();
            filter.RequireIngredient(new Ingredient("Фасоль", 100, "гр", "", "", 0, 220, 96, 0, 0, 0, 0));
            using (StreamReader streamReader = new StreamReader("C:/Users/Skullhacker/Downloads/DataBase1000menu/recipes1000menu.json"))
            {
                Recipes = JsonSerializer.Deserialize<List<Recipe>>(streamReader.ReadToEnd());
            }
           Recipes = Search.ApplyRequestFilter(filter);
            foreach (Recipe recipe in Recipes)
            {
                Console.WriteLine(recipe.Name);
            }
            Console.ReadLine();
        }
    }
}
