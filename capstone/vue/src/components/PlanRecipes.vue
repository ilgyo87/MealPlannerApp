<template>
<div >
    <div class="content-one-column" v-for="plan in rpList" v-bind:key="plan.rpId">
        <div >
            <div>{{getRecipeName(plan.recipeId)}}</div> 
            <select class="btn-search">
                <option  v-for="index in days" :key="index">{{index}}</option>
            </select>
            <a href="#" @click="deletePlanner(plan.rpId)">Delete</a>
        </div>
    
    </div>
    <calendar />
</div>
</template>


<script>
import recipesService from "@/services/RecipesService.js";
import Calendar from './Calendar.vue';

export default {
  components: { Calendar },
  name: "plan-recipes",
  data(){
    return {
      rpList: [],
      recipes: [],
      days:["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"]
    };
  },
  methods: {
      getRecipeName(recipeId){
          for(let i=0;i<this.recipes.length;i++){
              if(this.recipes[i].recipeId == recipeId){
                  return this.recipes[i].recipeName
              }
          }
      },
      getRpByPlannerId(){
        recipesService.getRpByPlannerId(this.$route.params.id).then(response => {
          this.rpList = response.data;
        });
      },
    deletePlanner(plannerId) {
      recipesService
        .deleteRp(plannerId)
        .then((res) => {
          if (res.status === 200) {
            this.getRpByPlannerId();
          }
        })
        .catch((err) => {
          alert(`Error occurred: ${err.message}`);
        });
    },
  },
 created(){
    recipesService.getRpByPlannerId(this.$route.params.id).then(response => {
          this.rpList = response.data;
        });
    recipesService.getList().then(response => {
          this.recipes = response.data;
        });  
      
  },
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