<template>
  <v-card v-for="equipo in equipos" :key="equipo.id" class="mb-4">
    <v-card-title>{{ equipo.nombre }}</v-card-title>
    <v-card-text>{{ equipo.descripcion }}</v-card-text>
    <v-card-subtitle>Tipo: {{ equipo.tipo }}</v-card-subtitle>
  </v-card>
</template>

<script>
export default {
  name: "EquiposList",
  props: {
    equipos: {
      type: Array,
      required: true,
    },
  },
};
</script>

<style scoped>
/* Puedes añadir estilos aquí si es necesario */
</style>