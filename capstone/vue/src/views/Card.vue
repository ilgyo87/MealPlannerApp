<template>
  <div class="content-one-column">
  <h1>Recipe Details</h1>
  <card-detail />
  </div>
</template>

<script>
import CardDetail from '@/components/CardDetail';

export default {
  components: {
    CardDetail
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