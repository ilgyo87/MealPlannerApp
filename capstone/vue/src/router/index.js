import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import Register from '../views/Register.vue'
import store from '../store/index'
import Recipes from '../views/Recipes.vue'
import MealPlans from '../views/MealPlans.vue'
import GroceryList from '../views/GroceryList.vue'
import AllRecipes from '../views/AllRecipes.vue'
import Card from '../views/Card.vue'
import EditCard from '../views/EditCard.vue'
import AddCard from '../views/AddCard.vue'
import AddPlan from '../views/AddPlan.vue'
import EditPlan from '../views/EditPlan.vue'
import PlanRecipes from '../components/PlanRecipes.vue'

Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/allrecipes',
      name: "all-recipes",
      component: AllRecipes,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/myrecipes",
      name: "my-recipes",
      component: Recipes,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/recipes/:id",
      name: "recipes",
      component: Card,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/mealplan",
      name: "meal-plan",
      component: MealPlans,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/grocerylist",
      name: "grocery-list",
      component: GroceryList,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/recipes/create',
      name: 'AddCard',
      component: AddCard
    },
    {
      path: '/recipes/:id/edit',
      name: 'EditCard',
      component: EditCard
    },
    {
      path: '/planner/create',
      name: 'AddPlan',
      component: AddPlan
    },
    {
      path: '/planner/:id/edit',
      name: 'EditPlan',
      component: EditPlan
    },
    {
      path: '/planner/:id',
      name: 'PlanRecipes',
      component: PlanRecipes
    }
    
  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '' ) {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;
