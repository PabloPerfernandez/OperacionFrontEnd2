<template>
  <v-row v-if="equipos.length > 0" dense>
    <v-col v-for="equipo in equipos" :key="equipo.equipoCodigo" cols="12" sm="6" md="4">
      <v-card outlined class="equipo-card">
        <v-card-title class="card-title">Equipo ID: {{ equipo.equipoCodigo }}</v-card-title>
        <v-card-text class="card-description">{{ equipo.descripcion || 'Sin descripción' }}</v-card-text>
        <v-card-subtitle class="card-subtitle">Tipo: {{ equipo.tipo }}</v-card-subtitle>

        <!-- Botones de editar y eliminar -->
        <v-card-actions class="card-actions">
          <v-btn @click="$emit('edit', equipo, 'equipo')" color="primary" class="action-btn">Editar</v-btn>
          <v-btn @click="$emit('delete', equipo, 'equipo')" color="error" class="action-btn">Eliminar</v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
export default {
  name: "EquiposList",
  props: {
    equipos: {
      type: Array,
      required: true,
    },
  },
};
</script>

<style scoped>
.equipo-card {
  background-color: blue;
  color: #f7f9fc;
  border-radius: 8px;
  padding: 16px;
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.15);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.equipo-card:hover {
  transform: scale(1.03);
  box-shadow: 0px 6px 18px rgba(0, 0, 0, 0.3);
}

.card-title {
  font-size: 1.3rem;
  font-weight: bold;
  color: #f7f9fc;
}

.card-description {
  font-size: 1rem;
  color: #d1d8e0;
  margin-top: 8px;
}

.card-subtitle {
  font-size: 1rem;
  color: #a5b1c2;
  margin-top: 4px;
}

.card-actions {
  display: flex;
  justify-content: flex-end;
  gap: 8px;
}

.action-btn {
  color: #ffffff;
  font-weight: 500;
}
</style>