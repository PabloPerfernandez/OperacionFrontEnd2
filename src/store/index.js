import { defineStore } from 'pinia';

export const useOperativoStore = defineStore('operativo', {
  state: () => ({
    operativos: []
  }),
  actions: {
    async fetchOperativos() {
      const response = await fetch('http://localhost:5000/api/Operativo');
      this.operativos = await response.json();
    }
  }
});