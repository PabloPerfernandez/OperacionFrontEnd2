<template>
  <v-container>
    <v-form @submit.prevent="submitForm">
      <v-select
        v-model="formData.tipo"
        :items="['Operativo', 'Misión', 'Equipo']"
        label="Tipo de Entrada"
        required
      ></v-select>

      <!-- Campos específicos para Operativo -->
      <template v-if="formData.tipo === 'Operativo'">
        <v-text-field
          v-model="formData.id"
          label="ID del Operativo"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.Nombre"
          label="Nombre del Operativo"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.Rol"
          label="Rol del Operativo"
          required
        ></v-text-field>

        <v-select
          v-model="formData.misionAsignada"
          :items="Misiones"
          label="Misión Asignada"
          required
        ></v-select>
      </template>

      <!-- Campos específicos para Misión -->
      <template v-if="formData.tipo === 'Misión'">
        <v-text-field
          v-model="formData.codigo"
          label="Código de misión"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.Descripcion"
          label="Descripción de la misión"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.Estado"
          label="Estado de la misión"
        ></v-text-field>

        <v-text-field
          v-model="formData.OperativoID"
          label="ID del Operativo asignado a la misión"
        ></v-text-field>
      </template>

      <!-- Campos específicos para Equipo -->
      <template v-if="formData.tipo === 'Equipo'">
        <v-text-field
          v-model="formData.equipoCodigo"
          label="ID del Equipo"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.equipoTipo"
          label="Tipo de Equipo"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.equipoDescripcion"
          label="Descripción del Equipo"
        ></v-text-field>

        <v-text-field
          v-model="formData.EstadoEquipo"
          label="Estado del Equipo"
        ></v-text-field>
      </template>

      <!-- Botón de enviar -->
      <v-btn type="submit" color="primary">
        {{ equipoSeleccionado || operativoSeleccionado || misionSeleccionada ? 'Actualizar' : 'Guardar' }}
      </v-btn>
    </v-form>
  </v-container>
</template>

<script>
import { ref, watch, defineComponent } from "vue";

export default defineComponent({
  props: {
    equipoSeleccionado: {
      type: Object,
      default: null,
    },
    operativoSeleccionado: {
      type: Object,
      default: null,
    },
    misionSeleccionada: {
      type: Object,
      default: null,
    },
  },
  setup(props, { emit }) {
    const formData = ref({
      tipo: "",
      id: "",
      nombre: "",
      rol: "",
      codigo: "",
      Descripcion: "",
      Estado: "",
      OperativoID: "",
      misionAsignada: "",
      equipoCodigo: "",
      equipoTipo: "",
      equipoDescripcion: "",
    });

    const misiones = ref([]);

    // Watchers para actualizar el formData cuando cambian las selecciones
    watch(
      () => props.equipoSeleccionado,
      (newVal) => {
        if (newVal) {
          formData.value = {
            tipo: "Equipo",
            equipoCodigo: newVal.equipoCodigo || "",
            equipoTipo: newVal.equipoTipo || "",
            equipoDescripcion: newVal.equipoDescripcion || "",
          };
        }
      },
      { immediate: true }
    );

    watch(
      () => props.operativoSeleccionado,
      (newVal) => {
        if (newVal) {
          formData.value = {
            tipo: "Operativo",
            id: newVal.id || "",
            nombre: newVal.nombre || "",
            rol: newVal.rol || "",
            misionAsignada: newVal.misionAsignada || "",
          };
        }
      },
      { immediate: true }
    );

    watch(
      () => props.misionSeleccionada,
      (newVal) => {
        if (newVal) {
          formData.value = {
            tipo: "Misión",
            codigo: newVal.codigo || "",
            Estado: newVal.Estado || "",
            Descripcion: newVal.Descripcion || "",
            OperativoID: newVal.OperativoID || "",
          };
        }
      },
      { immediate: true }
    );

    const submitForm = async () => {
      try {
        let apiUrl = "";
        let method = "POST";

        if (formData.value.tipo === "Operativo") {
          if (props.operativoSeleccionado) {
            apiUrl = `http://localhost:5056/api/operativo/${props.operativoSeleccionado.id}`;
            method = "PUT";
          } else {
            apiUrl = "http://localhost:5056/api/operativo/crear";
          }
        } else if (formData.value.tipo === "Misión") {
          if (props.misionSeleccionada) {
            apiUrl = `http://localhost:5056/api/mision/${props.misionSeleccionada.codigo}`;
            method = "PUT";
          } else {
            apiUrl = "http://localhost:5056/api/mision/crear";
          }
        } else if (formData.value.tipo === "Equipo") {
          if (props.equipoSeleccionado) {
            apiUrl = `http://localhost:5056/api/equipo/${props.equipoSeleccionado.equipoCodigo}`;
            method = "PUT";
          } else {
            apiUrl = "http://localhost:5056/api/equipo/crear";
          }
        }

        const response = await fetch(apiUrl, {
          method: method,
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(formData.value),
        });

        if (!response.ok) {
          throw new Error("Error al guardar los datos");
        }

        emit("data-saved");
        resetForm();
      } catch (error) {
        console.error("Error al guardar los datos:", error);
      }
    };

    const resetForm = () => {
      formData.value = {
        tipo: "",
        id: "",
        nombre: "",
        rol: "",
        codigo: "",
        Descripcion: "",
        Estado: "",
        OperativoID: "",
        misionAsignada: "",
        equipoCodigo: "",
        equipoTipo: "",
        equipoDescripcion: "",
      };
    };

    return {
      formData,
      misiones,
      submitForm,
      resetForm,
    };
  },
});
</script>