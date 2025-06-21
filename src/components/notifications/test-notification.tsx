import React, { useState } from 'react';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Input } from '@/components/ui/input';
import { useToast } from '@/hooks/use-toast';
import { v4 as uuidv4 } from 'uuid';
import { useNotifications, NotificationType } from '@/contexts/notification-context';

export function TestNotification() {
  const [title, setTitle] = useState('Nova notificação');
  const [message, setMessage] = useState('Esta é uma notificação de teste');
  const [type, setType] = useState<NotificationType>('info');
  
  const { toast } = useToast();
  const { notifications, unreadCount } = useNotifications();

  // This function simulates receiving a notification from the server
  const sendTestNotification = () => {
    // In a real app, this would come from SignalR
    const notification = {
      id: uuidv4(),
      title,
      message,
      type,
      timestamp: new Date(),
      read: false
    };

    // Simulate the SignalR event
    const event = new CustomEvent('notification', { detail: notification });
    window.dispatchEvent(event);

    toast({
      title: 'Notificação enviada',
      description: 'Uma notificação de teste foi enviada com sucesso',
    });
  };

  return (
    <Card>
      <CardHeader>
        <CardTitle>Testar Notificações</CardTitle>
      </CardHeader>
      <CardContent className="space-y-4">
        <div>
          <label className="text-sm font-medium text-gray-700 mb-2 block">
            Título
          </label>
          <Input
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Título da notificação"
          />
        </div>
        
        <div>
          <label className="text-sm font-medium text-gray-700 mb-2 block">
            Mensagem
          </label>
          <Input
            value={message}
            onChange={(e) => setMessage(e.target.value)}
            placeholder="Mensagem da notificação"
          />
        </div>
        
        <div>
          <label className="text-sm font-medium text-gray-700 mb-2 block">
            Tipo
          </label>
          <select
            value={type}
            onChange={(e) => setType(e.target.value as NotificationType)}
            className="w-full h-11 rounded-xl border border-gray-200 bg-white/50 backdrop-blur-sm px-4 py-2 text-sm focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary/20 focus-visible:border-primary"
          >
            <option value="info">Informação</option>
            <option value="success">Sucesso</option>
            <option value="warning">Aviso</option>
            <option value="error">Erro</option>
          </select>
        </div>
        
        <div className="pt-2">
          <Button onClick={sendTestNotification}>
            Enviar Notificação de Teste
          </Button>
        </div>
        
        <div className="pt-4 border-t border-gray-100">
          <p className="text-sm text-gray-600">
            Notificações ativas: {notifications.length} (Não lidas: {unreadCount})
          </p>
        </div>
      </CardContent>
    </Card>
  );
}