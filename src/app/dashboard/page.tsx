'use client'

import { useState } from 'react'
import { 
  Calendar, 
  Users, 
  DollarSign, 
  TrendingUp, 
  Clock, 
  Star,
  Plus,
  Filter,
  MoreVertical,
  ArrowUpRight,
  ArrowDownRight
} from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'

export default function DashboardPage() {
  const stats = [
    {
      title: 'Receita Hoje',
      value: 'R$ 2.450',
      change: '+12%',
      trend: 'up',
      icon: DollarSign,
      bgColor: 'bg-green-50',
      iconColor: 'text-green-600'
    },
    {
      title: 'Agendamentos',
      value: '24',
      change: '+8%',
      trend: 'up',
      icon: Calendar,
      bgColor: 'bg-blue-50',
      iconColor: 'text-blue-600'
    },
    {
      title: 'Novos Clientes',
      value: '12',
      change: '+15%',
      trend: 'up',
      icon: Users,
      bgColor: 'bg-purple-50',
      iconColor: 'text-purple-600'
    },
    {
      title: 'Taxa de Ocupação',
      value: '85%',
      change: '-3%',
      trend: 'down',
      icon: TrendingUp,
      bgColor: 'bg-orange-50',
      iconColor: 'text-orange-600'
    }
  ]

  const appointments = [
    {
      id: 1,
      client: 'Maria Silva',
      service: 'Corte + Escova',
      time: '09:00',
      duration: '1h 30min',
      price: 'R$ 120',
      status: 'confirmed',
      avatar: 'MS'
    },
    {
      id: 2,
      client: 'João Santos',
      service: 'Barba + Cabelo',
      time: '10:30',
      duration: '45min',
      price: 'R$ 80',
      status: 'in-progress',
      avatar: 'JS'
    },
    {
      id: 3,
      client: 'Ana Costa',
      service: 'Manicure',
      time: '14:00',
      duration: '1h',
      price: 'R$ 50',
      status: 'pending',
      avatar: 'AC'
    }
  ]

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'confirmed': return 'bg-green-100 text-green-800'
      case 'in-progress': return 'bg-blue-100 text-blue-800'
      case 'pending': return 'bg-yellow-100 text-yellow-800'
      default: return 'bg-gray-100 text-gray-800'
    }
  }

  const getStatusText = (status: string) => {
    switch (status) {
      case 'confirmed': return 'Confirmado'
      case 'in-progress': return 'Em Andamento'
      case 'pending': return 'Pendente'
      default: return 'Desconhecido'
    }
  }

  return (
    <DashboardLayout title="Dashboard" subtitle="Bem-vindo de volta, Felipe!">
      {/* Stats Grid */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
        {stats.map((stat, index) => (
          <Card key={stat.title} className="animate-fade-in-up" style={{ animationDelay: `${index * 0.1}s` }}>
            <CardContent className="p-6">
              <div className="flex items-center justify-between mb-4">
                <div className={`w-12 h-12 ${stat.bgColor} rounded-xl flex items-center justify-center`}>
                  <stat.icon className={`w-6 h-6 ${stat.iconColor}`} />
                </div>
                <div className={`flex items-center gap-1 text-sm font-medium ${
                  stat.trend === 'up' ? 'text-green-600' : 'text-red-600'
                }`}>
                  {stat.trend === 'up' ? <ArrowUpRight className="w-4 h-4" /> : <ArrowDownRight className="w-4 h-4" />}
                  {stat.change}
                </div>
              </div>
              <div>
                <p className="text-2xl font-bold text-gray-900 mb-1">{stat.value}</p>
                <p className="text-sm text-gray-600">{stat.title}</p>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        {/* Appointments */}
        <div className="lg:col-span-2">
          <Card className="animate-fade-in-up">
            <CardHeader>
              <div className="flex items-center justify-between">
                <div>
                  <CardTitle>Agendamentos de Hoje</CardTitle>
                  <p className="text-sm text-gray-600">24 agendamentos • 6 disponíveis</p>
                </div>
                <div className="flex items-center gap-3">
                  <Button variant="outline" size="sm">
                    <Filter className="w-4 h-4" />
                    Filtrar
                  </Button>
                  <Button size="sm">
                    <Plus className="w-4 h-4" />
                    Novo
                  </Button>
                </div>
              </div>
            </CardHeader>
            <CardContent>
              <div className="space-y-4">
                {appointments.map((appointment, index) => (
                  <div 
                    key={appointment.id}
                    className="flex items-center justify-between p-4 bg-white/50 rounded-xl border border-gray-100 hover:shadow-md transition-all duration-200"
                  >
                    <div className="flex items-center gap-4">
                      <div className="w-12 h-12 bg-gradient-primary rounded-full flex items-center justify-center">
                        <span className="text-white font-semibold text-sm">{appointment.avatar}</span>
                      </div>
                      <div>
                        <h3 className="font-semibold text-gray-900">{appointment.client}</h3>
                        <p className="text-sm text-gray-600">{appointment.service}</p>
                      </div>
                    </div>
                    
                    <div className="flex items-center gap-6">
                      <div className="text-right">
                        <p className="font-semibold text-gray-900">{appointment.time}</p>
                        <p className="text-sm text-gray-600">{appointment.duration}</p>
                      </div>
                      
                      <div className="text-right">
                        <p className="font-semibold text-gray-900">{appointment.price}</p>
                        <span className={`inline-block px-2 py-1 rounded-full text-xs font-medium ${getStatusColor(appointment.status)}`}>
                          {getStatusText(appointment.status)}
                        </span>
                      </div>
                      
                      <Button variant="ghost" size="icon">
                        <MoreVertical className="w-4 h-4" />
                      </Button>
                    </div>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>
        </div>

        {/* Sidebar */}
        <div className="space-y-6">
          <Card>
            <CardHeader>
              <CardTitle>Ações Rápidas</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-3">
                <Button className="w-full justify-start" variant="outline">
                  <Plus className="w-4 h-4" />
                  Novo Agendamento
                </Button>
                <Button className="w-full justify-start" variant="outline">
                  <Users className="w-4 h-4" />
                  Cadastrar Cliente
                </Button>
                <Button className="w-full justify-start" variant="outline">
                  <DollarSign className="w-4 h-4" />
                  Registrar Venda
                </Button>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle>Performance</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-4">
                <div>
                  <div className="flex justify-between items-center mb-2">
                    <span className="text-sm text-gray-600">Meta Mensal</span>
                    <span className="text-sm font-semibold">75%</span>
                  </div>
                  <div className="w-full bg-gray-200 rounded-full h-2">
                    <div className="bg-gradient-primary h-2 rounded-full" style={{ width: '75%' }}></div>
                  </div>
                </div>
                
                <div>
                  <div className="flex justify-between items-center mb-2">
                    <span className="text-sm text-gray-600">Satisfação</span>
                    <div className="flex items-center gap-1">
                      <Star className="w-4 h-4 text-yellow-500 fill-current" />
                      <span className="text-sm font-semibold">4.8</span>
                    </div>
                  </div>
                  <div className="w-full bg-gray-200 rounded-full h-2">
                    <div className="bg-gradient-to-r from-yellow-400 to-yellow-500 h-2 rounded-full" style={{ width: '96%' }}></div>
                  </div>
                </div>
              </div>
            </CardContent>
          </Card>
        </div>
      </div>
    </DashboardLayout>
  )
}