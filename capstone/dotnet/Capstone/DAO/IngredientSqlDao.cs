using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class IngredientSqlDao : IIngredientDao
    {
        private readonly string connectionString;

        public IngredientSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Ingredient> GetAllIngredients()
        {
            List<Ingredient> ingredList = new List<Ingredient>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM ingredient;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Ingredient ingred = new Ingredient();
                        ingred = GetIngredientFromReader(reader);
                        ingredList.Add(ingred);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return ingredList;
        }
        public Ingredient GetIngredientByIngredId(int ingredId)
        {
            Ingredient ingred = new Ingredient();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ingredient WHERE ingred_id = @ingred_id;", conn);
                    cmd.Parameters.AddWithValue("@ingred_id", ingredId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ingred = GetIngredientFromReader(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return ingred;
        }
        public List<Ingredient> GetIngredientsByTypeId(int ingredTypeId)
        {
            List<Ingredient> ingredList = new List<Ingredient>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ingredient WHERE type_id = @type_id;", conn);
                    cmd.Parameters.AddWithValue("@type_id", ingredTypeId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Ingredient ingred = new Ingredient();
                        ingred = GetIngredientFromReader(reader);
                        ingredList.Add(ingred);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return ingredList;
        }
        public List<Recipe> GetRecipesByIngredient(string term)
        {
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM recipe r " +
                                                    "JOIN recipes_ingredients ri ON r.recipe_id = ri.recipe_id " +
                                                    "WHERE ingred_id IN(SELECT ingred_id FROM ingredient WHERE name LIKE @term); ", conn);
                    cmd.Parameters.AddWithValue("@term", "%" + term + "%");
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Recipe recipe = new Recipe();
                        recipe = GetRecipeFromReader(reader);
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
        public Ingredient AddIngredient(Ingredient ingred)
        {
            int ingredId = 0;
            ingred.TypeId = ingred.TypeId == 0 ? 1 : ingred.TypeId;
            ingred.IngredImage = ingred.IngredImage == null ? "" : ingred.IngredImage;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO ingredient (name, description, type_id, ingred_image) " +
                                                    "OUTPUT INSERTED.ingred_id " +
                                                    "VALUES (@name, @description, @type_id, @ingred_image);", conn);
                    cmd.Parameters.AddWithValue("@name", ingred.Name);
                    cmd.Parameters.AddWithValue("@description", ingred.Description);
                    cmd.Parameters.AddWithValue("@type_id", ingred.TypeId);
                    cmd.Parameters.AddWithValue("@ingred_image", ingred.IngredImage);
                    ingredId = Convert.ToInt32(cmd.ExecuteScalar());
                    ingred.IngredId = ingredId;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return ingred;
        }

        public Ingredient UpdateIngredient(Ingredient ingred)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sqlText = "UPDATE ingredient SET name = @name, description = @description, type_id = @type_id, ingred_image = @ingred_image " +
                                     "FROM ingredient WHERE ingred_id = @ingred_id;";
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@name", ingred.Name);
                    cmd.Parameters.AddWithValue("@description", ingred.Description);
                    cmd.Parameters.AddWithValue("@type_id", ingred.TypeId);
                    cmd.Parameters.AddWithValue("@ingred_image", ingred.IngredImage);
                    cmd.Parameters.AddWithValue("@ingred_id", ingred.IngredId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return ingred;
        }
        public RecipesIngredients AddIngredientToRecipe(RecipesIngredients ri)
        {
            int riId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO recipes_ingredients (name, recipe_id, ingred_id, measure) " +
                                                    "OUTPUT INSERTED.ri_id " +
                                                    "VALUES (@name, @recipe_id, @ingred_id, @measure);", conn);
                    cmd.Parameters.AddWithValue("@name", ri.Name);
                    cmd.Parameters.AddWithValue("@recipe_id", ri.RecipeId);
                    cmd.Parameters.AddWithValue("@ingred_id", ri.IngredId);
                    cmd.Parameters.AddWithValue("@measure", ri.Measure);
                    riId = Convert.ToInt32(cmd.ExecuteScalar());
                    ri.riRecipeId = riId;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return ri;
        }

        public RecipesIngredients UpdateIngredientToRecipe(RecipesIngredients ri)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE recipes_ingredients SET name = @name, recipe_id = @recipe_id, ingred_id = @ingred_id, measure = @measure " +
                                                    "FROM recipes_ingredients WHERE ri_id = @ri_id;", conn);
                    cmd.Parameters.AddWithValue("@name", ri.Name);
                    cmd.Parameters.AddWithValue("@recipe_id", ri.RecipeId);
                    cmd.Parameters.AddWithValue("@ingred_id", ri.IngredId);
                    cmd.Parameters.AddWithValue("@measure", ri.Measure);
                    cmd.Parameters.AddWithValue("@ri_id", ri.riRecipeId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return ri;
        }

        public List<IngredType> GetAllIngredType()
        {
            List<IngredType> types = new List<IngredType>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM ingred_type;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        IngredType type = new IngredType();
                        type = GetIngredTypeFromReader(reader);
                        types.Add(type);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return types;
        }

        public IngredType GetIngredTypeById(int ingredTypeId)
        {
            IngredType type = new IngredType();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ingred_type WHERE type_id = @type_id;", conn);
                    cmd.Parameters.AddWithValue("@type_id", ingredTypeId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        type = GetIngredTypeFromReader(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return type;
        }

        public List<RecipesIngredients> GetAllRecipesIngredientsByRecipeId(int recipeId)
        {
            List<RecipesIngredients> riList= new List<RecipesIngredients>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM recipes_ingredients WHERE recipe_id = @recipe_id;", conn);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        RecipesIngredients ri = new RecipesIngredients();
                        ri = GetRecipesIngredientsFromReader(reader);
                        riList.Add(ri);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return riList;
        }

        private Ingredient GetIngredientFromReader(SqlDataReader reader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.IngredId = Convert.ToInt32(reader["ingred_id"]);
            ingredient.Name = Convert.ToString(reader["name"]);
            ingredient.Description = Convert.ToString(reader["description"]);
            ingredient.TypeId = Convert.ToString(reader["type_id"]) == ""? 0: Convert.ToInt32(reader["type_id"]);
            ingredient.IngredImage = Convert.ToString(reader["ingred_image"]);
            return ingredient;
        }

        private IngredType GetIngredTypeFromReader(SqlDataReader reader)
        {
            IngredType type = new IngredType();
            type.TypeId = Convert.ToInt32(reader["type_id"]);
            type.Name = Convert.ToString(reader["name"]);
            type.isFresh = Convert.ToBoolean(reader["isFresh"]);
            return type;
        }

        private RecipesIngredients GetRecipesIngredientsFromReader(SqlDataReader reader)
        {
            RecipesIngredients ri = new RecipesIngredients();
            ri.riRecipeId = Convert.ToInt32(reader["ri_id"]);
            ri.Name = Convert.ToString(reader["name"]);
            ri.RecipeId = Convert.ToInt32(reader["recipe_id"]);
            ri.IngredId = Convert.ToInt32(reader["ingred_id"]);
            ri.Measure = Convert.ToString(reader["measure"]);
            return ri;
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
