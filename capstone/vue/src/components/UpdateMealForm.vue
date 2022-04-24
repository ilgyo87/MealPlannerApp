<template>
    <div class="add-plan-popup">
        <p>Update Meal Plan {{this.plannerId}}</p>
        <div class="popup-content">
            <form class="form-new-plan" @submit.prevent>
                <div class="status-message error" v-show="errorMsg !== ''">{{errorMsg}}</div>
                <h2>Create a New Meal Plan Below</h2>
                <div class="group">
                    <label >MEAL PLAN NAME</label>
                    <input type="text" v-model="planner.name" />
                </div>

                <div class="form-group">
                    <h3>Can we feature your meal plan in the SMPL Community?</h3>
                    <label>Yes</label>
                    <input class="form-control" type="radio" v-model="planner.isSharable" value="true">
                    <label>No</label>
                    <input class="form-control" type="radio" v-model="planner.isSharable" value="false">
                    <button type="submit" @click="updatePlan()">CREATE</button>
                    <button type="button" @click.prevent="cancelForm">Cancel</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
import recipesService from "@/services/RecipesService.js";
export default {
    name: "meal-plan-form",
    props: ["plannerId"],
    data(){
        return{
            planner: {
                name: "",
                isSharable: ""
            },
            errorMsg: ""
        };
    },
     methods: {
    updatePlan() {
        const newPlanner = this.planner;
        newPlanner.isSharable = Boolean(this.planner.isSharable)
        newPlanner.plannerId = this.plannerId
        console.log(this.planner);
        recipesService
            .updatePlanner(newPlanner)
            .then((res) => {
            if (res.status === 200) {
                this.$router.push("/mealplan");
            }
            })
            .catch((err) => {
            alert(`Error occurred: ${err.message}`);
            });
    },
  cancelForm() {
      this.$router.push(`/mealplan`);
    },
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " + verb + " recipe. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " recipe. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " recipe. Request could not be created.";
      }
  },
  created(){
    recipesService
        .getPlannerById(this.plannerId)
        .then(response => {
          this.planner = response.data;
        })
        .catch(error => {
        if (error.response.status == 404) {
          this.$router.push({name: 'NotFound'});
        }
      });
  }
     }
};
</script>

<style scoped>
.form-new-plan {
  padding: 10px;
  margin-bottom: 10px;
}
.form-group {
  margin-bottom: 10px;
  margin-top: 10px;
}
label {
  display: inline-block;
  margin-bottom: 0.5rem;
}
.form-control {
  display: block;
  width: 80%;
  height: 30px;
  padding: 0.375rem 0.75rem;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.5;
  color: #495057;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
}
textarea.form-control {
  height: 75px;
  font-family: Arial, Helvetica, sans-serif;
}
select.form-control {
  width: 20%;
  display: inline-block;
  margin: 10px 20px 10px 10px;
}

.status-message {
  display: block;
  border-radius: 5px;
  font-weight: bold;
  font-size: 1rem;
  text-align: center;
  padding: 1rem;
  margin-bottom: 1rem;
}
.status-message.success {
  background-color: rgba(84, 120, 44, 1);
  color: white;
}
.status-message.error {
  background-color: rgba(230, 37, 30, 1);
  color: white;
}
.delete-btn:hover {
    background-color: rgba(230, 37, 30, 1);
}
</style>