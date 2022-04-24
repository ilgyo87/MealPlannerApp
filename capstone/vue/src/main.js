import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import store from './store/index'
import axios from 'axios'

/* import the library from fontawesome core */
import { library } from '@fortawesome/fontawesome-svg-core'

/* import the icons */
import { faHatWizard } from '@fortawesome/free-solid-svg-icons'
import { faFacebook, faInstagram, faTwitter } from '@fortawesome/free-brands-svg-icons'


/* import font awesome icon component: it is the magic that renders icons in our Vue projects */
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

/* Now we just tell the library what Font Awesome icons to use: */
library.add(faHatWizard)
library.add(faFacebook)
library.add(faInstagram)
library.add(faTwitter)

/* The final spell component is telling Vue.js about our Font Awesome icon component. */
Vue.component('font-awesome-icon', FontAwesomeIcon)


Vue.config.productionTip = false

axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
