<template>
  <div class="all-recipes">
    <input  class="search-recipes" type="text" v-model="searchBar" placeholder="... search recipes">
    <label for="by">Search by:</label>
    <select class="btn-search">
        <option  v-for="index in by" :key="index">{{index}}</option>
    </select>
    <div class="container">
        <div
        class="cards"
        v-for="recipe in filteredRecipes"
        :key="recipe.recipeId"
        >
            <router-link :to="{ name: 'recipes', params: { id: recipe.recipeId } }">
                <div class="card">
                    <img :src="recipe.recipeImage" style="width:100%">
                    <h2 class="recipe-name">{{recipe.recipeName}}</h2>       
                </div>
            </router-link>
        </div>
    </div>
  </div>
</template>
<script>
import recipesService from "@/services/RecipesService.js";
export default {
    name: "recipe-list",
    data(){
        return{
            recipes: [],
            searchBar:'',
            by: ["Category","Area","Ingredients"]
        };
    },
    created() {
        recipesService.getList().then(response =>{
            this.recipes = response.data;
        })
    },
    computed: {
        filteredRecipes(){
            return this.recipes.filter((recipe) => {
                return recipe.recipeName.toLowerCase().match(this.searchBar);
            });
        }
    }
}
</script>
<style scoped>
.container{
  display:grid;
  grid-template-columns: repeat(auto-fit, minmax(200px,1fr));
  grid-gap: 1rem;
}
.column{
    float: left;
    width: 25%;
    padding: 0 1rem;
}
.card {
    box-shadow: 0 4px 8px 0 rgba(0,0,0,.2);
    max-width: 300px;
    margin: auto;
    text-align: center;
}
.recipe-name {
  font-family: 'Montserrat', sans-serif;
  font-size: 1.25rem;
  font-weight: 400;
  line-height: 1.5;
  background-color: #f5c177;
  color: rgba(38, 47, 53, 1);
  padding: 0.25rem;
}
.recipe-name:hover {
  background-color: rgba(191, 163, 138, 1);
  color: white;
}
a:link {
  text-decoration: none;
}
.btn-search {
    padding: 1rem 0;
    margin-top: 0.75rem;
    margin-bottom: 1rem;
}   
</style>