import axios from 'axios';

// Configuração base do axios
const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor para adicionar o token de autenticação
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Serviços de autenticação
export const authService = {
  login: async (email: string, password: string) => {
    const response = await api.post('/auth/login', { email, password });
    return response.data;
  },
  register: async (userData: any) => {
    const response = await api.post('/auth/register', userData);
    return response.data;
  },
  logout: () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  },
  getCurrentUser: () => {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  },
};

// Serviços de negócios
export const businessService = {
  create: async (businessData: any) => {
    const response = await api.post('/businesses', businessData);
    return response.data;
  },
  getById: async (id: string) => {
    const response = await api.get(`/businesses/${id}`);
    return response.data;
  },
  update: async (id: string, businessData: any) => {
    const response = await api.put(`/businesses/${id}`, businessData);
    return response.data;
  },
};

// Serviços de agendamentos
export const appointmentService = {
  create: async (appointmentData: any) => {
    const response = await api.post('/appointments', appointmentData);
    return response.data;
  },
  getByBusinessId: async (businessId: string) => {
    const response = await api.get(`/appointments/business/${businessId}`);
    return response.data;
  },
  getByCustomerId: async (customerId: string) => {
    const response = await api.get(`/appointments/customer/${customerId}`);
    return response.data;
  },
  update: async (id: string, appointmentData: any) => {
    const response = await api.put(`/appointments/${id}`, appointmentData);
    return response.data;
  },
  cancel: async (id: string) => {
    const response = await api.put(`/appointments/${id}/cancel`);
    return response.data;
  },
};

export default api;