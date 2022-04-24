<template>
  <div class="content-one-column">
    <h1>Edit New PLan</h1>
    <update-meal-form :plannerId="parseInt($route.params.id)" />
  </div>
</template>

<script>
import UpdateMealForm from "@/components/UpdateMealForm";

export default {
  components: {
    UpdateMealForm
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