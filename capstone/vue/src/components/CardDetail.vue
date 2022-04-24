<template>
  <div class="card">
      <h1> {{ card.recipeName }} </h1>
      <img :src="card.recipeImage">
      <section>
        <iframe width="500" height="315" :src="card.youtube" frameborder="0"></iframe>
      </section>
      <h2>Cooking Instructions</h2>
      <p
      v-for="(instruct,index) in instructionsIntoArray(card.instructions)"
      :key="index">
      {{index + 1}}. {{instruct}}
      </p>
      <h2>Ingredients</h2>

      <div v-for="(ingred, index) in ri"
      :key="ingred.riRecipeId"
      >
       {{index + 1}}. {{ingred.name}}
      </div>

    <div class="recipeButtons">
      <router-link
        tag="button"
        :to="{ name: 'EditCard', params: {id: $route.params.id}}"
        class="btn editCard"
      >Edit Recipe</router-link>

      <button v-on:click="saveMyRecipe()">Save Recipe</button>
      <button @click="showPlans()">Add to Meal Plans</button>
      <div v-if="isShown">
        <div class="button" v-for="plan in plans" :key="plan.plannerId">
          <div @click="saveAndPush({plannerId: plan.plannerId, recipeId: card.recipeId, day: 'Monday', week: 1})">{{plan.name}}</div>
          
        </div>
      </div>
    </div>

    </div>
</template>

<script>
import recipesService from "@/services/RecipesService.js";


export default {
  name: "card-detail",
//   components:{
//   },
  data(){
    return {
      card: {},
      ri: [],
      editable: false,
      userId: "",
      plans: [],
      isShown: false,
    };
  },
  methods: {
    saveMyRecipe(){
      alert("Successfully added to your recipes");
      recipesService
        .saveRecipeToMyRecipes(this.card)
    },
    instructionsIntoArray(txt){
        const array = txt.split(". ");
        return array;
    },
    showPlans() {
      recipesService.getPlannerByUserId(this.userId).then((response) => {
        this.plans = response.data;
      });
      this.isShown = !this.isShown
    },
    addToPlan(rp){
      recipesService.addRp(rp)
    },
    saveAndPush(rp){
      this.addToPlan(rp);
      this.$router.push(`/mealplan`)
    }
  },
  created(){
    recipesService
        .getRecipeById(this.$route.params.id)
        .then(response => {
          this.card = response.data;
        });
    recipesService
        .GetAllRecipesIngredientsByRecipeId(this.$route.params.id)
        .then(response => {
          this.ri = response.data;
        });
        recipesService.getUserId().then((response) => {
      this.userId = response.data;
    });
  }
};
</script>

<style>
.recipeButtons{
  padding: 20px;
  display: flex;
  justify-content: center;
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