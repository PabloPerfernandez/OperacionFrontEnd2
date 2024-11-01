<template>
  <v-row v-if="misiones.length > 0" dense>
    <v-col v-for="mision in misiones" :key="mision.codigo" cols="12" sm="6" md="4">
      <v-card outlined class="mision-card">
        <v-card-title class="card-title">Misión Código: {{ mision.codigo }}</v-card-title>
        <v-card-subtitle class="card-subtitle">Asignada a: {{ mision.operativo?.nombre || 'No asignada' }}</v-card-subtitle>
        <v-card-text class="card-description">Descripción: {{ mision.descripcion || 'Sin descripción' }}</v-card-text>

        <!-- Mostrar equipos asociados a la misión -->
        <v-list v-if="mision.equipos && mision.equipos.length > 0" class="equipo-list">
          <v-list-item v-for="equipo in mision.equipos" :key="equipo.equipoCodigo" class="equipo-item">
            <v-list-item-content>
              <v-list-item-title class="equipo-title">Equipo Código: {{ equipo.equipoCodigo }}</v-list-item-title>
              <v-list-item-subtitle class="equipo-subtitle">Tipo: {{ equipo.tipo }}</v-list-item-subtitle>
              <v-list-item-text class="equipo-description">Descripción: {{ equipo.descripcion || 'Sin descripción' }}</v-list-item-text>
            </v-list-item-content>
          </v-list-item>
        </v-list>
        
        <v-card-text v-else class="no-equipo-text">Sin equipos asignados</v-card-text>

        <!-- Botones de editar y eliminar -->
        <v-card-actions class="card-actions">
          <v-btn @click="$emit('edit', mision, 'mision')" color="primary" class="action-btn">Editar</v-btn>
          <v-btn @click="$emit('delete', mision, 'mision')" color="error" class="action-btn">Eliminar</v-btn>
        </v-card-actions>

      </v-card>
    </v-col>
  </v-row>
</template>

<script>
export default {
  name: "MisionesList",
  props: {
    misiones: Array,
  },
};
</script>

<style scoped>
.mision-card {
  background-color: greenyellow;
  color: #f7f9fc;
  border-radius: 8px;
  padding: 16px;
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.15);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.mision-card:hover {
  transform: scale(1.03);
  box-shadow: 0px 6px 18px rgba(0, 0, 0, 0.3);
}

.card-title {
  font-size: 1.3rem;
  font-weight: bold;
  color: #e0e4f0;
}

.card-subtitle {
  font-size: 1rem;
  color: #a5b1c2;
  margin-top: 4px;
}

.card-description {
  font-size: 1rem;
  color: #d1d8e0;
  margin-top: 8px;
}

.equipo-list {
  margin-top: 8px;
}

.equipo-item {
  background-color: #51557e;
  color: #f7f9fc;
  padding: 8px;
  border-radius: 5px;
  margin-bottom: 5px;
}

.equipo-title {
  font-size: 1rem;
  font-weight: bold;
  color: #e0e4f0;
}

.equipo-subtitle {
  font-size: 0.9rem;
  color: #c1c8e4;
}

.equipo-description {
  font-size: 0.9rem;
  color: #d1d8e0;
}

.no-equipo-text {
  color: #a5b1c2;
  font-style: italic;
  margin-top: 8px;
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