using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class RecipesIngredients
    {
        public int riRecipeId { get; set; }
        public string Name { get; set; }
        public int RecipeId { get; set; }
        public int IngredId { get; set; }
        public string Measure { get; set; }
    }
}
