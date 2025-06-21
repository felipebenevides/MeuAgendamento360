'use client';

import React, { useEffect } from 'react';
import { useRouter } from 'next/navigation';
import { useAuthStore } from '@/store/auth';
import DashboardLayout from '@/components/layout/dashboard-layout';

export default function DashboardPageLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const { user, isAuthenticated } = useAuthStore();
  const router = useRouter();

  useEffect(() => {
    // Verificar se o usuário está autenticado
    if (!isAuthenticated) {
      router.push('/login');
      return;
    }

    // Verificar se o usuário é um dono de negócio sem negócio configurado
    if (user?.role === 'BusinessOwner' && !user?.businessId) {
      router.push('/onboarding');
      return;
    }
  }, [isAuthenticated, user, router]);

  if (!isAuthenticated) {
    return null;
  }

  return <DashboardLayout>{children}</DashboardLayout>;
}