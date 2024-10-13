<template>
  <v-container>
    <h1>OPERACIÓN BYTESTORM</h1>

    <!-- CudForm -->
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

    // Obtener los operativos
    const fetchOperativos = async () => {
      try {
        const response = await fetch("http://localhost:5056/api/operativo");
        if (!response.ok) throw new Error("Error al obtener los operativos");
        operativos.value = await response.json();
      } catch (error) {
        console.error("Error:", error);
      }
    };

    // Obtener las misiones
    const fetchMisiones = async () => {
      try {
        const response = await fetch("http://localhost:5056/api/mision");
        if (!response.ok) throw new Error("Error al obtener las misiones");
        misiones.value = await response.json();

        operativos.value.forEach((operativo) => {
          operativo.misiones = misiones.value.filter(
            (mision) => mision.operativoID === operativo.id
          );
        });
      } catch (error) {
        console.error("Error:", error);
      }
    };

    // Obtener los equipos
    const fetchEquipos = async () => {
      try {
        const response = await fetch("http://localhost:5056/api/equipo");
        if (!response.ok) throw new Error("Error al obtener los equipos");
        equipos.value = await response.json();

        equipos.value = equipos.value.map((equipo) => {
          const mision = misiones.value.find((m) => m.codigo === equipo.misionID);
          return { ...equipo, mision };
        });
      } catch (error) {
        console.error("Error:", error);
      }
    };

    // Edición de ítem
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

    // Eliminar
    const deleteItem = async (item, type) => {
      try {
        console.log("valor item", item);
        console.log("valor type", type);
        
        if (!item) {
          throw new Error("Ítem no válido o no encontrado para eliminar.");
        }

        let apiUrl = "";

        // Eliminar Operativo
        if (type == 'operativo' && item.id) {
          apiUrl = `http://localhost:5056/api/operativo/delete?id=${item.id}`;
        }
        // Eliminar Misión
        else if (type == 'mision' && item.codigo) {
          apiUrl = `http://localhost:5056/api/mision/delete?codigo=${item.codigo}`;
        }
        // Eliminar Equipo
        else if (type == 'equipo' && item.EquipoCodigo) {
          apiUrl = `http://localhost:5056/api/equipo/delete?EquipoCodigo=${item.EquipoCodigo}`;
        } else {
          throw new Error("No se puede determinar el tipo de ítem para eliminar.");
        }

        const response = await fetch(apiUrl, {
          method: "DELETE",
        });

        if (!response.ok) {
          throw new Error("Error al eliminar el ítem.");
        }

        handleDataSaved();
      } catch (error) {
        console.error("Error al eliminar el ítem:", error.message);
      }
    };

    const handleDataSaved = () => {
      selectedOperativo.value = null;
      selectedMision.value = null;
      selectedEquipo.value = null;
      fetchOperativos();
      fetchMisiones();
      fetchEquipos();
    };

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
      editItem,
      deleteItem,
      handleDataSaved,
    };
  },
};
</script>

<style scoped>
main {
  background-color: #9A2B2B;
}
</style>