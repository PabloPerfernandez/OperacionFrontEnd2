import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:5001/api',
  withCredentials: false,
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json'
  }
});

export default {
  // Operativos
  getOperativos() {
    return apiClient.get('/operativos');
  },
  addOperativo(operativo) {
    return apiClient.post('/operativos', operativo);
  },
  updateOperativo(id, operativo) {
    return apiClient.put(`/operativos/${id}`, operativo);
  },
  deleteOperativo(id) {
    return apiClient.delete(`/operativos/${id}`);
  },
  // Misiones
  getMisiones() {
    return apiClient.get('/misiones');
  },
  addMision(mision) {
    return apiClient.post('/misiones', mision);
  },
  updateMision(id, mision) {
    return apiClient.put(`/misiones/${id}`, mision);
  },
  deleteMision(id) {
    return apiClient.delete(`/misiones/${id}`);
  },
  // Equipos
  getEquipos() {
    return apiClient.get('/equipos');
  },
  addEquipo(equipo) {
    return apiClient.post('/equipos', equipo);
  },
  updateEquipo(id, equipo) {
    return apiClient.put(`/equipos/${id}`, equipo);
  },
  deleteEquipo(id) {
    return apiClient.delete(`/equipos/${id}`);
  }
};