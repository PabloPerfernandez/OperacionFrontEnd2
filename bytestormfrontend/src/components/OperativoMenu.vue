<template>
    <v-container>
      <!-- Itera sobre los operativos -->
      <v-row v-for="operativo in operativos" :key="operativo.ID">
        <v-col>
          <!-- Botón del menú desplegable para cada Operativo -->
          <v-menu
            offset-y
            close-on-click
            v-slot:activator="{ on, attrs }"
          >
            <v-btn
              v-bind="attrs"
              v-on="on"
              color="primary"
              outlined
            >
              {{ operativo.Nombre }} - {{ operativo.Rol }}
            </v-btn>
  
            <!-- Lista desplegable de Misiones -->
            <v-list>
              <v-list-item
                v-if="operativo.Misiones.length > 0"
                v-for="mision in operativo.Misiones"
                :key="mision.Codigo"
              >
                <v-list-item-content @click="mostrarMision(mision)">
                  <v-list-item-title>
                    Misión Código: {{ mision.Codigo }}
                  </v-list-item-title>
                  <v-list-item-subtitle>
                    Estado: {{ mision.Estado }}
                  </v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
              <v-list-item v-else>
                <v-list-item-content>
                  <v-list-item-title>
                    Sin Misiones Asignadas
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-menu>
        </v-col>
      </v-row>
  
      <!-- Modal para mostrar detalles de la misión seleccionada -->
      <v-dialog v-model="dialog" max-width="500px">
        <v-card>
          <v-card-title>Misión Detalles</v-card-title>
          <v-card-text v-if="misionSeleccionada">
            <p><strong>Código:</strong> {{ misionSeleccionada.Codigo }}</p>
            <p><strong>Descripción:</strong> {{ misionSeleccionada.Descripcion }}</p>
            <p><strong>Estado:</strong> {{ misionSeleccionada.Estado }}</p>
            <!-- Agrega más detalles según tu modelo de misión -->
          </v-card-text>
          <v-card-actions>
            <v-btn color="primary" text @click="dialog = false">Cerrar</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-container>
  </template>
  
  <script>
  export default {
    data() {
      return {
        // Datos prueba
        operativos: [
          {
            ID: 1,
            Nombre: 'Operativo 1',
            Rol: 'Rol A',
            Misiones: [
              { Codigo: 101, Descripcion: 'Misión 1', Estado: 'Activo' },
              { Codigo: 102, Descripcion: 'Misión 2', Estado: 'Completado' },
            ],
          },
          {
            ID: 2,
            Nombre: 'Operativo 2',
            Rol: 'Rol B',
            Misiones: [],
          },
        ],
        dialog: false,
        misionSeleccionada: null,
      };
    },
    methods: {
      // Diálogo con los detalles de la misión
      mostrarMision(mision) {
        this.misionSeleccionada = mision;
        this.dialog = true;
      },
    },
  };
  </script>
  
  <style scoped>
  
  </style>  