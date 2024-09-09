<template>
  <v-app>
    <v-main>
      <OperativoList />
    </v-main>
  </v-app>
</template>

<script>
import OperativoList from './components/OperativoList.vue';

export default {
  name: 'App',
  components: {
    OperativoList,
  },
};
</script>

<style>

</style>
