<template>
  <div class="content-one-column">
    <h1>Add new recipe</h1>
    <card-form />
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