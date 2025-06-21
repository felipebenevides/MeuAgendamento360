'use client';

import React from 'react';
import OnboardingWizard from '../../components/onboarding/OnboardingWizard';
import { ApiProvider } from '../../contexts/ApiContext';

const OnboardingPage: React.FC = () => {
  return (
    <ApiProvider>
      <div className="min-h-screen bg-gray-50 py-12">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center mb-8">
            <h1 className="text-3xl font-bold text-gray-900">
              Configuração do MeuAgendamento360
            </h1>
            <p className="mt-2 text-lg text-gray-600">
              Configure seu negócio em poucos passos
            </p>
          </div>
          
          <OnboardingWizard />
        </div>
      </div>
    </ApiProvider>
  );
};

export default OnboardingPage;