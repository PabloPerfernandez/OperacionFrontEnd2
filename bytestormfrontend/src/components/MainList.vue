<template>
  <v-container>
    <h1 class="app-title">OPERACIÓN BYTESTORM</h1>

    <!-- CrudForm -->
    <CrudForm
      :equipoSeleccionado="selectedEquipo"
      :operativoSeleccionado="selectedOperativo"
      :misionSeleccionada="selectedMision"
      @data-saved="handleDataSaved"
    />

    <h2 class="section-title">Lista de Operativos</h2>
    <OperativosList
      :operativos="operativos"
      @edit="editItem"
      @delete="deleteItem"
    />
    <v-divider class="my-5"></v-divider>
    <h2 class="section-title">Lista de Misiones</h2>
    <MisionesList
      :misiones="misiones"
      @edit="editItem"
      @delete="deleteItem"
    />

    <v-divider class="my-5"></v-divider>

    <h2 class="section-title">Lista de Equipos</h2>
    <EquiposList
      :equipos="equipos"
      @edit="editItem"
      @delete="deleteItem"
    />
  </v-container>
</template>

<script>
import { ref, onMounted } from "vue";
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

    /* const misionesFiltradas = computed(() => {
      return operativos.value
        .filter((operativo) => operativo.misiones && operativo.misiones.length > 0)
        .flatMap((operativo) =>
          operativo.misiones.map((mision) => ({
            ...mision,
            operativo,
          }))
        );
    }); */

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
    const editItem = (item, type) => {
      if (type == 'operativo') {
        selectedOperativo.value = item;
        selectedMision.value = null;
        selectedEquipo.value = null;
      } else if (type == 'mision') {
        selectedMision.value = item;
        selectedOperativo.value = null;
        selectedEquipo.value = null;
      } else if (type == 'equipo') {
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
        if (type === 'operativo' && item.id) {
          apiUrl = `http://localhost:5056/api/operativo/delete?id=${item.id}`;
        }
        // Eliminar Misión
        else if (type === 'mision' && item.codigo) {
          apiUrl = `http://localhost:5056/api/mision/delete?codigo=${item.codigo}`;
        }
        // Eliminar Equipo
        else if (type === 'equipo' && item.equipoCodigo) {
          apiUrl = `http://localhost:5056/api/equipo/delete?equipoCodigo=${item.equipoCodigo}`;
          console.log("URL de eliminación:", apiUrl);
        } else {
          throw new Error("No se puede determinar el tipo de ítem para eliminar.");
        }

        const response = await fetch(apiUrl, {
          method: "DELETE",
        });

        if (!response.ok) {
          throw new Error("Error al eliminar el ítem.");
        }

        handleDataSaved(); // Recargar datos tras la eliminación
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
      misiones,
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
.app-title {
  font-size: 2.5rem;
  color: #ffffff;
  text-align: center;
  margin-top: 20px;
  margin-bottom: 30px;
  font-weight: bold;
  text-shadow: 2px 2px #333;
}

.section-title {
  font-size: 1.8rem;
  color: #9A2B2B;
  margin-top: 30px;
  margin-bottom: 20px;
  text-transform: uppercase;
  letter-spacing: 1px;
  font-weight: bold;
}

.v-container {
  background-color: #5e1f1f;
  padding: 20px;
  border-radius: 10px;
}

.v-list-item-title {
  font-size: 1.2rem;
  color: #ffffff;
}

.v-list-item {
  background-color: #3e434c;
  margin-bottom: 8px;
  border-radius: 5px;
  padding: 10px;
}

.v-btn {
  margin: 5px;
  color: #ffffff;
}

.v-btn--primary {
  background-color: #5d9cec;
  border-radius: 8px;
  font-weight: bold;
  transition: background-color 0.3s ease;
}

.v-btn--primary:hover {
  background-color: #4a8be0;
}

.v-btn--icon {
  color: #9A2B2B;
}

.v-divider {
  background-color: #9A2B2B;
}

.v-form {
  background-color: #444a54;
  padding: 20px;
  border-radius: 8px;
  margin-bottom: 30px;
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.5);
}

.v-select, .v-text-field {
  background-color: #3e434c;
  color: #ffffff;
}

.v-select__selections, .v-text-field__slot {
  color: #ffffff;
}

.v-select__control, .v-text-field__control {
  border: 1px solid #5d9cec;
  border-radius: 8px;
}

.v-input__control:hover {
  border-color: #4a8be0;
}
</style>