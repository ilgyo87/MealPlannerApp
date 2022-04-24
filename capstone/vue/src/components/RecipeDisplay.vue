<template>
  <div class="all-recipes">
    <div
      class="recipe-container"
      v-for="(recipe, index) in this.$store.state.myRecipes"
      v-bind:key="recipe.id"
      v-on:click="setActiveRow(index)"
    >
      <h2>{{ recipe.recipeName }}</h2>
      <img class="recipeImage" v-bind:src="recipe.recipeImage" />

      <div id="recipe-modify-container">
        <button
          class="direction-btn"
          v-on:click="changeDisplay(), getIngredients(recipe)"
        >
          Recipe Instructions
        </button>
        <button class="delete-btn" v-on:click="deleteRecipe(recipe.recipeId)">
          DELETE
        </button>
        <button @click="showMealPlans()">Add to Meal Plans</button>
      <div v-if="isShown">
        <div class="button" v-for="plan in plans" :key="plan.plannerId">
          <div @click="saveAndPush({plannerId: plan.plannerId, recipeId: recipe.recipeId, day: 'Monday', week: 1})">{{plan.name}}</div>
          
        </div>
      </div>
        
      </div>
      <div class="instructions" v-if="displayInstructions === index">
        <h2>Instructions</h2>
        <p
          v-for="(instruct, index) in instructionsIntoArray(
            recipe.instructions
          )"
          :key="index"
        >
          {{ index + 1 }}. {{ instruct }}
        </p>
        <h2>Ingredients</h2>
        <div v-for="(ingred, index) in ingredients" :key="ingred.riRecipeId">
          {{ index + 1 }}. {{ ingred.name }}
        </div>
      </div>
      </div>
  </div>
</template>

<script>
import recipesService from "@/services/RecipesService.js";

export default {
  data() {
    return {
      displayInstructions: false,
      ingredients: [],
      userId: 0,
      plans: [],
      isShown: false,
    };
  },
  name: "recipe-display",
  methods: {
    getRecipes() {
      recipesService.getMyRecipesByUser().then((response) => {
        this.$store.commit("SET_RECIPES", response.data);
      });
    },
    getIngredients(recipe) {
      recipesService
        .GetAllRecipesIngredientsByRecipeId(recipe.recipeId)
        .then((response) => {
          this.ingredients = response.data;
        });
    },
    changeDisplay() {
      if (!this.displayInstructions) {
        this.displayInstructions = true;
      } else if (typeof this.displayInstructions === "number") {
        this.displayInstructions = false;
      } else {
        this.displayInstructions = false;
      }
    },
    deleteRecipe(recipeId) {
      recipesService.deleteFromMyRecipes(recipeId).then((response) => {
        if (response.status === 200) {
          this.$router.go(0);
        }
      });
    },
    instructionsIntoArray(txt) {
      const array = txt.split(". ");
      return array;
    },
    setActiveRow(index) {
      this.displayInstructions = index;
    },
    showMealPlans() {
      recipesService.getPlannerByUserId(this.userId).then((response) => {
        this.plans = response.data;
      });
      this.isShown = !this.isShown
    },
    saveAndPush(rp){
      this.addToPlan(rp);
      this.$router.push(`/mealplan`)
    },
    addToPlan(rp){
      recipesService.addRp(rp)
    },
  },
  created() {
    this.getRecipes();
    recipesService.getUserId().then((response) => {
      this.userId = response.data;
    });
  },
};
</script>

<style>
.recipe-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  border: 1px black solid;
  border-radius: 6px;
  padding: 1rem;
  margin: 10px;
}

#recipe-modify-container {
  display: flex;
  flex-direction: row;
  justify-content: center;
}

.recipeImage {
  width: 30%;
}


.button {
    border: 2px solid rgba(38, 47, 53, 0.7);
    color: #262f35;
    padding: .5rem;
    margin: .5rem;
    font-size: 1rem;
    font-weight: 600;
    cursor: pointer;
    text-transform: uppercase;
    transition-duration: 0.4s;
}

.button:hover {
  background-color: rgba(84, 120, 44, 0.5);
}
</style>