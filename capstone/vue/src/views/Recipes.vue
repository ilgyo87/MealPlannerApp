<template>
    <div class="content-one-column">
        <h1>My Recipes Page</h1>  
        <recipe-display/>
    </div>
</template>

<script>
import RecipeDisplay from '../components/RecipeDisplay'


export default {
    components:{
        RecipeDisplay,
    }
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
