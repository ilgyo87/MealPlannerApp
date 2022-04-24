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

    public class IngredientController : ControllerBase
    {
        private readonly IIngredientDao ingredientDao;

        public IngredientController(IIngredientDao ingredientDao)
        {
            this.ingredientDao = ingredientDao;
        }

        [HttpGet()]
        public ActionResult<List<Ingredient>> GetAllIngredients()
        {
            List<Ingredient> ingredients = ingredientDao.GetAllIngredients();
            if (ingredients != null)
            {
                return Ok(ingredients);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{ingredId}")]
        public ActionResult<Ingredient> GetIngredientByIngredId(int ingredId)
        {
            Ingredient ingred = ingredientDao.GetIngredientByIngredId(ingredId);

            if (ingred != null)
            {
                return Ok(ingred);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("t={typeId}")]
        public ActionResult<List<Ingredient>> GetIngredientsByTypeId(int typeId)
        {
            List<Ingredient> ingreds = ingredientDao.GetIngredientsByTypeId(typeId);
            if (ingreds != null)
            {
                return Ok(ingreds);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("r={term}")]
        public ActionResult<List<Recipe>> GetRecipesByIngredient(string term)
        {
            List<Recipe> recipes = ingredientDao.GetRecipesByIngredient(term);
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
        public ActionResult<Ingredient> AddIngredient(Ingredient ingred)
        {
            if (ingred != null)
            {
                Ingredient newIngred = ingredientDao.AddIngredient(ingred);
                return Created($"/ingredient/{newIngred.IngredId}", newIngred);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public ActionResult<Ingredient> UpdateIngredient(Ingredient ingred)
        {
            if (ingred != null)
            {
                Ingredient updatedIngred = ingredientDao.UpdateIngredient(ingred);
                return Created($"/ingredient/{updatedIngred.IngredId}", updatedIngred);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("/ri/post")]
        public ActionResult<RecipesIngredients> AddIngredientToRecipe(RecipesIngredients ri)
        {
            if (ri != null)
            {
                RecipesIngredients newRi = ingredientDao.AddIngredientToRecipe(ri);
                return Created($"/ri/{newRi.riRecipeId}", newRi);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("/ri/update")]
        public ActionResult<RecipesIngredients> UpdateIngredient(RecipesIngredients ri)
        {
            if (ri != null)
            {
                RecipesIngredients updatedRi = ingredientDao.UpdateIngredientToRecipe(ri);
                return Created($"/ri/{updatedRi.riRecipeId}", updatedRi);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/ri/{recipeId}")]
        public ActionResult<List<RecipesIngredients>> GetAllRecipesIngredientsByRecipeId(int recipeId)
        {
            List<RecipesIngredients> riList = ingredientDao.GetAllRecipesIngredientsByRecipeId(recipeId);
            if (riList != null)
            {
                return Ok(riList);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("type")]
        public ActionResult<List<IngredType>> GetAllIngredientTypes()
        {
            List<IngredType> types = ingredientDao.GetAllIngredType();
            if (types != null)
            {
                return Ok(types);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("type/{typeId}")]
        public ActionResult<IngredType> GetIngredTypeById(int typeId)
        {
            IngredType type = ingredientDao.GetIngredTypeById(typeId);

            if (type != null)
            {
                return Ok(type);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
