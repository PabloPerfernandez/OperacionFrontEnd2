<template>
    <v-container>
      <v-row>
        <v-col v-for="operativo in operativos" :key="operativo.id" cols="12" md="6">
          <v-card>
            <v-card-title>{{ operativo.nombre }}</v-card-title>
            <v-card-subtitle>{{ operativo.rol }}</v-card-subtitle>
            <v-card-actions>
              <v-btn color="primary" @click="openEditDialog(operativo)">Edit</v-btn>
              <v-btn color="red" @click="deleteOperativo(operativo.id)">Delete</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
  
      <v-dialog v-model="editDialog" max-width="500px">
        <v-card>
          <v-card-title>
            <span class="headline">Edit Operativo</span>
          </v-card-title>
          <v-card-text>
            <v-form ref="editForm">
              <v-text-field
                v-model="editedOperativo.nombre"
                label="Nombre"
                required
              ></v-text-field>
              <v-text-field
                v-model="editedOperativo.rol"
                label="Rol"
                required
              ></v-text-field>
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="closeEditDialog">Cancel</v-btn>
            <v-btn color="blue darken-1" text @click="saveEdit">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-container>
  </template>
  
  <script>
  import { ref } from 'vue';
  import { useOperativosStore } from '../store/operativos';
  
  export default {
    setup() {
      const operativosStore = useOperativosStore();
      operativosStore.fetchOperativos();
  
      const editDialog = ref(false);
      const editedOperativo = ref(null);
  
      const openEditDialog = (operativo) => {
        editedOperativo.value = { ...operativo };
        editDialog.value = true;
      };
  
      const closeEditDialog = () => {
        editDialog.value = false;
        editedOperativo.value = null;
      };
  
      const saveEdit = async () => {
        if (editedOperativo.value) {
          await operativosStore.updateOperativo(editedOperativo.value.id, editedOperativo.value);
          closeEditDialog();
        }
      };
  
      return {
        operativos: operativosStore.operativos,
        deleteOperativo: operativosStore.deleteOperativo,
        editDialog,
        editedOperativo,
        openEditDialog,
        closeEditDialog,
        saveEdit
      };
    }
  };
  </script>  