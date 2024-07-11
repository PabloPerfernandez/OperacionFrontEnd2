<template>
    <v-container>
      <v-row>
        <v-col v-for="operativo in operativos" :key="operativo.id" cols="12" md="4">
          <v-card>
            <v-card-title>{{ operativo.nombre }}</v-card-title>
            <v-card-subtitle>{{ operativo.rol }}</v-card-subtitle>
            <v-card-actions>
              <v-btn @click="editOperativo(operativo)">Edit</v-btn>
              <v-btn @click="deleteOperativo(operativo.id)">Delete</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </template>
  
  <script>
  import { useOperativoStore } from '../store';
  
  export default {
    setup() {
      const operativoStore = useOperativoStore();
      operativoStore.fetchOperativos();
  
      return {
        operativos: operativoStore.operativos,
        editOperativo: operativoStore.editOperativo,
        deleteOperativo: operativoStore.deleteOperativo
      };
    }
  };
  </script>