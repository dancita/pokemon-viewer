import { createRouter, createWebHistory } from 'vue-router';
import PokemonViewer from './components/PokemonViewer.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: PokemonViewer
  },
  {
    path: '/callback',
    name: 'callback'
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
