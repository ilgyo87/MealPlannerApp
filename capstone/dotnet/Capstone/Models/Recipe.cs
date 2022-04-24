using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string DrinkAlternate { get; set; }
        public string Instructions { get; set; }
        public string RecipeImage { get; set; }
        public string RecipeTags { get; set; }
        public string Youtube { get; set; }
        public string Source { get; set; }
        public string ImageSource { get; set; }
        public string Date { get; set; }
        public int AreaId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
