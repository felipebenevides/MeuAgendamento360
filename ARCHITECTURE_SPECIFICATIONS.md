# 🏗️ MeuAgendamento360 - Architecture Specifications

## 🎯 System Architecture Overview

### Frontend Architecture
```
Next.js 14 App Router
├── /app (App Router Structure)
├── /components (Reusable Components)
├── /lib (Utilities & Configurations)
├── /hooks (Custom React Hooks)
├── /store (Zustand State Management)
├── /types (TypeScript Definitions)
└── /utils (Helper Functions)
```

### State Management Architecture
```
Zustand Stores
├── authStore (Authentication State)
├── businessStore (Business Data)
├── appointmentStore (Scheduling State)
├── clientStore (Customer Data)
├── serviceStore (Service Management)
├── teamStore (Staff Management)
├── financialStore (Financial Data)
└── notificationStore (Alerts & Messages)
```

---

## 📊 Data Flow Specifications

### Authentication Flow
```mermaid
User Input → Form Validation → API Call → JWT Token → Store Update → Route Protection
```

**Data Points**:
- User credentials (email/password)
- JWT tokens (access/refresh)
- User roles and permissions
- Session management data

### Appointment Booking Flow
```mermaid
Service Selection → Staff Selection → Time Selection → Client Info → Payment → Confirmation
```

**Data Points**:
- Service details and pricing
- Staff availability windows
- Time slot reservations
- Client contact information
- Payment transaction data

### Real-time Updates Flow
```mermaid
Database Change → WebSocket Event → State Update → UI Re-render → User Notification
```

**Data Points**:
- Appointment status changes
- New booking notifications
- Payment confirmations
- Schedule modifications

---

## 🔧 Component Architecture

### Layout Components
```typescript
// Layout hierarchy
RootLayout
├── AuthLayout (Login/Register)
├── DashboardLayout (Main App)
│   ├── Sidebar Navigation
│   ├── Header with User Menu
│   ├── Main Content Area
│   └── Notification Center
└── PublicLayout (Business Pages)
    ├── Public Header
    ├── Content Area
    └── Public Footer
```

### Feature Components
```typescript
// Component structure per feature
Feature/
├── components/
│   ├── FeatureList.tsx
│   ├── FeatureCard.tsx
│   ├── FeatureForm.tsx
│   └── FeatureModal.tsx
├── hooks/
│   ├── useFeature.ts
│   └── useFeatureForm.ts
├── types/
│   └── feature.types.ts
└── utils/
    └── feature.utils.ts
```

---

## 🎨 Design System Architecture

### Theme Configuration
```typescript
// Tailwind theme extension
theme: {
  extend: {
    colors: {
      primary: {
        50: '#fdf2f8',   // Light pink
        500: '#ec4899',  // Main pink
        900: '#831843'   // Dark pink
      },
      secondary: {
        50: '#f0f9ff',   // Light blue
        500: '#0ea5e9',  // Main blue
        900: '#0c4a6e'   // Dark blue
      },
      accent: {
        50: '#fefce8',   // Light gold
        500: '#eab308',  // Main gold
        900: '#713f12'   // Dark gold
      }
    },
    fontFamily: {
      display: ['Playfair Display', 'serif'],
      body: ['Inter', 'sans-serif']
    }
  }
}
```

### Component Variants
```typescript
// Button component variants
const buttonVariants = {
  variant: {
    primary: 'bg-primary-500 text-white hover:bg-primary-600',
    secondary: 'bg-secondary-500 text-white hover:bg-secondary-600',
    outline: 'border-2 border-primary-500 text-primary-500 hover:bg-primary-50',
    ghost: 'text-primary-500 hover:bg-primary-50'
  },
  size: {
    sm: 'px-3 py-1.5 text-sm',
    md: 'px-4 py-2 text-base',
    lg: 'px-6 py-3 text-lg'
  }
}
```

---

## 📱 Responsive Design Specifications

### Breakpoint System
```typescript
const breakpoints = {
  sm: '640px',   // Mobile landscape
  md: '768px',   // Tablet portrait
  lg: '1024px',  // Tablet landscape
  xl: '1280px',  // Desktop
  '2xl': '1536px' // Large desktop
}
```

### Mobile-First Components
```typescript
// Responsive navigation
<nav className="
  fixed bottom-0 left-0 right-0 bg-white border-t
  md:static md:border-t-0 md:bg-transparent
  lg:flex lg:items-center lg:space-x-6
">
  {/* Mobile bottom nav / Desktop side nav */}
</nav>
```

---

## 🔐 Security Architecture

### Authentication Security
```typescript
// JWT token management
interface AuthTokens {
  accessToken: string;    // 15 minutes expiry
  refreshToken: string;   // 7 days expiry
  tokenType: 'Bearer';
}

// Role-based access control
interface UserPermissions {
  role: 'owner' | 'manager' | 'staff' | 'receptionist';
  permissions: string[];
  businessId: string;
}
```

### API Security
```typescript
// Axios interceptors for security
axios.interceptors.request.use((config) => {
  const token = authStore.getState().accessToken;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});
```

---

## 📊 Performance Specifications

### Code Splitting Strategy
```typescript
// Route-based code splitting
const Dashboard = lazy(() => import('./pages/Dashboard'));
const Appointments = lazy(() => import('./pages/Appointments'));
const Clients = lazy(() => import('./pages/Clients'));

// Component-based splitting
const CalendarView = lazy(() => import('./components/CalendarView'));
```

### Caching Strategy
```typescript
// React Query configuration
const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 5 * 60 * 1000,     // 5 minutes
      cacheTime: 10 * 60 * 1000,    // 10 minutes
      refetchOnWindowFocus: false,
    },
  },
});
```

### Image Optimization
```typescript
// Next.js Image component usage
<Image
  src={businessLogo}
  alt="Business Logo"
  width={200}
  height={100}
  priority={true}
  placeholder="blur"
  blurDataURL="data:image/jpeg;base64,..."
/>
```

---

## 🔄 State Management Specifications

### Zustand Store Structure
```typescript
// Authentication store
interface AuthStore {
  user: User | null;
  tokens: AuthTokens | null;
  isAuthenticated: boolean;
  login: (credentials: LoginCredentials) => Promise<void>;
  logout: () => void;
  refreshToken: () => Promise<void>;
}

// Business store
interface BusinessStore {
  business: Business | null;
  services: Service[];
  team: TeamMember[];
  clients: Client[];
  appointments: Appointment[];
  // Actions
  updateBusiness: (data: Partial<Business>) => void;
  addService: (service: Service) => void;
  updateService: (id: string, data: Partial<Service>) => void;
}
```

### Form State Management
```typescript
// React Hook Form with Zod validation
const appointmentSchema = z.object({
  clientId: z.string().min(1, 'Client is required'),
  serviceId: z.string().min(1, 'Service is required'),
  staffId: z.string().min(1, 'Staff member is required'),
  date: z.date(),
  time: z.string().regex(/^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/),
  notes: z.string().optional()
});
```

---

## 🌐 API Integration Specifications

### REST API Structure
```typescript
// API client configuration
const apiClient = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// API endpoints
const endpoints = {
  auth: {
    login: '/auth/login',
    register: '/auth/register',
    refresh: '/auth/refresh',
    logout: '/auth/logout'
  },
  business: {
    profile: '/business/profile',
    services: '/business/services',
    team: '/business/team'
  },
  appointments: {
    list: '/appointments',
    create: '/appointments',
    update: (id: string) => `/appointments/${id}`,
    delete: (id: string) => `/appointments/${id}`
  }
};
```

### WebSocket Integration
```typescript
// Real-time updates
const useWebSocket = () => {
  const socket = useRef<WebSocket | null>(null);
  
  useEffect(() => {
    socket.current = new WebSocket(process.env.NEXT_PUBLIC_WS_URL);
    
    socket.current.onmessage = (event) => {
      const data = JSON.parse(event.data);
      handleRealtimeUpdate(data);
    };
    
    return () => socket.current?.close();
  }, []);
};
```

---

## 📊 Analytics & Monitoring

### Performance Monitoring
```typescript
// Web Vitals tracking
import { getCLS, getFID, getFCP, getLCP, getTTFB } from 'web-vitals';

getCLS(console.log);
getFID(console.log);
getFCP(console.log);
getLCP(console.log);
getTTFB(console.log);
```

### Error Tracking
```typescript
// Error boundary implementation
class ErrorBoundary extends React.Component {
  componentDidCatch(error: Error, errorInfo: ErrorInfo) {
    // Log to monitoring service
    console.error('Error caught by boundary:', error, errorInfo);
  }
}
```

### User Analytics
```typescript
// Custom event tracking
const trackEvent = (eventName: string, properties: Record<string, any>) => {
  // Google Analytics 4
  gtag('event', eventName, properties);
  
  // Custom analytics
  analytics.track(eventName, properties);
};
```

---

## 🔧 Development Workflow

### Environment Configuration
```typescript
// Environment variables
interface EnvironmentConfig {
  NEXT_PUBLIC_API_URL: string;
  NEXT_PUBLIC_WS_URL: string;
  NEXT_PUBLIC_SUPABASE_URL: string;
  NEXT_PUBLIC_SUPABASE_ANON_KEY: string;
  NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY: string;
  NEXT_PUBLIC_GOOGLE_MAPS_API_KEY: string;
}
```

### Build Configuration
```typescript
// Next.js configuration
const nextConfig = {
  experimental: {
    appDir: true,
  },
  images: {
    domains: ['example.com', 'cdn.example.com'],
  },
  env: {
    CUSTOM_KEY: process.env.CUSTOM_KEY,
  },
};
```

---

## 🚀 Deployment Specifications

### Build Process
```bash
# Production build
npm run build
npm run start

# Development
npm run dev

# Testing
npm run test
npm run test:e2e
```

### Performance Budgets
```json
{
  "budgets": [
    {
      "type": "initial",
      "maximumWarning": "500kb",
      "maximumError": "1mb"
    },
    {
      "type": "anyComponentStyle",
      "maximumWarning": "2kb",
      "maximumError": "4kb"
    }
  ]
}
```

This architecture specification provides the technical foundation for implementing the MeuAgendamento360 frontend with optimal performance, security, and maintainability.