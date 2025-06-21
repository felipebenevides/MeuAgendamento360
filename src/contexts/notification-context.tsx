import React, { createContext, useContext, useState, useEffect } from 'react';
import { notificationService } from '@/lib/signalr';
import { useToast } from '@/hooks/use-toast';

export type NotificationType = 'info' | 'success' | 'warning' | 'error';

export interface Notification {
  id: string;
  title: string;
  message: string;
  type: NotificationType;
  timestamp: Date;
  read: boolean;
  link?: string;
}

interface NotificationContextType {
  notifications: Notification[];
  unreadCount: number;
  markAsRead: (id: string) => void;
  markAllAsRead: () => void;
  clearNotifications: () => void;
  deleteNotification: (id: string) => void;
}

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

export const useNotifications = () => {
  const context = useContext(NotificationContext);
  if (!context) {
    throw new Error('useNotifications must be used within a NotificationProvider');
  }
  return context;
};

export const NotificationProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [notifications, setNotifications] = useState<Notification[]>([]);
  const { toast } = useToast();

  useEffect(() => {
    const handleNewNotification = (notification: Notification) => {
      setNotifications(prev => [notification, ...prev]);
      
      // Show toast for new notifications
      toast({
        title: notification.title,
        description: notification.message,
        variant: notification.type === 'error' ? 'destructive' : 'default',
      });
    };

    // For testing purposes - listen for custom events
    const handleCustomEvent = (event: CustomEvent) => {
      handleNewNotification(event.detail);
    };

    const initSignalR = async () => {
      try {
        await notificationService.subscribe('ReceiveNotification', handleNewNotification);
      } catch (error) {
        console.error('Failed to subscribe to notifications:', error);
      }
    };

    initSignalR();
    
    // Add custom event listener for testing
    window.addEventListener('notification', handleCustomEvent as EventListener);

    // Cleanup on unmount
    return () => {
      notificationService.unsubscribe('ReceiveNotification', handleNewNotification);
      window.removeEventListener('notification', handleCustomEvent as EventListener);
    };
  }, [toast]);

  const unreadCount = notifications.filter(n => !n.read).length;

  const markAsRead = (id: string) => {
    setNotifications(prev => 
      prev.map(notification => 
        notification.id === id ? { ...notification, read: true } : notification
      )
    );
  };

  const markAllAsRead = () => {
    setNotifications(prev => 
      prev.map(notification => ({ ...notification, read: true }))
    );
  };

  const clearNotifications = () => {
    setNotifications([]);
  };

  const deleteNotification = (id: string) => {
    setNotifications(prev => prev.filter(notification => notification.id !== id));
  };

  return (
    <NotificationContext.Provider
      value={{
        notifications,
        unreadCount,
        markAsRead,
        markAllAsRead,
        clearNotifications,
        deleteNotification,
      }}
    >
      {children}
    </NotificationContext.Provider>
  );
};