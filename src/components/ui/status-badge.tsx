import { cn } from '@/lib/utils'

interface StatusBadgeProps {
  status: string
  size?: 'sm' | 'md' | 'lg'
  className?: string
  statusMap?: Record<string, { color: string; label: string }>
}

export function StatusBadge({ 
  status, 
  size = 'md', 
  className,
  statusMap
}: StatusBadgeProps) {
  // Default status mappings
  const defaultStatusMap: Record<string, { color: string; label: string }> = {
    active: { color: 'bg-green-100 text-green-800', label: 'Ativo' },
    inactive: { color: 'bg-gray-100 text-gray-800', label: 'Inativo' },
    pending: { color: 'bg-yellow-100 text-yellow-800', label: 'Pendente' },
    confirmed: { color: 'bg-green-100 text-green-800', label: 'Confirmado' },
    cancelled: { color: 'bg-red-100 text-red-800', label: 'Cancelado' },
    completed: { color: 'bg-blue-100 text-blue-800', label: 'Concluído' },
    'in-progress': { color: 'bg-blue-100 text-blue-800', label: 'Em Andamento' },
    vip: { color: 'bg-purple-100 text-purple-800', label: 'VIP' },
    high: { color: 'bg-green-100 text-green-800', label: 'Alta' },
    medium: { color: 'bg-blue-100 text-blue-800', label: 'Média' },
    low: { color: 'bg-yellow-100 text-yellow-800', label: 'Baixa' },
  }

  // Use provided statusMap or default
  const finalStatusMap = statusMap || defaultStatusMap
  
  // Get status config or fallback to gray
  const statusConfig = finalStatusMap[status] || { 
    color: 'bg-gray-100 text-gray-800', 
    label: status.charAt(0).toUpperCase() + status.slice(1) 
  }

  const sizeClasses = {
    sm: 'px-1.5 py-0.5 text-xs',
    md: 'px-2 py-1 text-xs',
    lg: 'px-3 py-1.5 text-sm'
  }

  return (
    <span className={cn(
      'inline-block rounded-full font-medium',
      statusConfig.color,
      sizeClasses[size],
      className
    )}>
      {statusConfig.label}
    </span>
  )
}