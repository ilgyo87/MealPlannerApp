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

    public class RecipeController : ControllerBase
    {
        private readonly IRecipeDao recipesDao;

        public RecipeController(IRecipeDao recipesDao)
        {
            this.recipesDao = recipesDao;
        }

        [HttpGet("/area/{areaId}")]
        public ActionResult<Recipe> GetAreaById(int areaId)
        {
            Area area = recipesDao.GetAreaById(areaId);
            if (area != null)
            {
                return Ok(area);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/category/{catId}")]
        public ActionResult<Recipe> GetCategoryById(int catId)
        {
            Category cat = recipesDao.GetCategoryById(catId);
            if (cat != null)
            {
                return Ok(cat);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{recipeId}")]
        public ActionResult<Recipe> GetRecipeById(int recipeId)
        {
            Recipe recipe = recipesDao.GetRecipeById(recipeId);
            if (recipe != null)
            {
                return Ok(recipe);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("c={catId}")]
        public ActionResult<List<Recipe>> GetRecipesByCategoriesId(int catId)
        {
            List<Recipe> recipes = recipesDao.GetRecipeListByCategoryId(catId);
            if (recipes != null)
            {
                return Ok(recipes);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("a={areaId}")]
        public ActionResult<List<Recipe>> GetRecipesByAreasId(int areaId)
        {
            List<Recipe> recipes = recipesDao.GetRecipeListByAreaId(areaId);
            if (recipes != null)
            {
                return Ok(recipes);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet()]
        public ActionResult<List<Recipe>> GetAllRecipes()
        {
            List<Recipe> recipes = recipesDao.GetAllRecipes();

            if (recipes != null)
            {
                return Ok(recipes);
            }

            else
            {
                return BadRequest();
            }
        }


        [HttpGet("f={charLetter}")]
        public ActionResult<List<Recipe>> GetRecipeListByLetter(char charlLetter)
        {
            List<Recipe> recipes = recipesDao.GetRecipeListByLetter(charlLetter);
            if (recipes != null)
            {
                return Ok(recipes);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("s={term}")]
        public ActionResult<List<Recipe>> GetRecipeListByTerm(string term)
        {
            List<Recipe> recipes = recipesDao.GetRecipeListBySearchterm(term);
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
        public ActionResult<Recipe> AddRecipe(Recipe recipe)
        {
            if (recipe != null)
            {
                //recipe.UserId = Int32.Parse(User.FindFirst("sub")?.Value);
                Recipe newRecipe = recipesDao.AddRecipe(recipe);
                
                return Created($"/recipe/{newRecipe.RecipeId}", newRecipe);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public ActionResult<Recipe> UpdateRecipe(Recipe recipe)
        {
            if (recipe != null)
            {
                Recipe updatedRecipe = recipesDao.UpdateRecipe(recipe);
                return Created($"/recipe/{updatedRecipe.RecipeId}", updatedRecipe);

            }
            else
            {
                return BadRequest();
            }
        }

    }
}


