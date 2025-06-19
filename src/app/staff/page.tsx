'use client'

import { useState } from 'react'
import { UserCheck, Plus, Search, Filter, Phone, Mail, Calendar, MoreVertical, Star, Scissors } from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'

export default function StaffPage() {
  const staffMembers = [
    {
      id: 1,
      name: 'Ana Costa',
      email: 'ana@email.com',
      phone: '(11) 99999-9999',
      role: 'Cabeleireira',
      services: ['Corte Feminino', 'Coloração', 'Maquiagem'],
      appointments: 156,
      rating: 4.8,
      status: 'active',
      avatar: 'AC'
    },
    {
      id: 2,
      name: 'Carlos Lima',
      email: 'carlos@email.com',
      phone: '(11) 88888-8888',
      role: 'Barbeiro',
      services: ['Corte Masculino', 'Barba'],
      appointments: 124,
      rating: 4.9,
      status: 'active',
      avatar: 'CL'
    },
    {
      id: 3,
      name: 'Lucia Ferreira',
      email: 'lucia@email.com',
      phone: '(11) 77777-7777',
      role: 'Manicure',
      services: ['Manicure', 'Pedicure', 'Unhas em Gel'],
      appointments: 98,
      rating: 4.7,
      status: 'active',
      avatar: 'LF'
    },
    {
      id: 4,
      name: 'Roberto Santos',
      email: 'roberto@email.com',
      phone: '(11) 66666-6666',
      role: 'Massagista',
      services: ['Massagem Relaxante', 'Massagem Terapêutica'],
      appointments: 45,
      rating: 4.5,
      status: 'inactive',
      avatar: 'RS'
    }
  ]

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'active': return 'bg-green-100 text-green-800'
      case 'inactive': return 'bg-gray-100 text-gray-800'
      default: return 'bg-gray-100 text-gray-800'
    }
  }

  const getStatusText = (status: string) => {
    switch (status) {
      case 'active': return 'Ativo'
      case 'inactive': return 'Inativo'
      default: return 'Desconhecido'
    }
  }

  return (
    <DashboardLayout title="Equipe" subtitle="Gerencie sua equipe de profissionais">
      <div className="space-y-6">
        {/* Header Actions */}
        <div className="flex flex-col sm:flex-row gap-4 justify-between">
          <div className="flex items-center gap-4">
            <div className="relative">
              <Search className="w-5 h-5 absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
              <Input placeholder="Buscar profissionais..." className="pl-10 w-80" />
            </div>
            <Button variant="outline">
              <Filter className="w-4 h-4" />
              Filtros
            </Button>
          </div>
          
          <Button>
            <Plus className="w-4 h-4" />
            Novo Profissional
          </Button>
        </div>

        {/* Stats Cards */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
          <Card>
            <CardContent className="p-6">
              <div className="flex items-center gap-4">
                <div className="w-12 h-12 bg-blue-50 rounded-xl flex items-center justify-center">
                  <UserCheck className="w-6 h-6 text-blue-600" />
                </div>
                <div>
                  <p className="text-2xl font-bold text-gray-900">12</p>
                  <p className="text-sm text-gray-600">Total de Profissionais</p>
                </div>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardContent className="p-6">
              <div className="flex items-center gap-4">
                <div className="w-12 h-12 bg-green-50 rounded-xl flex items-center justify-center">
                  <Calendar className="w-6 h-6 text-green-600" />
                </div>
                <div>
                  <p className="text-2xl font-bold text-gray-900">423</p>
                  <p className="text-sm text-gray-600">Agendamentos</p>
                </div>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardContent className="p-6">
              <div className="flex items-center gap-4">
                <div className="w-12 h-12 bg-purple-50 rounded-xl flex items-center justify-center">
                  <Star className="w-6 h-6 text-purple-600" />
                </div>
                <div>
                  <p className="text-2xl font-bold text-gray-900">4.8</p>
                  <p className="text-sm text-gray-600">Avaliação Média</p>
                </div>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardContent className="p-6">
              <div className="flex items-center gap-4">
                <div className="w-12 h-12 bg-orange-50 rounded-xl flex items-center justify-center">
                  <Scissors className="w-6 h-6 text-orange-600" />
                </div>
                <div>
                  <p className="text-2xl font-bold text-gray-900">18</p>
                  <p className="text-sm text-gray-600">Serviços Oferecidos</p>
                </div>
              </div>
            </CardContent>
          </Card>
        </div>

        {/* Staff List */}
        <Card>
          <CardHeader>
            <CardTitle>Profissionais</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {staffMembers.map((staff) => (
                <div 
                  key={staff.id}
                  className="flex items-center justify-between p-4 bg-white/50 rounded-xl border border-gray-100 hover:shadow-md transition-all duration-200"
                >
                  <div className="flex items-center gap-4">
                    <div className="w-12 h-12 bg-gradient-primary rounded-full flex items-center justify-center">
                      <span className="text-white font-semibold text-sm">{staff.avatar}</span>
                    </div>
                    <div>
                      <h3 className="font-semibold text-gray-900">{staff.name}</h3>
                      <p className="text-sm text-gray-600">{staff.role}</p>
                      <div className="flex items-center gap-4 mt-1">
                        <span className="flex items-center gap-1 text-xs text-gray-600">
                          <Mail className="w-3 h-3" />
                          {staff.email}
                        </span>
                        <span className="flex items-center gap-1 text-xs text-gray-600">
                          <Phone className="w-3 h-3" />
                          {staff.phone}
                        </span>
                      </div>
                    </div>
                  </div>
                  
                  <div className="flex items-center gap-6">
                    <div className="text-center">
                      <p className="text-sm font-semibold text-gray-900">{staff.appointments}</p>
                      <p className="text-xs text-gray-600">Agendamentos</p>
                    </div>
                    
                    <div className="text-center">
                      <p className="text-sm font-semibold text-gray-900">{staff.services.length}</p>
                      <p className="text-xs text-gray-600">Serviços</p>
                    </div>
                    
                    <div className="text-center flex items-center gap-1">
                      <Star className="w-4 h-4 text-yellow-500 fill-current" />
                      <p className="text-sm font-semibold text-gray-900">{staff.rating}</p>
                    </div>
                    
                    <span className={`inline-block px-2 py-1 rounded-full text-xs font-medium ${getStatusColor(staff.status)}`}>
                      {getStatusText(staff.status)}
                    </span>
                    
                    <div className="flex items-center gap-2">
                      <Button variant="outline" size="sm">
                        <Calendar className="w-3 h-3" />
                        Agenda
                      </Button>
                      <Button variant="ghost" size="icon">
                        <MoreVertical className="w-4 h-4" />
                      </Button>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      </div>
    </DashboardLayout>
  )
}