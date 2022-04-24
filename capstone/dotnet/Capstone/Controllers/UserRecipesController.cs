using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class UserRecipesController : Controller
    {
        private readonly IUserRecipesDao userRecipesDao;

        public UserRecipesController(IUserRecipesDao userRecipesDao)
        {
            this.userRecipesDao = userRecipesDao;
        }

        [HttpGet("user")]
        public int getUserId()
        {
            return Int32.Parse(User.FindFirst("sub")?.Value);
        }

        [HttpGet()]
        public ActionResult<List<Recipe>> GetUserRecipesById()
        {
            int userId = Int32.Parse(User.FindFirst("sub")?.Value);

            List<Recipe> recipes = userRecipesDao.GetUserRecipes(userId);

            if (recipes != null)
            {
                return Ok(recipes);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("post")]
        public ActionResult<UserRecipes> AddRecipe(Recipe recipe)
        {

            int userId = Int32.Parse(User.FindFirst("sub")?.Value);

            UserRecipes userRecipes = userRecipesDao.AddRecipe(recipe.RecipeId, userId);


            if (userRecipes != null)
            {
                return Created($"/userrecipes/{userRecipes.id}", userRecipes);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("{recipeId}=delete")]
        public void DeleteRecipe(int recipeId)
        {
            int userId = Int32.Parse(User.FindFirst("sub")?.Value);

            userRecipesDao.DeleteRecipe(recipeId, userId);
        }
    }
}
