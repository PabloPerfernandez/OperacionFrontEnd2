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
          v-model="formData.operativoID"
          label="ID del Operativo"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.nombre"
          label="Nombre del Operativo"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.rol"
          label="Rol del Operativo"
          required
        ></v-text-field>

        <v-select
          v-model="formData.misionAsignada"
          :items="misiones"
          label="Misión Asignada"
          required
        ></v-select>
      </template>

      <!-- Campos específicos para Misión -->
      <template v-if="formData.tipo === 'Misión'">
        <v-text-field
          v-model="formData.misionCodigo"
          label="Código de Misión"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.nombre"
          label="Nombre de la Misión"
          required
        ></v-text-field>

        <v-textarea
          v-model="formData.descripcion"
          label="Descripción de la Misión"
        ></v-textarea>
      </template>

      <!-- Campos específicos para Equipo -->
      <template v-if="formData.tipo === 'Equipo'">
        <v-text-field
          v-model="formData.EquipoCodigo"
          label="ID del Equipo"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.nombre"
          label="Nombre del Equipo"
          required
        ></v-text-field>

        <v-textarea
          v-model="formData.descripcion"
          label="Descripción del Equipo"
        ></v-textarea>
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
      operativoID: "",
      nombre: "",
      rol: "",
      misionAsignada: "",
      misionCodigo: "",
      EquipoCodigo: "",
      descripcion: "",
    });

    const misiones = ref([]);

    // Watchers para actualizar el formData cuando cambian las selecciones
    watch(
      () => props.equipoSeleccionado,
      (newVal) => {
        if (newVal) {
          formData.value = {
            tipo: "Equipo",
            EquipoCodigo: newVal.EquipoCodigo || "",
            nombre: newVal.nombre || "",
            descripcion: newVal.descripcion || "",
            rol: "",
            misionAsignada: "",
            operativoID: "",
            misionCodigo: "",
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
            operativoID: newVal.operativoID || "",
            nombre: newVal.nombre || "",
            rol: newVal.rol || "",
            misionAsignada: newVal.misionAsignada || "",
            EquipoCodigo: "",
            misionCodigo: "",
            descripcion: "",
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
            misionCodigo: newVal.misionCodigo || "",
            nombre: newVal.nombre || "",
            descripcion: newVal.descripcion || "",
            rol: "",
            misionAsignada: "",
            EquipoCodigo: "",
            operativoID: "",
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
        } else if (formData.value.tipo === "Mision") {
          if (props.misionSeleccionada) {
            apiUrl = `http://localhost:5056/api/mision/${props.misionSeleccionada.codigo}`;
            method = "PUT";
          } else {
            apiUrl = "http://localhost:5056/api/mision/crear";
          }
        } else if (formData.value.tipo === "Equipo") {
          if (props.equipoSeleccionado) {
            apiUrl = `http://localhost:5056/api/equipo/${props.equipoSeleccionado.EquipoCodigo}`;
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
        operativoID: "",
        nombre: "",
        rol: "",
        misionAsignada: "",
        misionCodigo: "",
        EquipoCodigo: "",
        descripcion: "",
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