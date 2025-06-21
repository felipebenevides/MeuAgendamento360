import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import { Toaster } from '@/components/ui/toaster'
import { NotificationProvider } from '@/contexts/notification-context'
import { NotificationToast } from '@/components/notifications/notification-toast'
import { ApiProvider } from '@/contexts/ApiContext'
import './globals.css'

const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
  title: 'MeuAgendamento360 - Gestão Completa para seu Negócio',
  description: 'Plataforma completa de gestão para salões, barbearias e clínicas estéticas',
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="pt-BR">
      <body className={inter.className}>
        <ApiProvider>
          <NotificationProvider>
            {children}
            <Toaster />
            <NotificationToast />
          </NotificationProvider>
        </ApiProvider>
      </body>
    </html>
  )
}