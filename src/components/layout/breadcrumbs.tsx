'use client'

import { usePathname } from 'next/navigation'
import Link from 'next/link'
import { ChevronRight, Home } from 'lucide-react'

interface BreadcrumbsProps {
  homeHref?: string
  items?: { label: string; href?: string }[]
}

export function Breadcrumbs({ homeHref = '/dashboard', items }: BreadcrumbsProps) {
  const pathname = usePathname()
  
  // Generate breadcrumbs from pathname if items not provided
  const breadcrumbs = items || generateBreadcrumbs(pathname)

  return (
    <nav className="flex" aria-label="Breadcrumb">
      <ol className="flex items-center space-x-2">
        <li>
          <Link 
            href={homeHref}
            className="text-gray-500 hover:text-gray-700 transition-colors"
          >
            <Home className="w-4 h-4" />
          </Link>
        </li>
        
        {breadcrumbs.map((item, index) => (
          <li key={index} className="flex items-center">
            <ChevronRight className="w-4 h-4 text-gray-400 mx-1" />
            {item.href ? (
              <Link 
                href={item.href}
                className="text-sm text-gray-500 hover:text-gray-700 transition-colors"
              >
                {item.label}
              </Link>
            ) : (
              <span className="text-sm font-medium text-gray-900">
                {item.label}
              </span>
            )}
          </li>
        ))}
      </ol>
    </nav>
  )
}

// Helper function to generate breadcrumbs from pathname
function generateBreadcrumbs(pathname: string) {
  // Remove leading slash and split by '/'
  const paths = pathname.substring(1).split('/')
  
  // Map paths to breadcrumb items
  return paths.map((path, index) => {
    // Skip empty paths
    if (!path) return { label: 'Home' }
    
    // Format path for display (capitalize, replace hyphens)
    const label = path
      .split('-')
      .map(word => word.charAt(0).toUpperCase() + word.slice(1))
      .join(' ')
    
    // Create href for all but the last item
    const href = index < paths.length - 1
      ? `/${paths.slice(0, index + 1).join('/')}`
      : undefined
    
    return { label, href }
  })
}