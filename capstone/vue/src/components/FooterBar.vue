<template>
    <footer>
        <div class="left">
          <a target="_blank" href="">
    <img src="../assets/smpl_logo1.png" alt="SMPL" width="auto" height="50">
  </a>
            <h2>SMPL</h2>
            <p>Shawn's Meal Planner &copy; 2022</p>
        </div>

        <div class="social-media">
            <h2>Follow Us</h2>
            <font-awesome-icon class="icons" icon="fa-brands fa-facebook" />
            <font-awesome-icon class="icons" icon="fa-brands fa-instagram" />
            <font-awesome-icon class="icons" icon="fa-brands fa-twitter" />
        </div>
    </footer>
</template>

<script>
export default {
    name: "footer-bar",
}
</script>

<style scoped>
/* Grid row styles */
footer {
  background: #BFA38A;
  display: grid;
  grid-template-columns: 1fr 1fr;
}
.left {
  grid-area: left;
  grid-area: 1/1/2/2;
  text-align: left;
  padding: 1rem;
}

.social-media {
  grid-area: right;
  grid-area: 1/2/2/3;
  border-left: 2px solid #c6c6c6;
  text-align: left;
  padding: 1rem;
}
.icons {
  padding: 0.5rem;
  color: #faf8f6;
  border: 1px solid #faf8f6;

}

@media (max-width: 650px) {
  footer {
      grid-template-columns: 1fr;
  }

 .left {
  grid-area: left;
  grid-area: 1/1/2/2;
  text-align: center;
  padding: 1rem;
}

.social-media {
  grid-area: right;
  grid-area: 2/1/3/2;
  border-top: 2px solid #c6c6c6;
  text-align: center;
  padding: 1rem;
}
}
</style>