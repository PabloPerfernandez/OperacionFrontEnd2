<template>
  <v-row v-if="operativos.length > 0" dense>
    <v-col v-for="operativo in operativos" :key="operativo.id" cols="12" sm="6" md="4">
      <v-card outlined>

        <v-card-title>Operativo ID: {{ operativo.id }}</v-card-title>
        <v-card-subtitle>Nombre: {{ operativo.nombre }}</v-card-subtitle>
        <v-card-text>Rol: {{ operativo.rol }}</v-card-text>

        <!-- Misiones dentro del operativo -->
        <v-list v-if="operativo.misiones && operativo.misiones.length > 0">
          <v-list-item v-for="mision in operativo.misiones" :key="mision.codigo">
            <v-list-item-content>
              <v-list-item-title>Misión Código: {{ mision.codigo }}</v-list-item-title>
              <v-list-item-subtitle>Descripción: {{ mision.descripcion }}</v-list-item-subtitle>

              <!-- Equipos dentro de cada misión -->
              <v-list v-if="mision.equipos && mision.equipos.length > 0">
                <v-list-item v-for="equipo in mision.equipos" :key="equipo.equipoCodigo">
                  <v-list-item-content>
                    <v-list-item-title>Equipo Código: {{ equipo.equipoCodigo }}</v-list-item-title>
                    <v-list-item-subtitle>Tipo: {{ equipo.tipo }}</v-list-item-subtitle>
                    <v-list-item-text>Descripción: {{ equipo.descripcion }}</v-list-item-text>
                  </v-list-item-content>
                </v-list-item>
              </v-list>

              <v-card-text v-else>Sin equipos asignados</v-card-text>
            </v-list-item-content>
          </v-list-item>
        </v-list>

        <v-card-text v-else>Sin misiones asignadas</v-card-text>

        <!-- Botones de editar y eliminar -->
        <v-card-actions>
          <v-btn @click="$emit('edit', operativo, 'operativo')" color="primary">Editar</v-btn>
          <v-btn @click="$emit('delete', operativo, 'operativo')" color="error">Eliminar</v-btn>
        </v-card-actions>

      </v-card>
    </v-col>
  </v-row>
</template>

<script>
export default {
  name: 'OperativosList',
  props: {
    operativos: Array,
  },
};
</script>