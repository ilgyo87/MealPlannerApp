using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class UserRecipesSqlDao : IUserRecipesDao
    {
        private readonly string connectionString;

        public UserRecipesSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Recipe> GetUserRecipes(int userId)
        {
            List<Recipe> recipeList = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM recipe " +
                                                    "JOIN user_recipes ur ON ur.recipe_id = recipe.recipe_id " +
                                                    "WHERE ur.user_id = @userId; ", conn);

                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Recipe recipe = new Recipe();
                        recipe = GetRecipeFromReader(reader);
                        recipeList.Add(recipe);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return recipeList;
        }

        public UserRecipes AddRecipe(int recipeId, int userId)
        {
            UserRecipes userRecipes = new UserRecipes();

            int id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO user_recipes(user_id, recipe_id) " +
                                                    "OUTPUT INSERTED.id " +
                                                    "VALUES (@userId, @recipeId)", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);

                    id = Convert.ToInt32(cmd.ExecuteScalar());
                    userRecipes.id = id;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return userRecipes;
        }

        public void DeleteRecipe(int recipeId, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM user_recipes " +
                                                    "WHERE recipe_id = @recipeId AND user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
        }

        private Recipe GetRecipeFromReader(SqlDataReader reader)
        {
            Recipe myRecipe = new Recipe();

            myRecipe.RecipeId = Convert.ToInt32(reader["recipe_id"]);
            myRecipe.RecipeName = Convert.ToString(reader["recipe_name"]);
            myRecipe.DrinkAlternate = Convert.ToString(reader["drink_alternate"]);
            myRecipe.CategoryId = Convert.ToInt32(reader["category_id"]);
            myRecipe.AreaId = Convert.ToInt32(reader["area_id"]);
            myRecipe.Instructions = Convert.ToString(reader["instructions"]);
            myRecipe.RecipeImage = Convert.ToString(reader["recipe_image"]);
            myRecipe.RecipeTags = Convert.ToString(reader["recipe_tags"]);
            myRecipe.Youtube = Convert.ToString(reader["youtube"]);
            myRecipe.Source = Convert.ToString(reader["source"]);
            myRecipe.ImageSource = Convert.ToString(reader["image_source"]);
            myRecipe.Date = Convert.ToString(reader["date"]);
            myRecipe.UserId = Convert.ToInt32(reader["user_id"]);

            return myRecipe;
        }
    }
}
