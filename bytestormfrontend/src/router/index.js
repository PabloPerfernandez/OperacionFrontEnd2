import { createRouter, createWebHistory } from 'vue-router';
import IntroScreen from '@/components/IntroScreen.vue';
import MainList from '@/components/MainList.vue';

const routes = [
  {
    path: '/', // Pantalla de bienvenida
    name: 'IntroScreen',
    component: IntroScreen,
  },
  {
    path: '/operativos', // Web en sí
    name: 'MainList',
    component: MainList,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;