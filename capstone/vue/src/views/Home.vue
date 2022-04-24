<template>
<div class="content-one-column">
  <h1>Cooking &#38; Shopping, it's SMPL!</h1>
  <div class="grid3-columns">
    <div class="left1">
      <h2>Over 1,000 recipes</h2>
      <p>Edit or save recipes, and find cooking instructions.</p>
    </div>  
    <router-link class="btn-feature btn-1" tag="button" v-bind:to="{ name: 'all-recipes'}">All Recipes</router-link>
    <div class="middle">
        <h2>Add favorite meals</h2>
        <p>Enter a new recipe into your favorites.</p>
    </div>    
    <router-link class="btn-feature btn-2"
        tag="button"
        :to="{ name: 'AddCard', params: {}}"
      >Create Recipe</router-link>
    
    <div class="right2">
        <h2>Too busy to cook?</h2>
      <p>Save time, create a grocery list.</p>
    </div>
    <button class="btn-feature btn-3"><router-link v-bind:to="{ name: 'grocery-list' }" v-if="$store.state.token != ''">Make shopping list</router-link></button>
</div>
</div>
      
</template>

<script>
export default {
  name: "home"
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

/* 3-COLUMN GRID */
.grid3-columns {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-rows: 1fr auto;
  grid-template-areas:
    "left1 middle right2"
    "btn-1 btn-2 btn-3";
  gap: 1rem;
  margin: 2rem;
}
.left1 {
  grid-area: left;
  grid-area: 1/1/2/2;
  border: 2px solid #c6c6c6;
  text-align: left;
  padding: 1rem;
}

.middle {
  grid-area: middle;
  grid-area: 1/2/2/3;
  border: 2px solid #c6c6c6;
  text-align: let;
  padding: 1rem;
}

.right2 {
  grid-area: right;
  grid-area: 1/3/2/4;
  border: 2px solid #c6c6c6;
  text-align: left;
  padding: 1rem;
}

.btn-1 {
  grid-area: btn-3;
  grid-area: 2/1/3/2;
}
.btn-2 {
  grid-area: btn-3;
  grid-area: 2/2/3/3;
}
.btn-3 {
  grid-area: btn-3;
  grid-area: 2/3/3/4;
}

.btn-feature {
  width: 100%;
  background-color: #f5c177;
  transition-duration: 0.4s;
  border: none;
  line-height: 3rem;
  margin-bottom: 1rem;
}

.btn-feature:hover {
    background-color: rgba(84, 120, 44, 0.5);
}

button a {
  text-decoration: none;
  color: #262f35;
}

@media (max-width: 650px) {
.grid3-columns {
    grid-template-columns: auto;
    grid-template-rows: repeat(6, 1fr);
    margin: 2rem 0 0 0;
    grid-template-areas:
    "left1"
    "btn-1"
    "middle"
    "btn-2" 
    "right2"
    "btn-3";
  }

 .left1 {
  grid-area: left;
  grid-area: 1/1/2/2;
  text-align: center;
  padding: 1rem;
}
.btn-1 {
  grid-area: btn-3;
  grid-area: 2/1/3/2;
}
.middle {
  grid-area: middle;
  grid-area: 3/1/4/2;
  text-align: center;
  padding: 1rem;
}
.btn-2 {
  grid-area: btn-3;
  grid-area: 4/1/5/2;
}
.right2 {
  grid-area: right;
  grid-area: 5/1/6/2;
  text-align: center;
  padding: 1rem;
}
.btn-3 {
  grid-area: btn-3;
  grid-area: 6/1/7/2;
}
}
</style>