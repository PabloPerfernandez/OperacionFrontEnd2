import apiClient from './apiClient';

export default {
  getAll() {
    return apiClient.get('/operativos');
  },
  get(id) {
    return apiClient.get(`/operativos/${id}`);
  },
  create(data) {
    return apiClient.post('/operativos', data);
  },
  update(id, data) {
    return apiClient.put(`/operativos/${id}`, data);
  },
  delete(id) {
    return apiClient.delete(`/operativos/${id}`);
  },
};
