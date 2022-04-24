using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;


namespace Capstone.DAO
{
    public interface IPlannerDao
    {
        // List of all planners
        List<Planner> GetAllPlanners();

        // Retrieve specific planner by Planner Id
        Planner GetPlannerByPlannerId(int plannerId);


        // Retrieve planner(s) by logged in user
        List<Planner> GetPlannerByUserId(int userId);

        // Add/Create a meal plan
        Planner AddMealPlan(Planner plan);


        // Update/Modify a meal plan
        bool UpdateMealPlan(Planner planner);

        void DeletePlanner(int plannerId);
    }
}
