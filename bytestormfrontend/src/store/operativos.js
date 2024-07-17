import { defineStore } from 'pinia';
import apiClient from '../services/apiClient';

export const useOperativosStore = defineStore('operativos', {
  state: () => ({
    operativos: [],
  }),
  actions: {
    async fetchOperativos() {
      try {
        const response = await apiClient.get('/operativos');
        this.operativos = response.data;
      } catch (error) {
        console.error('Failed to fetch operativos:', error);
      }
    },
    async addOperativo(operativo) {
      try {
        const response = await apiClient.post('/operativos', operativo);
        this.operativos.push(response.data);
      } catch (error) {
        console.error('Failed to add operativo:', error);
      }
    },
    async updateOperativo(id, updatedOperativo) {
      try {
        await apiClient.put(`/operativos/${id}`, updatedOperativo);
        const index = this.operativos.findIndex(operativo => operativo.id === id);
        if (index !== -1) {
          this.operativos[index] = updatedOperativo;
        }
      } catch (error) {
        console.error('Failed to update operativo:', error);
      }
    },
    async deleteOperativo(id) {
      try {
        await apiClient.delete(`/operativos/${id}`);
        this.operativos = this.operativos.filter(operativo => operativo.id !== id);
      } catch (error) {
        console.error('Failed to delete operativo:', error);
      }
    },
  },
});