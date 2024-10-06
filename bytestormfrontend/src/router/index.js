import { createRouter, createWebHistory } from 'vue-router';
import IntroScreen from '@/components/IntroScreen.vue';
import MainList from '@/components/MainList.vue';

const routes = [
  {
    path: '/', // Ruta raíz que cargará la pantalla de bienvenida
    name: 'IntroScreen',
    component: IntroScreen,
  },
  {
    path: '/operativos', // Ruta que cargará el MainList.vue
    name: 'MainList',
    component: MainList,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;