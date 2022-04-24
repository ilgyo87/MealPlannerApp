using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class RecipeSqlDao : IRecipeDao
    {
        private readonly string connectionString;

        public RecipeSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Category GetCategoryById(int catId)
        {
            Category cat = new Category();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM category WHERE category_id = @catId;", conn);
                    cmd.Parameters.AddWithValue("@catId", catId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cat = GetCategoryFromReader(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return cat;
        }

        public Area GetAreaById(int areaId)
        {
            Area area = new Area();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM area WHERE area_id = @areaId;", conn);
                    cmd.Parameters.AddWithValue("@areaId", areaId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        area = GetAreaFromReader(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return area;
        }

        public Recipe GetRecipeById(int recipeId)
        {
            Recipe recipe = new Recipe();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT recipe_id, recipe_name, drink_alternate, instructions, recipe_image, recipe_tags, youtube, source, image_source, date, area_id, user_id, category_id FROM recipe WHERE recipe_id = @recipe_id;", conn);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);

                    SqlDataReader reader= cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        recipe = GetRecipeFromReader(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return recipe;
        }

        public List<Recipe> GetAllRecipes()
        {
            List<Recipe> recipeList = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM recipe;", conn);

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

        public List<Recipe> GetRecipeListByLetter(char charLetter)
        {
            List<Recipe> recipeList = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM recipe " +
                                                    "WHERE recipe_name LIKE @charLetter;", conn);
                    cmd.Parameters.AddWithValue("@charLetter", charLetter+"%");
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
                Console.WriteLine(e); ;
            }
            return recipeList;
        }

        public List<Recipe> GetRecipeListBySearchterm(string term)
        {
            List<Recipe> recipeList = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM recipe " +
                                                    "WHERE recipe_name LIKE @term;", conn);
                    cmd.Parameters.AddWithValue("@term", "%" + term + "%");
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
                Console.WriteLine(e); ;
            }
            return recipeList;
        }

        public List<Recipe> GetRecipeListByCategoryId(int catId)
        {
            List<Recipe> recipeList = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM recipe " +
                                                    "WHERE category_id = @category_id;", conn);
                    cmd.Parameters.AddWithValue("@category_id", catId);
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
                Console.WriteLine(e); ;
            }
            return recipeList;
        }

        public List<Recipe> GetRecipeListByAreaId(int areaId)
        {
            List<Recipe> recipeList = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM recipe " +
                                                    "WHERE area_id = @area_id;", conn);
                    cmd.Parameters.AddWithValue("@area_id", areaId);
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
                Console.WriteLine(e); ;
            }
            return recipeList;
        }

        public Recipe AddRecipe(Recipe recipe)
        {
            int recipeId = 0;
            try
            {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                            SqlCommand cmd = new SqlCommand("INSERT INTO recipe (recipe_name, drink_alternate, category_id, area_id, instructions, recipe_image, recipe_tags, youtube, source, image_source, date, user_id) " +
                                                        "OUTPUT INSERTED.recipe_id " +
                                                        "VALUES (@recipe_name, @drink_alternate, @category_id, @area_id, @instructions, @recipe_image, @recipe_tags, @youtube, @source, @image_source, @date, @userId);", conn);
                            cmd.Parameters.AddWithValue("@recipe_name", recipe.RecipeName);                             
                            cmd.Parameters.AddWithValue("@drink_alternate", recipe.DrinkAlternate);
                            cmd.Parameters.AddWithValue("@category_id", recipe.CategoryId);
                            cmd.Parameters.AddWithValue("@area_id", recipe.AreaId);
                            cmd.Parameters.AddWithValue("@instructions", recipe.Instructions);
                            cmd.Parameters.AddWithValue("@recipe_image", recipe.RecipeImage);
                            cmd.Parameters.AddWithValue("@recipe_tags", recipe.RecipeTags);
                            cmd.Parameters.AddWithValue("@youtube", recipe.Youtube);
                            cmd.Parameters.AddWithValue("@source", recipe.Source);
                            cmd.Parameters.AddWithValue("@image_source", recipe.ImageSource);
                            cmd.Parameters.AddWithValue("@date", recipe.Date);
                            cmd.Parameters.AddWithValue("@userId", recipe.UserId);

                            recipeId = Convert.ToInt32(cmd.ExecuteScalar());
                            recipe.RecipeId = recipeId;
                    }
                }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return recipe;
        }

        public Recipe UpdateRecipe(Recipe recipe)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sqlText = "UPDATE recipe SET recipe_name = @recipe_name, drink_alternate = @drink_alternate, category_id = @category_id, area_id = @area_id, instructions = @instructions, recipe_image = @recipe_image, recipe_tags = @recipe_tags, youtube = @youtube, source = @source, image_source = @image_source, date = @date " +
                                     "FROM recipe WHERE recipe_id = @recipe_id;";
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@recipe_name", recipe.RecipeName);
                    cmd.Parameters.AddWithValue("@drink_alternate", recipe.DrinkAlternate);
                    cmd.Parameters.AddWithValue("@category_id", recipe.CategoryId);
                    cmd.Parameters.AddWithValue("@area_id", recipe.AreaId);
                    cmd.Parameters.AddWithValue("@instructions", recipe.Instructions);
                    cmd.Parameters.AddWithValue("@recipe_image", recipe.RecipeImage);
                    cmd.Parameters.AddWithValue("@recipe_tags", recipe.RecipeTags);
                    cmd.Parameters.AddWithValue("@youtube", recipe.Youtube);
                    cmd.Parameters.AddWithValue("@source", recipe.Source);
                    cmd.Parameters.AddWithValue("@image_source", recipe.ImageSource);
                    cmd.Parameters.AddWithValue("@date", recipe.Date);
                    cmd.Parameters.AddWithValue("@recipe_id", recipe.RecipeId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return recipe;
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
        private Category GetCategoryFromReader(SqlDataReader reader)
        {
            Category cat = new Category();

            cat.CategoryId = Convert.ToInt32(reader["category_id"]);
            cat.Name = Convert.ToString(reader["name"]);
            cat.CategoryImage = Convert.ToString(reader["category_image"]);
            cat.CategoryDescription = Convert.ToString(reader["category_description"]);

            return cat;
        }
        private Area GetAreaFromReader(SqlDataReader reader)
        {
            Area area = new Area();

            area.AreaId = Convert.ToInt32(reader["area_id"]);
            area.Name = Convert.ToString(reader["name"]);

            return area;
        }

    }
}


