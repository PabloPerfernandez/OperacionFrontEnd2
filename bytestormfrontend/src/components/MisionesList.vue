<template>
  <v-container>
    <v-row>
      <v-col v-for="mision in misiones" :key="mision.codigo" cols="12" md="6">
        <v-card>
          <v-card-title>{{ mision.descripcion }}</v-card-title>
          <v-card-subtitle>{{ mision.estado }}</v-card-subtitle>
          <v-card-actions>
            <v-btn color="primary" @click="openEditDialog(mision)">Edit</v-btn>
            <v-btn color="red" @click="deleteMision(mision.codigo)">Delete</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>

    <v-dialog v-model="editDialog" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">Edit Mision</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="editForm">
            <v-text-field
              v-model="editedMision.descripcion"
              label="Descripción"
              required
            ></v-text-field>
            <v-select
              v-model="editedMision.estado"
              :items="['Planificada', 'Activa', 'Completada']"
              label="Estado"
              required
            ></v-select>
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
import { useMisionesStore } from '../store/misiones';

export default {
  setup() {
    const misionesStore = useMisionesStore();
    misionesStore.fetchMisiones();

    const editDialog = ref(false);
    const editedMision = ref(null);

    const openEditDialog = (mision) => {
      editedMision.value = { ...mision };
      editDialog.value = true;
    };

    const closeEditDialog = () => {
      editDialog.value = false;
      editedMision.value = null;
    };

    const saveEdit = async () => {
      if (editedMision.value) {
        await misionesStore.updateMision(editedMision.value.codigo, editedMision.value);
        closeEditDialog();
      }
    };

    return {
      misiones: misionesStore.misiones,
      deleteMision: misionesStore.deleteMision,
      editDialog,
      editedMision,
      openEditDialog,
      closeEditDialog,
      saveEdit
    };
  }
};
</script>