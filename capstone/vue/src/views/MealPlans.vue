<template>
    <div class="content-one-column">
        <h1>My Meal Plan Page</h1>
        <div>
        <router-link
        tag="button"
        :to="{ name: 'AddPlan', params: {}}"
        class="btn editCard"
      >ADD NEW MEAL PLAN</router-link></div>
  <div class="try">
      <my-meal-plans :userId="this.userId" />
      
        <!-- <calendar /> -->
        </div>
    </div>
</template>

<script>

// import Calendar from '../components/Calendar.vue';
import MyMealPlans from '../components/MyMealPlans.vue';
import recipesService from "@/services/RecipesService.js";

export default {
  components: {
    // Calendar,
    MyMealPlans
  },
   data() {
    return {
      userId: 1,
    };
  },
  created() {
    recipesService.getUserId().then(response => {
          this.userId = response.data;
        });
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

button {
  border: 2px solid rgba(38, 47, 53, 0.7);
    color: #262f35;
    padding: .5rem;
    margin: .5rem 0;
    font-size: 1rem;
    font-weight: 600;
    cursor: pointer;
    text-transform: uppercase;
    transition-duration: 0.4s;

}

.try {
  padding: 0;
}
</style>
