'use client'

import { DashboardLayout } from '@/components/layout/dashboard-layout'
import { TestNotification } from '@/components/notifications/test-notification'

export default function TestNotificationsPage() {
  return (
    <DashboardLayout title="Testar Notificações" subtitle="Envie notificações de teste para verificar o sistema">
      <div className="max-w-md mx-auto">
        <TestNotification />
      </div>
    </DashboardLayout>
  )
}