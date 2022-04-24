<template>
    <div class="content-one-column">
        <h1>Add Ingredients to Your Grocery List</h1>
        <grocery-list />
    </div>
</template>

<script>
import GroceryList from '../components/GroceryList'

export default {
    components: {GroceryList}
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
