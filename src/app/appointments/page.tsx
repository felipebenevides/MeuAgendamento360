'use client'

import { useState } from 'react'
import { Calendar, Plus, Filter, Search, Clock, User } from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'

export default function AppointmentsPage() {
  const [view, setView] = useState<'day' | 'week' | 'month'>('day')
  
  const appointments = [
    {
      id: 1,
      time: '09:00',
      duration: 90,
      client: 'Maria Silva',
      service: 'Corte + Escova',
      staff: 'Ana Costa',
      price: 120,
      status: 'confirmed'
    },
    {
      id: 2,
      time: '10:30',
      duration: 45,
      client: 'João Santos',
      service: 'Barba + Cabelo',
      staff: 'Carlos Lima',
      price: 80,
      status: 'in-progress'
    },
    {
      id: 3,
      time: '14:00',
      duration: 60,
      client: 'Ana Costa',
      service: 'Manicure',
      staff: 'Lucia Ferreira',
      price: 50,
      status: 'pending'
    }
  ]

  const timeSlots = Array.from({ length: 12 }, (_, i) => {
    const hour = 8 + i
    return `${hour.toString().padStart(2, '0')}:00`
  })

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'confirmed': return 'border-l-green-500 bg-green-50'
      case 'in-progress': return 'border-l-blue-500 bg-blue-50'
      case 'pending': return 'border-l-yellow-500 bg-yellow-50'
      default: return 'border-l-gray-500 bg-gray-50'
    }
  }

  return (
    <DashboardLayout title="Agendamentos" subtitle="Gerencie todos os agendamentos">
      <div className="space-y-6">
        {/* Header Actions */}
        <div className="flex flex-col sm:flex-row gap-4 justify-between">
          <div className="flex items-center gap-4">
            <div className="relative">
              <Search className="w-5 h-5 absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
              <Input placeholder="Buscar agendamentos..." className="pl-10 w-80" />
            </div>
            <Button variant="outline">
              <Filter className="w-4 h-4" />
              Filtros
            </Button>
          </div>
          
          <div className="flex items-center gap-3">
            <div className="flex bg-white/50 rounded-lg p-1">
              {(['day', 'week', 'month'] as const).map((viewType) => (
                <button
                  key={viewType}
                  onClick={() => setView(viewType)}
                  className={`px-3 py-1 rounded-md text-sm font-medium transition-colors ${
                    view === viewType
                      ? 'bg-white shadow-sm text-gray-900'
                      : 'text-gray-600 hover:text-gray-900'
                  }`}
                >
                  {viewType === 'day' ? 'Dia' : viewType === 'week' ? 'Semana' : 'Mês'}
                </button>
              ))}
            </div>
            <Button>
              <Plus className="w-4 h-4" />
              Novo Agendamento
            </Button>
          </div>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-4 gap-6">
          {/* Calendar View */}
          <div className="lg:col-span-3">
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <Calendar className="w-5 h-5" />
                  Hoje - 19 de Junho, 2024
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="space-y-2">
                  {timeSlots.map((time) => {
                    const appointment = appointments.find(apt => apt.time === time)
                    
                    return (
                      <div key={time} className="flex items-center gap-4 py-2 border-b border-gray-100 last:border-0">
                        <div className="w-16 text-sm text-gray-600 font-medium">
                          {time}
                        </div>
                        
                        <div className="flex-1">
                          {appointment ? (
                            <div className={`p-4 rounded-lg border-l-4 ${getStatusColor(appointment.status)}`}>
                              <div className="flex items-center justify-between">
                                <div>
                                  <h3 className="font-semibold text-gray-900">{appointment.client}</h3>
                                  <p className="text-sm text-gray-600">{appointment.service}</p>
                                  <div className="flex items-center gap-4 mt-2 text-xs text-gray-500">
                                    <span className="flex items-center gap-1">
                                      <User className="w-3 h-3" />
                                      {appointment.staff}
                                    </span>
                                    <span className="flex items-center gap-1">
                                      <Clock className="w-3 h-3" />
                                      {appointment.duration}min
                                    </span>
                                  </div>
                                </div>
                                <div className="text-right">
                                  <p className="font-semibold text-gray-900">R$ {appointment.price}</p>
                                  <Button variant="ghost" size="sm">
                                    Editar
                                  </Button>
                                </div>
                              </div>
                            </div>
                          ) : (
                            <div className="p-4 border-2 border-dashed border-gray-200 rounded-lg text-center">
                              <p className="text-sm text-gray-500">Horário disponível</p>
                              <Button variant="ghost" size="sm" className="mt-1">
                                <Plus className="w-3 h-3" />
                                Agendar
                              </Button>
                            </div>
                          )}
                        </div>
                      </div>
                    )
                  })}
                </div>
              </CardContent>
            </Card>
          </div>

          {/* Sidebar */}
          <div className="space-y-6">
            <Card>
              <CardHeader>
                <CardTitle>Resumo do Dia</CardTitle>
              </CardHeader>
              <CardContent>
                <div className="space-y-4">
                  <div className="flex justify-between">
                    <span className="text-sm text-gray-600">Total de Agendamentos</span>
                    <span className="font-semibold">24</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="text-sm text-gray-600">Confirmados</span>
                    <span className="font-semibold text-green-600">18</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="text-sm text-gray-600">Pendentes</span>
                    <span className="font-semibold text-yellow-600">4</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="text-sm text-gray-600">Em Andamento</span>
                    <span className="font-semibold text-blue-600">2</span>
                  </div>
                  <hr />
                  <div className="flex justify-between">
                    <span className="text-sm text-gray-600">Receita Prevista</span>
                    <span className="font-semibold text-green-600">R$ 2.450</span>
                  </div>
                </div>
              </CardContent>
            </Card>

            <Card>
              <CardHeader>
                <CardTitle>Próximos Agendamentos</CardTitle>
              </CardHeader>
              <CardContent>
                <div className="space-y-3">
                  {appointments.slice(0, 3).map((apt) => (
                    <div key={apt.id} className="p-3 bg-gray-50 rounded-lg">
                      <div className="flex justify-between items-start">
                        <div>
                          <p className="font-medium text-sm">{apt.client}</p>
                          <p className="text-xs text-gray-600">{apt.service}</p>
                        </div>
                        <span className="text-xs font-medium text-gray-900">{apt.time}</span>
                      </div>
                    </div>
                  ))}
                </div>
              </CardContent>
            </Card>
          </div>
        </div>
      </div>
    </DashboardLayout>
  )
}