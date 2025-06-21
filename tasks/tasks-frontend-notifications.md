# 🔔 Sistema de Notificações - MeuAgendamento360

## 🎯 Visão Geral
Implementar um sistema de notificações em tempo real para o MeuAgendamento360, permitindo que usuários recebam alertas instantâneos sobre agendamentos, cancelamentos, lembretes e outras atividades importantes do sistema.

## 📋 Requisitos Funcionais

### 1️⃣ **Notificações em Tempo Real**
- [ ] **1.1 Configuração do SignalR**
  - Instalar pacotes necessários
  - Configurar hub de conexão
  - Implementar autenticação no hub
  - Estabelecer conexão persistente

- [ ] **1.2 Tipos de Notificações**
  - Novos agendamentos
  - Cancelamentos
  - Lembretes (15min, 1h, 24h antes)
  - Alterações de horário
  - Mensagens de clientes
  - Alertas do sistema
  - Atualizações de status

- [ ] **1.3 Entrega de Notificações**
  - Entrega em tempo real via SignalR
  - Fallback para polling em caso de falha
  - Persistência no banco de dados
  - Entrega garantida mesmo offline

### 2️⃣ **Interface de Notificações**
- [ ] **2.1 Toast Notifications**
  - Exibir notificações temporárias
  - Animações de entrada/saída
  - Níveis de prioridade visual
  - Ações rápidas em notificações

- [ ] **2.2 Centro de Notificações**
  - Dropdown no header
  - Badge com contador
  - Lista de notificações recentes
  - Opção para ver todas as notificações

- [ ] **2.3 Página de Notificações**
  - Histórico completo de notificações
  - Filtros por tipo e data
  - Paginação e busca
  - Ações em lote

- [ ] **2.4 Indicadores de Status**
  - Não lida/lida
  - Prioridade (alta, média, baixa)
  - Categorias por cor
  - Ícones por tipo

### 3️⃣ **Gerenciamento de Notificações**
- [ ] **3.1 Preferências de Notificação**
  - Configurações por tipo de notificação
  - Canais de entrega (app, email, SMS)
  - Horários silenciosos
  - Níveis de prioridade

- [ ] **3.2 Ações em Notificações**
  - Marcar como lida/não lida
  - Arquivar notificações
  - Excluir notificações
  - Executar ações contextuais

- [ ] **3.3 Agrupamento e Organização**
  - Agrupar por tipo
  - Ordenar por data/prioridade
  - Categorizar automaticamente
  - Destacar notificações importantes

- [ ] **3.4 Persistência e Sincronização**
  - Armazenar histórico no servidor
  - Sincronizar entre dispositivos
  - Manter estado local
  - Limitar retenção de dados

### 4️⃣ **Canais de Notificação**
- [ ] **4.1 Notificações no App**
  - Entrega em tempo real
  - Persistência no app
  - Indicadores visuais
  - Sons e vibrações

- [ ] **4.2 Notificações por Email**
  - Templates personalizados
  - Agrupamento inteligente
  - Links diretos para ações
  - Opção de cancelar inscrição

- [ ] **4.3 Notificações por SMS**
  - Mensagens concisas
  - Links encurtados
  - Priorização de conteúdo
  - Otimização de custos

- [ ] **4.4 Notificações Push**
  - Configuração de service worker
  - Solicitação de permissão
  - Entrega quando app fechado
  - Deep linking para conteúdo

## 🎨 **Design e UX**

### **Componentes de Notificação**
- [ ] **Toast Component**
  - Design minimalista
  - Animações suaves
  - Níveis de severidade
  - Ações contextuais

- [ ] **Notification Dropdown**
  - Lista compacta
  - Previews de conteúdo
  - Indicadores de não lido
  - Ações rápidas

- [ ] **Notification Page**
  - Layout responsivo
  - Filtros avançados
  - Visualização detalhada
  - Estados vazios

- [ ] **Notification Settings**
  - Interface intuitiva
  - Toggles por categoria
  - Presets de configuração
  - Visualização de exemplo

### **Experiência do Usuário**
- [ ] **Feedback Sonoro e Tátil**
  - Sons distintos por tipo
  - Vibração em mobile
  - Personalização de alertas
  - Modo silencioso

- [ ] **Priorização e Relevância**
  - Algoritmo de relevância
  - Destaque para urgentes
  - Agrupamento inteligente
  - Redução de ruído

- [ ] **Acessibilidade**
  - Alto contraste
  - Compatibilidade com leitores de tela
  - Navegação por teclado
  - Tempo ajustável de exibição

- [ ] **Responsividade**
  - Adaptação para mobile
  - Posicionamento contextual
  - Touch-friendly
  - Economia de espaço

## 🛠️ **Implementação Técnica**

### **Arquitetura de Notificações**
```typescript
interface Notification {
  id: string;
  type: NotificationType;
  title: string;
  message: string;
  createdAt: Date;
  read: boolean;
  priority: 'high' | 'medium' | 'low';
  category: string;
  actions?: NotificationAction[];
  metadata?: Record<string, any>;
  recipientId: string;
}

enum NotificationType {
  APPOINTMENT_CREATED,
  APPOINTMENT_UPDATED,
  APPOINTMENT_CANCELLED,
  APPOINTMENT_REMINDER,
  CUSTOMER_MESSAGE,
  SYSTEM_ALERT,
  PAYMENT_RECEIVED,
  STAFF_ASSIGNED
}

interface NotificationAction {
  label: string;
  action: string;
  url?: string;
  payload?: Record<string, any>;
}

interface NotificationPreference {
  userId: string;
  type: NotificationType;
  channels: {
    app: boolean;
    email: boolean;
    sms: boolean;
    push: boolean;
  };
  quietHours?: {
    enabled: boolean;
    start: string; // HH:MM
    end: string; // HH:MM
  };
}
```

### **SignalR Hub**
```typescript
// NotificationHub.ts
class NotificationHub {
  private connection: HubConnection;
  
  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl('/notificationHub')
      .withAutomaticReconnect()
      .build();
      
    this.connection.on('ReceiveNotification', this.handleNotification);
    this.connection.onreconnecting(this.handleReconnecting);
    this.connection.onreconnected(this.handleReconnected);
    this.connection.onclose(this.handleConnectionClosed);
  }
  
  async start() {
    try {
      await this.connection.start();
      console.log('Connected to notification hub');
      this.subscribeToGroups();
    } catch (err) {
      console.error('Failed to connect to hub:', err);
      setTimeout(() => this.start(), 5000);
    }
  }
  
  private subscribeToGroups() {
    const userId = getUserId();
    const businessId = getBusinessId();
    
    this.connection.invoke('JoinUserGroup', userId);
    this.connection.invoke('JoinBusinessGroup', businessId);
  }
  
  private handleNotification(notification: Notification) {
    // Process incoming notification
    notificationStore.addNotification(notification);
    
    // Show toast if appropriate
    if (shouldShowToast(notification)) {
      showToastNotification(notification);
    }
    
    // Update badge count
    updateUnreadCount();
  }
}
```

### **Notification Store**
```typescript
// notificationStore.ts
import create from 'zustand';

interface NotificationState {
  notifications: Notification[];
  unreadCount: number;
  addNotification: (notification: Notification) => void;
  markAsRead: (id: string) => void;
  markAllAsRead: () => void;
  deleteNotification: (id: string) => void;
  fetchNotifications: () => Promise<void>;
}

const useNotificationStore = create<NotificationState>((set, get) => ({
  notifications: [],
  unreadCount: 0,
  
  addNotification: (notification) => {
    set(state => ({
      notifications: [notification, ...state.notifications],
      unreadCount: state.unreadCount + 1
    }));
  },
  
  markAsRead: async (id) => {
    await api.markNotificationAsRead(id);
    
    set(state => ({
      notifications: state.notifications.map(n => 
        n.id === id ? { ...n, read: true } : n
      ),
      unreadCount: Math.max(0, state.unreadCount - 1)
    }));
  },
  
  markAllAsRead: async () => {
    await api.markAllNotificationsAsRead();
    
    set(state => ({
      notifications: state.notifications.map(n => ({ ...n, read: true })),
      unreadCount: 0
    }));
  },
  
  deleteNotification: async (id) => {
    await api.deleteNotification(id);
    
    set(state => {
      const notification = state.notifications.find(n => n.id === id);
      const unreadDelta = notification && !notification.read ? 1 : 0;
      
      return {
        notifications: state.notifications.filter(n => n.id !== id),
        unreadCount: Math.max(0, state.unreadCount - unreadDelta)
      };
    });
  },
  
  fetchNotifications: async () => {
    const response = await api.getNotifications();
    
    set({
      notifications: response.data,
      unreadCount: response.data.filter(n => !n.read).length
    });
  }
}));
```

### **API Endpoints**
- `GET /api/notifications` - Listar notificações
- `GET /api/notifications/unread-count` - Obter contagem não lidas
- `PUT /api/notifications/:id/read` - Marcar como lida
- `PUT /api/notifications/read-all` - Marcar todas como lidas
- `DELETE /api/notifications/:id` - Excluir notificação
- `GET /api/notifications/preferences` - Obter preferências
- `PUT /api/notifications/preferences` - Atualizar preferências

## 📱 **Responsividade e Mobile**

### **Adaptações Mobile**
- [ ] Posicionamento otimizado para touch
- [ ] Gestos para marcar como lida/excluir
- [ ] Economia de espaço em telas pequenas
- [ ] Feedback tátil em notificações

### **PWA e Offline**
- [ ] Armazenamento local de notificações
- [ ] Sincronização quando online
- [ ] Notificações push quando app fechado
- [ ] Badge no ícone do app

## 🧪 **Testes**

### **Testes Unitários**
- [ ] Testar lógica de notificações
- [ ] Validar store e actions
- [ ] Verificar renderização de componentes
- [ ] Testar formatação e filtros

### **Testes de Integração**
- [ ] Testar conexão SignalR
- [ ] Validar entrega de notificações
- [ ] Verificar sincronização
- [ ] Testar preferências e configurações

### **Testes de UX**
- [ ] Validar experiência em diferentes dispositivos
- [ ] Testar performance com muitas notificações
- [ ] Verificar acessibilidade
- [ ] Testar diferentes cenários de uso

## 📅 **Cronograma de Implementação**

### **Semana 1: Infraestrutura**
- Configurar SignalR
- Implementar store de notificações
- Criar componentes básicos
- Conectar com API

### **Semana 2: Interface e UX**
- Desenvolver toast notifications
- Implementar dropdown no header
- Criar página de notificações
- Adicionar indicadores visuais

### **Semana 3: Canais e Preferências**
- Implementar preferências de notificação
- Adicionar suporte a email
- Configurar notificações push
- Desenvolver templates de SMS

### **Semana 4: Polimento e Testes**
- Otimizar performance
- Implementar testes
- Refinar UX
- Documentar sistema