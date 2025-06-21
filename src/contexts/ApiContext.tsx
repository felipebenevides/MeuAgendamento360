import React, { createContext, useContext, useState, ReactNode } from 'react';
import { authService, businessService, appointmentService } from '../services/api';
import { mockAuthService, mockBusinessService, mockServiceService, mockAppointmentService } from '../services/mockApi';

interface ApiContextType {
  useMockApi: boolean;
  toggleApiMode: () => void;
  auth: typeof authService | typeof mockAuthService;
  business: typeof businessService | typeof mockBusinessService;
  service: typeof mockServiceService;
  appointment: typeof appointmentService | typeof mockAppointmentService;
}

const ApiContext = createContext<ApiContextType | undefined>(undefined);

export const ApiProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  // Por padrão, usamos a API mock para apresentação
  const [useMockApi, setUseMockApi] = useState(true);

  const toggleApiMode = () => {
    setUseMockApi(!useMockApi);
  };

  // Seleciona os serviços com base no modo atual
  const auth = useMockApi ? mockAuthService : authService;
  const business = useMockApi ? mockBusinessService : businessService;
  const service = mockServiceService; // Não temos um serviço real para serviços ainda
  const appointment = useMockApi ? mockAppointmentService : appointmentService;

  return (
    <ApiContext.Provider
      value={{
        useMockApi,
        toggleApiMode,
        auth,
        business,
        service,
        appointment,
      }}
    >
      {children}
    </ApiContext.Provider>
  );
};

export const useApi = () => {
  const context = useContext(ApiContext);
  if (context === undefined) {
    throw new Error('useApi deve ser usado dentro de um ApiProvider');
  }
  return context;
};