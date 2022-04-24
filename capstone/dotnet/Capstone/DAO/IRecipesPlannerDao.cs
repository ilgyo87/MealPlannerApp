using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IRecipesPlannerDao
    {
        List<RecipesPlanner> GetAllRps();
        List<RecipesPlanner> GetRecipesByPlanner(int plannerId);
        RecipesPlanner AddRecipesPlanner(RecipesPlanner plan);
        RecipesPlanner UpdateRecipesPlanner(RecipesPlanner planner);
        void DeleteRecipesPlanner(int plannerId);

    }
}
