// src/services/apiService.js
import axios from '../plugins/axios'; // Ajusta la ruta según donde esté tu archivo axios.js

// Función para obtener los operativos
export const getOperativos = async () => {
  try {
    const response = await axios.get('/api/operativos'); // Ajusta la ruta según tu API
    return response.data;
  } catch (error) {
    console.error('Error fetching operativos:', error);
    throw error;
  }
};

// Puedes agregar más funciones para otras peticiones aquí

export default {
  getOperativos,
};