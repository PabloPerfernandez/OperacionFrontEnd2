<template>
    <v-container class="d-flex justify-center">
    <h1>OPERACIÓN BYTESTORM</h1>
  </v-container>

  <v-container>
    <h2>Lista de Operativos</h2>

    <!-- Mostrar operativos en tarjetas -->
    <v-row v-if="operativos.length > 0" dense>
      <v-col
        v-for="operativo in operativos"
        :key="operativo.id"
        cols="12"
        sm="6"
        md="4"
      >
        <!-- Tarjeta de Operativo -->
        <v-card outlined>
          <v-card-title>
            Operativo ID: {{ operativo.id }}
          </v-card-title>
          <v-card-subtitle>
            Nombre: {{ operativo.nombre }}
          </v-card-subtitle>
          <v-card-text>
            Rol: {{ operativo.rol }}
          </v-card-text>

          <!-- Mostrar misiones del operativo -->
          <v-list v-if="operativo.misiones && operativo.misiones.length > 0">
            <v-list-item v-for="mision in operativo.misiones" :key="mision.codigo">
              <v-list-item-content>
                <v-list-item-title>
                  Código de Misión: {{ mision.codigo }}
                </v-list-item-title>
                <v-list-item-subtitle>
                  Descripción: {{ mision.descripcion }}
                </v-list-item-subtitle>

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
              </v-list-item-content>
            </v-list-item>
          </v-list>

          <!-- Si no tiene misiones asignadas -->
          <v-card-text v-else>Sin misiones asignadas</v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <!-- Mostrar todas las misiones en tarjetas debajo de los operativos -->
    <v-divider class="my-5"></v-divider>
    <h2>Lista de Misiones</h2>

    <v-row dense>
      <v-col v-for="mision in misionesFiltradas" :key="mision?.codigo" cols="12" sm="6" md="4">
        <v-card outlined>
          <v-card-title>Misión Código: {{ mision?.codigo }}</v-card-title>
          <v-card-subtitle>Asignada a: {{ mision?.operativo?.nombre }}</v-card-subtitle>
          <v-card-text>Descripción: {{ mision?.descripcion }}</v-card-text>

          <!-- Mostrar equipos asociados a la misión -->
          <v-list v-if="mision?.equipos && mision.equipos.length > 0">
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

    <!-- Nueva sección: Mostrar lista de equipos debajo de los operativos y misiones -->
    <v-divider class="my-5"></v-divider>
    <h2>Lista de Equipos</h2>

    <v-row dense>
      <v-col
        v-for="equipo in equiposFiltrados"
        :key="equipo?.equipoCodigo"
        cols="12"
        sm="6"
        md="4"
      >
        <v-card outlined>
          <v-card-title>Equipo Código: {{ equipo?.equipoCodigo }}</v-card-title>
          <v-card-subtitle>Tipo: {{ equipo?.tipo }}</v-card-subtitle>
          <v-card-text>Descripción: {{ equipo?.descripcion }}</v-card-text>
          <v-card-subtitle v-if="equipo?.mision">Asignado a Misión: {{ equipo?.mision?.codigo }}</v-card-subtitle>
        </v-card>
      </v-col>
    </v-row>

    <!-- Mensaje si no hay operativos con misiones -->
    <v-row v-if="misionesFiltradas.length === 0">
      <v-col>
        <v-alert type="info" border="left" colored-border elevation="2">
          No hay misiones asignadas a ningún operativo
        </v-alert>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "OperativoList",
  data() {
    return {
      operativos: [],
      misiones: [],
      equipos: [],
    };
  },
  computed: {
    misionesFiltradas() {
      return this.operativos
        .filter((operativo) => operativo.misiones && operativo.misiones.length > 0)
        .map((operativo) => {
          return operativo.misiones.map((mision) => ({
            ...mision,
            operativo,
          }));
        })
        .flat();
    },
    // Filtrar los equipos que tienen una misión asignada
    equiposFiltrados() {
      return this.equipos.filter((equipo) => equipo.misionID != null);
    },
  },
  mounted() {
    this.fetchOperativos();
    this.fetchMisiones();
    this.fetchEquipos();
  },
  methods: {
    async fetchOperativos() {
      try {
        const response = await fetch("http://localhost:5056/api/operativo");
        if (!response.ok) throw new Error("Error al obtener los operativos");
        this.operativos = await response.json();
      } catch (error) {
        console.error("Error:", error);
      }
    },
    async fetchMisiones() {
      try {
        const response = await fetch("http://localhost:5056/api/mision");
        if (!response.ok) throw new Error("Error al obtener las misiones");
        const misiones = await response.json();

        // Asignar las misiones al operativo correspondiente usando OperativoID
        this.operativos.forEach((operativo) => {
          operativo.misiones = misiones.filter((mision) => mision.operativoID === operativo.id);
        });
      } catch (error) {
        console.error("Error:", error);
      }
    },
    async fetchEquipos() {
      try {
        const response = await fetch("http://localhost:5056/api/equipo");
        if (!response.ok) throw new Error("Error al obtener los equipos");
        const equipos = await response.json();

        // Asignar los equipos a la misión correspondiente usando MisionID
        this.equipos = equipos.map((equipo) => {
          const mision = this.misiones.find((m) => m.codigo === equipo.misionID);
          return { ...equipo, mision };
        });
      } catch (error) {
        console.error("Error:", error);
      }
    },
  },
};
</script>

<style scoped>
</style>