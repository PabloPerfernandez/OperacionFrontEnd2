<template>
  <v-container>
    <h1>OPERACIÓN BYTESTORM</h1>

    <!-- Componente CRUD para agregar nuevos datos -->
    <CrudForm @data-saved="fetchEquipos" />

    <h2>Lista de Operativos</h2>
    <OperativosList :operativos="operativos" />

    <v-divider class="my-5"></v-divider>

    <h2>Lista de Misiones</h2>
    <MisionesList :misiones="misionesFiltradas" />

    <v-divider class="my-5"></v-divider>

    <h2>Lista de Equipos</h2>
    <EquiposList :equipos="equipos" />
  </v-container>
</template>

<script>
import OperativosList from './OperativosList.vue';
import MisionesList from './MisionesList.vue';
import EquiposList from './EquiposList.vue';
import CrudForm from './CrudForm.vue';

export default {
  name: "MainList",
  components: {
    OperativosList,
    MisionesList,
    EquiposList,
    CrudForm
  },
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
        .flatMap((operativo) => 
          operativo.misiones.map((mision) => ({
            ...mision,
            operativo,
          }))
        );
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
        this.misiones = await response.json();

        // Asignar las misiones al operativo correspondiente usando OperativoID
        this.operativos.forEach((operativo) => {
          operativo.misiones = this.misiones.filter((mision) => mision.operativoID === operativo.id);
        });
      } catch (error) {
        console.error("Error:", error);
      }
    },
    async fetchEquipos() {
      try {
        const response = await fetch("http://localhost:5056/api/equipo");
        if (!response.ok) throw new Error("Error al obtener los equipos");
        this.equipos = await response.json();

        // Asignar los equipos a la misión correspondiente usando MisionID
        this.equipos = this.equipos.map((equipo) => {
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
/* Puedes añadir estilos aquí si es necesario */
</style>