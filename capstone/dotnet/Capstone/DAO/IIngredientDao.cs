using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IIngredientDao
    {
        List<Ingredient> GetAllIngredients();
        Ingredient GetIngredientByIngredId(int ingredId);
        List<Ingredient> GetIngredientsByTypeId(int ingredTypeId);
        List<Recipe> GetRecipesByIngredient(string term);
        Ingredient AddIngredient(Ingredient ingred);
        Ingredient UpdateIngredient(Ingredient ingred);
        RecipesIngredients AddIngredientToRecipe(RecipesIngredients ri);
        RecipesIngredients UpdateIngredientToRecipe(RecipesIngredients ri);
        List<IngredType> GetAllIngredType();
        IngredType GetIngredTypeById(int ingredTypeId);
        List<RecipesIngredients> GetAllRecipesIngredientsByRecipeId(int recipeId);
        //IngredType AddIngredType(IngredType type);
        //IngredType UpdateIngredType(IngredType type);

    }
}
