import { defineStore } from 'pinia';
import apiClient from '../services/apiClient';

export const useEquiposStore = defineStore('equipos', {
  state: () => ({
    equipos: [],
  }),
  actions: {
    async fetchEquipos() {
      try {
        const response = await apiClient.get('/equipos');
        this.equipos = response.data;
      } catch (error) {
        console.error('Failed to fetch equipos:', error);
      }
    },
    async addEquipo(equipo) {
      try {
        const response = await apiClient.post('/equipos', equipo);
        this.equipos.push(response.data);
      } catch (error) {
        console.error('Failed to add equipo:', error);
      }
    },
    async updateEquipo(id, updatedEquipo) {
      try {
        await apiClient.put(`/equipos/${id}`, updatedEquipo);
        const index = this.equipos.findIndex(equipo => equipo.id === id);
        if (index !== -1) {
          this.equipos[index] = updatedEquipo;
        }
      } catch (error) {
        console.error('Failed to update equipo:', error);
      }
    },
    async deleteEquipo(id) {
      try {
        await apiClient.delete(`/equipos/${id}`);
        this.equipos = this.equipos.filter(equipo => equipo.id !== id);
      } catch (error) {
        console.error('Failed to delete equipo:', error);
      }
    },
  },
});
