using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IRecipeDao
    {
        Category GetCategoryById(int catId);
        Area GetAreaById(int areaId);
        Recipe GetRecipeById(int recipeId);
        List<Recipe> GetAllRecipes();
        List<Recipe> GetRecipeListByLetter(char charLetter);
        List<Recipe> GetRecipeListBySearchterm(string term);
        List<Recipe> GetRecipeListByCategoryId(int catId);
        List<Recipe> GetRecipeListByAreaId(int areaId);
        Recipe AddRecipe(Recipe recipe);
        Recipe UpdateRecipe(Recipe recipe);
        //Category AddCategory(Category cat);
        //Category UpdateCategory(Category cat);
        //Area AddArea(Area area);
        //Area UpdateArea(Area area);

    }
}
