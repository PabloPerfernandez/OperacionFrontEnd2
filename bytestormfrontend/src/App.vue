<template>
  <div id="app">
    <AppNavigation />
    <router-view></router-view>
  </div>
</template>

<script>
import AppNavigation from './components/AppNavigation.vue';

export default {
  name: 'App',
  components: {
    AppNavigation,
  },
};
</script>

<style>
/* Add your styles here */
</style>