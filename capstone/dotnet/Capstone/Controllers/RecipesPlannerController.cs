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

    public class RecipesPlannerController : ControllerBase
    {
        private readonly IRecipesPlannerDao rpDao;

        public RecipesPlannerController(IRecipesPlannerDao rpDao)
        {
            this.rpDao = rpDao;
        }

        [HttpGet()]
        public ActionResult<List<RecipesPlanner>> GetAllRps()
        {
            List<RecipesPlanner> recipes = rpDao.GetAllRps();
            if (recipes != null)
            {
                return Ok(recipes);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("plan={plannerId}")]
        public ActionResult<List<RecipesPlanner>> GetRpById(int plannerId)
        {
            List<RecipesPlanner> recipes = rpDao.GetRecipesByPlanner(plannerId);
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
        public ActionResult<RecipesPlanner> AddRp(RecipesPlanner recipe)
        {
            if (recipe != null)
            {
                //recipe.UserId = Int32.Parse(User.FindFirst("sub")?.Value);
                RecipesPlanner newRecipe = rpDao.AddRecipesPlanner(recipe);

                return Created($"/recipesplanner/{newRecipe.rpId}", newRecipe);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public ActionResult<RecipesPlanner> UpdateRp(RecipesPlanner recipe)
        {
            if (recipe != null)
            {
                RecipesPlanner updatedRecipe = rpDao.UpdateRecipesPlanner(recipe);
                return Created($"/recipesplanner/{updatedRecipe.rpId}", updatedRecipe);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{plannerId}")]
        public void DeletePlanner(int plannerId)
        {
            rpDao.DeleteRecipesPlanner(plannerId);
        }
    }
}
