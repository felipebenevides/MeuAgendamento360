'use client'

import { useState } from 'react'
import { DashboardLayout } from '@/components/layout/dashboard-layout'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Bell, Calendar, DollarSign, MessageSquare, Users } from 'lucide-react'

export default function NotificationSettingsPage() {
  const [preferences, setPreferences] = useState({
    appointments: {
      email: true,
      push: true,
      sms: false
    },
    reminders: {
      email: true,
      push: true,
      sms: true
    },
    marketing: {
      email: false,
      push: false,
      sms: false
    },
    payments: {
      email: true,
      push: false,
      sms: false
    },
    system: {
      email: true,
      push: true,
      sms: false
    }
  })

  const togglePreference = (category: keyof typeof preferences, channel: keyof typeof preferences.appointments) => {
    setPreferences(prev => ({
      ...prev,
      [category]: {
        ...prev[category],
        [channel]: !prev[category][channel]
      }
    }))
  }

  const handleSave = async () => {
    // Here you would save the preferences to the API
    console.log('Saving preferences:', preferences)
    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 1000))
    alert('Preferências salvas com sucesso!')
  }

  return (
    <DashboardLayout title="Preferências de Notificações" subtitle="Gerencie como deseja receber notificações">
      <div className="space-y-6 max-w-4xl mx-auto">
        <Card>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Bell className="w-5 h-5" />
              Configurações de Notificações
            </CardTitle>
          </CardHeader>
          <CardContent>
            <p className="text-sm text-gray-600 mb-6">
              Escolha quais notificações deseja receber e por quais canais.
            </p>

            <div className="space-y-8">
              {/* Appointments Notifications */}
              <div className="border-b pb-6">
                <div className="flex items-center gap-3 mb-4">
                  <Calendar className="w-5 h-5 text-primary" />
                  <h3 className="font-medium">Agendamentos</h3>
                </div>
                
                <div className="grid grid-cols-4 gap-4">
                  <div className="col-span-1"></div>
                  <div className="text-center text-sm font-medium">Email</div>
                  <div className="text-center text-sm font-medium">Push</div>
                  <div className="text-center text-sm font-medium">SMS</div>
                  
                  <div className="text-sm">Novos agendamentos</div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.appointments.email ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('appointments', 'email')}
                    />
                  </div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.appointments.push ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('appointments', 'push')}
                    />
                  </div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.appointments.sms ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('appointments', 'sms')}
                    />
                  </div>
                </div>
              </div>
              
              {/* Reminders */}
              <div className="border-b pb-6">
                <div className="flex items-center gap-3 mb-4">
                  <Bell className="w-5 h-5 text-primary" />
                  <h3 className="font-medium">Lembretes</h3>
                </div>
                
                <div className="grid grid-cols-4 gap-4">
                  <div className="col-span-1"></div>
                  <div className="text-center text-sm font-medium">Email</div>
                  <div className="text-center text-sm font-medium">Push</div>
                  <div className="text-center text-sm font-medium">SMS</div>
                  
                  <div className="text-sm">Lembretes de agendamentos</div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.reminders.email ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('reminders', 'email')}
                    />
                  </div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.reminders.push ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('reminders', 'push')}
                    />
                  </div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.reminders.sms ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('reminders', 'sms')}
                    />
                  </div>
                </div>
              </div>
              
              {/* Marketing */}
              <div className="border-b pb-6">
                <div className="flex items-center gap-3 mb-4">
                  <MessageSquare className="w-5 h-5 text-primary" />
                  <h3 className="font-medium">Marketing</h3>
                </div>
                
                <div className="grid grid-cols-4 gap-4">
                  <div className="col-span-1"></div>
                  <div className="text-center text-sm font-medium">Email</div>
                  <div className="text-center text-sm font-medium">Push</div>
                  <div className="text-center text-sm font-medium">SMS</div>
                  
                  <div className="text-sm">Promoções e novidades</div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.marketing.email ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('marketing', 'email')}
                    />
                  </div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.marketing.push ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('marketing', 'push')}
                    />
                  </div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.marketing.sms ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('marketing', 'sms')}
                    />
                  </div>
                </div>
              </div>
              
              {/* Payments */}
              <div className="border-b pb-6">
                <div className="flex items-center gap-3 mb-4">
                  <DollarSign className="w-5 h-5 text-primary" />
                  <h3 className="font-medium">Pagamentos</h3>
                </div>
                
                <div className="grid grid-cols-4 gap-4">
                  <div className="col-span-1"></div>
                  <div className="text-center text-sm font-medium">Email</div>
                  <div className="text-center text-sm font-medium">Push</div>
                  <div className="text-center text-sm font-medium">SMS</div>
                  
                  <div className="text-sm">Confirmações de pagamento</div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.payments.email ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('payments', 'email')}
                    />
                  </div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.payments.push ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('payments', 'push')}
                    />
                  </div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.payments.sms ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('payments', 'sms')}
                    />
                  </div>
                </div>
              </div>
              
              {/* System */}
              <div className="pb-6">
                <div className="flex items-center gap-3 mb-4">
                  <Users className="w-5 h-5 text-primary" />
                  <h3 className="font-medium">Sistema</h3>
                </div>
                
                <div className="grid grid-cols-4 gap-4">
                  <div className="col-span-1"></div>
                  <div className="text-center text-sm font-medium">Email</div>
                  <div className="text-center text-sm font-medium">Push</div>
                  <div className="text-center text-sm font-medium">SMS</div>
                  
                  <div className="text-sm">Atualizações do sistema</div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.system.email ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('system', 'email')}
                    />
                  </div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.system.push ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('system', 'push')}
                    />
                  </div>
                  <div className="text-center">
                    <button 
                      className={`w-6 h-6 rounded ${preferences.system.sms ? 'bg-primary' : 'bg-gray-200'}`}
                      onClick={() => togglePreference('system', 'sms')}
                    />
                  </div>
                </div>
              </div>
            </div>
            
            <div className="mt-8 flex justify-end">
              <Button onClick={handleSave}>
                Salvar Preferências
              </Button>
            </div>
          </CardContent>
        </Card>
      </div>
    </DashboardLayout>
  )
}