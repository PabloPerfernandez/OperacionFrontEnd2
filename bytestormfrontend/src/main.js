// main.js
import { createApp } from 'vue';
import App from './App.vue';
import vuetify from './plugins/vuetify.js'; // Asegúrate de que esta ruta sea correcta
import router from './router';

createApp(App)
  .use(router)
  .use(vuetify)
  .mount('#app');