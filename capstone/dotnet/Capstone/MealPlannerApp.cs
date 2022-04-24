using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Service.Models;
using Capstone.Service;
using System.Data.SqlClient;

namespace Capstone
{
    public class mealPlannerApp
    {

        private string connectionString = "Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;";
        private readonly ApiService apiService;

        public mealPlannerApp(string apiUrl)
        {
            apiService = new ApiService(apiUrl);
        }

        public void Run()
        {
            InputAreasIntoDatabase();
            InputCategoriesIntoDatabase();
            InputIngredientsIntoDatabase();
            InputRecipesIntoDatabase();
            InputIngredientsFromRecipesIntoDatabase();
        }

        private void InputIngredientsIntoDatabase()
        {
            int ingredId = 0;
            try
            {
                List<Ingredient> ingreds = apiService.GetIngredientList();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (Ingredient ingred in ingreds)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO ingredient (name, description, type_id) " +
                                                    "OUTPUT INSERTED.ingred_id " +
                                                    "VALUES (@name, @description, (SELECT type_id FROM ingred_type WHERE name = @type_id));", conn);
                        cmd.Parameters.AddWithValue("@name", ingred.strIngredient);
                        if (ingred.strDescription == null)
                        {
                            cmd.Parameters.AddWithValue("@description", "NULL");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@description", ingred.strDescription);
                        }

                        if (ingred.strType == null)
                        {
                            cmd.Parameters.AddWithValue("@type_id", "NULL");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@type_id", ingred.strType);
                        }

                        ingredId = Convert.ToInt32(cmd.ExecuteScalar());
                        ingred.idIngredient = ingredId.ToString();
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Successful Input");
        }


        private void InputCategoriesIntoDatabase()
        {
            int catId = 0;
            List<Categories> cats = apiService.GetCategoriesList();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (Categories cat in cats)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO category (name, category_image, category_description) " +
                                                    "OUTPUT INSERTED.category_id " +
                                                    "VALUES (@name, @category_image, @category_description);", conn);
                        cmd.Parameters.AddWithValue("@name", cat.strCategory);
                        cmd.Parameters.AddWithValue("@category_image", cat.strCategoryThumb);
                        cmd.Parameters.AddWithValue("@category_description", cat.strCategoryDescription);

                        catId = Convert.ToInt32(cmd.ExecuteScalar());
                        cat.idCategory = catId.ToString();
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Successful Input");
        }

        private void InputAreasIntoDatabase()
        {
            int areaId = 0;
            try
            {
                List<Areas> areas = apiService.GetAreasList();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (Areas area in areas)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO area (name) " +
                                                    "OUTPUT INSERTED.area_id " +
                                                    "VALUES (@name);", conn);
                        cmd.Parameters.AddWithValue("@name", area.strArea);


                        areaId = Convert.ToInt32(cmd.ExecuteScalar());
                        area.idArea = areaId.ToString();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Successful Input");
        }

        private void InputRecipesIntoDatabase()
        {
            int recipeId = 0;
            try
            {
                //took out q,u,x, z
                string alphabet = "abcdefghijklmnoprstvwy";

                foreach (char c in alphabet)
                {
                    List<Meals> recipes = apiService.GetMealsListByLetter(c);

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        foreach (Meals recipe in recipes)
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO recipe (recipe_name, drink_alternate, category_id, area_id, instructions, recipe_image, recipe_tags, youtube, source, image_source, date, user_id) " +
                                                        "OUTPUT INSERTED.recipe_id " +
                                                        "VALUES (@recipe_name, @drink_alternate, (SELECT category_id FROM category WHERE name = @category_id), (SELECT area_id FROM area WHERE name = @area_id), @instructions, @recipe_image, @recipe_tags, @youtube, @source, @image_source, @date, @userId);", conn);
                            cmd.Parameters.AddWithValue("@recipe_name", recipe.strMeal);
                            if (recipe.strDrinkAlternate == null)
                            {
                                cmd.Parameters.AddWithValue("@drink_alternate", "NULL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@drink_alternate", recipe.strDrinkAlternate);
                            }
                            if (recipe.strCategory == null)
                            {
                                cmd.Parameters.AddWithValue("@category_id", "NULL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@category_id", recipe.strCategory);
                            }
                            if (recipe.strArea == null)
                            {
                                cmd.Parameters.AddWithValue("@area_id", "NULL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@area_id", recipe.strArea);
                            }
                            if (recipe.strInstructions == null)
                            {
                                cmd.Parameters.AddWithValue("@instructions", "NULL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@instructions", recipe.strInstructions);
                            }
                            if (recipe.strMealThumb == null)
                            {
                                cmd.Parameters.AddWithValue("@recipe_image", "NULL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@recipe_image", recipe.strMealThumb);
                            }
                            if (recipe.strTags == null)
                            {
                                cmd.Parameters.AddWithValue("@recipe_tags", "NULL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@recipe_tags", recipe.strTags);
                            }
                            if (recipe.strYoutube == null)
                            {
                                cmd.Parameters.AddWithValue("@youtube", "NULL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@youtube", recipe.strYoutube);
                            }
                            if (recipe.strSource == null)
                            {
                                cmd.Parameters.AddWithValue("@source", "NULL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@source", recipe.strSource);
                            }
                            if (recipe.strImageSource == null)
                            {
                                cmd.Parameters.AddWithValue("@image_source", "NULL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@image_source", recipe.strImageSource);
                            }
                            if (recipe.dateModified == null)
                            {
                                cmd.Parameters.AddWithValue("@date", "NULL");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@date", recipe.dateModified);
                            }
                            cmd.Parameters.AddWithValue("@userId", 1);


                            recipeId = Convert.ToInt32(cmd.ExecuteScalar());
                            recipe.idMeal = recipeId.ToString();
                        }

                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Successful Input");
        }

        private void InputIngredientsFromRecipesIntoDatabase()
        {
            try
            {
                //took out q,u,x, z
                string alphabet = "abcdefghijklmnoprstvwy";

                foreach (char c in alphabet)
                {
                    List<Meals> recipes = apiService.GetMealsListByLetter(c);

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        foreach (Meals recipe in recipes)
                        {
                            List<string> names = new List<string>();
                            List<string> measures = new List<string>();
                            List<string> nameMeasures = new List<string>();

                            names.Add(recipe.strIngredient1); names.Add(recipe.strIngredient2); names.Add(recipe.strIngredient3); names.Add(recipe.strIngredient4);
                            names.Add(recipe.strIngredient5); names.Add(recipe.strIngredient6); names.Add(recipe.strIngredient7); names.Add(recipe.strIngredient8);
                            names.Add(recipe.strIngredient9); names.Add(recipe.strIngredient10); names.Add(recipe.strIngredient11); names.Add(recipe.strIngredient12);
                            names.Add(recipe.strIngredient13); names.Add(recipe.strIngredient14); names.Add(recipe.strIngredient15); names.Add(recipe.strIngredient16);
                            names.Add(recipe.strIngredient17); names.Add(recipe.strIngredient18); names.Add(recipe.strIngredient19); names.Add(recipe.strIngredient20);

                            measures.Add(recipe.strMeasure1); measures.Add(recipe.strMeasure2); measures.Add(recipe.strMeasure3); measures.Add(recipe.strMeasure4);
                            measures.Add(recipe.strMeasure5); measures.Add(recipe.strMeasure6); measures.Add(recipe.strMeasure7); measures.Add(recipe.strMeasure8);
                            measures.Add(recipe.strMeasure9); measures.Add(recipe.strMeasure10); measures.Add(recipe.strMeasure11); measures.Add(recipe.strMeasure12);
                            measures.Add(recipe.strMeasure13); measures.Add(recipe.strMeasure14); measures.Add(recipe.strMeasure15); measures.Add(recipe.strMeasure16);
                            measures.Add(recipe.strMeasure17); measures.Add(recipe.strMeasure18); measures.Add(recipe.strMeasure19); measures.Add(recipe.strMeasure20);

                            for (int i = 0; i < names.Count; i++)
                            {
                                nameMeasures.Add(names[i] + " " + measures[i]);
                            }


                            Console.WriteLine("ingredient name count: " + names.Count);
                            Console.WriteLine("ingredient measure count: " + measures.Count);

                            for (int i = 0; i < names.Count; i++)
                            {
                                if ((names[i] == "" || names[i] == "null" || names[i] == null) && (measures[i] == "" || measures[i] == " " || measures[i] == "null" || measures[i] == null))
                                {
                                    break;
                                }
                                SqlCommand cmd = new SqlCommand("INSERT INTO recipes_ingredients (name, recipe_id, ingred_id, measure) " +
                                                        "OUTPUT INSERTED.ri_id " +
                                                        "VALUES (@name, (SELECT recipe_id FROM recipe WHERE recipe_name = @recipe_id), (SELECT ingred_id FROM ingredient WHERE name = @ingred_id), @measure);", conn);
                                cmd.Parameters.AddWithValue("@name", nameMeasures[i]);
                                cmd.Parameters.AddWithValue("@recipe_id", recipe.strMeal);
                                cmd.Parameters.AddWithValue("@ingred_id", names[i]);
                                if (measures[i] == "")
                                {
                                    cmd.Parameters.AddWithValue("@measure", "NULL");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@measure", measures[i]);
                                }
                                Convert.ToInt32(cmd.ExecuteScalar());

                                Console.WriteLine($"{recipe.strMeal} {names[i]} {measures[i]} added");
                            }
                        }

                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Successful Input");
        }

        private void IngredientList()
        {

            List<Ingredient> ingreds = apiService.GetIngredientList();
            if (ingreds != null)
            {
                Console.WriteLine("\n*********** Ingredients ***********");
                foreach (Ingredient ingred in ingreds)
                {
                    Console.WriteLine($"Id: {ingred.idIngredient} | Description: {ingred.strDescription} | Name: {ingred.strIngredient} | Type: {ingred.strType}");
                }
                Console.WriteLine("****************************");
                Console.WriteLine("");

            }
        }

        private void GetIngredientList()
        {

            List<Meals> ingreds = apiService.GetMealsListByLetter('a');
            List<string> names = new List<string>();
            List<string> measures = new List<string>();
            if (ingreds != null)
            {
                Console.WriteLine("\n*********** Ingredients ***********");
                foreach (Meals recipe in ingreds)
                {
                    Console.WriteLine(recipe.strMeal);
                    names.Add(recipe.strIngredient1); names.Add(recipe.strIngredient2); names.Add(recipe.strIngredient3); names.Add(recipe.strIngredient4);
                    names.Add(recipe.strIngredient5); names.Add(recipe.strIngredient6); names.Add(recipe.strIngredient7); names.Add(recipe.strIngredient8);
                    names.Add(recipe.strIngredient9); names.Add(recipe.strIngredient10); names.Add(recipe.strIngredient11); names.Add(recipe.strIngredient12);
                    names.Add(recipe.strIngredient13); names.Add(recipe.strIngredient14); names.Add(recipe.strIngredient15); names.Add(recipe.strIngredient16);
                    names.Add(recipe.strIngredient17); names.Add(recipe.strIngredient18); names.Add(recipe.strIngredient19); names.Add(recipe.strIngredient20);

                    measures.Add(recipe.strMeasure1); measures.Add(recipe.strMeasure2); measures.Add(recipe.strMeasure3); measures.Add(recipe.strMeasure4);
                    measures.Add(recipe.strMeasure5); measures.Add(recipe.strMeasure6); measures.Add(recipe.strMeasure7); measures.Add(recipe.strMeasure8);
                    measures.Add(recipe.strMeasure9); measures.Add(recipe.strMeasure10); measures.Add(recipe.strMeasure11); measures.Add(recipe.strMeasure12);
                    measures.Add(recipe.strMeasure13); measures.Add(recipe.strMeasure14); measures.Add(recipe.strMeasure15); measures.Add(recipe.strMeasure16);
                    measures.Add(recipe.strMeasure17); measures.Add(recipe.strMeasure18); measures.Add(recipe.strMeasure19); measures.Add(recipe.strMeasure20);
                    for (int i = 0; i < 20; i++)
                    {
                        if (names[i] == "" && measures[i] == "")
                        {
                            break;
                        }
                        Console.WriteLine($"{recipe.strMeal} {names[i]} {measures[i]}");
                    }
                }
            }
        }

        private void AreaList()
        {

            List<Areas> ingreds = apiService.GetAreasList();
            if (ingreds != null)
            {
                Console.WriteLine("\n*********** Area***********");
                foreach (Areas ingred in ingreds)
                {
                    Console.WriteLine($"NAME: {ingred.strArea}");
                }
                Console.WriteLine("****************************");
                Console.WriteLine("");

            }
        }

    }
}