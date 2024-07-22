// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import EquiposView from '../views/EquiposView.vue';
import MisionesView from '../views/MisionesView.vue';
import OperativosView from '../views/OperativosView.vue';

const routes = [
  { path: '/', redirect: '/equipos' },
  { path: '/equipos', component: EquiposView },
  { path: '/misiones', component: MisionesView },
  { path: '/operativos', component: OperativosView },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;