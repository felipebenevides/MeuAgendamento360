'use client'

import { useState } from 'react'
import { 
  BarChart3, 
  Download, 
  Calendar, 
  Users, 
  DollarSign, 
  Scissors, 
  UserCheck,
  Filter,
  ChevronDown
} from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'
import { StatsCard } from '@/components/ui/stats-card'

export default function ReportsPage() {
  const [dateRange, setDateRange] = useState<'week' | 'month' | 'quarter' | 'year'>('month')
  const [activeTab, setActiveTab] = useState('overview')
  
  const tabs = [
    { id: 'overview', name: 'Visão Geral' },
    { id: 'financial', name: 'Financeiro' },
    { id: 'appointments', name: 'Agendamentos' },
    { id: 'customers', name: 'Clientes' },
    { id: 'staff', name: 'Equipe' }
  ]

  const stats = {
    revenue: 12450,
    appointments: 248,
    newCustomers: 32,
    avgTicket: 120
  }

  return (
    <DashboardLayout title="Relatórios" subtitle="Análise de desempenho do seu negócio">
      <div className="space-y-6">
        {/* Header Actions */}
        <div className="flex flex-col sm:flex-row gap-4 justify-between">
          <div className="flex items-center gap-4">
            <div className="flex bg-white/50 rounded-lg p-1">
              {tabs.map((tab) => (
                <button
                  key={tab.id}
                  onClick={() => setActiveTab(tab.id)}
                  className={`px-3 py-2 rounded-md text-sm font-medium transition-colors ${
                    activeTab === tab.id
                      ? 'bg-white shadow-sm text-gray-900'
                      : 'text-gray-600 hover:text-gray-900'
                  }`}
                >
                  {tab.name}
                </button>
              ))}
            </div>
          </div>
          
          <div className="flex items-center gap-3">
            <div className="flex bg-white/50 rounded-lg p-1">
              {(['week', 'month', 'quarter', 'year'] as const).map((range) => (
                <button
                  key={range}
                  onClick={() => setDateRange(range)}
                  className={`px-3 py-1 rounded-md text-sm font-medium transition-colors ${
                    dateRange === range
                      ? 'bg-white shadow-sm text-gray-900'
                      : 'text-gray-600 hover:text-gray-900'
                  }`}
                >
                  {range === 'week' ? 'Semana' : 
                   range === 'month' ? 'Mês' : 
                   range === 'quarter' ? 'Trimestre' : 'Ano'}
                </button>
              ))}
            </div>
            <Button variant="outline">
              <Filter className="w-4 h-4 mr-2" />
              Filtros
            </Button>
            <Button>
              <Download className="w-4 h-4 mr-2" />
              Exportar
            </Button>
          </div>
        </div>

        {/* Stats Cards */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
          <StatsCard
            title="Receita Total"
            value={`R$ ${stats.revenue}`}
            icon={DollarSign}
            trend={{ value: "+15%", direction: "up" }}
            color="green"
          />
          
          <StatsCard
            title="Agendamentos"
            value={stats.appointments}
            icon={Calendar}
            trend={{ value: "+8%", direction: "up" }}
            color="blue"
          />
          
          <StatsCard
            title="Novos Clientes"
            value={stats.newCustomers}
            icon={Users}
            trend={{ value: "+12%", direction: "up" }}
            color="purple"
          />
          
          <StatsCard
            title="Ticket Médio"
            value={`R$ ${stats.avgTicket}`}
            icon={DollarSign}
            trend={{ value: "+5%", direction: "up" }}
            color="orange"
          />
        </div>

        {/* Main Chart */}
        <Card>
          <CardHeader className="flex flex-row items-center justify-between">
            <CardTitle>Receita x Agendamentos</CardTitle>
            <Button variant="outline" size="sm">
              <ChevronDown className="w-4 h-4 mr-2" />
              Opções
            </Button>
          </CardHeader>
          <CardContent>
            <div className="h-80 flex items-center justify-center bg-gray-50 rounded-lg">
              <p className="text-gray-500">Gráfico de Receita x Agendamentos</p>
            </div>
          </CardContent>
        </Card>

        {/* Report Sections */}
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <Card>
            <CardHeader>
              <CardTitle>Serviços Mais Populares</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-64 flex items-center justify-center bg-gray-50 rounded-lg">
                <p className="text-gray-500">Gráfico de Serviços Mais Populares</p>
              </div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader>
              <CardTitle>Desempenho da Equipe</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-64 flex items-center justify-center bg-gray-50 rounded-lg">
                <p className="text-gray-500">Gráfico de Desempenho da Equipe</p>
              </div>
            </CardContent>
          </Card>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <Card>
            <CardHeader>
              <CardTitle>Horários de Pico</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-64 flex items-center justify-center bg-gray-50 rounded-lg">
                <p className="text-gray-500">Gráfico de Horários de Pico</p>
              </div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader>
              <CardTitle>Retenção de Clientes</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-64 flex items-center justify-center bg-gray-50 rounded-lg">
                <p className="text-gray-500">Gráfico de Retenção de Clientes</p>
              </div>
            </CardContent>
          </Card>
        </div>

        {/* KPI Summary */}
        <Card>
          <CardHeader>
            <CardTitle>Principais Indicadores de Desempenho (KPIs)</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
              <div className="p-4 bg-white/50 rounded-lg border border-gray-100">
                <div className="flex items-center gap-3 mb-2">
                  <div className="w-8 h-8 bg-blue-100 rounded-lg flex items-center justify-center">
                    <Calendar className="w-4 h-4 text-blue-600" />
                  </div>
                  <h3 className="font-semibold text-gray-900">Taxa de Ocupação</h3>
                </div>
                <p className="text-2xl font-bold text-gray-900 mb-1">85%</p>
                <p className="text-sm text-green-600 flex items-center">
                  <span className="mr-1">↑</span> 5% em relação ao período anterior
                </p>
              </div>
              
              <div className="p-4 bg-white/50 rounded-lg border border-gray-100">
                <div className="flex items-center gap-3 mb-2">
                  <div className="w-8 h-8 bg-green-100 rounded-lg flex items-center justify-center">
                    <Users className="w-4 h-4 text-green-600" />
                  </div>
                  <h3 className="font-semibold text-gray-900">Taxa de Retorno</h3>
                </div>
                <p className="text-2xl font-bold text-gray-900 mb-1">72%</p>
                <p className="text-sm text-green-600 flex items-center">
                  <span className="mr-1">↑</span> 3% em relação ao período anterior
                </p>
              </div>
              
              <div className="p-4 bg-white/50 rounded-lg border border-gray-100">
                <div className="flex items-center gap-3 mb-2">
                  <div className="w-8 h-8 bg-purple-100 rounded-lg flex items-center justify-center">
                    <UserCheck className="w-4 h-4 text-purple-600" />
                  </div>
                  <h3 className="font-semibold text-gray-900">Satisfação</h3>
                </div>
                <p className="text-2xl font-bold text-gray-900 mb-1">4.8/5</p>
                <p className="text-sm text-green-600 flex items-center">
                  <span className="mr-1">↑</span> 0.2 em relação ao período anterior
                </p>
              </div>
            </div>
          </CardContent>
        </Card>
      </div>
    </DashboardLayout>
  )
}