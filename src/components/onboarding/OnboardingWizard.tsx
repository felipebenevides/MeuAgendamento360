import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { useApi } from '../../contexts/ApiContext';

// Etapas do onboarding
enum OnboardingStep {
  WELCOME,
  BUSINESS_INFO,
  SERVICES,
  STAFF,
  COMPLETE,
}

const OnboardingWizard: React.FC = () => {
  const router = useRouter();
  const { business, service } = useApi();
  const [currentStep, setCurrentStep] = useState<OnboardingStep>(OnboardingStep.WELCOME);
  const [businessData, setBusinessData] = useState({
    name: '',
    slug: '',
    description: '',
    phone: '',
    email: '',
    website: '',
    address: {
      street: '',
      number: '',
      complement: '',
      neighborhood: '',
      city: '',
      state: '',
      postalCode: '',
      country: 'Brasil',
    },
  });
  const [services, setServices] = useState<any[]>([
    { name: '', description: '', durationMinutes: 30, price: 0 },
  ]);
  const [staff, setStaff] = useState<any[]>([
    { name: '', email: '', position: '', bio: '' },
  ]);
  const [loading, setLoading] = useState(false);

  const handleBusinessChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    
    if (name.startsWith('address.')) {
      const addressField = name.split('.')[1];
      setBusinessData({
        ...businessData,
        address: {
          ...businessData.address,
          [addressField]: value,
        },
      });
    } else {
      setBusinessData({
        ...businessData,
        [name]: value,
      });
    }
  };

  const handleServiceChange = (index: number, e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    const updatedServices = [...services];
    updatedServices[index] = {
      ...updatedServices[index],
      [name]: name === 'price' || name === 'durationMinutes' ? Number(value) : value,
    };
    setServices(updatedServices);
  };

  const addService = () => {
    setServices([
      ...services,
      { name: '', description: '', durationMinutes: 30, price: 0 },
    ]);
  };

  const handleStaffChange = (index: number, e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    const updatedStaff = [...staff];
    updatedStaff[index] = {
      ...updatedStaff[index],
      [name]: value,
    };
    setStaff(updatedStaff);
  };

  const addStaff = () => {
    setStaff([...staff, { name: '', email: '', position: '', bio: '' }]);
  };

  const nextStep = () => {
    setCurrentStep(currentStep + 1);
  };

  const prevStep = () => {
    setCurrentStep(currentStep - 1);
  };

  const handleSubmit = async () => {
    setLoading(true);
    try {
      // Simular criação do negócio
      const createdBusiness = await business.create(businessData);
      
      // Simular criação dos serviços
      for (const serviceData of services) {
        if (serviceData.name) {
          await service.create({
            ...serviceData,
            businessId: createdBusiness.id,
          });
        }
      }
      
      // Avançar para a etapa final
      setCurrentStep(OnboardingStep.COMPLETE);
    } catch (error) {
      console.error('Erro ao criar negócio:', error);
    } finally {
      setLoading(false);
    }
  };

  const finishOnboarding = () => {
    router.push('/dashboard');
  };

  const renderStep = () => {
    switch (currentStep) {
      case OnboardingStep.WELCOME:
        return (
          <div className="space-y-6">
            <h2 className="text-2xl font-bold">Bem-vindo ao MeuAgendamento360!</h2>
            <p className="text-gray-600">
              Vamos configurar seu negócio em poucos passos simples. Você poderá
              adicionar informações sobre seu negócio, serviços e funcionários.
            </p>
            <div className="flex justify-end">
              <button
                onClick={nextStep}
                className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
              >
                Começar
              </button>
            </div>
          </div>
        );

      case OnboardingStep.BUSINESS_INFO:
        return (
          <div className="space-y-6">
            <h2 className="text-2xl font-bold">Informações do Negócio</h2>
            <div className="space-y-4">
              <div>
                <label className="block text-sm font-medium text-gray-700">
                  Nome do Negócio
                </label>
                <input
                  type="text"
                  name="name"
                  value={businessData.name}
                  onChange={handleBusinessChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  placeholder="Ex: Salão de Beleza Exemplo"
                />
              </div>
              
              <div>
                <label className="block text-sm font-medium text-gray-700">
                  Slug (URL)
                </label>
                <input
                  type="text"
                  name="slug"
                  value={businessData.slug}
                  onChange={handleBusinessChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  placeholder="Ex: salao-beleza-exemplo"
                />
              </div>
              
              <div>
                <label className="block text-sm font-medium text-gray-700">
                  Descrição
                </label>
                <textarea
                  name="description"
                  value={businessData.description}
                  onChange={handleBusinessChange}
                  rows={3}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  placeholder="Descreva seu negócio"
                />
              </div>
              
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Telefone
                  </label>
                  <input
                    type="text"
                    name="phone"
                    value={businessData.phone}
                    onChange={handleBusinessChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    placeholder="(00) 00000-0000"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Email
                  </label>
                  <input
                    type="email"
                    name="email"
                    value={businessData.email}
                    onChange={handleBusinessChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    placeholder="contato@exemplo.com"
                  />
                </div>
              </div>
              
              <div>
                <label className="block text-sm font-medium text-gray-700">
                  Website
                </label>
                <input
                  type="text"
                  name="website"
                  value={businessData.website}
                  onChange={handleBusinessChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  placeholder="www.exemplo.com"
                />
              </div>
              
              <h3 className="text-lg font-medium mt-6">Endereço</h3>
              
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Rua
                  </label>
                  <input
                    type="text"
                    name="address.street"
                    value={businessData.address.street}
                    onChange={handleBusinessChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Número
                  </label>
                  <input
                    type="text"
                    name="address.number"
                    value={businessData.address.number}
                    onChange={handleBusinessChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
              </div>
              
              <div>
                <label className="block text-sm font-medium text-gray-700">
                  Complemento
                </label>
                <input
                  type="text"
                  name="address.complement"
                  value={businessData.address.complement}
                  onChange={handleBusinessChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                />
              </div>
              
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Bairro
                  </label>
                  <input
                    type="text"
                    name="address.neighborhood"
                    value={businessData.address.neighborhood}
                    onChange={handleBusinessChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Cidade
                  </label>
                  <input
                    type="text"
                    name="address.city"
                    value={businessData.address.city}
                    onChange={handleBusinessChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
              </div>
              
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Estado
                  </label>
                  <input
                    type="text"
                    name="address.state"
                    value={businessData.address.state}
                    onChange={handleBusinessChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    CEP
                  </label>
                  <input
                    type="text"
                    name="address.postalCode"
                    value={businessData.address.postalCode}
                    onChange={handleBusinessChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
              </div>
            </div>
            
            <div className="flex justify-between">
              <button
                onClick={prevStep}
                className="px-4 py-2 border border-gray-300 rounded-md hover:bg-gray-50"
              >
                Voltar
              </button>
              <button
                onClick={nextStep}
                className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
              >
                Próximo
              </button>
            </div>
          </div>
        );

      case OnboardingStep.SERVICES:
        return (
          <div className="space-y-6">
            <h2 className="text-2xl font-bold">Serviços</h2>
            <p className="text-gray-600">
              Adicione os serviços que você oferece. Você pode adicionar mais
              serviços depois.
            </p>
            
            {services.map((service, index) => (
              <div key={index} className="p-4 border rounded-md space-y-4">
                <h3 className="font-medium">Serviço {index + 1}</h3>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Nome do Serviço
                  </label>
                  <input
                    type="text"
                    name="name"
                    value={service.name}
                    onChange={(e) => handleServiceChange(index, e)}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    placeholder="Ex: Corte de Cabelo"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Descrição
                  </label>
                  <textarea
                    name="description"
                    value={service.description}
                    onChange={(e) => handleServiceChange(index, e)}
                    rows={2}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    placeholder="Descreva o serviço"
                  />
                </div>
                
                <div className="grid grid-cols-2 gap-4">
                  <div>
                    <label className="block text-sm font-medium text-gray-700">
                      Duração (minutos)
                    </label>
                    <input
                      type="number"
                      name="durationMinutes"
                      value={service.durationMinutes}
                      onChange={(e) => handleServiceChange(index, e)}
                      className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                      min="1"
                    />
                  </div>
                  
                  <div>
                    <label className="block text-sm font-medium text-gray-700">
                      Preço (R$)
                    </label>
                    <input
                      type="number"
                      name="price"
                      value={service.price}
                      onChange={(e) => handleServiceChange(index, e)}
                      className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                      min="0"
                      step="0.01"
                    />
                  </div>
                </div>
              </div>
            ))}
            
            <button
              onClick={addService}
              className="w-full py-2 border border-dashed border-gray-300 rounded-md text-gray-500 hover:bg-gray-50"
            >
              + Adicionar Serviço
            </button>
            
            <div className="flex justify-between">
              <button
                onClick={prevStep}
                className="px-4 py-2 border border-gray-300 rounded-md hover:bg-gray-50"
              >
                Voltar
              </button>
              <button
                onClick={nextStep}
                className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
              >
                Próximo
              </button>
            </div>
          </div>
        );

      case OnboardingStep.STAFF:
        return (
          <div className="space-y-6">
            <h2 className="text-2xl font-bold">Funcionários</h2>
            <p className="text-gray-600">
              Adicione os funcionários que trabalham no seu negócio. Você pode
              adicionar mais funcionários depois.
            </p>
            
            {staff.map((staffMember, index) => (
              <div key={index} className="p-4 border rounded-md space-y-4">
                <h3 className="font-medium">Funcionário {index + 1}</h3>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Nome Completo
                  </label>
                  <input
                    type="text"
                    name="name"
                    value={staffMember.name}
                    onChange={(e) => handleStaffChange(index, e)}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Email
                  </label>
                  <input
                    type="email"
                    name="email"
                    value={staffMember.email}
                    onChange={(e) => handleStaffChange(index, e)}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Cargo
                  </label>
                  <input
                    type="text"
                    name="position"
                    value={staffMember.position}
                    onChange={(e) => handleStaffChange(index, e)}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Bio
                  </label>
                  <textarea
                    name="bio"
                    value={staffMember.bio}
                    onChange={(e) => handleStaffChange(index, e)}
                    rows={2}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
              </div>
            ))}
            
            <button
              onClick={addStaff}
              className="w-full py-2 border border-dashed border-gray-300 rounded-md text-gray-500 hover:bg-gray-50"
            >
              + Adicionar Funcionário
            </button>
            
            <div className="flex justify-between">
              <button
                onClick={prevStep}
                className="px-4 py-2 border border-gray-300 rounded-md hover:bg-gray-50"
              >
                Voltar
              </button>
              <button
                onClick={handleSubmit}
                disabled={loading}
                className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 disabled:bg-blue-400"
              >
                {loading ? 'Salvando...' : 'Concluir'}
              </button>
            </div>
          </div>
        );

      case OnboardingStep.COMPLETE:
        return (
          <div className="space-y-6 text-center">
            <div className="mx-auto w-16 h-16 bg-green-100 rounded-full flex items-center justify-center">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-8 w-8 text-green-600"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M5 13l4 4L19 7"
                />
              </svg>
            </div>
            <h2 className="text-2xl font-bold">Parabéns!</h2>
            <p className="text-gray-600">
              Seu negócio foi configurado com sucesso. Agora você pode começar a
              usar o MeuAgendamento360 para gerenciar seus agendamentos.
            </p>
            <div className="pt-4">
              <button
                onClick={finishOnboarding}
                className="px-6 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
              >
                Ir para o Dashboard
              </button>
            </div>
          </div>
        );

      default:
        return null;
    }
  };

  return (
    <div className="max-w-3xl mx-auto p-6 bg-white rounded-lg shadow-md">
      {/* Barra de progresso */}
      <div className="mb-8">
        <div className="flex items-center justify-between">
          {[
            'Bem-vindo',
            'Informações',
            'Serviços',
            'Funcionários',
            'Concluído',
          ].map((step, index) => (
            <div
              key={index}
              className={`flex flex-col items-center ${
                index <= currentStep ? 'text-blue-600' : 'text-gray-400'
              }`}
            >
              <div
                className={`w-8 h-8 rounded-full flex items-center justify-center ${
                  index <= currentStep
                    ? 'bg-blue-600 text-white'
                    : 'bg-gray-200 text-gray-500'
                }`}
              >
                {index < currentStep ? (
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-5 w-5"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                  >
                    <path
                      fillRule="evenodd"
                      d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z"
                      clipRule="evenodd"
                    />
                  </svg>
                ) : (
                  index + 1
                )}
              </div>
              <span className="text-xs mt-1">{step}</span>
            </div>
          ))}
        </div>
        <div className="mt-2 h-1 bg-gray-200 rounded-full">
          <div
            className="h-1 bg-blue-600 rounded-full"
            style={{ width: `${(currentStep / 4) * 100}%` }}
          ></div>
        </div>
      </div>

      {/* Conteúdo da etapa atual */}
      {renderStep()}
    </div>
  );
};

export default OnboardingWizard;