import { defineStore } from 'pinia';
import apiClient from '../services/apiClient';

export const useMisionesStore = defineStore('misiones', {
  state: () => ({
    misiones: [],
  }),
  actions: {
    async fetchMisiones() {
      try {
        const response = await apiClient.get('/misiones');
        this.misiones = response.data;
      } catch (error) {
        console.error('Failed to fetch misiones:', error);
      }
    },
    async addMision(mision) {
      try {
        const response = await apiClient.post('/misiones', mision);
        this.misiones.push(response.data);
      } catch (error) {
        console.error('Failed to add mision:', error);
      }
    },
    async updateMision(id, updatedMision) {
      try {
        await apiClient.put(`/misiones/${id}`, updatedMision);
        const index = this.misiones.findIndex(mision => mision.id === id);
        if (index !== -1) {
          this.misiones[index] = updatedMision;
        }
      } catch (error) {
        console.error('Failed to update mision:', error);
      }
    },
    async deleteMision(id) {
      try {
        await apiClient.delete(`/misiones/${id}`);
        this.misiones = this.misiones.filter(mision => mision.id !== id);
      } catch (error) {
        console.error('Failed to delete mision:', error);
      }
    },
  },
});