import { createRouter, createWebHistory } from 'vue-router';
import OperativoList from '../components/OperativoList.vue';

const routes = [
  {
    path: '/operativos',
    name: 'OperativoList',
    component: OperativoList,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;