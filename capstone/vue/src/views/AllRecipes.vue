<template>
  <div class="content-one-column">
    <h1>Recipes</h1>
    <all-recipes />
  </div>
</template>

<script>
import AllRecipes from '../components/AllRecipes.vue'
export default {
  components: { AllRecipes },

}
</script>

<style scoped>
* {
  box-sizing: border-box;
  padding: 0 3rem;
  margin: 0;
}

.content-one-column {
  display: grid;
  width: 100%;
  margin: 0;
  padding: 0;  
}
</style>