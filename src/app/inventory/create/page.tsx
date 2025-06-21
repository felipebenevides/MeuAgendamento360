'use client'

import { useState } from 'react'
import { ArrowLeft, Save, Package, Tag, ShoppingCart, AlertTriangle } from 'lucide-react'
import { useRouter } from 'next/navigation'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Card, CardContent, CardHeader, CardTitle, CardFooter } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'

export default function CreateProductPage() {
  const router = useRouter()
  const [loading, setLoading] = useState(false)
  const [formData, setFormData] = useState({
    name: '',
    sku: '',
    description: '',
    category: 'hair',
    price: '',
    cost: '',
    stock: '0',
    minStock: '5',
    supplier: '',
    brand: '',
    expirationDate: '',
    location: '',
    status: 'active'
  })

  const categories = [
    { id: 'hair', name: 'Cabelo' },
    { id: 'skin', name: 'Pele' },
    { id: 'nails', name: 'Unhas' },
    { id: 'makeup', name: 'Maquiagem' },
    { id: 'accessories', name: 'Acessórios' }
  ]

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target
    setFormData(prev => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setLoading(true)
    
    try {
      // Here you would make an API call to create the product
      // await api.post('/inventory', formData)
      
      // Simulate API call
      await new Promise(resolve => setTimeout(resolve, 1000))
      
      // Navigate back to inventory list
      router.push('/inventory')
    } catch (error) {
      console.error('Error creating product:', error)
    } finally {
      setLoading(false)
    }
  }

  return (
    <DashboardLayout title="Novo Produto" subtitle="Adicione um novo produto ao estoque">
      <div className="max-w-3xl mx-auto">
        <form onSubmit={handleSubmit} className="space-y-6">
          <Card>
            <CardHeader>
              <CardTitle>Informações do Produto</CardTitle>
            </CardHeader>
            <CardContent className="space-y-4">
              <Input
                label="Nome do Produto"
                name="name"
                value={formData.name}
                onChange={handleChange}
                placeholder="Ex: Shampoo Profissional, Esmalte"
                icon={<Package className="w-5 h-5" />}
                required
              />
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <Input
                  label="SKU (Código)"
                  name="sku"
                  value={formData.sku}
                  onChange={handleChange}
                  placeholder="Ex: SH-001"
                  required
                />
                
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Categoria
                  </label>
                  <select
                    name="category"
                    value={formData.category}
                    onChange={handleChange}
                    className="w-full h-11 rounded-xl border border-gray-200 bg-white/50 backdrop-blur-sm px-4 py-2 text-sm focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary/20 focus-visible:border-primary"
                    required
                  >
                    {categories.map(category => (
                      <option key={category.id} value={category.id}>
                        {category.name}
                      </option>
                    ))}
                  </select>
                </div>
              </div>
              
              <div>
                <label className="text-sm font-medium text-gray-700 mb-2 block">
                  Descrição
                </label>
                <textarea
                  name="description"
                  value={formData.description}
                  onChange={handleChange}
                  placeholder="Descreva o produto em detalhes..."
                  className="w-full min-h-[100px] rounded-xl border border-gray-200 bg-white/50 backdrop-blur-sm px-4 py-2 text-sm focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary/20 focus-visible:border-primary"
                />
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <Input
                  label="Marca"
                  name="brand"
                  value={formData.brand}
                  onChange={handleChange}
                  placeholder="Ex: L'Oréal, Wella"
                />
                
                <Input
                  label="Fornecedor"
                  name="supplier"
                  value={formData.supplier}
                  onChange={handleChange}
                  placeholder="Nome do fornecedor"
                />
              </div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader>
              <CardTitle>Preços e Estoque</CardTitle>
            </CardHeader>
            <CardContent className="space-y-4">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Preço de Venda (R$)
                  </label>
                  <Input
                    type="number"
                    name="price"
                    value={formData.price}
                    onChange={handleChange}
                    placeholder="0.00"
                    icon={<Tag className="w-5 h-5" />}
                    min="0"
                    step="0.01"
                    required
                  />
                </div>
                
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Custo (R$)
                  </label>
                  <Input
                    type="number"
                    name="cost"
                    value={formData.cost}
                    onChange={handleChange}
                    placeholder="0.00"
                    icon={<ShoppingCart className="w-5 h-5" />}
                    min="0"
                    step="0.01"
                    required
                  />
                </div>
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Quantidade em Estoque
                  </label>
                  <Input
                    type="number"
                    name="stock"
                    value={formData.stock}
                    onChange={handleChange}
                    placeholder="0"
                    min="0"
                    required
                  />
                </div>
                
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Estoque Mínimo
                  </label>
                  <Input
                    type="number"
                    name="minStock"
                    value={formData.minStock}
                    onChange={handleChange}
                    placeholder="5"
                    icon={<AlertTriangle className="w-5 h-5" />}
                    min="0"
                    required
                  />
                </div>
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Data de Validade
                  </label>
                  <Input
                    type="date"
                    name="expirationDate"
                    value={formData.expirationDate}
                    onChange={handleChange}
                  />
                </div>
                
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Localização no Estoque
                  </label>
                  <Input
                    name="location"
                    value={formData.location}
                    onChange={handleChange}
                    placeholder="Ex: Prateleira A, Gaveta 2"
                  />
                </div>
              </div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader>
              <CardTitle>Status do Produto</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="flex items-center gap-4">
                <label className={`
                  flex items-center gap-3 p-3 rounded-xl border cursor-pointer transition-all
                  ${formData.status === 'active' 
                    ? 'border-primary bg-primary/5' 
                    : 'border-gray-200 hover:border-gray-300'}
                `}>
                  <input
                    type="radio"
                    name="status"
                    value="active"
                    checked={formData.status === 'active'}
                    onChange={handleChange}
                    className="sr-only"
                  />
                  <div className={`
                    w-5 h-5 rounded-full border-2 flex items-center justify-center
                    ${formData.status === 'active'
                      ? 'border-primary'
                      : 'border-gray-300'}
                  `}>
                    {formData.status === 'active' && (
                      <div className="w-3 h-3 rounded-full bg-primary" />
                    )}
                  </div>
                  <span>Ativo</span>
                </label>
                
                <label className={`
                  flex items-center gap-3 p-3 rounded-xl border cursor-pointer transition-all
                  ${formData.status === 'inactive' 
                    ? 'border-primary bg-primary/5' 
                    : 'border-gray-200 hover:border-gray-300'}
                `}>
                  <input
                    type="radio"
                    name="status"
                    value="inactive"
                    checked={formData.status === 'inactive'}
                    onChange={handleChange}
                    className="sr-only"
                  />
                  <div className={`
                    w-5 h-5 rounded-full border-2 flex items-center justify-center
                    ${formData.status === 'inactive'
                      ? 'border-primary'
                      : 'border-gray-300'}
                  `}>
                    {formData.status === 'inactive' && (
                      <div className="w-3 h-3 rounded-full bg-primary" />
                    )}
                  </div>
                  <span>Inativo</span>
                </label>
              </div>
            </CardContent>
            <CardFooter className="flex justify-between">
              <Button 
                type="button" 
                variant="outline" 
                onClick={() => router.push('/inventory')}
              >
                <ArrowLeft className="w-4 h-4" />
                Voltar
              </Button>
              
              <Button type="submit" loading={loading}>
                <Save className="w-4 h-4" />
                Salvar Produto
              </Button>
            </CardFooter>
          </Card>
        </form>
      </div>
    </DashboardLayout>
  )
}