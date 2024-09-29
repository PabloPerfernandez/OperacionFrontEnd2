<template>
  <v-app>
    <v-main>
      <MainList />
    </v-main>
  </v-app>
</template>

<script>
import MainList from './components/MainList.vue';

export default {
  name: 'App',
  components: {
    MainList,
  },
};
</script>

<style>

</style>