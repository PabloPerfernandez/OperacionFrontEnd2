<template>
  <v-container>
    <v-form @submit.prevent="submitForm">
      <!-- Campo común para el tipo de entrada -->
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
      </template>

      <!-- Campos específicos para Equipo -->
      <template v-if="formData.tipo === 'Equipo'">
        <v-text-field
          v-model="formData.equipoID"
          label="ID del Equipo"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.nombre"
          label="Nombre del Equipo"
          required
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
export default {
  props: {
    equipoSeleccionado: {
      type: Object,
      default: null
    },
    operativoSeleccionado: {
      type: Object,
      default: null
    },
    misionSeleccionada: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      formData: {
        tipo: '', // 'Operativo', 'Misión', 'Equipo'
        operativoID: '', // Para Operativo
        nombre: '',
        rol: '', // Para Operativo
        misionAsignada: '', // Para Operativo
        misionCodigo: '', // Para Misión
        equipoID: '', // Para Equipo
      },
      misiones: [] // Aquí puedes cargar las misiones disponibles
    };
  },
  watch: {
    // Cargar los datos del elemento seleccionado en el formulario
    equipoSeleccionado: {
      immediate: true,
      handler(newVal) {
        if (newVal) {
          this.formData.tipo = 'Equipo';
          this.formData.equipoID = newVal.equipoID;
          this.formData.nombre = newVal.nombre;
        }
      }
    },
    operativoSeleccionado: {
      immediate: true,
      handler(newVal) {
        if (newVal) {
          this.formData.tipo = 'Operativo';
          this.formData.operativoID = newVal.operativoID;
          this.formData.nombre = newVal.nombre;
          this.formData.rol = newVal.rol;
          this.formData.misionAsignada = newVal.misionAsignada;
        }
      }
    },
    misionSeleccionada: {
      immediate: true,
      handler(newVal) {
        if (newVal) {
          this.formData.tipo = 'Misión';
          this.formData.misionCodigo = newVal.misionCodigo;
          this.formData.nombre = newVal.nombre;
        }
      }
    }
  },
  methods: {
    async submitForm() {
      try {
        let apiUrl = '';
        let method = 'POST';

        if (this.formData.tipo === 'Operativo') {
          if (this.operativoSeleccionado) {
            apiUrl = `http://localhost:5056/api/operativo/${this.operativoSeleccionado.operativoID}`;
            method = 'PUT';
          } else {
            apiUrl = 'http://localhost:5056/api/operativo/crear';
          }
        } else if (this.formData.tipo === 'Misión') {
          if (this.misionSeleccionada) {
            apiUrl = `http://localhost:5056/api/mision/${this.misionSeleccionada.misionCodigo}`;
            method = 'PUT';
          } else {
            apiUrl = 'http://localhost:5056/api/mision/crear';
          }
        } else if (this.formData.tipo === 'Equipo') {
          if (this.equipoSeleccionado) {
            apiUrl = `http://localhost:5056/api/equipo/${this.equipoSeleccionado.equipoID}`;
            method = 'PUT';
          } else {
            apiUrl = 'http://localhost:5056/api/equipo/crear';
          }
        }

        const response = await fetch(apiUrl, {
          method: method,
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            nombre: this.formData.nombre,
            ...(this.formData.tipo === 'Operativo' && {
              operativoID: this.formData.operativoID,
              rol: this.formData.rol,
              misionAsignada: this.formData.misionAsignada
            }),
            ...(this.formData.tipo === 'Misión' && {
              misionCodigo: this.formData.misionCodigo
            }),
            ...(this.formData.tipo === 'Equipo' && {
              equipoID: this.formData.equipoID
            })
          })
        });

        if (!response.ok) {
          throw new Error('Error al guardar los datos');
        }

        this.$emit('data-saved');
        this.resetForm();
      } catch (error) {
        console.error('Error al guardar los datos:', error);
      }
    },
    resetForm() {
      this.formData = {
        tipo: '',
        operativoID: '',
        nombre: '',
        rol: '',
        misionAsignada: '',
        misionCodigo: '',
        equipoID: ''
      };
    }
  }
};
</script>