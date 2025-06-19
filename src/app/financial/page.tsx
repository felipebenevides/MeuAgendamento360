'use client'

import { useState } from 'react'
import { 
  DollarSign, 
  Plus, 
  Search, 
  Filter, 
  TrendingUp, 
  TrendingDown, 
  Calendar, 
  CreditCard, 
  Download,
  ArrowUpRight,
  ArrowDownRight
} from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'
import { StatsCard } from '@/components/ui/stats-card'
import { StatusBadge } from '@/components/ui/status-badge'

export default function FinancialPage() {
  const [dateRange, setDateRange] = useState<'day' | 'week' | 'month' | 'year'>('month')
  
  const transactions = [
    {
      id: 1,
      date: '2024-06-19',
      description: 'Corte + Escova - Maria Silva',
      amount: 120,
      type: 'income',
      category: 'service',
      paymentMethod: 'credit_card',
      status: 'completed'
    },
    {
      id: 2,
      date: '2024-06-19',
      description: 'Barba + Cabelo - João Santos',
      amount: 80,
      type: 'income',
      category: 'service',
      paymentMethod: 'pix',
      status: 'completed'
    },
    {
      id: 3,
      date: '2024-06-18',
      description: 'Compra de Produtos - Fornecedor X',
      amount: 350,
      type: 'expense',
      category: 'inventory',
      paymentMethod: 'bank_transfer',
      status: 'completed'
    },
    {
      id: 4,
      date: '2024-06-17',
      description: 'Pagamento de Aluguel',
      amount: 1200,
      type: 'expense',
      category: 'rent',
      paymentMethod: 'bank_transfer',
      status: 'completed'
    },
    {
      id: 5,
      date: '2024-06-15',
      description: 'Manicure - Ana Costa',
      amount: 50,
      type: 'income',
      category: 'service',
      paymentMethod: 'cash',
      status: 'completed'
    }
  ]

  const stats = {
    revenue: 2450,
    expenses: 1550,
    profit: 900,
    profitMargin: 36.7
  }

  const getPaymentMethodText = (method: string) => {
    switch (method) {
      case 'credit_card': return 'Cartão de Crédito'
      case 'debit_card': return 'Cartão de Débito'
      case 'cash': return 'Dinheiro'
      case 'pix': return 'PIX'
      case 'bank_transfer': return 'Transferência'
      default: return method
    }
  }

  return (
    <DashboardLayout title="Financeiro" subtitle="Gerencie suas finanças">
      <div className="space-y-6">
        {/* Header Actions */}
        <div className="flex flex-col sm:flex-row gap-4 justify-between">
          <div className="flex items-center gap-4">
            <div className="relative">
              <Search className="w-5 h-5 absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
              <Input placeholder="Buscar transações..." className="pl-10 w-80" />
            </div>
            <Button variant="outline">
              <Filter className="w-4 h-4" />
              Filtros
            </Button>
          </div>
          
          <div className="flex items-center gap-3">
            <div className="flex bg-white/50 rounded-lg p-1">
              {(['day', 'week', 'month', 'year'] as const).map((range) => (
                <button
                  key={range}
                  onClick={() => setDateRange(range)}
                  className={`px-3 py-1 rounded-md text-sm font-medium transition-colors ${
                    dateRange === range
                      ? 'bg-white shadow-sm text-gray-900'
                      : 'text-gray-600 hover:text-gray-900'
                  }`}
                >
                  {range === 'day' ? 'Dia' : 
                   range === 'week' ? 'Semana' : 
                   range === 'month' ? 'Mês' : 'Ano'}
                </button>
              ))}
            </div>
            <Button>
              <Plus className="w-4 h-4" />
              Nova Transação
            </Button>
          </div>
        </div>

        {/* Stats Cards */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
          <StatsCard
            title="Receita"
            value={`R$ ${stats.revenue}`}
            icon={TrendingUp}
            trend={{ value: "+12%", direction: "up" }}
            color="green"
          />
          
          <StatsCard
            title="Despesas"
            value={`R$ ${stats.expenses}`}
            icon={TrendingDown}
            trend={{ value: "+5%", direction: "down" }}
            color="red"
          />
          
          <StatsCard
            title="Lucro"
            value={`R$ ${stats.profit}`}
            icon={DollarSign}
            trend={{ value: "+8%", direction: "up" }}
            color="blue"
          />
          
          <StatsCard
            title="Margem de Lucro"
            value={`${stats.profitMargin}%`}
            icon={TrendingUp}
            trend={{ value: "+2%", direction: "up" }}
            color="purple"
          />
        </div>

        {/* Revenue Chart */}
        <Card>
          <CardHeader>
            <CardTitle>Receita vs Despesas</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="h-80 flex items-center justify-center bg-gray-50 rounded-lg">
              <p className="text-gray-500">Gráfico de Receita vs Despesas</p>
            </div>
          </CardContent>
        </Card>

        {/* Transactions */}
        <Card>
          <CardHeader className="flex flex-row items-center justify-between">
            <CardTitle>Transações Recentes</CardTitle>
            <Button variant="outline" size="sm">
              <Download className="w-4 h-4 mr-2" />
              Exportar
            </Button>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {transactions.map((transaction) => (
                <div 
                  key={transaction.id}
                  className="flex items-center justify-between p-4 bg-white/50 rounded-xl border border-gray-100 hover:shadow-md transition-all duration-200"
                >
                  <div className="flex items-center gap-4">
                    <div className={`w-10 h-10 rounded-full flex items-center justify-center ${
                      transaction.type === 'income' 
                        ? 'bg-green-100 text-green-600' 
                        : 'bg-red-100 text-red-600'
                    }`}>
                      {transaction.type === 'income' ? (
                        <ArrowUpRight className="w-5 h-5" />
                      ) : (
                        <ArrowDownRight className="w-5 h-5" />
                      )}
                    </div>
                    <div>
                      <h3 className="font-semibold text-gray-900">{transaction.description}</h3>
                      <p className="text-sm text-gray-600">{transaction.date}</p>
                    </div>
                  </div>
                  
                  <div className="flex items-center gap-6">
                    <div className="text-center">
                      <p className={`font-semibold ${
                        transaction.type === 'income' ? 'text-green-600' : 'text-red-600'
                      }`}>
                        {transaction.type === 'income' ? '+' : '-'}R$ {transaction.amount}
                      </p>
                      <p className="text-xs text-gray-600">{getPaymentMethodText(transaction.paymentMethod)}</p>
                    </div>
                    
                    <StatusBadge status={transaction.status} size="sm" />
                    
                    <Button variant="ghost" size="sm">
                      Detalhes
                    </Button>
                  </div>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>

        {/* Financial Summary */}
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <Card>
            <CardHeader>
              <CardTitle>Receita por Categoria</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-64 flex items-center justify-center bg-gray-50 rounded-lg">
                <p className="text-gray-500">Gráfico de Receita por Categoria</p>
              </div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader>
              <CardTitle>Despesas por Categoria</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-64 flex items-center justify-center bg-gray-50 rounded-lg">
                <p className="text-gray-500">Gráfico de Despesas por Categoria</p>
              </div>
            </CardContent>
          </Card>
        </div>
      </div>
    </DashboardLayout>
  )
}