<template>
  <v-container>
    <h1>OPERACIÓN BYTESTORM</h1>

    <!-- Componente CRUD para agregar o editar datos -->
    <CrudForm
      :equipoSeleccionado="selectedEquipo"
      :operativoSeleccionado="selectedOperativo"
      :misionSeleccionada="selectedMision"
      @data-saved="handleDataSaved"
    />

    <h2>Lista de Operativos</h2>
    <OperativosList
      :operativos="operativos"
      @edit="editItem"
      @delete="deleteItem"
    />
    <v-divider class="my-5"></v-divider>

    <h2>Lista de Misiones</h2>
    <MisionesList
      :misiones="misionesFiltradas"
      @edit="editItem"
      @delete="deleteItem"
    />

    <v-divider class="my-5"></v-divider>

    <h2>Lista de Equipos</h2>
    <EquiposList
      :equipos="equipos"
      @edit="editItem"
      @delete="deleteItem"
    />
  </v-container>
</template>

<script>
import { ref, onMounted, computed } from "vue";
import OperativosList from "./OperativosList.vue";
import MisionesList from "./MisionesList.vue";
import EquiposList from "./EquiposList.vue";
import CrudForm from "./CrudForm.vue";

export default {
  name: "MainList",
  components: {
    OperativosList,
    MisionesList,
    EquiposList,
    CrudForm,
  },
  setup() {
    const operativos = ref([]);
    const misiones = ref([]);
    const equipos = ref([]);
    const selectedOperativo = ref(null);
    const selectedMision = ref(null);
    const selectedEquipo = ref(null);

    // Función para filtrar y mapear las misiones con sus operativos
    const misionesFiltradas = computed(() => {
      return operativos.value
        .filter((operativo) => operativo.misiones && operativo.misiones.length > 0)
        .flatMap((operativo) =>
          operativo.misiones.map((mision) => ({
            ...mision,
            operativo,
          }))
        );
    });

    // Función para obtener los operativos
    const fetchOperativos = async () => {
      try {
        const response = await fetch("http://localhost:5056/api/operativo");
        if (!response.ok) throw new Error("Error al obtener los operativos");
        operativos.value = await response.json();
      } catch (error) {
        console.error("Error:", error);
      }
    };

    // Función para obtener las misiones
    const fetchMisiones = async () => {
      try {
        const response = await fetch("http://localhost:5056/api/mision");
        if (!response.ok) throw new Error("Error al obtener las misiones");
        misiones.value = await response.json();

        // Asignar las misiones al operativo correspondiente usando OperativoID
        operativos.value.forEach((operativo) => {
          operativo.misiones = misiones.value.filter(
            (mision) => mision.operativoID === operativo.id
          );
        });
      } catch (error) {
        console.error("Error:", error);
      }
    };

    // Función para obtener los equipos
    const fetchEquipos = async () => {
      try {
        const response = await fetch("http://localhost:5056/api/equipo");
        if (!response.ok) throw new Error("Error al obtener los equipos");
        equipos.value = await response.json();

        // Asignar los equipos a la misión correspondiente usando MisionID
        equipos.value = equipos.value.map((equipo) => {
          const mision = misiones.value.find((m) => m.codigo === equipo.misionID);
          return { ...equipo, mision };
        });
      } catch (error) {
        console.error("Error:", error);
      }
    };

    // Función para manejar la edición de un ítem (equipo, operativo o misión)
    const editItem = (item) => {
      if (item.operativoID) {
        selectedOperativo.value = item;
        selectedMision.value = null;
        selectedEquipo.value = null;
      } else if (item.misionCodigo) {
        selectedMision.value = item;
        selectedOperativo.value = null;
        selectedEquipo.value = null;
      } else {
        selectedEquipo.value = item;
        selectedOperativo.value = null;
        selectedMision.value = null;
      }
    };

    // Función para eliminar un ítem (equipo, operativo o misión)
    const deleteItem = async (item) => {
      try {
        let apiUrl = "";

        if (item.operativoID) {
          apiUrl = `http://localhost:5056/api/operativo/delete/${item.operativoID}`;
        } else if (item.misionCodigo) {
          apiUrl = `http://localhost:5056/api/mision/delete/${item.misionCodigo}`;
        } else {
          apiUrl = `http://localhost:5056/api/equipo0/delete/${item.equipoID}`;
        }

        const response = await fetch(apiUrl, {
          method: "DELETE",
        });

        if (!response.ok) {
          throw new Error("Error al eliminar el ítem");
        }

        handleDataSaved(); // Recargar los datos después de eliminar
      } catch (error) {
        console.error("Error al eliminar el ítem:", error);
      }
    };

    // Función que se ejecuta después de guardar o eliminar datos
    const handleDataSaved = () => {
      selectedOperativo.value = null;
      selectedMision.value = null;
      selectedEquipo.value = null;
      fetchOperativos();
      fetchMisiones();
      fetchEquipos();
    };

    // Llamar a las funciones fetch cuando el componente se monta
    onMounted(() => {
      fetchOperativos();
      fetchMisiones();
      fetchEquipos();
    });

    return {
      operativos,
      misionesFiltradas,
      equipos,
      selectedOperativo,
      selectedMision,
      selectedEquipo,
      editItem, // Método para editar el ítem
      deleteItem, // Método para eliminar el ítem
      handleDataSaved, // Método para recargar los datos después de guardar o eliminar
    };
  },
};
</script>

<style scoped>
main {
  background-color: #9A2B2B;
}
</style>