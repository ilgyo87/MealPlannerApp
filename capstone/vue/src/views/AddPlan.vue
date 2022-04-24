<template>
  <div class="content-one-column">
    <h1>Add New Plan</h1>
    <meal-plan-form />
  </div>
</template>

<script>
import MealPlanForm from "@/components/MealPlanForm";

export default {
  components: {
    MealPlanForm
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