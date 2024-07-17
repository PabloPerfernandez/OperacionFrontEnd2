<template>
  <v-container>
    <v-row>
      <v-col v-for="equipo in equipos" :key="equipo.id" cols="12" md="6">
        <v-card>
          <v-card-title>{{ equipo.tipo }}</v-card-title>
          <v-card-subtitle>{{ equipo.descripcion }}</v-card-subtitle>
          <v-card-actions>
            <v-btn color="primary" @click="openEditDialog(equipo)">Edit</v-btn>
            <v-btn color="red" @click="deleteEquipo(equipo.id)">Delete</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>

    <v-dialog v-model="editDialog" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">Edit Equipo</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="editForm">
            <v-text-field
              v-model="editedEquipo.tipo"
              label="Tipo"
              required
            ></v-text-field>
            <v-text-field
              v-model="editedEquipo.descripcion"
              label="Descripción"
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
import { useEquiposStore } from '../store/equipos';

export default {
  setup() {
    const equiposStore = useEquiposStore();
    equiposStore.fetchEquipos();

    const editDialog = ref(false);
    const editedEquipo = ref(null);

    const openEditDialog = (equipo) => {
      editedEquipo.value = { ...equipo };
      editDialog.value = true;
    };

    const closeEditDialog = () => {
      editDialog.value = false;
      editedEquipo.value = null;
    };

    const saveEdit = async () => {
      if (editedEquipo.value) {
        await equiposStore.updateEquipo(editedEquipo.value.id, editedEquipo.value);
        closeEditDialog();
      }
    };

    return {
      equipos: equiposStore.equipos,
      deleteEquipo: equiposStore.deleteEquipo,
      editDialog,
      editedEquipo,
      openEditDialog,
      closeEditDialog,
      saveEdit
    };
  }
};
</script>