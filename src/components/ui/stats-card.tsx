import { LucideIcon } from 'lucide-react'
import { Card, CardContent } from './card'

interface StatsCardProps {
  title: string
  value: string | number
  icon: LucideIcon
  trend?: {
    value: string
    direction: 'up' | 'down' | 'neutral'
  }
  color?: 'blue' | 'green' | 'purple' | 'orange' | 'red'
}

export function StatsCard({ title, value, icon: Icon, trend, color = 'blue' }: StatsCardProps) {
  const colorMap = {
    blue: {
      bg: 'bg-blue-50',
      text: 'text-blue-600',
    },
    green: {
      bg: 'bg-green-50',
      text: 'text-green-600',
    },
    purple: {
      bg: 'bg-purple-50',
      text: 'text-purple-600',
    },
    orange: {
      bg: 'bg-orange-50',
      text: 'text-orange-600',
    },
    red: {
      bg: 'bg-red-50',
      text: 'text-red-600',
    },
  }

  const getTrendColor = (direction: 'up' | 'down' | 'neutral') => {
    switch (direction) {
      case 'up': return 'text-green-600'
      case 'down': return 'text-red-600'
      case 'neutral': return 'text-gray-600'
    }
  }

  const getTrendIcon = (direction: 'up' | 'down' | 'neutral') => {
    switch (direction) {
      case 'up': return '↑'
      case 'down': return '↓'
      case 'neutral': return '→'
    }
  }

  return (
    <Card className="animate-fade-in-up">
      <CardContent className="p-6">
        <div className="flex items-center justify-between mb-4">
          <div className={`w-12 h-12 ${colorMap[color].bg} rounded-xl flex items-center justify-center`}>
            <Icon className={`w-6 h-6 ${colorMap[color].text}`} />
          </div>
          
          {trend && (
            <div className={`flex items-center gap-1 text-sm font-medium ${getTrendColor(trend.direction)}`}>
              {getTrendIcon(trend.direction)}
              {trend.value}
            </div>
          )}
        </div>
        
        <div>
          <p className="text-2xl font-bold text-gray-900 mb-1">{value}</p>
          <p className="text-sm text-gray-600">{title}</p>
        </div>
      </CardContent>
    </Card>
  )
}