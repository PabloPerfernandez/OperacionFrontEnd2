<template>
  <v-row v-if="equipos.length > 0" dense>
    <v-col v-for="equipo in equipos" :key="equipo.equipoCodigo" cols="12" sm="6" md="4">
      <v-card outlined>

        <v-card-title>Equipo ID: {{ equipo.equipoCodigo }}</v-card-title>
        <v-card-text>{{ equipo.descripcion || 'Sin descripción' }}</v-card-text>
        <v-card-subtitle>Tipo: {{ equipo.tipo }}</v-card-subtitle>
        
        <!-- Botones de editar y eliminar -->
        <v-card-actions>
          <v-btn @click="$emit('edit', equipo, 'equipo')" color="primary">Editar</v-btn>
          <v-btn @click="$emit('delete', equipo, 'equipo')" color="error">Eliminar</v-btn>
        </v-card-actions>

      </v-card>
    </v-col>
  </v-row>
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

<style scoped></style>