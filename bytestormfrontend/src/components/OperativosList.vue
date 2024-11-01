<template>
  <v-row v-if="operativos.length > 0" dense>
    <v-col v-for="operativo in operativos" :key="operativo.id" cols="12" sm="6" md="4">
      <v-card outlined class="operativo-card">
        <v-card-title class="card-title">Operativo ID: {{ operativo.id }}</v-card-title>
        <v-card-subtitle class="card-subtitle">Nombre: {{ operativo.nombre }}</v-card-subtitle>
        <v-card-text>Rol: {{ operativo.rol }}</v-card-text>

        <!-- Misiones dentro del operativo -->
        <v-list v-if="operativo.misiones && operativo.misiones.length > 0" dense>
          <v-list-item v-for="mision in operativo.misiones" :key="mision.codigo" class="mision-item">
            <v-list-item-content>
              <v-list-item-title class="mision-title">Misión Código: {{ mision.codigo }}</v-list-item-title>
              <v-list-item-subtitle class="mision-subtitle">Descripción: {{ mision.descripcion }}</v-list-item-subtitle>

              <!-- Equipos dentro de cada misión -->
              <v-list v-if="mision.equipos && mision.equipos.length > 0" dense>
                <v-list-item v-for="equipo in mision.equipos" :key="equipo.equipoCodigo" class="equipo-item">
                  <v-list-item-content>
                    <v-list-item-title class="equipo-title">Equipo Código: {{ equipo.equipoCodigo }}</v-list-item-title>
                    <v-list-item-subtitle class="equipo-subtitle">Tipo: {{ equipo.tipo }}</v-list-item-subtitle>
                    <v-list-item-text>Descripción: {{ equipo.descripcion }}</v-list-item-text>
                  </v-list-item-content>
                </v-list-item>
              </v-list>

              <v-card-text v-else class="no-equipos-text">Sin equipos asignados</v-card-text>
            </v-list-item-content>
          </v-list-item>
        </v-list>

        <v-card-text v-else class="no-misiones-text">Sin misiones asignadas</v-card-text>

        <!-- Botones de editar y eliminar -->
        <v-card-actions class="card-actions">
          <v-btn @click="$emit('edit', operativo, 'operativo')" color="primary" class="action-btn">Editar</v-btn>
          <v-btn @click="$emit('delete', operativo, 'operativo')" color="error" class="action-btn">Eliminar</v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
export default {
  name: 'OperativosList',
  props: {
    operativos: Array,
  },
};
</script>

<style scoped>
.operativo-card {
  background-color: purple;
  color: #ecf0f1;
  border-radius: 8px;
  padding: 16px;
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.15);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.operativo-card:hover {
  transform: scale(1.03);
  box-shadow: 0px 6px 18px rgba(0, 0, 0, 0.3);
}

.card-title {
  font-size: 1.3rem;
  font-weight: bold;
  color: #ecf0f1;
}

.card-subtitle {
  font-size: 1rem;
  color: #bdc3c7;
  margin-top: 4px;
}

.mision-item {
  background-color: #34495e;
  border-radius: 6px;
  margin-bottom: 8px;
  padding: 8px;
}

.mision-title {
  font-size: 1.1rem;
  font-weight: 500;
  color: #ecf0f1;
}

.mision-subtitle {
  color: #bdc3c7;
}

.equipo-item {
  background-color: #3b5998;
  border-radius: 6px;
  margin: 4px 0;
  padding: 8px;
}

.equipo-title {
  font-size: 1rem;
  font-weight: 500;
  color: #ecf0f1;
}

.equipo-subtitle {
  color: #bdc3c7;
}

.no-misiones-text, .no-equipos-text {
  font-size: 0.9rem;
  color: #95a5a6;
  text-align: center;
  padding: 8px 0;
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