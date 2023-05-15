using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FinalVoyage
{
    public class RequestFilter
    {
        public List<Ingredient> RequiredIngredients { get; private set; }
        public List<Ingredient> IgnoredIngredients { get; private set; }
        public List<Tag> WantedTags { get; private set; }
        public List<Tag> UnwantedTags { get; private set; }

        public RequestFilter(
            List<Ingredient> required,
            List<Ingredient> ignored,
            List<Tag> wantedTags,
            List<Tag> unwantedTags)
        {
            RequiredIngredients = required;
            IgnoredIngredients = ignored;
            WantedTags = wantedTags;
            UnwantedTags = unwantedTags;
        }

        public RequestFilter()
        {
            RequiredIngredients = new List<Ingredient>();
            IgnoredIngredients = new List<Ingredient>();
            WantedTags = new List<Tag>();
            UnwantedTags = new List<Tag>();
        }

        public void DisableIngredient(Ingredient ingredient)
        {
            int id = ingredient.ID;
            if (!IgnoredIngredients.Any(x => x.ID == id))
            {
                IgnoredIngredients.Add(ingredient);
            }
        }
        public void EnableIngredient(Ingredient ingredient)
        {
            int id = ingredient.ID;
            if (IgnoredIngredients.Any(x => x.ID == id))
            {
                IgnoredIngredients.Remove(ingredient);
            }
        }
        public void UnrequireIngredient(Ingredient ingredient)
        {
            int id = ingredient.ID;
            if (RequiredIngredients.Any(x => x.ID == id))
            {
                RequiredIngredients.Remove(ingredient);
            }
        }
        public void RequireIngredient(Ingredient ingredient)
        {
            int id = ingredient.ID;
            if (!RequiredIngredients.Any(x => x.ID == id))
            {
                RequiredIngredients.Add(ingredient);
            }
        }

        public void RequireTag(Tag tag)
        {
            if (!WantedTags.Contains(tag))
            {
                WantedTags.Add(tag);
            }
        }

        public void UnwantTag(Tag tag)
        {
            if (!UnwantedTags.Contains(tag))
            {
                UnwantedTags.Add(tag);
            }
        }
        public void RemoveWantedTag(Tag tag)
        {
            if (WantedTags.Contains(tag))
            {
                WantedTags.Remove(tag);
            }
        }

        public void RemoveUnwantedTag(Tag tag)
        {
            if (UnwantedTags.Contains(tag))
            {
                UnwantedTags.Remove(tag);
            }
        }
    }
}
