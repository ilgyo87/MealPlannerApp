<template>
    <div>
      <input type="text" v-model="searchBar" placeholder='What would you like to buy?'>
      <div class="container">
        <div class="grocery-list">  
          <ul>
            <li v-for="ingred in filteredIngredients" :key="ingred.ingredId"
              @click="addToGroceries(ingred.name)">
              {{ ingred.name }}
            </li>
          </ul>
        </div>
        <div class="groceries">
          <h3>
          List: {{groceryList.length}}
          </h3>
          <div v-for="(item,index) in groceryList" :key="item">
            <div @click="remove(index)">
            {{item}}
            </div>
          </div>
        </div>
      </div>
    </div>
</template>


<script>
import recipesService from "@/services/RecipesService.js";

export default {
  name: "grocery-list",
  data(){
    return {
      searchBar:'',
      ingredientList: [],
      groceryList:[]
    };
  },
  methods: {
    addToGroceries(item){
      this.groceryList.push(item);
    },
    remove(index){
      this.groceryList.splice(index,1)
    }
  },
 created(){
    recipesService.getAllIngredients().then(response => {
          this.ingredientList = response.data;
        });
  },
  computed: {
    filteredIngredients() {
      return this.ingredientList.filter((ingredient) => {
        return ingredient.name.toLowerCase().match(this.searchBar);
      });
    }
  }
}
</script>

<style>
.container {
  display:grid;
  grid-template-columns: auto auto;
  gap:10px;
}

.grocery-list {

    width:450px;
    background: #fff;
    margin: 50px auto;
    font-family: 'Roboto Condensed', sans-serif;
    border-radius: 10px;
}

.groceries {
    width:450px;
    background: #fff;
    margin: 50px auto;
    font-family: 'Roboto Condensed', sans-serif;
    border-radius: 10px;
    font-size: 25px;
}

ul {
    list-style-type: none;
    margin:0px;
    padding:0px;
}
li {
    font-size: 24px;
    border-bottom:1px solid #f2f2f2;
    padding:10px 20px;
}
h3{
  font-size: 35px;
  justify-content: center;
  justify-items: center;
  color: green;
  
}

    
</style>