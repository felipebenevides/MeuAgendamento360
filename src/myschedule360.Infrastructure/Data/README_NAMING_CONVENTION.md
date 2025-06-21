# Convenção de Nomenclatura Snake Case

Este documento descreve a convenção de nomenclatura adotada no projeto MeuAgendamento360 para o banco de dados.

## Regras

1. **Tabelas**:
   - Todas as tabelas devem ter o prefixo `tb_`
   - Nomes de tabelas devem usar snake_case (minúsculas com underscores)
   - Exemplo: `tb_user_profiles` em vez de `UserProfiles`

2. **Colunas**:
   - Todas as colunas devem usar snake_case
   - Exemplo: `first_name` em vez de `FirstName`

3. **Chaves Primárias**:
   - Prefixo `pk_` seguido pelo nome da tabela
   - Exemplo: `pk_tb_users`

4. **Chaves Estrangeiras**:
   - Prefixo `fk_` seguido pelo nome da tabela dependente e da tabela principal
   - Exemplo: `fk_tb_appointments_tb_users`

5. **Índices**:
   - Prefixo `ix_` seguido pelo nome da tabela e colunas
   - Exemplo: `ix_tb_users_email`

## Como Aplicar

A convenção é aplicada automaticamente pelo Entity Framework Core através da classe `SnakeCaseNamingConvention`.

### Para Novas Tabelas

Não é necessário configurar manualmente os nomes das tabelas e colunas. O sistema aplicará automaticamente a convenção.

### Para Tabelas Existentes

Se você estiver migrando tabelas existentes para esta convenção:

1. Crie uma nova migração:
   ```
   dotnet ef migrations add ApplySnakeCaseNamingConvention
   ```

2. Aplique a migração:
   ```
   dotnet ef database update
   ```

## Implementação

A implementação da convenção está na classe `SnakeCaseNamingConvention.cs` e é aplicada no método `OnModelCreating` do `ApplicationDbContext`.

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configure entity properties with specific requirements
    ConfigureEntityProperties(modelBuilder);
    
    // Apply snake_case naming convention to all tables and columns
    modelBuilder.ApplySnakeCaseNamingConvention();
    
    base.OnModelCreating(modelBuilder);
}
```

## Observações

- Esta convenção facilita a integração com sistemas que preferem snake_case (como muitas ferramentas PostgreSQL)
- Mantém consistência entre o banco de dados e as APIs
- Facilita consultas SQL manuais (não é necessário usar aspas para nomes de colunas)