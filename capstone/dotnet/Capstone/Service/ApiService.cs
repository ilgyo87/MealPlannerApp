using System.Collections.Generic;
using RestSharp;
using System.Net;
using System.Net.Http;
using Capstone.Service.Models;
using System.Threading;
using System;

namespace Capstone.Service
{
    public class ApiService
    {
        public static RestClient client = new RestClient("http://themealdb.com/api/json/v2/9973533/");

        public ApiService(string apiUrl) { }

        public List<Ingredient> GetIngredientList()
        {
            List<Ingredient> ingredientsList = new List<Ingredient>();
            RestRequest request = new RestRequest("list.php?i=list");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayIngredients ingredient = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayIngredients>(response.Content);
            for (int i = 0; i < ingredient.meals.Length; i++)
            {
                ingredientsList.Add(ingredient.meals[i]);
            }
            return ingredientsList;
        }

        public List<Meals> GetMealsListByLetter(char c)
        {
            List<Meals> mealsList = new List<Meals>();

            RestRequest request = new RestRequest($"search.php?f={c}");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayMeals meal = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayMeals>(response.Content);

            for (int i = 0; i < meal.meals.Length; i++)
            {
                mealsList.Add(meal.meals[i]);
            }
            return mealsList;


        }

        public List<Categories> GetCategoriesList()
        {
            List<Categories> catsList = new List<Categories>();
            RestRequest request = new RestRequest($"categories.php");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayCategories cat = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayCategories>(response.Content);
            for (int i = 0; i < cat.categories.Length; i++)
            {
                catsList.Add(cat.categories[i]);
            }
            return catsList;
        }

        public List<Areas> GetAreasList()
        {
            List<Areas> areasList = new List<Areas>();
            RestRequest request = new RestRequest($"list.php?a=list");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayAreas area = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayAreas>(response.Content);
            for (int i = 0; i < area.meals.Length; i++)
            {
                areasList.Add(area.meals[i]);
            }
            return areasList;
        }

        public List<Meals> GetMealsByName(string recipe)
        {
            List<Meals> mealsList = new List<Meals>();
            RestRequest request = new RestRequest($"search.php?s={recipe}");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayMeals meal = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayMeals>(response.Content);
            for (int i = 0; i < meal.meals.Length; i++)
            {
                mealsList.Add(meal.meals[i]);
            }
            return mealsList;
        }

        public Meals GetMealById(int idNumber)
        {
            RestRequest request = new RestRequest($"lookup.php?i={idNumber}");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayMeals meal = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayMeals>(response.Content);

            return meal.meals[0];
        }

        public Meals GetMealByRandom()
        {
            RestRequest request = new RestRequest($"random.php");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayMeals meal = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayMeals>(response.Content);

            return meal.meals[0];
        }

        public List<MealsIngredients> GetMealsByIngredients(string ingredients)
        {
            //the ingredients need to be seperated by a comma if multiple ingredients
            List<MealsIngredients> mealsList = new List<MealsIngredients>();
            RestRequest request = new RestRequest($"filter.php?i={ingredients}");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayMealsIngredients meal = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayMealsIngredients>(response.Content);
            for (int i = 0; i < meal.meals.Length; i++)
            {
                mealsList.Add(meal.meals[i]);
            }
            return mealsList;
        }

        public List<Meals> GetMealsByCategory(string category)
        {
            //the ingredients need to be seperated by a comma if multiple ingredients
            List<Meals> mealsList = new List<Meals>();
            RestRequest request = new RestRequest($"filter.php?c={category}");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayMeals meal = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayMeals>(response.Content);
            for (int i = 0; i < meal.meals.Length; i++)
            {
                mealsList.Add(meal.meals[i]);
            }
            return mealsList;
        }

        public List<Meals> GetMealsByArea(string area)
        {
            //the ingredients need to be seperated by a comma if multiple ingredients
            List<Meals> mealsList = new List<Meals>();
            RestRequest request = new RestRequest($"filter.php?a={area}");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayMeals meal = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayMeals>(response.Content);
            for (int i = 0; i < meal.meals.Length; i++)
            {
                mealsList.Add(meal.meals[i]);
            }
            return mealsList;
        }

        public List<Meals> GetMealsByNew()
        {
            //the ingredients need to be seperated by a comma if multiple ingredients
            List<Meals> mealsList = new List<Meals>();
            RestRequest request = new RestRequest($"latest.php");
            IRestResponse response = client.Get(request);
            CheckForError(response);

            if (response.IsSuccessful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error");
            }
            ArrayMeals meal = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayMeals>(response.Content);
            for (int i = 0; i < meal.meals.Length; i++)
            {
                mealsList.Add(meal.meals[i]);
            }
            return mealsList;
        }

        /// <summary>
        /// Checks RestSharp response for errors. If error, writes a log message and throws an exception 
        /// if the call was not successful. If no error, just returns to caller.
        /// </summary>
        /// <param name="response">Response returned from a RestSharp method call.</param>
        /// <param name="action">Description of the action the application was taking. Written to the log file for context.</param>
        public void CheckForError(IRestResponse response)
        {
            string message;
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                message = $"Error occurred - unable to reach server. Response status was '{response.ResponseStatus}'.";
                throw new HttpRequestException(message, response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    message = $"Authorization is required and the user has not logged in.";
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    message = $"The user does not have permission.";
                }
                else
                {
                    message = $"An http error occurred. Status code {(int)response.StatusCode} {response.StatusDescription}";
                }
                throw new HttpRequestException(message, response.ErrorException);
            }
        }

    }
}
