import { z } from 'zod'

// Brazilian validation functions
export const validateCPF = (cpf: string): boolean => {
  const cleanCPF = cpf.replace(/\D/g, '')
  if (cleanCPF.length !== 11) return false
  
  // Check for repeated digits
  if (/^(\d)\1{10}$/.test(cleanCPF)) return false
  
  // Validate check digits
  let sum = 0
  for (let i = 0; i < 9; i++) {
    sum += parseInt(cleanCPF[i]) * (10 - i)
  }
  let digit1 = 11 - (sum % 11)
  if (digit1 > 9) digit1 = 0
  
  sum = 0
  for (let i = 0; i < 10; i++) {
    sum += parseInt(cleanCPF[i]) * (11 - i)
  }
  let digit2 = 11 - (sum % 11)
  if (digit2 > 9) digit2 = 0
  
  return digit1 === parseInt(cleanCPF[9]) && digit2 === parseInt(cleanCPF[10])
}

export const validateCNPJ = (cnpj: string): boolean => {
  const cleanCNPJ = cnpj.replace(/\D/g, '')
  if (cleanCNPJ.length !== 14) return false
  
  // Check for repeated digits
  if (/^(\d)\1{13}$/.test(cleanCNPJ)) return false
  
  // Validate check digits
  const weights1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]
  const weights2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]
  
  let sum = 0
  for (let i = 0; i < 12; i++) {
    sum += parseInt(cleanCNPJ[i]) * weights1[i]
  }
  let digit1 = sum % 11 < 2 ? 0 : 11 - (sum % 11)
  
  sum = 0
  for (let i = 0; i < 13; i++) {
    sum += parseInt(cleanCNPJ[i]) * weights2[i]
  }
  let digit2 = sum % 11 < 2 ? 0 : 11 - (sum % 11)
  
  return digit1 === parseInt(cleanCNPJ[12]) && digit2 === parseInt(cleanCNPJ[13])
}

export const validateCEP = (cep: string): boolean => {
  const cleanCEP = cep.replace(/\D/g, '')
  return cleanCEP.length === 8
}

// Zod schemas
export const registerSchema = z.object({
  // Personal Information
  email: z.string().email('Email inválido'),
  password: z.string().min(8, 'Senha deve ter pelo menos 8 caracteres'),
  confirmPassword: z.string(),
  firstName: z.string().min(2, 'Nome deve ter pelo menos 2 caracteres'),
  lastName: z.string().min(2, 'Sobrenome deve ter pelo menos 2 caracteres'),
  cpf: z.string().refine(validateCPF, 'CPF inválido'),
  phone: z.string().min(10, 'Telefone inválido'),
  
  // Business Information
  businessName: z.string().min(2, 'Nome do negócio deve ter pelo menos 2 caracteres'),
  businessType: z.string().min(1, 'Selecione o tipo de negócio'),
  cnpj: z.string().optional().refine((val) => !val || validateCNPJ(val), 'CNPJ inválido'),
  
  // Address Information
  street: z.string().min(5, 'Endereço deve ter pelo menos 5 caracteres'),
  number: z.string().min(1, 'Número é obrigatório'),
  complement: z.string().optional(),
  neighborhood: z.string().min(2, 'Bairro deve ter pelo menos 2 caracteres'),
  city: z.string().min(2, 'Cidade deve ter pelo menos 2 caracteres'),
  state: z.string().length(2, 'Estado deve ter 2 caracteres'),
  cep: z.string().refine(validateCEP, 'CEP inválido'),
}).refine((data) => data.password === data.confirmPassword, {
  message: 'Senhas não coincidem',
  path: ['confirmPassword'],
})

export type RegisterForm = z.infer<typeof registerSchema>