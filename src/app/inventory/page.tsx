'use client'

import { useState } from 'react'
import { 
  Package, 
  Plus, 
  Search, 
  Filter, 
  AlertTriangle, 
  ShoppingCart, 
  BarChart3, 
  Tag,
  MoreVertical
} from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'
import { StatsCard } from '@/components/ui/stats-card'
import { StatusBadge } from '@/components/ui/status-badge'
import Link from 'next/link'

export default function InventoryPage() {
  const [activeCategory, setActiveCategory] = useState('all')
  
  const categories = [
    { id: 'all', name: 'Todos' },
    { id: 'hair', name: 'Cabelo' },
    { id: 'skin', name: 'Pele' },
    { id: 'nails', name: 'Unhas' },
    { id: 'makeup', name: 'Maquiagem' },
    { id: 'accessories', name: 'Acessórios' }
  ]
  
  const products = [
    {
      id: 1,
      name: 'Shampoo Profissional',
      sku: 'SH-001',
      category: 'hair',
      price: 45,
      cost: 25,
      stock: 32,
      minStock: 10,
      supplier: 'Fornecedor A',
      status: 'in_stock'
    },
    {
      id: 2,
      name: 'Condicionador Profissional',
      sku: 'CO-001',
      category: 'hair',
      price: 48,
      cost: 28,
      stock: 28,
      minStock: 10,
      supplier: 'Fornecedor A',
      status: 'in_stock'
    },
    {
      id: 3,
      name: 'Máscara Hidratante',
      sku: 'MA-001',
      category: 'hair',
      price: 60,
      cost: 35,
      stock: 15,
      minStock: 8,
      supplier: 'Fornecedor B',
      status: 'in_stock'
    },
    {
      id: 4,
      name: 'Esmalte Vermelho',
      sku: 'ES-001',
      category: 'nails',
      price: 18,
      cost: 8,
      stock: 5,
      minStock: 10,
      supplier: 'Fornecedor C',
      status: 'low_stock'
    },
    {
      id: 5,
      name: 'Base Líquida',
      sku: 'MK-001',
      category: 'makeup',
      price: 75,
      cost: 45,
      stock: 0,
      minStock: 5,
      supplier: 'Fornecedor D',
      status: 'out_of_stock'
    }
  ]

  const stats = {
    totalProducts: 48,
    totalValue: 5840,
    lowStock: 8,
    outOfStock: 3
  }

  const getStockStatus = (product: any) => {
    if (product.stock === 0) return 'out_of_stock'
    if (product.stock < product.minStock) return 'low_stock'
    return 'in_stock'
  }

  const filteredProducts = activeCategory === 'all' 
    ? products 
    : products.filter(product => product.category === activeCategory)

  return (
    <DashboardLayout title="Estoque" subtitle="Gerencie seus produtos e estoque">
      <div className="space-y-6">
        {/* Header Actions */}
        <div className="flex flex-col sm:flex-row gap-4 justify-between">
          <div className="flex items-center gap-4">
            <div className="relative">
              <Search className="w-5 h-5 absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
              <Input placeholder="Buscar produtos..." className="pl-10 w-80" />
            </div>
            <Button variant="outline">
              <Filter className="w-4 h-4" />
              Filtros
            </Button>
          </div>
          
          <Link href="/inventory/create">
            <Button>
              <Plus className="w-4 h-4" />
              Novo Produto
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
          <StatsCard
            title="Total de Produtos"
            value={stats.totalProducts}
            icon={Package}
            color="blue"
          />
          
          <StatsCard
            title="Valor em Estoque"
            value={`R$ ${stats.totalValue}`}
            icon={Tag}
            color="green"
          />
          
          <StatsCard
            title="Estoque Baixo"
            value={stats.lowStock}
            icon={AlertTriangle}
            color="orange"
          />
          
          <StatsCard
            title="Sem Estoque"
            value={stats.outOfStock}
            icon={Package}
            color="red"
          />
        </div>

        {/* Products List */}
        <Card>
          <CardHeader>
            <CardTitle>Lista de Produtos</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {filteredProducts.map((product) => (
                <div 
                  key={product.id}
                  className="flex items-center justify-between p-4 bg-white/50 rounded-xl border border-gray-100 hover:shadow-md transition-all duration-200"
                >
                  <div className="flex items-center gap-4">
                    <div className="w-12 h-12 bg-gradient-primary rounded-xl flex items-center justify-center">
                      <Package className="w-6 h-6 text-white" />
                    </div>
                    <div>
                      <h3 className="font-semibold text-gray-900">{product.name}</h3>
                      <div className="flex items-center gap-4 mt-1">
                        <span className="text-xs text-gray-600">SKU: {product.sku}</span>
                        <span className="text-xs text-gray-600">Fornecedor: {product.supplier}</span>
                      </div>
                    </div>
                  </div>
                  
                  <div className="flex items-center gap-6">
                    <div className="text-center">
                      <p className="text-sm font-semibold text-gray-900">R$ {product.price}</p>
                      <p className="text-xs text-gray-600">Preço</p>
                    </div>
                    
                    <div className="text-center">
                      <p className="text-sm font-semibold text-gray-900">{product.stock}</p>
                      <p className="text-xs text-gray-600">Em Estoque</p>
                    </div>
                    
                    <StatusBadge 
                      status={getStockStatus(product)} 
                      statusMap={{
                        in_stock: { color: 'bg-green-100 text-green-800', label: 'Em Estoque' },
                        low_stock: { color: 'bg-yellow-100 text-yellow-800', label: 'Estoque Baixo' },
                        out_of_stock: { color: 'bg-red-100 text-red-800', label: 'Sem Estoque' }
                      }}
                    />
                    
                    <div className="flex items-center gap-2">
                      <Button variant="outline" size="sm">
                        <ShoppingCart className="w-3 h-3" />
                        Comprar
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

        {/* Inventory Analytics */}
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <Card>
            <CardHeader>
              <CardTitle>Valor por Categoria</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-64 flex items-center justify-center bg-gray-50 rounded-lg">
                <p className="text-gray-500">Gráfico de Valor por Categoria</p>
              </div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader>
              <CardTitle>Produtos Mais Vendidos</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-64 flex items-center justify-center bg-gray-50 rounded-lg">
                <p className="text-gray-500">Gráfico de Produtos Mais Vendidos</p>
              </div>
            </CardContent>
          </Card>
        </div>
      </div>
    </DashboardLayout>
  )
}