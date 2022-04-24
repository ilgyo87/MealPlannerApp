using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Planner
    {
        public int PlannerId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool IsSharable { get; set; }
    }

    public class UserPlanner
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public int PlannerId { get; set; }
        public int RecipeId { get; set; }
    }
}
