using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class PlannerSqlDao : IPlannerDao
    {
        // set up connection string 
        private readonly string connectionString;

        public PlannerSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        // Get all stored Planners from DB, store into planner model
        public List<Planner> GetAllPlanners()
        {
            List<Planner> plannerList = new List<Planner>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlText = "SELECT * FROM planner";

                    SqlCommand cmd = new SqlCommand(sqlText, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Planner planner = new Planner();
                        planner = GetPlannerFromReader(reader);
                        plannerList.Add(planner);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return plannerList;

        }

        // retrieve specific planner (by planner Id)
        public Planner GetPlannerByPlannerId(int plannerId)
        {
            Planner planner = new Planner();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlText = "SELECT * FROM planner WHERE planner_id = @planner_id";

                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@planner_id", plannerId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        planner = GetPlannerFromReader(reader);

                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return planner;
        }


        // Retrieving a planner(s) from DB by user ID, store into planner model
        public List<Planner> GetPlannerByUserId(int userId)
        {
            List<Planner> plannerList = new List<Planner>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //string sqlText = "SELECT u.user_id, username, planner_id, name, isSharable FROM planner as p JOIN users as u ON p.user_id = u.user_id WHERE p.user_id = @user_id"; 
                    // string sqlText = "SELECT r.planner_id, r.name, r.user_id, r.day, r.week, r.isSharable FROM planner as r JOIN recipes_planner as rp ON rp.planner_id = r.planner_id JOIN users as u ON r.user_id = u.user_id WHERE u.user_id = @user_id";
                    string sqlText = "SELECT * FROM planner WHERE user_id = @user_id";


                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Planner planner = new Planner();
                        planner = GetPlannerFromReader(reader);
                        plannerList.Add(planner);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return plannerList;
        }


        // Add new meal plan

        public Planner AddMealPlan(Planner plan)
        {
            int plannerId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlText = "INSERT INTO planner (name, user_id, isSharable) VALUES (@name, @user_id, @isSharable)";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);

                    cmd.Parameters.AddWithValue("@name", plan.Name);
                    cmd.Parameters.AddWithValue("@user_id", plan.UserId);
                    cmd.Parameters.AddWithValue("@isSharable", plan.IsSharable);
                    plannerId = Convert.ToInt32(cmd.ExecuteScalar());
                    plan.PlannerId = plannerId;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return plan;
        }

        // Update meal plan
        //public Planner UpdateMealPlan(Planner planner)
        public bool UpdateMealPlan(Planner planner)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlText = "UPDATE planner SET name = @name, isSharable = @isSharable WHERE planner_id = @planner_id;";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@name", planner.Name);
                    cmd.Parameters.AddWithValue("@isSharable", planner.IsSharable);
                    cmd.Parameters.AddWithValue("@planner_id", planner.PlannerId);
                    int rowsAffected = cmd.ExecuteNonQuery();


                    if (rowsAffected > 0)
                    {
                        return true; // change was successful
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (SqlException)
            {
                throw;
            }

        }

        public void DeletePlanner(int plannerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlTxt = "DELETE FROM recipes_planner WHERE planner_id = @planner_id;" +
                                "DELETE FROM planner WHERE planner_id = @planner_id;";
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                cmd.Parameters.AddWithValue("@planner_id", plannerId);

                cmd.ExecuteNonQuery();
            }
        }




        // Mapping reader
        private Planner GetPlannerFromReader(SqlDataReader reader)
        {
            Planner myPlanner = new Planner();

            myPlanner.PlannerId = Convert.ToInt32(reader["planner_id"]);
            myPlanner.Name = Convert.ToString(reader["name"]);
            /*   myPlanner.UserId = Convert.ToInt32(reader["user_id"]);*/  // causes an error when value is NULL so I commented it out
            myPlanner.IsSharable = Convert.ToBoolean(reader["isSharable"]);

            return myPlanner;
        }

    }
}
