'use client'

import { useState } from 'react'
import { ArrowLeft, Save, User, Mail, Phone, Calendar, Scissors } from 'lucide-react'
import { useRouter } from 'next/navigation'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Card, CardContent, CardHeader, CardTitle, CardFooter } from '@/components/ui/card'
import { DashboardLayout } from '@/components/layout/dashboard-layout'
import { InputMask } from '@/components/forms/input-mask'

export default function CreateStaffPage() {
  const router = useRouter()
  const [loading, setLoading] = useState(false)
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    phone: '',
    cpf: '',
    birthdate: '',
    role: '',
    specialties: '',
    startDate: '',
    workDays: {
      monday: true,
      tuesday: true,
      wednesday: true,
      thursday: true,
      friday: true,
      saturday: false,
      sunday: false
    },
    workHours: {
      start: '09:00',
      end: '18:00'
    },
    commissionRate: '50'
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target
    setFormData(prev => ({ ...prev, [name]: value }))
  }

  const handleWorkDayChange = (day: string) => {
    setFormData(prev => ({
      ...prev,
      workDays: {
        ...prev.workDays,
        [day]: !prev.workDays[day as keyof typeof prev.workDays]
      }
    }))
  }

  const handleWorkHoursChange = (type: 'start' | 'end', value: string) => {
    setFormData(prev => ({
      ...prev,
      workHours: {
        ...prev.workHours,
        [type]: value
      }
    }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setLoading(true)
    
    try {
      // Here you would make an API call to create the staff member
      // await api.post('/staff', formData)
      
      // Simulate API call
      await new Promise(resolve => setTimeout(resolve, 1000))
      
      // Navigate back to staff list
      router.push('/staff')
    } catch (error) {
      console.error('Error creating staff member:', error)
    } finally {
      setLoading(false)
    }
  }

  return (
    <DashboardLayout title="Novo Profissional" subtitle="Adicione um novo profissional à sua equipe">
      <div className="max-w-3xl mx-auto">
        <form onSubmit={handleSubmit} className="space-y-6">
          <Card>
            <CardHeader>
              <CardTitle>Informações Pessoais</CardTitle>
            </CardHeader>
            <CardContent className="space-y-4">
              <Input
                label="Nome Completo"
                name="name"
                value={formData.name}
                onChange={handleChange}
                placeholder="Digite o nome completo"
                icon={<User className="w-5 h-5" />}
                required
              />
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <Input
                  label="Email"
                  name="email"
                  type="email"
                  value={formData.email}
                  onChange={handleChange}
                  placeholder="email@exemplo.com"
                  icon={<Mail className="w-5 h-5" />}
                  required
                />
                
                <InputMask
                  label="Telefone"
                  name="phone"
                  mask="phone"
                  value={formData.phone}
                  onChange={handleChange}
                  placeholder="(00) 00000-0000"
                  required
                />
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <InputMask
                  label="CPF"
                  name="cpf"
                  mask="cpf"
                  value={formData.cpf}
                  onChange={handleChange}
                  placeholder="000.000.000-00"
                  required
                />
                
                <Input
                  label="Data de Nascimento"
                  name="birthdate"
                  type="date"
                  value={formData.birthdate}
                  onChange={handleChange}
                  required
                />
              </div>
            </CardContent>
          </Card>
          
          <Card>
            <CardHeader>
              <CardTitle>Informações Profissionais</CardTitle>
            </CardHeader>
            <CardContent className="space-y-4">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <Input
                  label="Cargo/Função"
                  name="role"
                  value={formData.role}
                  onChange={handleChange}
                  placeholder="Ex: Cabeleireiro, Barbeiro, Manicure"
                  icon={<Scissors className="w-5 h-5" />}
                  required
                />
                
                <Input
                  label="Data de Início"
                  name="startDate"
                  type="date"
                  value={formData.startDate}
                  onChange={handleChange}
                  icon={<Calendar className="w-5 h-5" />}
                  required
                />
              </div>
              
              <div>
                <label className="text-sm font-medium text-gray-700 mb-2 block">
                  Especialidades
                </label>
                <textarea
                  name="specialties"
                  value={formData.specialties}
                  onChange={handleChange}
                  placeholder="Liste as especialidades separadas por vírgula (ex: Corte feminino, Coloração, Penteados)"
                  className="w-full min-h-[80px] rounded-xl border border-gray-200 bg-white/50 backdrop-blur-sm px-4 py-2 text-sm focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary/20 focus-visible:border-primary"
                />
              </div>
              
              <div>
                <label className="text-sm font-medium text-gray-700 mb-2 block">
                  Dias de Trabalho
                </label>
                <div className="flex flex-wrap gap-2">
                  {Object.entries(formData.workDays).map(([day, checked]) => (
                    <button
                      key={day}
                      type="button"
                      onClick={() => handleWorkDayChange(day)}
                      className={`px-3 py-2 rounded-lg text-sm font-medium transition-colors ${
                        checked
                          ? 'bg-primary text-white'
                          : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
                      }`}
                    >
                      {day.charAt(0).toUpperCase() + day.slice(1)}
                    </button>
                  ))}
                </div>
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Horário de Início
                  </label>
                  <Input
                    type="time"
                    value={formData.workHours.start}
                    onChange={(e) => handleWorkHoursChange('start', e.target.value)}
                    required
                  />
                </div>
                
                <div>
                  <label className="text-sm font-medium text-gray-700 mb-2 block">
                    Horário de Término
                  </label>
                  <Input
                    type="time"
                    value={formData.workHours.end}
                    onChange={(e) => handleWorkHoursChange('end', e.target.value)}
                    required
                  />
                </div>
              </div>
              
              <div>
                <label className="text-sm font-medium text-gray-700 mb-2 block">
                  Taxa de Comissão (%)
                </label>
                <Input
                  type="number"
                  name="commissionRate"
                  value={formData.commissionRate}
                  onChange={handleChange}
                  min="0"
                  max="100"
                  required
                />
              </div>
            </CardContent>
          </Card>
          
          <Card>
            <CardFooter className="flex justify-between">
              <Button 
                type="button" 
                variant="outline" 
                onClick={() => router.push('/staff')}
              >
                <ArrowLeft className="w-4 h-4" />
                Voltar
              </Button>
              
              <Button type="submit" loading={loading}>
                <Save className="w-4 h-4" />
                Salvar Profissional
              </Button>
            </CardFooter>
          </Card>
        </form>
      </div>
    </DashboardLayout>
  )
}