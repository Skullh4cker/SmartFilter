using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FinalVoyage
{
    public class Search
    {
        public static int GetIndexOf(string str, int from = 0)
        {
            for (int i = from; i < Program.Recipes.Count; ++i)
            {
                if (Program.Recipes[i].Name.ToLower().Contains(str.ToLower()))
                    return i;
            }
            return -1;
        }

        public static List<Recipe> ApplyRequestFilter(RequestFilter filter)
        {
            var recipes = new Dictionary<Recipe, int>();

            for (int i = 0; i < Program.Recipes.Count; ++i)
            {
                var ingredients = Program.Recipes[i].Ingredients;
                var tags = Program.Recipes[i].Tags;

                bool withoutForbidden = true;
                int mandatoryIngredients = 0;
                int priority = 0;

                foreach (var tag in tags)
                {
                    if (filter.WantedTags.Contains(new Tag(tag)))
                    {
                        priority++;
                    }
                    else if (filter.UnwantedTags.Contains(new Tag(tag)))
                    {
                        priority--;
                    }
                }

                foreach (var ingredient in ingredients)
                {
                    var id = ingredient.ID;

                    if (filter.RequiredIngredients.Any(s => s.ID == id))
                    {
                        mandatoryIngredients++;
                    }
                    if (filter.IgnoredIngredients.Any(s => s.ID == id))
                    {
                        withoutForbidden = false;
                        break;
                    }

                    foreach (var n in filter.RequiredIngredients)
                    {
                        double density;
                        if (n.GramsInCup != 0) density = n.GramsInCup / 200;
                        else density = 1;
                        switch (n.Measurement)
                        {
                            case ("кг"):
                                n.Quantity *= 1000;
                                n.Measurement = "гр";
                                break;
                            case ("шт."):
                                n.Quantity *= n.GramsInPce;
                                n.Measurement = "гр";
                                break;
                            case ("мл"):
                                n.Quantity *= density;
                                n.Measurement = "гр";
                                break;
                        }
                        if (n.ID == id && n.Quantity > ingredient.Quantity)
                        {
                            withoutForbidden = false;
                            goto conclusion;
                        }
                    }
                }
            conclusion:
                if (withoutForbidden && mandatoryIngredients == filter.RequiredIngredients.Count)
                {
                    recipes.Add(Program.Recipes[i], priority);
                }
            }
            var y = recipes.OrderBy(x => x.Value).ToDictionary(z => z.Key, a => a.Value).Keys.ToList();
            y.Reverse();
            return y;
        }
    }
}
