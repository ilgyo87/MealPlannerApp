<template>
  <nav class="flex-header">
    <div class="logo"><a target="_blank" href="">
    <img src="../assets/smpl_logo2.png" alt="SMPL" width="auto" height="100">
  </a></div>
    <router-link class="menu" tag= "nav-link" v-bind:to="{ name: 'home' }">Home</router-link>
    <router-link class="menu" tag= "nav-link" v-bind:to="{ name: 'my-recipes' }" v-if="$store.state.token != ''">My Recipes</router-link>
    <router-link class="menu" tag= "nav-link" v-bind:to="{ name: 'meal-plan' }" v-if="$store.state.token != ''">My Meal Plans</router-link> 
    <router-link class="menu" tag= "nav-link" v-bind:to="{ name: 'grocery-list' }" v-if="$store.state.token != ''">Grocery List</router-link> 
    <router-link  class="btn-logout" id="logout-btn" tag= "button1" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
  </nav>
</template>

<script>
export default {
    name: 'nav-bar',
}
</script>

<style>
.flex-header {
  display: flex;
  align-items: center;
  width: auto;
  background: #54782c;
  padding: 1rem 2rem;
  margin: 0;
  height: auto;
  text-align: center;
}
.logo {
  flex: 1 1 100px;
  color: #f5c177;
  font-size: 3em;
  font-weight: 400;
  letter-spacing: 0.05em;
  text-align: left;
}

.menu {
  flex: 0 0 auto;
  text-decoration: none;
  border: none;
  font-size: 1.5rem;
  color: #fff;
  padding: 1rem;
  /* alignment */
}

.menu:hover {
  color: #000;
  background: #f5c177;
}

.menu:active {
  background: rgba(191, 163, 138, 1);
}

.btn-logout {
  background: rgba(250, 248, 246, 0.3);
  color: white;
  margin: 0 0 0 2rem;
  transition-duration: 0.4s;
  font-size: 1.5rem;
  font-weight: 500;
  border: none;
  text-decoration: none;
  padding: 1rem 2rem;
  text-align: center;
  text-decoration: none;
  display: inline-block;
}

.btn-logout:hover {
  background-color: rgba(250, 248, 246, 1); 
  color: rgba(38, 47, 53, 0.8);
}

@media (max-width: 1100px) {
.flex-header {
  flex-wrap: wrap;
  margin: 0;
  padding: 0;
  } 

.logo {
    text-align: center;
    background: #faf8f6;
  }

.menu {
    flex: 1 1 100%;
    border-bottom: none;
    text-align: center;
    padding: 1rem;
    border-bottom: 1px solid white;
  }

.btn-logout {
    flex: 1;
    border-bottom: none;
    text-align: center;
    padding: 1rem;
    margin: 0;
}

}
</style>