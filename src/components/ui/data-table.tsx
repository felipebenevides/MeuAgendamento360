'use client'

import { useState } from 'react'
import { 
  ChevronDown, 
  ChevronUp, 
  ChevronsLeft, 
  ChevronsRight, 
  ChevronLeft, 
  ChevronRight,
  Search,
  Filter
} from 'lucide-react'
import { Input } from './input'
import { Button } from './button'
import { Card, CardContent, CardHeader, CardTitle } from './card'
import { EmptyState } from './empty-state'
import { Loading } from './loading'

interface Column<T> {
  header: string
  accessorKey: keyof T | ((row: T) => any)
  cell?: (row: T) => React.ReactNode
  sortable?: boolean
}

interface DataTableProps<T> {
  columns: Column<T>[]
  data: T[]
  title?: string
  isLoading?: boolean
  emptyState?: {
    icon: any
    title: string
    description: string
    action?: {
      label: string
      onClick: () => void
    }
  }
  pagination?: {
    pageIndex: number
    pageSize: number
    pageCount: number
    onPageChange: (page: number) => void
  }
  searchable?: boolean
  onSearch?: (query: string) => void
  filterComponent?: React.ReactNode
}

export function DataTable<T>({
  columns,
  data,
  title,
  isLoading,
  emptyState,
  pagination,
  searchable,
  onSearch,
  filterComponent
}: DataTableProps<T>) {
  const [sortBy, setSortBy] = useState<keyof T | null>(null)
  const [sortDirection, setSortDirection] = useState<'asc' | 'desc'>('asc')
  const [searchQuery, setSearchQuery] = useState('')

  const handleSort = (column: Column<T>) => {
    if (!column.sortable) return

    const accessorKey = typeof column.accessorKey === 'string' ? column.accessorKey : null
    if (!accessorKey) return

    if (sortBy === accessorKey) {
      setSortDirection(sortDirection === 'asc' ? 'desc' : 'asc')
    } else {
      setSortBy(accessorKey)
      setSortDirection('asc')
    }
  }

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault()
    if (onSearch) {
      onSearch(searchQuery)
    }
  }

  const renderCell = (row: T, column: Column<T>) => {
    if (column.cell) {
      return column.cell(row)
    }

    if (typeof column.accessorKey === 'function') {
      return column.accessorKey(row)
    }

    return row[column.accessorKey] as React.ReactNode
  }

  return (
    <Card>
      {title && (
        <CardHeader>
          <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
            <CardTitle>{title}</CardTitle>
            
            <div className="flex flex-col sm:flex-row gap-4">
              {searchable && (
                <form onSubmit={handleSearch} className="relative">
                  <Search className="w-4 h-4 absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
                  <Input
                    placeholder="Buscar..."
                    className="pl-9 w-full sm:w-60"
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                  />
                </form>
              )}
              
              {filterComponent}
            </div>
          </div>
        </CardHeader>
      )}
      
      <CardContent>
        {isLoading ? (
          <div className="py-12">
            <Loading text="Carregando dados..." />
          </div>
        ) : data.length === 0 && emptyState ? (
          <EmptyState
            icon={emptyState.icon}
            title={emptyState.title}
            description={emptyState.description}
            action={emptyState.action}
          />
        ) : (
          <>
            <div className="overflow-x-auto">
              <table className="w-full">
                <thead>
                  <tr className="border-b border-gray-200">
                    {columns.map((column, index) => (
                      <th
                        key={index}
                        className={`px-4 py-3 text-left text-sm font-medium text-gray-600 ${
                          column.sortable ? 'cursor-pointer hover:bg-gray-50' : ''
                        }`}
                        onClick={() => column.sortable && handleSort(column)}
                      >
                        <div className="flex items-center gap-1">
                          {column.header}
                          {column.sortable && sortBy === column.accessorKey && (
                            sortDirection === 'asc' ? (
                              <ChevronUp className="w-4 h-4" />
                            ) : (
                              <ChevronDown className="w-4 h-4" />
                            )
                          )}
                        </div>
                      </th>
                    ))}
                  </tr>
                </thead>
                <tbody>
                  {data.map((row, rowIndex) => (
                    <tr
                      key={rowIndex}
                      className="border-b border-gray-100 last:border-0 hover:bg-gray-50"
                    >
                      {columns.map((column, colIndex) => (
                        <td key={colIndex} className="px-4 py-3 text-sm">
                          {renderCell(row, column)}
                        </td>
                      ))}
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            
            {pagination && (
              <div className="flex items-center justify-between mt-4">
                <div className="text-sm text-gray-600">
                  Mostrando {pagination.pageIndex * pagination.pageSize + 1} a{' '}
                  {Math.min((pagination.pageIndex + 1) * pagination.pageSize, data.length)}{' '}
                  de {data.length} resultados
                </div>
                
                <div className="flex items-center gap-2">
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => pagination.onPageChange(0)}
                    disabled={pagination.pageIndex === 0}
                  >
                    <ChevronsLeft className="w-4 h-4" />
                  </Button>
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => pagination.onPageChange(pagination.pageIndex - 1)}
                    disabled={pagination.pageIndex === 0}
                  >
                    <ChevronLeft className="w-4 h-4" />
                  </Button>
                  
                  <span className="text-sm">
                    Página {pagination.pageIndex + 1} de {pagination.pageCount}
                  </span>
                  
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => pagination.onPageChange(pagination.pageIndex + 1)}
                    disabled={pagination.pageIndex === pagination.pageCount - 1}
                  >
                    <ChevronRight className="w-4 h-4" />
                  </Button>
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => pagination.onPageChange(pagination.pageCount - 1)}
                    disabled={pagination.pageIndex === pagination.pageCount - 1}
                  >
                    <ChevronsRight className="w-4 h-4" />
                  </Button>
                </div>
              </div>
            )}
          </>
        )}
      </CardContent>
    </Card>
  )
}