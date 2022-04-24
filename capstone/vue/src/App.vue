<template>
  <div id="app">

    <nav-bar />

    <div id="container">
    <router-view />      
    </div>

    <footer-bar />

  </div>
</template>


// Either keep the Nav and Footer in App.vue (delete those components) ... OR use components and include script below.
<script>
import NavBar from '@/components/NavBar'
import FooterBar from '@/components/FooterBar'

export default {
  name: 'app',
  components:{
    NavBar,
    FooterBar
  }
}
</script>


<style>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,400;0,500;0,600;1,400;1,500;1,600&display=swap');
* {
  box-sizing: border-box;
  padding: 0;
  margin: 0;
  
}

#app {
  background: #faf8f6;
  display: grid;
  grid-template-columns: 1fr;
  grid-template-rows: minmax(75px, auto) 1fr minmax(100px, auto);
  /* height: 100vh; */
  row-gap: 3rem;
  grid-template-areas: 
    "header" 
    "container"
    "footer";
}

header {
  grid-area: header;
  grid-area: 1/1/2/2; 
}

#container {
  grid-area: container;
  grid-area: 2/1/3/2;
}

footer {
  grid-area: footer;
  grid-area: 3/1/4/2;
}

/* Global Font and Color */
body {
  font-family: 'Montserrat', sans-serif;
  font-size: 1rem;
  letter-spacing: 0.05rem;
  color: #262f35;
}

h1 {
  font-family: 'Montserrat', sans-serif;
  font-size: 3rem;
  font-weight: 600;
  letter-spacing: 0.05rem;
  line-height: 1.5;
  }

h2 {
  font-family: 'Montserrat', sans-serif;
  font-size: 1.75rem;
  font-weight: 400;
  line-height: 1.5;
  margin-bottom: 0.25rem;
  }

h3 {
  font-family: 'Montserrat', sans-serif;
  font-size: 1.25rem;
  font-weight: 500;
  line-height: 1.3;
  margin-bottom: 0.25rem;
  }

.sr-only {
  font-family: Georgia, 'Times New Roman', Times, serif;
  font-size: 1.5rem;
  font-weight: 600;
  text-transform: uppercase;
  }

/* BUTTON Global Styles */

button {
    border: 2px solid rgba(38, 47, 53, 0.7);
    color: #262f35;
    padding: .5rem;
    margin: .5rem;
    font-size: 1rem;
    font-weight: 600;
    cursor: pointer;
    text-transform: uppercase;
    transition-duration: 0.4s;
}

button:hover {
  background-color: rgba(84, 120, 44, 0.5);
}

.delete-btn:hover {
    background-color: rgba(230, 37, 30, 1);
}

.btn-feature {
  width: 100%;
  background-color: #f5c177;
  transition-duration: 0.4s;
  border: none;
  line-height: 3rem;
}

.btn-feature:hover {
    background-color: rgba(84, 120, 44, 0.5);
}

input {
  font-family: 'Montserrat', sans-serif;
  font-size: 1.25rem;
  width: 100%;
  line-height: 3;
  margin-bottom: 10px;
  text-align: center;
  border: none;
  background-color: #e9e9e9;
}

/* Later-stage Nice-to-have... 

@media (max-width: 650px) {
  body {
  }
  } */

</style>