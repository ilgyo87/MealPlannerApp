<template>
  <div class="content-one-column">
    <h1>Edit Card</h1>
    <card-form :recipeId="parseInt($route.params.id)" />
  </div>
</template>

<script>
import CardForm from "@/components/CardForm";

export default {
  components: {
    CardForm
  }
};
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
