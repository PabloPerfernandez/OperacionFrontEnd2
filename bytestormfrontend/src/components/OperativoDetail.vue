<!-- src/components/OperativoDetail.vue -->
<template>
    <div>
      <h2>Detalles del Operativo</h2>
      <p><strong>Nombre:</strong> {{ operativo.nombre }}</p>
      <p><strong>Rol:</strong> {{ operativo.rol }}</p>
      <p><strong>Misiones Asignadas:</strong> {{ operativo.misionAsignada }}</p>
      <router-link to="/">Volver a la lista</router-link>
    </div>
</template>
  
<script>
  import axios from 'axios';
  
  export default {
    props: ['id'],
    data() {
      return {
        operativo: {},
      };
    },
    created() {
      this.fetchOperativo();
    },
    methods: {
      async fetchOperativo() {
        try {
          const response = await axios.get(`http://localhost:5000/api/operativos/${this.id}`); // Ajusta la URL
          this.operativo = response.data;
        } catch (error) {
          console.error('Error fetching operativo:', error);
        }
      },
    },
  };
</script>

<style scoped>

</style>