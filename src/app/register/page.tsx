'use client'

import { useState } from 'react'
import { useRouter } from 'next/navigation'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { InputMask } from '@/components/forms/input-mask'
import { useAuthStore } from '@/store/auth'
import { registerSchema, type RegisterForm } from '@/lib/validations'
import api from '@/lib/api'
import toast from 'react-hot-toast'
import Link from 'next/link'
import { 
  Sparkles, 
  User, 
  Building2, 
  MapPin, 
  Check, 
  ArrowRight, 
  ArrowLeft,
  Eye,
  EyeOff,
  Shield,
  Zap,
  Clock
} from 'lucide-react'

const businessTypes = [
  { value: 'salon', label: 'Salão de Beleza', icon: '💇‍♀️' },
  { value: 'barbershop', label: 'Barbearia', icon: '✂️' },
  { value: 'clinic', label: 'Clínica Estética', icon: '🏥' },
  { value: 'spa', label: 'Spa', icon: '🧘‍♀️' },
  { value: 'other', label: 'Outro', icon: '🏢' },
]

const brazilianStates = [
  'AC', 'AL', 'AP', 'AM', 'BA', 'CE', 'DF', 'ES', 'GO', 'MA',
  'MT', 'MS', 'MG', 'PA', 'PB', 'PR', 'PE', 'PI', 'RJ', 'RN',
  'RS', 'RO', 'RR', 'SC', 'SP', 'SE', 'TO'
]

const steps = [
  { number: 1, title: 'Dados Pessoais', icon: User, description: 'Suas informações básicas' },
  { number: 2, title: 'Dados do Negócio', icon: Building2, description: 'Informações da empresa' },
  { number: 3, title: 'Endereço', icon: MapPin, description: 'Localização do negócio' },
]

export default function RegisterPage() {
  const [isLoading, setIsLoading] = useState(false)
  const [step, setStep] = useState(1)
  const [showPassword, setShowPassword] = useState(false)
  const [showConfirmPassword, setShowConfirmPassword] = useState(false)
  const router = useRouter()
  const login = useAuthStore((state) => state.login)

  const {
    register,
    handleSubmit,
    formState: { errors },
    watch,
    setValue,
  } = useForm<RegisterForm>({
    resolver: zodResolver(registerSchema),
  })

  const onSubmit = async (data: RegisterForm) => {
    setIsLoading(true)
    try {
      const response = await api.post('/auth/register', {
        email: data.email,
        password: data.password,
        firstName: data.firstName,
        lastName: data.lastName,
        cpf: data.cpf.replace(/\D/g, ''),
        phone: data.phone.replace(/\D/g, ''),
        businessName: data.businessName,
        businessType: data.businessType,
        cnpj: data.cnpj?.replace(/\D/g, ''),
        street: data.street,
        number: data.number,
        complement: data.complement,
        neighborhood: data.neighborhood,
        city: data.city,
        state: data.state,
        cep: data.cep.replace(/\D/g, ''),
      })

      const { token, refreshToken, userId, businessId } = response.data

      const user = {
        id: userId,
        email: data.email,
        firstName: data.firstName,
        lastName: data.lastName,
        role: 'BusinessOwner' as const,
        businessId,
      }

      login(token, refreshToken, user)
      toast.success('Conta criada com sucesso!')
      router.push('/onboarding')
    } catch (error: any) {
      toast.error(error.response?.data?.message || 'Erro ao criar conta')
    } finally {
      setIsLoading(false)
    }
  }

  const nextStep = () => setStep(step + 1)
  const prevStep = () => setStep(step - 1)

  const benefits = [
    { icon: Shield, title: "100% Seguro", description: "Dados protegidos com criptografia" },
    { icon: Zap, title: "Setup Rápido", description: "Configure em menos de 5 minutos" },
    { icon: Clock, title: "Suporte 24/7", description: "Ajuda sempre que precisar" }
  ]

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 via-blue-50 to-indigo-100 relative overflow-hidden">
      {/* Background Elements */}
      <div className="absolute inset-0 bg-gradient-to-br from-primary/5 via-transparent to-secondary/5" />
      <div className="absolute top-0 right-0 w-96 h-96 bg-accent/10 rounded-full blur-3xl translate-x-1/2 -translate-y-1/2" />
      <div className="absolute bottom-0 left-0 w-72 h-72 bg-secondary/10 rounded-full blur-3xl -translate-x-1/2 translate-y-1/2" />
      
      <div className="relative min-h-screen py-12 px-4 sm:px-6 lg:px-8">
        <div className="max-w-4xl mx-auto">
          {/* Header */}
          <div className="text-center mb-12 animate-fade-in-up">
            <Link href="/" className="inline-flex items-center gap-2 mb-8">
              <div className="w-10 h-10 bg-gradient-primary rounded-xl flex items-center justify-center">
                <Sparkles className="w-6 h-6 text-white" />
              </div>
              <span className="text-2xl font-bold text-gradient">MeuAgendamento360</span>
            </Link>
            
            <h1 className="text-4xl font-bold text-gray-900 mb-4">
              Crie sua conta <span className="text-gradient">gratuitamente</span>
            </h1>
            <p className="text-xl text-gray-600 mb-8">
              Junte-se a mais de 1.000+ negócios que já transformaram sua gestão
            </p>
            
            <div className="flex justify-center items-center gap-8 mb-8">
              {benefits.map((benefit, index) => (
                <div 
                  key={benefit.title}
                  className="flex items-center gap-2 animate-slide-in-right"
                  style={{ animationDelay: `${index * 0.2}s` }}
                >
                  <div className="w-8 h-8 bg-gradient-primary rounded-lg flex items-center justify-center">
                    <benefit.icon className="w-4 h-4 text-white" />
                  </div>
                  <div className="text-left">
                    <p className="text-sm font-semibold text-gray-900">{benefit.title}</p>
                    <p className="text-xs text-gray-600">{benefit.description}</p>
                  </div>
                </div>
              ))}
            </div>
            
            <p className="text-sm text-gray-600">
              Já tem uma conta?{' '}
              <Link href="/login" className="font-semibold text-primary hover:text-primary-dark transition-colors">
                Faça login aqui
              </Link>
            </p>
          </div>

          <div className="glass-effect rounded-2xl p-8 shadow-xl border border-white/20 animate-fade-in-up">
            {/* Progress Steps */}
            <div className="mb-12">
              <div className="flex items-center justify-between relative">
                {steps.map((stepItem, index) => (
                  <div key={stepItem.number} className="flex flex-col items-center relative z-10">
                    <div className={`w-12 h-12 rounded-xl flex items-center justify-center transition-all duration-300 ${
                      stepItem.number <= step 
                        ? 'bg-gradient-primary text-white shadow-glow' 
                        : 'bg-gray-200 text-gray-600'
                    }`}>
                      {stepItem.number < step ? (
                        <Check className="w-6 h-6" />
                      ) : (
                        <stepItem.icon className="w-6 h-6" />
                      )}
                    </div>
                    <div className="mt-3 text-center">
                      <p className={`text-sm font-semibold ${
                        stepItem.number <= step ? 'text-gray-900' : 'text-gray-500'
                      }`}>
                        {stepItem.title}
                      </p>
                      <p className="text-xs text-gray-500 mt-1">{stepItem.description}</p>
                    </div>
                  </div>
                ))}
                
                {/* Progress Line */}
                <div className="absolute top-6 left-0 right-0 h-0.5 bg-gray-200 -z-10">
                  <div 
                    className="h-full bg-gradient-primary transition-all duration-500"
                    style={{ width: `${((step - 1) / (steps.length - 1)) * 100}%` }}
                  />
                </div>
              </div>
            </div>

            <form onSubmit={handleSubmit(onSubmit)} className="space-y-8">
              {/* Step 1: Personal Information */}
              {step === 1 && (
                <div className="space-y-6 animate-fade-in-up">
                  <div className="text-center mb-8">
                    <h2 className="text-2xl font-bold text-gray-900 mb-2">Vamos começar com você</h2>
                    <p className="text-gray-600">Conte-nos um pouco sobre você</p>
                  </div>
                  
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">Nome</label>
                      <Input
                        {...register('firstName')}
                        placeholder="Seu nome"
                        className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                          errors.firstName ? 'border-red-500' : ''
                        }`}
                      />
                      {errors.firstName && (
                        <p className="text-sm text-red-600">{errors.firstName.message}</p>
                      )}
                    </div>
                    
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">Sobrenome</label>
                      <Input
                        {...register('lastName')}
                        placeholder="Seu sobrenome"
                        className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                          errors.lastName ? 'border-red-500' : ''
                        }`}
                      />
                      {errors.lastName && (
                        <p className="text-sm text-red-600">{errors.lastName.message}</p>
                      )}
                    </div>
                  </div>
                  
                  <div className="space-y-2">
                    <label className="text-sm font-medium text-gray-700">E-mail</label>
                    <Input
                      {...register('email')}
                      type="email"
                      placeholder="seu@email.com"
                      className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                        errors.email ? 'border-red-500' : ''
                      }`}
                    />
                    {errors.email && (
                      <p className="text-sm text-red-600">{errors.email.message}</p>
                    )}
                  </div>
                  
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">CPF</label>
                      <InputMask
                        {...register('cpf')}
                        mask="cpf"
                        placeholder="000.000.000-00"
                        className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                          errors.cpf ? 'border-red-500' : ''
                        }`}
                      />
                      {errors.cpf && (
                        <p className="text-sm text-red-600">{errors.cpf.message}</p>
                      )}
                    </div>
                    
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">Telefone</label>
                      <InputMask
                        {...register('phone')}
                        mask="phone"
                        placeholder="(00) 00000-0000"
                        className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                          errors.phone ? 'border-red-500' : ''
                        }`}
                      />
                      {errors.phone && (
                        <p className="text-sm text-red-600">{errors.phone.message}</p>
                      )}
                    </div>
                  </div>
                  
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">Senha</label>
                      <div className="relative">
                        <Input
                          {...register('password')}
                          type={showPassword ? 'text' : 'password'}
                          placeholder="Sua senha"
                          className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 pr-12 ${
                            errors.password ? 'border-red-500' : ''
                          }`}
                        />
                        <button
                          type="button"
                          onClick={() => setShowPassword(!showPassword)}
                          className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600"
                        >
                          {showPassword ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
                        </button>
                      </div>
                      {errors.password && (
                        <p className="text-sm text-red-600">{errors.password.message}</p>
                      )}
                    </div>
                    
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">Confirmar Senha</label>
                      <div className="relative">
                        <Input
                          {...register('confirmPassword')}
                          type={showConfirmPassword ? 'text' : 'password'}
                          placeholder="Confirme sua senha"
                          className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 pr-12 ${
                            errors.confirmPassword ? 'border-red-500' : ''
                          }`}
                        />
                        <button
                          type="button"
                          onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                          className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600"
                        >
                          {showConfirmPassword ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
                        </button>
                      </div>
                      {errors.confirmPassword && (
                        <p className="text-sm text-red-600">{errors.confirmPassword.message}</p>
                      )}
                    </div>
                  </div>
                  
                  <Button 
                    type="button" 
                    onClick={nextStep} 
                    className="w-full h-12 bg-gradient-primary hover:shadow-glow transition-all duration-300 text-white font-semibold rounded-xl"
                  >
                    Continuar
                    <ArrowRight className="w-5 h-5 ml-2" />
                  </Button>
                </div>
              )}

              {/* Step 2: Business Information */}
              {step === 2 && (
                <div className="space-y-6 animate-fade-in-up">
                  <div className="text-center mb-8">
                    <h2 className="text-2xl font-bold text-gray-900 mb-2">Sobre seu negócio</h2>
                    <p className="text-gray-600">Vamos conhecer sua empresa</p>
                  </div>
                  
                  <div className="space-y-2">
                    <label className="text-sm font-medium text-gray-700">Nome do Negócio</label>
                    <Input
                      {...register('businessName')}
                      placeholder="Nome do seu negócio"
                      className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                        errors.businessName ? 'border-red-500' : ''
                      }`}
                    />
                    {errors.businessName && (
                      <p className="text-sm text-red-600">{errors.businessName.message}</p>
                    )}
                  </div>
                  
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">Tipo de Negócio</label>
                      <select
                        {...register('businessType')}
                        className="h-12 w-full rounded-xl border border-gray-200 bg-white/50 px-4 py-2 text-sm focus:border-primary focus:ring-primary/20 transition-all duration-200"
                      >
                        <option value="">Selecione o tipo</option>
                        {businessTypes.map((type) => (
                          <option key={type.value} value={type.value}>
                            {type.icon} {type.label}
                          </option>
                        ))}
                      </select>
                      {errors.businessType && (
                        <p className="text-sm text-red-600">{errors.businessType.message}</p>
                      )}
                    </div>
                    
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">CNPJ (Opcional)</label>
                      <InputMask
                        {...register('cnpj')}
                        mask="cnpj"
                        placeholder="00.000.000/0000-00"
                        className="h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200"
                      />
                      {errors.cnpj && (
                        <p className="text-sm text-red-600">{errors.cnpj.message}</p>
                      )}
                    </div>
                  </div>
                  
                  <div className="flex gap-4">
                    <Button 
                      type="button" 
                      variant="outline" 
                      onClick={prevStep}
                      className="h-12 px-8 rounded-xl border-2 hover:bg-gray-50"
                    >
                      <ArrowLeft className="w-5 h-5 mr-2" />
                      Voltar
                    </Button>
                    <Button 
                      type="button" 
                      onClick={nextStep} 
                      className="flex-1 h-12 bg-gradient-primary hover:shadow-glow transition-all duration-300 text-white font-semibold rounded-xl"
                    >
                      Continuar
                      <ArrowRight className="w-5 h-5 ml-2" />
                    </Button>
                  </div>
                </div>
              )}

              {/* Step 3: Address Information */}
              {step === 3 && (
                <div className="space-y-6 animate-fade-in-up">
                  <div className="text-center mb-8">
                    <h2 className="text-2xl font-bold text-gray-900 mb-2">Onde você está?</h2>
                    <p className="text-gray-600">Endereço do seu negócio</p>
                  </div>
                  
                  <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
                    <div className="md:col-span-3 space-y-2">
                      <label className="text-sm font-medium text-gray-700">Endereço</label>
                      <Input
                        {...register('street')}
                        placeholder="Rua, Avenida, etc."
                        className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                          errors.street ? 'border-red-500' : ''
                        }`}
                      />
                      {errors.street && (
                        <p className="text-sm text-red-600">{errors.street.message}</p>
                      )}
                    </div>
                    
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">Número</label>
                      <Input
                        {...register('number')}
                        placeholder="123"
                        className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                          errors.number ? 'border-red-500' : ''
                        }`}
                      />
                      {errors.number && (
                        <p className="text-sm text-red-600">{errors.number.message}</p>
                      )}
                    </div>
                  </div>
                  
                  <div className="space-y-2">
                    <label className="text-sm font-medium text-gray-700">Complemento (Opcional)</label>
                    <Input
                      {...register('complement')}
                      placeholder="Apartamento, sala, etc."
                      className="h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200"
                    />
                  </div>
                  
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">Bairro</label>
                      <Input
                        {...register('neighborhood')}
                        placeholder="Nome do bairro"
                        className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                          errors.neighborhood ? 'border-red-500' : ''
                        }`}
                      />
                      {errors.neighborhood && (
                        <p className="text-sm text-red-600">{errors.neighborhood.message}</p>
                      )}
                    </div>
                    
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">CEP</label>
                      <InputMask
                        {...register('cep')}
                        mask="cep"
                        placeholder="00000-000"
                        className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                          errors.cep ? 'border-red-500' : ''
                        }`}
                      />
                      {errors.cep && (
                        <p className="text-sm text-red-600">{errors.cep.message}</p>
                      )}
                    </div>
                  </div>
                  
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">Cidade</label>
                      <Input
                        {...register('city')}
                        placeholder="Nome da cidade"
                        className={`h-12 bg-white/50 border-gray-200 focus:border-primary focus:ring-primary/20 transition-all duration-200 ${
                          errors.city ? 'border-red-500' : ''
                        }`}
                      />
                      {errors.city && (
                        <p className="text-sm text-red-600">{errors.city.message}</p>
                      )}
                    </div>
                    
                    <div className="space-y-2">
                      <label className="text-sm font-medium text-gray-700">Estado</label>
                      <select
                        {...register('state')}
                        className="h-12 w-full rounded-xl border border-gray-200 bg-white/50 px-4 py-2 text-sm focus:border-primary focus:ring-primary/20 transition-all duration-200"
                      >
                        <option value="">Selecione o estado</option>
                        {brazilianStates.map((state) => (
                          <option key={state} value={state}>
                            {state}
                          </option>
                        ))}
                      </select>
                      {errors.state && (
                        <p className="text-sm text-red-600">{errors.state.message}</p>
                      )}
                    </div>
                  </div>
                  
                  <div className="flex gap-4">
                    <Button 
                      type="button" 
                      variant="outline" 
                      onClick={prevStep}
                      className="h-12 px-8 rounded-xl border-2 hover:bg-gray-50"
                    >
                      <ArrowLeft className="w-5 h-5 mr-2" />
                      Voltar
                    </Button>
                    <Button 
                      type="submit" 
                      disabled={isLoading}
                      className="flex-1 h-12 bg-gradient-primary hover:shadow-glow transition-all duration-300 text-white font-semibold rounded-xl"
                    >
                      {isLoading ? (
                        <div className="flex items-center gap-2">
                          <div className="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin" />
                          Criando conta...
                        </div>
                      ) : (
                        <>
                          Criar Conta
                          <Check className="w-5 h-5 ml-2" />
                        </>
                      )}
                    </Button>
                  </div>
                </div>
              )}
            </form>
          </div>
          
          {/* Footer */}
          <div className="text-center mt-8 text-sm text-gray-500">
            <p>Ao criar uma conta, você concorda com nossos Termos de Uso e Política de Privacidade</p>
          </div>
        </div>
      </div>
    </div>
  )
}