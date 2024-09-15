<template>
  <v-container>
    <h1>Lista de Operativos</h1>
    <v-list>
      <template v-if="operativos.length > 0">
        <v-list-item
          v-for="operativo in operativos"
          :key="operativo.id"
        >
          <v-list-item-content>
            <v-list-item-title>{{ operativo.nombre }}</v-list-item-title>
            <v-list-item-subtitle>{{ operativo.rol }}</v-list-item-subtitle>

            <!-- Mostrar misiones -->
            <v-list v-if="operativo.misiones.length > 0">
              <v-list-item
                v-for="mision in operativo.misiones"
                :key="mision.id"
              >
                <v-list-item-content>
                  <v-list-item-title>Código: {{ mision.codigo }}</v-list-item-title>
                  <v-list-item-subtitle>Descripción: {{ mision.descripcion }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-list>

            <!-- Mensaje si no hay misiones -->
            <v-list-item v-else>
              <v-list-item-content>
                <v-list-item-title>Sin Misiones Asignadas</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-item-content>
        </v-list-item>
      </template>

      <!-- Mensaje si no hay operativos -->
      <template v-else>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>No hay operativos disponibles</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </template>
    </v-list>
  </v-container>
</template>

<script>
export default {
  name: 'OperativoList',
  data() {
    return {
      operativos: [],
    };
  },
  mounted() {
    this.fetchOperativos();
  },
  methods: {
    async fetchOperativos() {
      try {
        const response = await fetch('http://localhost:5056/api/operativo');
        if (!response.ok) throw new Error('Error al obtener los operativo');
        this.operativos = await response.json();
      } catch (error) {
        console.error('Error:', error);
      }
    },
  },
};
</script>

<style scoped>

</style>