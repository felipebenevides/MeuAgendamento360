'use client'

import { DashboardLayout } from '@/components/layout/dashboard-layout'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Bell, Settings, User, Lock, Globe, CreditCard } from 'lucide-react'
import Link from 'next/link'

export default function SettingsPage() {
  const settingsCategories = [
    {
      title: 'Notificações',
      description: 'Gerencie suas preferências de notificações',
      icon: Bell,
      href: '/settings/notifications',
      color: 'bg-blue-50 text-blue-600'
    },
    {
      title: 'Perfil',
      description: 'Atualize suas informações pessoais',
      icon: User,
      href: '/settings/profile',
      color: 'bg-green-50 text-green-600'
    },
    {
      title: 'Segurança',
      description: 'Altere sua senha e configure autenticação',
      icon: Lock,
      href: '/settings/security',
      color: 'bg-purple-50 text-purple-600'
    },
    {
      title: 'Empresa',
      description: 'Configure informações da sua empresa',
      icon: Globe,
      href: '/settings/business',
      color: 'bg-orange-50 text-orange-600'
    },
    {
      title: 'Pagamentos',
      description: 'Gerencie métodos de pagamento e faturamento',
      icon: CreditCard,
      href: '/settings/payments',
      color: 'bg-red-50 text-red-600'
    },
    {
      title: 'Testar Notificações',
      description: 'Envie notificações de teste para verificar o sistema',
      icon: Bell,
      href: '/settings/notifications/test',
      color: 'bg-yellow-50 text-yellow-600'
    }
  ]

  return (
    <DashboardLayout title="Configurações" subtitle="Gerencie as configurações do sistema">
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {settingsCategories.map((category) => (
          <Link key={category.title} href={category.href}>
            <Card className="hover:shadow-md transition-all duration-200 cursor-pointer h-full">
              <CardContent className="p-6">
                <div className="flex items-center gap-4">
                  <div className={`w-12 h-12 rounded-xl flex items-center justify-center ${category.color}`}>
                    <category.icon className="w-6 h-6" />
                  </div>
                  <div>
                    <h3 className="font-semibold text-gray-900">{category.title}</h3>
                    <p className="text-sm text-gray-600">{category.description}</p>
                  </div>
                </div>
              </CardContent>
            </Card>
          </Link>
        ))}
      </div>
    </DashboardLayout>
  )
}