// Mock de dados para apresentação
const mockUsers = [
  {
    id: '1',
    email: 'admin@meuagendamento360.com',
    password: 'admin123',
    fullName: 'Administrador',
    role: 'admin',
  },
  {
    id: '2',
    email: 'business@meuagendamento360.com',
    password: 'business123',
    fullName: 'Dono do Negócio',
    role: 'business_owner',
  },
  {
    id: '3',
    email: 'staff@meuagendamento360.com',
    password: 'staff123',
    fullName: 'Funcionário',
    role: 'staff',
  },
  {
    id: '4',
    email: 'customer@meuagendamento360.com',
    password: 'customer123',
    fullName: 'Cliente',
    role: 'customer',
  },
];

const mockBusinesses = [
  {
    id: '1',
    name: 'Salão de Beleza Exemplo',
    slug: 'salao-beleza-exemplo',
    description: 'Um salão de beleza completo com diversos serviços.',
    ownerId: '2',
    logoUrl: 'https://via.placeholder.com/150',
    phone: '(11) 99999-9999',
    email: 'contato@salaobeleza.com',
    website: 'www.salaobeleza.com',
    address: {
      street: 'Rua Exemplo',
      number: '123',
      complement: 'Sala 45',
      neighborhood: 'Centro',
      city: 'São Paulo',
      state: 'SP',
      postalCode: '01234-567',
      country: 'Brasil',
    },
  },
];

const mockServices = [
  {
    id: '1',
    name: 'Corte de Cabelo',
    description: 'Corte de cabelo masculino ou feminino',
    durationMinutes: 30,
    price: 50.0,
    businessId: '1',
    isActive: true,
  },
  {
    id: '2',
    name: 'Manicure',
    description: 'Tratamento completo para unhas',
    durationMinutes: 45,
    price: 35.0,
    businessId: '1',
    isActive: true,
  },
  {
    id: '3',
    name: 'Pedicure',
    description: 'Tratamento completo para unhas dos pés',
    durationMinutes: 45,
    price: 40.0,
    businessId: '1',
    isActive: true,
  },
];

const mockAppointments = [
  {
    id: '1',
    customerId: '4',
    staffId: '3',
    serviceId: '1',
    businessId: '1',
    startTime: '2025-06-25T10:00:00Z',
    endTime: '2025-06-25T10:30:00Z',
    status: 'confirmed',
    notes: 'Cliente prefere corte curto',
  },
  {
    id: '2',
    customerId: '4',
    staffId: '3',
    serviceId: '2',
    businessId: '1',
    startTime: '2025-06-26T14:00:00Z',
    endTime: '2025-06-26T14:45:00Z',
    status: 'scheduled',
    notes: '',
  },
];

// Serviço de mock para autenticação
export const mockAuthService = {
  login: async (email: string, password: string) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        const user = mockUsers.find(
          (u) => u.email === email && u.password === password
        );
        if (user) {
          const { password, ...userWithoutPassword } = user;
          resolve({
            token: 'mock-jwt-token',
            user: userWithoutPassword,
          });
        } else {
          reject(new Error('Credenciais inválidas'));
        }
      }, 500);
    });
  },
  register: async (userData: any) => {
    return new Promise((resolve) => {
      setTimeout(() => {
        const newUser = {
          id: `${mockUsers.length + 1}`,
          email: userData.email,
          fullName: userData.fullName,
          role: userData.role || 'customer',
        };
        resolve({
          token: 'mock-jwt-token',
          user: newUser,
        });
      }, 500);
    });
  },
  logout: () => {
    // Não faz nada no mock
  },
  getCurrentUser: () => {
    const storedUser = localStorage.getItem('user');
    return storedUser ? JSON.parse(storedUser) : null;
  },
};

// Serviço de mock para negócios
export const mockBusinessService = {
  create: async (businessData: any) => {
    return new Promise((resolve) => {
      setTimeout(() => {
        const newBusiness = {
          id: `${mockBusinesses.length + 1}`,
          ...businessData,
        };
        resolve(newBusiness);
      }, 500);
    });
  },
  getById: async (id: string) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        const business = mockBusinesses.find((b) => b.id === id);
        if (business) {
          resolve(business);
        } else {
          reject(new Error('Negócio não encontrado'));
        }
      }, 500);
    });
  },
  update: async (id: string, businessData: any) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        const businessIndex = mockBusinesses.findIndex((b) => b.id === id);
        if (businessIndex !== -1) {
          const updatedBusiness = {
            ...mockBusinesses[businessIndex],
            ...businessData,
          };
          resolve(updatedBusiness);
        } else {
          reject(new Error('Negócio não encontrado'));
        }
      }, 500);
    });
  },
  getAll: async () => {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve(mockBusinesses);
      }, 500);
    });
  },
};

// Serviço de mock para serviços
export const mockServiceService = {
  getByBusinessId: async (businessId: string) => {
    return new Promise((resolve) => {
      setTimeout(() => {
        const services = mockServices.filter((s) => s.businessId === businessId);
        resolve(services);
      }, 500);
    });
  },
};

// Serviço de mock para agendamentos
export const mockAppointmentService = {
  create: async (appointmentData: any) => {
    return new Promise((resolve) => {
      setTimeout(() => {
        const newAppointment = {
          id: `${mockAppointments.length + 1}`,
          ...appointmentData,
        };
        resolve(newAppointment);
      }, 500);
    });
  },
  getByBusinessId: async (businessId: string) => {
    return new Promise((resolve) => {
      setTimeout(() => {
        const appointments = mockAppointments.filter(
          (a) => a.businessId === businessId
        );
        resolve(appointments);
      }, 500);
    });
  },
  getByCustomerId: async (customerId: string) => {
    return new Promise((resolve) => {
      setTimeout(() => {
        const appointments = mockAppointments.filter(
          (a) => a.customerId === customerId
        );
        resolve(appointments);
      }, 500);
    });
  },
  update: async (id: string, appointmentData: any) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        const appointmentIndex = mockAppointments.findIndex((a) => a.id === id);
        if (appointmentIndex !== -1) {
          const updatedAppointment = {
            ...mockAppointments[appointmentIndex],
            ...appointmentData,
          };
          resolve(updatedAppointment);
        } else {
          reject(new Error('Agendamento não encontrado'));
        }
      }, 500);
    });
  },
  cancel: async (id: string) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        const appointmentIndex = mockAppointments.findIndex((a) => a.id === id);
        if (appointmentIndex !== -1) {
          const updatedAppointment = {
            ...mockAppointments[appointmentIndex],
            status: 'cancelled',
          };
          resolve(updatedAppointment);
        } else {
          reject(new Error('Agendamento não encontrado'));
        }
      }, 500);
    });
  },
};