'use client'

import { useState } from 'react'
import { 
  Settings, 
  User, 
  Building2, 
  Clock, 
  CreditCard, 
  Bell, 
  Shield, 
  Globe,
  Save
} from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'
import { Avatar } from '@/components/ui/avatar'

export default function SettingsPage() {
  const [activeTab, setActiveTab] = useState('profile')
  
  const tabs = [
    { id: 'profile', name: 'Perfil', icon: User },
    { id: 'business', name: 'Empresa', icon: Building2 },
    { id: 'schedule', name: 'Horários', icon: Clock },
    { id: 'payment', name: 'Pagamentos', icon: CreditCard },
    { id: 'notifications', name: 'Notificações', icon: Bell },
    { id: 'security', name: 'Segurança', icon: Shield },
    { id: 'integrations', name: 'Integrações', icon: Globe }
  ]

  const renderTabContent = () => {
    switch (activeTab) {
      case 'profile':
        return <ProfileSettings />
      case 'business':
        return <BusinessSettings />
      case 'schedule':
        return <ScheduleSettings />
      case 'payment':
        return <PaymentSettings />
      case 'notifications':
        return <NotificationSettings />
      case 'security':
        return <SecuritySettings />
      case 'integrations':
        return <IntegrationSettings />
      default:
        return <ProfileSettings />
    }
  }

  return (
    <DashboardLayout title="Configurações" subtitle="Gerencie as configurações do sistema">
      <div className="grid grid-cols-1 lg:grid-cols-4 gap-6">
        {/* Sidebar */}
        <Card className="lg:col-span-1">
          <CardContent className="p-6">
            <nav className="space-y-1">
              {tabs.map((tab) => (
                <button
                  key={tab.id}
                  onClick={() => setActiveTab(tab.id)}
                  className={`w-full flex items-center gap-3 px-3 py-2 rounded-lg text-sm font-medium transition-colors ${
                    activeTab === tab.id
                      ? 'bg-gradient-primary text-white'
                      : 'text-gray-600 hover:text-gray-900 hover:bg-gray-100'
                  }`}
                >
                  <tab.icon className="w-5 h-5" />
                  {tab.name}
                </button>
              ))}
            </nav>
          </CardContent>
        </Card>

        {/* Content */}
        <div className="lg:col-span-3">
          {renderTabContent()}
        </div>
      </div>
    </DashboardLayout>
  )
}

function ProfileSettings() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Informações do Perfil</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="space-y-6">
          <div className="flex flex-col items-center sm:flex-row sm:items-start gap-6">
            <div className="flex flex-col items-center gap-2">
              <Avatar name="Felipe Benevides" size="xl" />
              <Button variant="outline" size="sm">Alterar Foto</Button>
            </div>
            
            <div className="flex-1 space-y-4">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div className="space-y-2">
                  <label className="text-sm font-medium text-gray-700">Nome</label>
                  <Input defaultValue="Felipe" />
                </div>
                <div className="space-y-2">
                  <label className="text-sm font-medium text-gray-700">Sobrenome</label>
                  <Input defaultValue="Benevides" />
                </div>
              </div>
              
              <div className="space-y-2">
                <label className="text-sm font-medium text-gray-700">Email</label>
                <Input defaultValue="felipe@email.com" type="email" />
              </div>
              
              <div className="space-y-2">
                <label className="text-sm font-medium text-gray-700">Telefone</label>
                <Input defaultValue="(11) 99999-9999" />
              </div>
              
              <div className="space-y-2">
                <label className="text-sm font-medium text-gray-700">CPF</label>
                <Input defaultValue="123.456.789-00" disabled />
              </div>
            </div>
          </div>
          
          <div className="flex justify-end">
            <Button>
              <Save className="w-4 h-4 mr-2" />
              Salvar Alterações
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}

function BusinessSettings() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Informações da Empresa</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="space-y-6">
          <div className="space-y-4">
            <div className="space-y-2">
              <label className="text-sm font-medium text-gray-700">Nome da Empresa</label>
              <Input defaultValue="Salão Exemplo" />
            </div>
            
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div className="space-y-2">
                <label className="text-sm font-medium text-gray-700">CNPJ</label>
                <Input defaultValue="12.345.678/0001-90" />
              </div>
              <div className="space-y-2">
                <label className="text-sm font-medium text-gray-700">Telefone Comercial</label>
                <Input defaultValue="(11) 3333-4444" />
              </div>
            </div>
            
            <div className="space-y-2">
              <label className="text-sm font-medium text-gray-700">Endereço</label>
              <Input defaultValue="Rua Exemplo, 123" />
            </div>
            
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
              <div className="space-y-2">
                <label className="text-sm font-medium text-gray-700">Cidade</label>
                <Input defaultValue="São Paulo" />
              </div>
              <div className="space-y-2">
                <label className="text-sm font-medium text-gray-700">Estado</label>
                <Input defaultValue="SP" />
              </div>
              <div className="space-y-2">
                <label className="text-sm font-medium text-gray-700">CEP</label>
                <Input defaultValue="01234-567" />
              </div>
            </div>
            
            <div className="space-y-2">
              <label className="text-sm font-medium text-gray-700">Site</label>
              <Input defaultValue="https://www.exemplo.com.br" />
            </div>
          </div>
          
          <div className="flex justify-end">
            <Button>
              <Save className="w-4 h-4 mr-2" />
              Salvar Alterações
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}

function ScheduleSettings() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Horários de Funcionamento</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="space-y-6">
          {['Segunda-feira', 'Terça-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sábado', 'Domingo'].map((day, index) => (
            <div key={day} className="flex items-center justify-between border-b border-gray-100 pb-4 last:border-0">
              <div className="flex items-center gap-4">
                <input type="checkbox" className="rounded text-primary" defaultChecked={index < 6} />
                <span className="font-medium">{day}</span>
              </div>
              
              <div className="flex items-center gap-2">
                <Input 
                  type="time" 
                  className="w-32" 
                  defaultValue={index < 6 ? "09:00" : ""}
                  disabled={index === 6}
                />
                <span>até</span>
                <Input 
                  type="time" 
                  className="w-32" 
                  defaultValue={index < 5 ? "18:00" : index === 5 ? "13:00" : ""}
                  disabled={index === 6}
                />
              </div>
            </div>
          ))}
          
          <div className="flex justify-end">
            <Button>
              <Save className="w-4 h-4 mr-2" />
              Salvar Alterações
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}

function PaymentSettings() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Métodos de Pagamento</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="space-y-6">
          <div className="space-y-4">
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div className="flex items-center gap-4">
                <input type="checkbox" className="rounded text-primary" defaultChecked />
                <span className="font-medium">Dinheiro</span>
              </div>
              <Button variant="outline" size="sm">Configurar</Button>
            </div>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div className="flex items-center gap-4">
                <input type="checkbox" className="rounded text-primary" defaultChecked />
                <span className="font-medium">Cartão de Crédito</span>
              </div>
              <Button variant="outline" size="sm">Configurar</Button>
            </div>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div className="flex items-center gap-4">
                <input type="checkbox" className="rounded text-primary" defaultChecked />
                <span className="font-medium">Cartão de Débito</span>
              </div>
              <Button variant="outline" size="sm">Configurar</Button>
            </div>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div className="flex items-center gap-4">
                <input type="checkbox" className="rounded text-primary" defaultChecked />
                <span className="font-medium">PIX</span>
              </div>
              <Button variant="outline" size="sm">Configurar</Button>
            </div>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div className="flex items-center gap-4">
                <input type="checkbox" className="rounded text-primary" />
                <span className="font-medium">Transferência Bancária</span>
              </div>
              <Button variant="outline" size="sm">Configurar</Button>
            </div>
          </div>
          
          <div className="flex justify-end">
            <Button>
              <Save className="w-4 h-4 mr-2" />
              Salvar Alterações
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}

function NotificationSettings() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Configurações de Notificações</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="space-y-6">
          <div className="space-y-4">
            <h3 className="font-medium text-gray-900">Notificações por Email</h3>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div>
                <p className="font-medium">Novos Agendamentos</p>
                <p className="text-sm text-gray-600">Receba notificações quando novos agendamentos forem criados</p>
              </div>
              <input type="checkbox" className="rounded text-primary" defaultChecked />
            </div>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div>
                <p className="font-medium">Cancelamentos</p>
                <p className="text-sm text-gray-600">Receba notificações quando agendamentos forem cancelados</p>
              </div>
              <input type="checkbox" className="rounded text-primary" defaultChecked />
            </div>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div>
                <p className="font-medium">Relatórios</p>
                <p className="text-sm text-gray-600">Receba relatórios diários/semanais/mensais</p>
              </div>
              <input type="checkbox" className="rounded text-primary" defaultChecked />
            </div>
            
            <h3 className="font-medium text-gray-900 mt-8">Notificações por SMS</h3>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div>
                <p className="font-medium">Lembretes de Agendamento</p>
                <p className="text-sm text-gray-600">Enviar lembretes para clientes antes do agendamento</p>
              </div>
              <input type="checkbox" className="rounded text-primary" defaultChecked />
            </div>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div>
                <p className="font-medium">Confirmações</p>
                <p className="text-sm text-gray-600">Enviar confirmações após agendamentos</p>
              </div>
              <input type="checkbox" className="rounded text-primary" defaultChecked />
            </div>
          </div>
          
          <div className="flex justify-end">
            <Button>
              <Save className="w-4 h-4 mr-2" />
              Salvar Alterações
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}

function SecuritySettings() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Segurança</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="space-y-6">
          <div className="space-y-4">
            <h3 className="font-medium text-gray-900">Alterar Senha</h3>
            
            <div className="space-y-2">
              <label className="text-sm font-medium text-gray-700">Senha Atual</label>
              <Input type="password" />
            </div>
            
            <div className="space-y-2">
              <label className="text-sm font-medium text-gray-700">Nova Senha</label>
              <Input type="password" />
            </div>
            
            <div className="space-y-2">
              <label className="text-sm font-medium text-gray-700">Confirmar Nova Senha</label>
              <Input type="password" />
            </div>
            
            <h3 className="font-medium text-gray-900 mt-8">Autenticação de Dois Fatores</h3>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div>
                <p className="font-medium">Ativar Autenticação de Dois Fatores</p>
                <p className="text-sm text-gray-600">Adicione uma camada extra de segurança à sua conta</p>
              </div>
              <Button variant="outline" size="sm">Configurar</Button>
            </div>
            
            <h3 className="font-medium text-gray-900 mt-8">Sessões Ativas</h3>
            
            <div className="p-4 bg-white/50 rounded-lg border border-gray-100">
              <div className="flex items-center justify-between mb-4">
                <div>
                  <p className="font-medium">Chrome em Windows</p>
                  <p className="text-sm text-gray-600">São Paulo, Brasil • Ativo agora</p>
                </div>
                <span className="text-xs bg-green-100 text-green-800 px-2 py-1 rounded-full">Atual</span>
              </div>
              
              <div className="flex items-center justify-between">
                <div>
                  <p className="font-medium">iPhone 13</p>
                  <p className="text-sm text-gray-600">São Paulo, Brasil • Última atividade: 2 horas atrás</p>
                </div>
                <Button variant="outline" size="sm">Encerrar</Button>
              </div>
            </div>
          </div>
          
          <div className="flex justify-end">
            <Button>
              <Save className="w-4 h-4 mr-2" />
              Salvar Alterações
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}

function IntegrationSettings() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Integrações</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="space-y-6">
          <div className="space-y-4">
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div className="flex items-center gap-4">
                <div className="w-10 h-10 bg-blue-100 rounded-lg flex items-center justify-center">
                  <Globe className="w-6 h-6 text-blue-600" />
                </div>
                <div>
                  <p className="font-medium">Google Calendar</p>
                  <p className="text-sm text-gray-600">Sincronize seus agendamentos com o Google Calendar</p>
                </div>
              </div>
              <Button variant="outline">Conectar</Button>
            </div>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div className="flex items-center gap-4">
                <div className="w-10 h-10 bg-green-100 rounded-lg flex items-center justify-center">
                  <Globe className="w-6 h-6 text-green-600" />
                </div>
                <div>
                  <p className="font-medium">WhatsApp Business</p>
                  <p className="text-sm text-gray-600">Envie notificações e lembretes via WhatsApp</p>
                </div>
              </div>
              <Button variant="outline">Conectar</Button>
            </div>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div className="flex items-center gap-4">
                <div className="w-10 h-10 bg-purple-100 rounded-lg flex items-center justify-center">
                  <Globe className="w-6 h-6 text-purple-600" />
                </div>
                <div>
                  <p className="font-medium">Instagram</p>
                  <p className="text-sm text-gray-600">Permita que clientes agendem diretamente pelo Instagram</p>
                </div>
              </div>
              <Button variant="outline">Conectar</Button>
            </div>
            
            <div className="flex items-center justify-between p-4 bg-white/50 rounded-lg border border-gray-100">
              <div className="flex items-center gap-4">
                <div className="w-10 h-10 bg-orange-100 rounded-lg flex items-center justify-center">
                  <Globe className="w-6 h-6 text-orange-600" />
                </div>
                <div>
                  <p className="font-medium">Mercado Pago</p>
                  <p className="text-sm text-gray-600">Processe pagamentos online</p>
                </div>
              </div>
              <Button variant="outline">Conectar</Button>
            </div>
          </div>
          
          <div className="flex justify-end">
            <Button>
              <Save className="w-4 h-4 mr-2" />
              Salvar Alterações
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}