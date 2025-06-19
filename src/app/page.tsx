'use client'

import { useEffect, useState } from 'react'
import { useRouter } from 'next/navigation'
import { useAuthStore } from '@/store/auth'
import { Sparkles } from 'lucide-react'

export default function HomePage() {
  const router = useRouter()
  const isAuthenticated = useAuthStore((state) => state.isAuthenticated)
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    // Simular um pequeno delay para mostrar o loading elegante
    const timer = setTimeout(() => {
      if (isAuthenticated) {
        router.push('/dashboard')
      } else {
        router.push('/login')
      }
      setIsLoading(false)
    }, 1500)

    return () => clearTimeout(timer)
  }, [isAuthenticated, router])

  if (!isLoading) return null

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 via-blue-50 to-indigo-100 relative overflow-hidden">
      {/* Background Elements */}
      <div className="absolute inset-0 bg-gradient-to-br from-primary/5 via-transparent to-accent/5" />
      <div className="absolute top-0 left-0 w-72 h-72 bg-primary/10 rounded-full blur-3xl -translate-x-1/2 -translate-y-1/2" />
      <div className="absolute bottom-0 right-0 w-96 h-96 bg-secondary/10 rounded-full blur-3xl translate-x-1/2 translate-y-1/2" />
      
      <div className="relative min-h-screen flex items-center justify-center">
        <div className="text-center animate-fade-in-up">
          {/* Logo */}
          <div className="inline-flex items-center gap-3 mb-8">
            <div className="w-16 h-16 bg-gradient-primary rounded-2xl flex items-center justify-center animate-pulse-glow">
              <Sparkles className="w-8 h-8 text-white" />
            </div>
            <span className="text-4xl font-bold text-gradient">MeuAgendamento360</span>
          </div>
          
          {/* Loading Animation */}
          <div className="relative mb-8">
            <div className="w-24 h-24 mx-auto">
              {/* Outer Ring */}
              <div className="absolute inset-0 rounded-full border-4 border-primary/20"></div>
              {/* Spinning Ring */}
              <div className="absolute inset-0 rounded-full border-4 border-transparent border-t-primary animate-spin"></div>
              {/* Inner Glow */}
              <div className="absolute inset-2 rounded-full bg-gradient-primary opacity-20 animate-pulse"></div>
            </div>
          </div>
          
          {/* Loading Text */}
          <div className="space-y-2">
            <h2 className="text-2xl font-bold text-gray-900">
              Carregando sua experiência
            </h2>
            <p className="text-gray-600 animate-pulse">
              Preparando tudo para você...
            </p>
          </div>
          
          {/* Progress Dots */}
          <div className="flex justify-center items-center gap-2 mt-8">
            <div className="w-2 h-2 bg-primary rounded-full animate-bounce"></div>
            <div className="w-2 h-2 bg-primary rounded-full animate-bounce" style={{ animationDelay: '0.1s' }}></div>
            <div className="w-2 h-2 bg-primary rounded-full animate-bounce" style={{ animationDelay: '0.2s' }}></div>
          </div>
        </div>
      </div>
    </div>
  )
}