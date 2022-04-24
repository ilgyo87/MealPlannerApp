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
    public class PlannerController : ControllerBase
    {
        private readonly IPlannerDao plannerDao;

        public PlannerController(IPlannerDao plannerDao)
        {
            this.plannerDao = plannerDao;
        }


        // handle requests to see all planners
        [HttpGet()]

        public ActionResult<List<Planner>> GetAllPlanners()
        {
            List<Planner> planners = plannerDao.GetAllPlanners();

            if (planners != null)
            {
                return Ok(planners);
            }
            else
            {
                return BadRequest();
            }
        }

        //handle requests to see a specific planner(by planner Id)
        [HttpGet("{plannerId}")]
        public ActionResult<Planner> GetPlannerById(int plannerId)
        {
            Planner planner = plannerDao.GetPlannerByPlannerId(plannerId);
            if (planner != null)
            {
                return Ok(planner);
            }
            else
            {
                return BadRequest();
            }
        }


        // handle requests to see planner(s) based on user id
        [HttpGet("userId={userId}")]
        public ActionResult<List<Planner>> GetPlannerByUserId(int userId)
        {
            List<Planner> planner = plannerDao.GetPlannerByUserId(userId);
            if (planner != null)
            {
                return Ok(planner);
            }
            else
            {
                return BadRequest();
            }
        }


        // handle requests to create a new meal plan
        [HttpPost("post")]
        public ActionResult AddMealPlan(Planner userParam)
        {
            if (userParam != null)
            {
                Planner newPlanner = plannerDao.AddMealPlan(userParam);
                return Created($"/planner/{newPlanner.PlannerId}", newPlanner);
            }
            else
            {
                return BadRequest();
            }

        }

        // handle requests to update a single meal plan
        [HttpPut("update")]

        public ActionResult<Planner> UpdateMealPlan(Planner planner)
        {

            //if (plannerDao.GetPlannerByPlannerId(plannerId) != null)
            //{
            //    if (planner.PlannerId == 0)
            //    {
            //        planner.PlannerId = plannerId;
            //    }
            //    return Ok(plannerDao.UpdateMealPlan(planner));
            //}
            //return NotFound();

            bool result = plannerDao.UpdateMealPlan(planner);

            if (result == true)
            {
                Planner updatedMealPlan = plannerDao.GetPlannerByPlannerId(planner.PlannerId);
                return Ok(updatedMealPlan);
            }
            return NotFound();
        }

        [HttpDelete("{plannerId}")]
        public void DeletePlanner(int plannerId)
        {
            plannerDao.DeletePlanner(plannerId);
        }
    }
}
