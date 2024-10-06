<template>
  <v-container>
    <v-card v-for="equipo in equipos" :key="equipo.id" class="mb-4">
    <v-card-title>{{ equipo.nombre }}</v-card-title>
    <v-card-text>{{ equipo.descripcion }}</v-card-text>
    <v-card-subtitle>Tipo: {{ equipo.tipo }}</v-card-subtitle>
    
    <!-- Botones de editar y eliminar -->
    <v-card-actions>
      <v-btn @click="$emit('edit', equipo)" color="primary">Editar</v-btn>
      <v-btn @click="$emit('delete', equipo.id)" color="error">Eliminar</v-btn>
    </v-card-actions>
  </v-card>
  </v-container>
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
/* Estilos opcionales */
</style>
