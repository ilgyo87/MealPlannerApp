using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserRecipesDao
    {
        List<Recipe> GetUserRecipes(int userId);
        UserRecipes AddRecipe(int recipeId, int userId);

        void DeleteRecipe(int recipeId, int userId);
    }
}
