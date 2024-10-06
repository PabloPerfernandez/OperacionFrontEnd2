<template>
  <v-app>
    <v-main>
      <router-view /> <!-- Lugar donde se cargarán las rutas -->
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: 'App',
};
</script>

<style>
main {
  background-color: #9A2B2B;
}
</style>