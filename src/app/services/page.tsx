'use client'

import { useState } from 'react'
import { Scissors, Plus, Search, Filter, Clock, DollarSign, Users, MoreVertical, ToggleLeft, ToggleRight } from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'
import Link from 'next/link'

export default function ServicesPage() {
  const [activeCategory, setActiveCategory] = useState('all')
  
  const categories = [
    { id: 'all', name: 'Todos' },
    { id: 'hair', name: 'Cabelo' },
    { id: 'beard', name: 'Barba' },
    { id: 'nails', name: 'Unhas' },
    { id: 'makeup', name: 'Maquiagem' },
    { id: 'spa', name: 'Spa' }
  ]
  
  const services = [
    {
      id: 1,
      name: 'Corte Feminino',
      description: 'Corte, lavagem e finalização',
      price: 120,
      duration: 60,
      category: 'hair',
      staff: ['Ana Costa', 'Lucia Ferreira'],
      status: 'active',
      popularity: 'high'
    },
    {
      id: 2,
      name: 'Barba Completa',
      description: 'Barba com toalha quente e produtos especiais',
      price: 60,
      duration: 30,
      category: 'beard',
      staff: ['Carlos Lima'],
      status: 'active',
      popularity: 'medium'
    },
    {
      id: 3,
      name: 'Manicure',
      description: 'Tratamento completo para unhas',
      price: 50,
      duration: 45,
      category: 'nails',
      staff: ['Lucia Ferreira'],
      status: 'active',
      popularity: 'high'
    },
    {
      id: 4,
      name: 'Maquiagem para Eventos',
      description: 'Maquiagem completa para festas e eventos',
      price: 150,
      duration: 90,
      category: 'makeup',
      staff: ['Ana Costa'],
      status: 'inactive',
      popularity: 'low'
    }
  ]

  const getPopularityColor = (popularity: string) => {
    switch (popularity) {
      case 'high': return 'bg-green-100 text-green-800'
      case 'medium': return 'bg-blue-100 text-blue-800'
      case 'low': return 'bg-yellow-100 text-yellow-800'
      default: return 'bg-gray-100 text-gray-800'
    }
  }

  const getPopularityText = (popularity: string) => {
    switch (popularity) {
      case 'high': return 'Alta'
      case 'medium': return 'Média'
      case 'low': return 'Baixa'
      default: return 'Desconhecida'
    }
  }

  const filteredServices = activeCategory === 'all' 
    ? services 
    : services.filter(service => service.category === activeCategory)

  return (
    <DashboardLayout title="Serviços" subtitle="Gerencie os serviços oferecidos">
      <div className="space-y-6">
        {/* Header Actions */}
        <div className="flex flex-col sm:flex-row gap-4 justify-between">
          <div className="flex items-center gap-4">
            <div className="relative">
              <Search className="w-5 h-5 absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
              <Input placeholder="Buscar serviços..." className="pl-10 w-80" />
            </div>
            <Button variant="outline">
              <Filter className="w-4 h-4" />
              Filtros
            </Button>
          </div>
          
          <Link href="/services/create">
            <Button>
              <Plus className="w-4 h-4" />
              Novo Serviço
            </Button>
          </Link>
        </div>

        {/* Categories */}
        <div className="flex flex-wrap gap-2">
          {categories.map(category => (
            <button
              key={category.id}
              onClick={() => setActiveCategory(category.id)}
              className={`px-4 py-2 rounded-full text-sm font-medium transition-colors ${
                activeCategory === category.id
                  ? 'bg-gradient-primary text-white shadow-glow'
                  : 'bg-white text-gray-700 hover:bg-gray-100'
              }`}
            >
              {category.name}
            </button>
          ))}
        </div>

        {/* Stats Cards */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
          <Card>
            <CardContent className="p-6">
              <div className="flex items-center gap-4">
                <div className="w-12 h-12 bg-blue-50 rounded-xl flex items-center justify-center">
                  <Scissors className="w-6 h-6 text-blue-600" />
                </div>
                <div>
                  <p className="text-2xl font-bold text-gray-900">24</p>
                  <p className="text-sm text-gray-600">Total de Serviços</p>
                </div>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardContent className="p-6">
              <div className="flex items-center gap-4">
                <div className="w-12 h-12 bg-green-50 rounded-xl flex items-center justify-center">
                  <DollarSign className="w-6 h-6 text-green-600" />
                </div>
                <div>
                  <p className="text-2xl font-bold text-gray-900">R$ 12.450</p>
                  <p className="text-sm text-gray-600">Receita Mensal</p>
                </div>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardContent className="p-6">
              <div className="flex items-center gap-4">
                <div className="w-12 h-12 bg-purple-50 rounded-xl flex items-center justify-center">
                  <Clock className="w-6 h-6 text-purple-600" />
                </div>
                <div>
                  <p className="text-2xl font-bold text-gray-900">45 min</p>
                  <p className="text-sm text-gray-600">Duração Média</p>
                </div>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardContent className="p-6">
              <div className="flex items-center gap-4">
                <div className="w-12 h-12 bg-orange-50 rounded-xl flex items-center justify-center">
                  <Users className="w-6 h-6 text-orange-600" />
                </div>
                <div>
                  <p className="text-2xl font-bold text-gray-900">186</p>
                  <p className="text-sm text-gray-600">Agendamentos</p>
                </div>
              </div>
            </CardContent>
          </Card>
        </div>

        {/* Services List */}
        <Card>
          <CardHeader>
            <CardTitle>Lista de Serviços</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {filteredServices.map((service) => (
                <div 
                  key={service.id}
                  className="flex items-center justify-between p-4 bg-white/50 rounded-xl border border-gray-100 hover:shadow-md transition-all duration-200"
                >
                  <div className="flex items-center gap-4">
                    <div className="w-12 h-12 bg-gradient-primary rounded-xl flex items-center justify-center">
                      <Scissors className="w-6 h-6 text-white" />
                    </div>
                    <div>
                      <h3 className="font-semibold text-gray-900">{service.name}</h3>
                      <p className="text-sm text-gray-600">{service.description}</p>
                    </div>
                  </div>
                  
                  <div className="flex items-center gap-6">
                    <div className="text-center">
                      <p className="text-sm font-semibold text-gray-900">R$ {service.price}</p>
                      <p className="text-xs text-gray-600">Preço</p>
                    </div>
                    
                    <div className="text-center">
                      <p className="text-sm font-semibold text-gray-900">{service.duration} min</p>
                      <p className="text-xs text-gray-600">Duração</p>
                    </div>
                    
                    <div className="text-center">
                      <p className="text-sm font-semibold text-gray-900">{service.staff.length}</p>
                      <p className="text-xs text-gray-600">Profissionais</p>
                    </div>
                    
                    <span className={`inline-block px-2 py-1 rounded-full text-xs font-medium ${getPopularityColor(service.popularity)}`}>
                      {getPopularityText(service.popularity)}
                    </span>
                    
                    <div className="flex items-center gap-2">
                      <Button variant="ghost" size="sm" className="gap-2">
                        {service.status === 'active' ? (
                          <>
                            <ToggleRight className="w-4 h-4 text-green-600" />
                            <span className="text-green-600">Ativo</span>
                          </>
                        ) : (
                          <>
                            <ToggleLeft className="w-4 h-4 text-gray-400" />
                            <span className="text-gray-400">Inativo</span>
                          </>
                        )}
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