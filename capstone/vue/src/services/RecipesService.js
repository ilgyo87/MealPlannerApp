import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315"
});

export default {
    getList() {
        return http.get('/recipe');
    },
    getAreaById(areaId){
        return http.get(`/area/${areaId}`);
    },
    getCategoryById(categoryId){
        return http.get(`/category/${categoryId}`);
    },
    getRecipeById(recipeId){
        return http.get(`/recipe/${recipeId}`);
    },
    GetRecipesByCategoriesId(catId){
        return http.get(`/recipe/c=${catId}`);
    },
    GetRecipesByAreasId(areaId){
        return http.get(`/recipe/a=${areaId}`);
    },
    GetAllRecipesIngredientsByRecipeId(recipeId){
        return http.get(`/ri/${recipeId}`);
    },
    GetAllMyRecipes(userId){
        return http.get(`/planner/userId=${userId}`)
    },
    updateRecipe(recipe){
        return http.put('/recipe/update', recipe)
    },
    addRecipe(recipe) {
        return http.post(`/recipe/post`, recipe);
      },
    getAllIngredients() {
        return http.get('/ingredient');
    },
    getPlannerByUserId(id) {
        return http.get(`/planner/userId=${id}`);
    },
    saveRecipeToMyRecipes(recipe) {
        return http.post(`/userrecipes/post`, recipe);
    },
    getMyRecipesByUser() {
        return http.get('/userrecipes');
    },
    deleteFromMyRecipes(recipeId) {
        alert("Recipe deleted");
        return http.delete(`/userrecipes/${recipeId}=delete`);
    },
    getIngredientById(ingredId){
        return http.get(`/ingredient/${ingredId}`);
    },
    getIngredientsByTypeId(typeId){
        return http.get(`/ingredient/t=${typeId}`)
    },
    addIngredient(ingred){
        return http.post(`/ingredient/post`, ingred);
    },
    updateIngredient(ingred){
        return http.push('/recipe/update', ingred)
    },
    addIngredientToRecipe(ri){
        return http.post(`/ri/post`, ri);
    },
    updateIngredientToRecipe(ri){
        return http.push('/ri/update', ri);
    },
    addPlanner(planner){
        return http.post('/planner/post', planner);
    },
    deletePlanner(plannerId){
        return http.delete(`/planner/${plannerId}`);
    },
    getPlannerById(plannerId){
        return http.get(`/planner/${plannerId}`)
    },
    updatePlanner(planner){
        return http.put(`/planner/update`, planner)
    },
    getRpByPlannerId(plannerId){
        return http.get(`/recipesplanner/plan=${plannerId}`)
    },
    addRp(rp){
        return http.post(`/recipesplanner/post`, rp)
    },
    updateRp(rp){
        return http.put(`/recipesplanner/update`, rp)
    },
    deleteRp(plannerId){
        return http.delete(`/recipesplanner/${plannerId}`);
    },
    getAllRps(){
        return http.get('/recipesplanner')
    },
    getUserId(){
        return http.get('/userrecipes/user')
    },
}