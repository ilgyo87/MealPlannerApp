using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class RecipesPlannerSqlDao : IRecipesPlannerDao
    {
        private readonly string connectionString;

        public RecipesPlannerSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<RecipesPlanner> GetAllRps()
        {
            List<RecipesPlanner> recipes = new List<RecipesPlanner>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM recipes_planner;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        RecipesPlanner recipe = new RecipesPlanner();
                        recipe = GetRecipesPlannerFromReader(reader);
                        recipes.Add(recipe);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e); ;
            }
            return recipes;
        }
        public List<RecipesPlanner> GetRecipesByPlanner(int plannerId)
        {
            List<RecipesPlanner> recipes = new List<RecipesPlanner>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM recipes_planner " +
                                                    "WHERE planner_id = @planner_id; ", conn);
                    cmd.Parameters.AddWithValue("@planner_id", plannerId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        RecipesPlanner recipe = new RecipesPlanner();
                        recipe = GetRecipesPlannerFromReader(reader);
                        recipes.Add(recipe);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e); ;
            }
            return recipes;
        }

        public RecipesPlanner AddRecipesPlanner(RecipesPlanner plan)
        {
            int plannerId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlText = "INSERT INTO recipes_planner (planner_id, recipe_id, day, week) VALUES (@planner_id, @recipe_id, @day, @week)";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);

                    cmd.Parameters.AddWithValue("@planner_id", plan.PlannerId);
                    cmd.Parameters.AddWithValue("@recipe_id", plan.RecipeId);
                    cmd.Parameters.AddWithValue("@day", plan.Day);
                    cmd.Parameters.AddWithValue("@week", plan.Week);
                    plannerId = Convert.ToInt32(cmd.ExecuteScalar());
                    plan.rpId = plannerId;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return plan;
        }

        public RecipesPlanner UpdateRecipesPlanner(RecipesPlanner planner)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "UPDATE recipes_planner SET planner_id = @planner_id, recipe_id = @recipe_id, day = @day, week = @week WHERE rp_id = @rp_id;";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@planner_id", planner.PlannerId);
                    cmd.Parameters.AddWithValue("@recipe_id", planner.RecipeId);
                    cmd.Parameters.AddWithValue("@day", planner.Day);
                    cmd.Parameters.AddWithValue("@week", planner.Week);
                    cmd.Parameters.AddWithValue("@rp_id", planner.rpId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return planner;
        }

        public void DeleteRecipesPlanner(int plannerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlTxt = "DELETE FROM recipes_planner WHERE rp_id = @rp_id;";                        
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                cmd.Parameters.AddWithValue("@rp_id", plannerId);

                cmd.ExecuteNonQuery();
            }
        }


        private RecipesPlanner GetRecipesPlannerFromReader(SqlDataReader reader)
        {
            RecipesPlanner myPlanner = new RecipesPlanner();
            myPlanner.rpId = Convert.ToInt32(reader["rp_id"]);
            myPlanner.PlannerId = Convert.ToInt32(reader["planner_id"]);
            myPlanner.RecipeId = Convert.ToInt32(reader["recipe_id"]);
            myPlanner.Day = Convert.ToString(reader["day"]);
            myPlanner.Week = Convert.ToInt32(reader["week"]);

            return myPlanner;
        }
    }
}
