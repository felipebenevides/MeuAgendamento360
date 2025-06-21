'use client'

import { useState } from 'react'
import { ArrowLeft, Save, Scissors, Clock, DollarSign, Users } from 'lucide-react'
import { useRouter } from 'next/navigation'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Card, CardContent, CardHeader, CardTitle, CardFooter } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'

export default function CreateServicePage() {
  const router = useRouter()
  const [loading, setLoading] = useState(false)
  const [formData, setFormData] = useState({
    name: '',
    description: '',
    category: 'hair',
    price: '',
    duration: '60',
    staff: [] as string[],
    status: 'active'
  })

  // Sample staff data - in a real app, this would come from an API
  const staffMembers = [
    { id: '1', name: 'Ana Costa', role: 'Cabeleireira' },
    { id: '2', name: 'Carlos Lima', role: 'Barbeiro' },
    { id: '3', name: 'Lucia Ferreira', role: 'Manicure' },
    { id: '4', name: 'Roberto Santos', role: 'Massagista' }
  ]

  const categories = [
    { id: 'hair', name: 'Cabelo' },
    { id: 'beard', name: 'Barba' },
    { id: 'nails', name: 'Unhas' },
    { id: 'makeup', name: 'Maquiagem' },
    { id: 'spa', name: 'Spa' }
  ]

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target
    setFormData(prev => ({ ...prev, [name]: value }))
  }

  const handleStaffToggle = (staffId: string) => {
    setFormData(prev => {
      const isSelected = prev.staff.includes(staffId)
      if (isSelected) {
        return {
          ...prev,
          staff: prev.staff.filter(id => id !== staffId)
        }
      } else {
        return {
          ...prev,
          staff: [...prev.staff, staffId]
        }
      }
    })
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setLoading(true)
    
    try {
      // Here you would make an API call to create the service
      // await api.post('/services', formData)
      
      // Simulate API call
      await new Promise(resolve => setTimeout(resolve, 1000))
      
      // Navigate back to services list
      router.push('/services')
    } catch (error) {
      console.error('Error creating service:', error)
    } finally {
      setLoading(false)
    }
  }

  return (
    <DashboardLayout title="Novo Serviço" subtitle="Adicione um novo serviço ao seu catálogo">
      <div className="max-w-3xl mx-auto">
        <form onSubmit={handleSubmit} className="space-y-6">
          <Card>
            <CardHeader>
              <CardTitle>Informações do Serviço</CardTitle>
            </CardHeader>
            <CardContent className="space-y-4">
              <Input
                label="Nome do Serviço"
                name="name"
                value={formData.name}
                onChange={handleChange}
                placeholder="Ex: Corte Feminino, Barba Completa"
                icon={<Scissors className="w-5 h-5" />}
                required
              />
              
              <div>
                <label className="text-sm font-medium text-gray-700 mb-2 block">
                  Descrição
                </label>
                <textarea
                  name="description"
                  value={formData.description}
                  onChange={handleChange}
                  placeholder="Descreva o serviço em detalhes..."
                  className="w-full min-h-[100px] rounded-xl border border-gray-200 bg-white/50 backdrop-blur-sm px-4 py-2 text-sm focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary/20 focus-visible:border-primary"
                />
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
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
                
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Preço (R$)
                  </label>
                  <Input
                    type="number"
                    name="price"
                    value={formData.price}
                    onChange={handleChange}
                    placeholder="0.00"
                    icon={<DollarSign className="w-5 h-5" />}
                    min="0"
                    step="0.01"
                    required
                  />
                </div>
                
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Duração (minutos)
                  </label>
                  <Input
                    type="number"
                    name="duration"
                    value={formData.duration}
                    onChange={handleChange}
                    placeholder="60"
                    icon={<Clock className="w-5 h-5" />}
                    min="5"
                    step="5"
                    required
                  />
                </div>
              </div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader>
              <CardTitle>Profissionais Disponíveis</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-2">
                <p className="text-sm text-gray-600 mb-4">
                  Selecione os profissionais que podem realizar este serviço:
                </p>
                
                <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
                  {staffMembers.map(staff => (
                    <div 
                      key={staff.id}
                      onClick={() => handleStaffToggle(staff.id)}
                      className={`
                        flex items-center gap-3 p-3 rounded-xl border cursor-pointer transition-all
                        ${formData.staff.includes(staff.id) 
                          ? 'border-primary bg-primary/5' 
                          : 'border-gray-200 hover:border-gray-300'}
                      `}
                    >
                      <div className={`
                        w-5 h-5 rounded-md border flex items-center justify-center
                        ${formData.staff.includes(staff.id)
                          ? 'bg-primary border-primary'
                          : 'border-gray-300'}
                      `}>
                        {formData.staff.includes(staff.id) && (
                          <svg width="12" height="12" viewBox="0 0 12 12" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M10 3L4.5 8.5L2 6" stroke="white" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
                          </svg>
                        )}
                      </div>
                      
                      <div>
                        <p className="font-medium text-gray-900">{staff.name}</p>
                        <p className="text-xs text-gray-600">{staff.role}</p>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader>
              <CardTitle>Status do Serviço</CardTitle>
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
                onClick={() => router.push('/services')}
              >
                <ArrowLeft className="w-4 h-4" />
                Voltar
              </Button>
              
              <Button type="submit" loading={loading}>
                <Save className="w-4 h-4" />
                Salvar Serviço
              </Button>
            </CardFooter>
          </Card>
        </form>
      </div>
    </DashboardLayout>
  )
}