<template>
  <form v-on:submit.prevent="submitForm" class="cardForm">
    <div class="status-message error" v-show="errorMsg !== ''">{{errorMsg}}</div>
    <div class="form-group">
      <label for="title">Recipe Name:</label>
      <input id="title" type="text" class="form-control" v-model="recipe.recipeName" autocomplete="off" />
    </div>
    <div class="form-group">
      <label for="tag">Area:</label>
      <select id="tag" class="form-control" v-model="recipe.areaId">
        <option value=1>American</option>
        <option value=2>British</option>
        <option value=3>Canadian</option>
      </select>
      <label for="status">Category:</label>
      <select id="tag" class="form-control" v-model="recipe.categoryId">
        <option value=1>Beef</option>
        <option value=2>Chicken</option>
        <option value=3>Dessert</option>
      </select>
    </div>
    <div class="form-group">
      <label for="description">Instructions:</label>
      <textarea id="description" class="form-control" v-model="recipe.instructions"></textarea>
    </div>
    <div class="form-group">
      <label for="title">Image:</label>
      <input id="title" type="text" class="form-control" v-model="recipe.recipeImage" autocomplete="off" />
    </div>
    <div class="form-group">
      <label for="title">Youtube:</label>
      <input id="title" type="text" class="form-control" v-model="recipe.youtube" autocomplete="off" />
    </div>
    <button>Submit</button>
    <button class="delete-btn" v-on:click.prevent="cancelForm" type="cancel">Cancel</button>
  </form>
</template>

<script>
import recipesService from "@/services/RecipesService.js";
import moment from "moment";

export default {
  name: "card-form",
  props: {
    recipeId: {
      type: Number,
      default: 0
    }
  },
  data() {
    return {
      recipe: {
        recipeName: "",
        drinkAlternate: "",
        categoryId: 0,
        recipeTags: "",
        areaId: 0,
        instructions: "",
        source: "",
        recipeImage: null,
        youtube: "",
        date:"",
        imageSource: "",
        userId: 1
      },
      errorMsg: ""
    };
  },
  methods: {
    submitForm() {
      alert("submitting")
      const newRecipe = {
        recipeId: Number(this.$route.params.id),
        recipeName: this.recipe.recipeName,
        drinkAlternate: this.recipe.drinkAlternate,
        categoryId: this.recipe.categoryId,
        recipeTags: this.recipe.recipeTags,
        source: this.recipe.source,
        areaId: this.recipe.areaId,
        instructions: this.recipe.instructions,
        recipeImage: this.recipe.recipeImage,
        youtube: this.recipe.youtube,
        imageSource: this.recipe.imageSource,
        date: moment().format("MMM Do YYYY"),
        userId: 1
      };

      if (this.recipeId === 0) {
        // add
        this.recipe.categoryId = parseInt(this.recipe.categoryId)
        this.recipe.areaId = parseInt(this.recipe.areaId)
        recipesService
          .addRecipe(this.recipe)
          .then(response => {
            if (response.status === 201) {
              this.$router.push(`/allrecipes/`);
            }
          })
          .catch(error => {
            this.handleErrorResponse(error, "adding");
          });
      } else {
        // update
        newRecipe.categoryId = parseInt(this.recipe.categoryId)
        newRecipe.areaId = parseInt(this.recipe.areaId)
        recipesService
          .updateRecipe(newRecipe)
          .then(response => {
            if (response.status === 201) {
              this.$router.push(`/recipes/${newRecipe.recipeId}`);
            }
          })
          .catch(error => {
            this.handleErrorResponse(error, "updating");
          });
      }
    },
    cancelForm() {
      this.$router.push(`/recipes/${this.$route.params.id}`);
    },
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " + verb + " recipe. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " recipe. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " recipe. Request could not be created.";
      }
    }
  },
  created() {
    if (this.recipeId != 0) {
      recipesService
        .getRecipeById(this.recipeId)
        .then(response => {
          this.recipe = response.data;
        })
        .catch(error => {
          if (error.response && error.response.status === 404) {
            alert(
              "Recipe not available. This recipe may have been deleted or you have entered an invalid recipe ID."
            );
            this.$router.push("/");
          }
        });
    }
  }
};
</script>


<style>
.cardForm {
  padding: 10px;
  margin-bottom: 10px;
}
.form-group {
  margin-bottom: 10px;
  margin-top: 10px;
}
label {
  display: inline-block;
  margin-bottom: 0.5rem;
}
.form-control {
  display: block;
  width: 80%;
  height: 30px;
  padding: 0.375rem 0.75rem;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.5;
  color: #495057;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
}
textarea.form-control {
  height: 75px;
  font-family: Arial, Helvetica, sans-serif;
}
select.form-control {
  width: 20%;
  display: inline-block;
  margin: 10px 20px 10px 10px;
}

.status-message {
  display: block;
  border-radius: 5px;
  font-weight: bold;
  font-size: 1rem;
  text-align: center;
  padding: 1rem;
  margin-bottom: 1rem;
}
.status-message.success {
  background-color: rgba(84, 120, 44, 1);
  color: white;
}
.status-message.error {
  background-color: rgba(230, 37, 30, 1);
  color: white;
}
</style>