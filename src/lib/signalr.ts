import * as signalR from '@microsoft/signalr';

class NotificationService {
  private connection: signalR.HubConnection | null = null;
  private connectionPromise: Promise<void> | null = null;
  private listeners: Map<string, ((data: any) => void)[]> = new Map();

  constructor() {
    this.initConnection();
  }

  private initConnection() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${process.env.NEXT_PUBLIC_API_URL}/notificationHub`)
      .withAutomaticReconnect()
      .build();

    this.connectionPromise = this.connection.start()
      .catch(err => console.error('Error starting SignalR connection:', err));
    
    this.connection.onclose(() => {
      console.log('SignalR connection closed');
    });
  }

  async ensureConnection() {
    if (this.connection?.state === signalR.HubConnectionState.Connected) {
      return;
    }
    
    if (this.connectionPromise) {
      await this.connectionPromise;
    } else {
      this.initConnection();
      await this.connectionPromise;
    }
  }

  async subscribe(eventName: string, callback: (data: any) => void) {
    await this.ensureConnection();
    
    if (!this.listeners.has(eventName)) {
      this.listeners.set(eventName, []);
      
      if (this.connection) {
        this.connection.on(eventName, (data) => {
          const callbacks = this.listeners.get(eventName) || [];
          callbacks.forEach(cb => cb(data));
        });
      }
    }
    
    const callbacks = this.listeners.get(eventName) || [];
    callbacks.push(callback);
    this.listeners.set(eventName, callbacks);
  }

  async unsubscribe(eventName: string, callback: (data: any) => void) {
    const callbacks = this.listeners.get(eventName) || [];
    const index = callbacks.indexOf(callback);
    
    if (index !== -1) {
      callbacks.splice(index, 1);
      this.listeners.set(eventName, callbacks);
    }
    
    if (callbacks.length === 0) {
      this.listeners.delete(eventName);
      if (this.connection) {
        this.connection.off(eventName);
      }
    }
  }

  async disconnect() {
    if (this.connection) {
      await this.connection.stop();
      this.connection = null;
      this.connectionPromise = null;
    }
  }
}

export const notificationService = new NotificationService();