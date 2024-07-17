import { createRouter, createWebHistory } from 'vue-router';
import OperativosView from '../views/OperativosView.vue';
import MisionesView from '../views/MisionesView.vue';
import EquiposView from '../views/EquiposView.vue';

const routes = [
    { path: '/operativos', component: OperativosView },
    { path: '/misiones', component: MisionesView },
    { path: '/equipos', component: EquiposView },
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;