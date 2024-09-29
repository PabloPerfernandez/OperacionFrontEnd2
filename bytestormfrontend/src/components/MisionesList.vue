<template>
  <v-row dense>
    <v-col v-for="mision in misiones" :key="mision.codigo" cols="12" sm="6" md="4">
      <v-card outlined>
        <v-card-title>Misión Código: {{ mision.codigo }}</v-card-title>
        <v-card-subtitle>Asignada a: {{ mision.operativo?.nombre }}</v-card-subtitle>
        <v-card-text>Descripción: {{ mision.descripcion }}</v-card-text>

        <!-- Mostrar equipos asociados a la misión -->
        <v-list v-if="mision.equipos && mision.equipos.length > 0">
          <v-list-item v-for="equipo in mision.equipos" :key="equipo.equipoCodigo">
            <v-list-item-content>
              <v-list-item-title>Equipo Código: {{ equipo.equipoCodigo }}</v-list-item-title>
              <v-list-item-subtitle>Tipo: {{ equipo.tipo }}</v-list-item-subtitle>
              <v-list-item-text>Descripción: {{ equipo.descripcion }}</v-list-item-text>
            </v-list-item-content>
          </v-list-item>
        </v-list>

        <!-- Si no tiene equipos asignados -->
        <v-card-text v-else>Sin equipos asignados</v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
export default {
  name: 'MisionesList',
  props: {
    misiones: Array,
  },
};
</script>