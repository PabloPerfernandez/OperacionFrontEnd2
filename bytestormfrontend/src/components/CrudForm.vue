<template>
  <v-card outlined>
    <v-card-title>Formulario CRUD</v-card-title>
    <v-card-text>
      <v-form>
        <!-- Campos del formulario -->
        <v-text-field
          v-model="formData.nombre"
          label="Nombre"
          required
        ></v-text-field>

        <v-text-field
          v-model="formData.descripcion"
          label="Descripción"
          required
        ></v-text-field>

        <v-select
          v-model="formData.tipo"
          :items="['Operativo', 'Misión', 'Equipo']"
          label="Tipo"
          required
        ></v-select>

        <!-- Botón de envío -->
        <v-btn color="primary" @click="submitForm">Guardar</v-btn>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  name: 'CrudForm',
  data() {
    return {
      formData: {
        nombre: '',
        descripcion: '',
        tipo: '',
      },
    };
  },
  methods: {
    async submitForm() {
  try {
    const response = await fetch('http://localhost:5056/api/crear', { // Cambia a 5056
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.formData),
    });

    if (!response.ok) {
      throw new Error('Error al guardar los datos');
    }

    const result = await response.json();
    console.log('Datos guardados correctamente:', result);

    // Emitir un evento para que MainList.vue recargue los datos
    this.$emit('data-saved');

    // Reiniciar el formulario
    this.formData = { nombre: '', descripcion: '', tipo: '' };
  } catch (error) {
    console.error('Error al guardar los datos:', error);
  }
},


        // Emitir un evento para que MainList.vue recargue los datos
        this.$emit('data-saved');

        // Reiniciar el formulario
        this.formData = { nombre: '', descripcion: '', tipo: '' };
      } catch (error) {
        console.error('Error al guardar los datos:', error);
      }
    },
  },
};
</script>

<style scoped>
/* Estilos personalizados */
</style>