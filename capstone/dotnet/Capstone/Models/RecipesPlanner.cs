using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class RecipesPlanner
    {
        public int rpId { get; set; }
        public int PlannerId { get; set; }
        public int RecipeId { get; set; }
        public string Day { get; set; }
        public int Week { get; set; }
    }
}
