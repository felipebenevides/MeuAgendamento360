# ⚡ Frontend - Tasks Imediatas - MeuAgendamento360

## 🎯 Tasks para Implementação AGORA

### 🔧 **TASK 1: Configuração de Estado e HTTP Client**

#### 1.1 Instalar Dependências
```bash
cd frontend
npm install zustand axios @brazilian-utils/cpf @brazilian-utils/cnpj react-input-mask
```

#### 1.2 Configurar Axios
- [ ] Criar `src/lib/api.ts` com configuração do Axios
- [ ] Implementar interceptors para JWT
- [ ] Configurar base URL e timeout
- [ ] Tratamento de erros padronizado

#### 1.3 Configurar Zustand
- [ ] Criar `src/stores/authStore.ts`
- [ ] Implementar estado de autenticação
- [ ] Persistência no localStorage
- [ ] Actions para login/logout

### 🔐 **TASK 2: Sistema de Autenticação Básico**

#### 2.1 Tipos TypeScript
- [ ] Criar `src/types/auth.ts` com interfaces
- [ ] Tipos para User, LoginRequest, RegisterRequest
- [ ] Tipos para responses da API

#### 2.2 Serviços de API
- [ ] Criar `src/services/authService.ts`
- [ ] Implementar login, register, logout
- [ ] Refresh token logic
- [ ] Forgot/reset password

#### 2.3 Páginas de Autenticação
- [ ] Criar `src/pages/Login.tsx`
- [ ] Criar `src/pages/Register.tsx`
- [ ] Formulários com validação Zod
- [ ] Estados de loading e erro

### 🛡️ **TASK 3: Proteção de Rotas**

#### 3.1 AuthGuard Component
- [ ] Criar `src/components/auth/AuthGuard.tsx`
- [ ] Verificar autenticação
- [ ] Redirect para login se não autenticado

#### 3.2 Atualizar Rotas
- [ ] Proteger rota `/dashboard`
- [ ] Adicionar rotas de autenticação
- [ ] Redirect após login

### 🎨 **TASK 4: Melhorar Design System**

#### 4.1 Configurar Tema
- [ ] Atualizar `tailwind.config.ts` com cores personalizadas
- [ ] Adicionar fontes Google (Playfair Display + Inter)
- [ ] Criar tokens de design

#### 4.2 Componentes de Layout
- [ ] Criar `src/components/layout/Header.tsx`
- [ ] Criar `src/components/layout/Sidebar.tsx`
- [ ] Layout responsivo para dashboard

## 📁 Estrutura de Arquivos a Criar

```
src/
├── lib/
│   └── api.ts                 # Configuração Axios
├── stores/
│   └── authStore.ts          # Estado de autenticação
├── types/
│   └── auth.ts               # Tipos de autenticação
├── services/
│   └── authService.ts        # Serviços de API
├── components/
│   ├── auth/
│   │   ├── AuthGuard.tsx     # Proteção de rotas
│   │   ├── LoginForm.tsx     # Formulário de login
│   │   └── RegisterForm.tsx  # Formulário de registro
│   └── layout/
│       ├── Header.tsx        # Header principal
│       ├── Sidebar.tsx       # Sidebar dashboard
│       └── DashboardLayout.tsx # Layout do dashboard
└── pages/
    ├── Login.tsx             # Página de login
    └── Register.tsx          # Página de registro
```

## 🚀 Implementação Passo a Passo

### Passo 1: Instalar Dependências
```bash
cd frontend
npm install zustand axios @brazilian-utils/cpf @brazilian-utils/cnpj react-input-mask
```

### Passo 2: Configurar API Client
Criar `src/lib/api.ts`:
```typescript
import axios from 'axios';

const api = axios.create({
  baseURL: process.env.VITE_API_URL || 'http://localhost:5000/api',
  timeout: 10000,
});

// Request interceptor para adicionar token
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Response interceptor para tratar erros
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default api;
```

### Passo 3: Criar Store de Autenticação
Criar `src/stores/authStore.ts`:
```typescript
import { create } from 'zustand';
import { persist } from 'zustand/middleware';

interface User {
  id: string;
  email: string;
  name: string;
  role: string;
}

interface AuthState {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
  login: (user: User, token: string) => void;
  logout: () => void;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      user: null,
      token: null,
      isAuthenticated: false,
      login: (user, token) => {
        localStorage.setItem('token', token);
        set({ user, token, isAuthenticated: true });
      },
      logout: () => {
        localStorage.removeItem('token');
        set({ user: null, token: null, isAuthenticated: false });
      },
    }),
    {
      name: 'auth-storage',
    }
  )
);
```

### Passo 4: Criar Tipos
Criar `src/types/auth.ts`:
```typescript
export interface User {
  id: string;
  email: string;
  name: string;
  role: string;
  businessId?: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface AuthResponse {
  user: User;
  token: string;
  refreshToken: string;
}
```

### Passo 5: Criar Serviços
Criar `src/services/authService.ts`:
```typescript
import api from '@/lib/api';
import { LoginRequest, RegisterRequest, AuthResponse } from '@/types/auth';

export const authService = {
  login: async (data: LoginRequest): Promise<AuthResponse> => {
    const response = await api.post('/auth/login', data);
    return response.data;
  },

  register: async (data: RegisterRequest): Promise<AuthResponse> => {
    const response = await api.post('/auth/register', data);
    return response.data;
  },

  logout: async (): Promise<void> => {
    await api.post('/auth/logout');
  },

  refreshToken: async (): Promise<AuthResponse> => {
    const response = await api.post('/auth/refresh');
    return response.data;
  },
};
```

## ✅ Checklist de Implementação

### Configuração Base
- [ ] Instalar dependências necessárias
- [ ] Configurar Axios com interceptors
- [ ] Criar store Zustand para autenticação
- [ ] Definir tipos TypeScript

### Autenticação
- [ ] Implementar serviços de API
- [ ] Criar páginas de login e registro
- [ ] Implementar proteção de rotas
- [ ] Testar fluxo completo

### Layout e Design
- [ ] Configurar tema personalizado
- [ ] Criar componentes de layout
- [ ] Implementar navegação
- [ ] Testar responsividade

### Integração
- [ ] Conectar com backend
- [ ] Testar todas as APIs
- [ ] Implementar tratamento de erros
- [ ] Validar fluxos de usuário

## 🎯 Resultado Esperado

Após implementar essas tasks, teremos:

1. ✅ Sistema de autenticação completo
2. ✅ Proteção de rotas funcionando
3. ✅ Layout base do dashboard
4. ✅ Integração com backend
5. ✅ Design system personalizado

## 📞 Próximos Passos

1. **Implementar tasks acima** (1-2 dias)
2. **Testar integração** com backend
3. **Criar wizard de onboarding** do negócio
4. **Implementar CRUD de serviços**
5. **Adicionar gestão de equipe**

---

**Prioridade**: 🔥 ALTA
**Tempo Estimado**: 2-3 dias
**Dependências**: Backend APIs funcionando