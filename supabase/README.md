# Migrações Supabase para MeuAgendamento360

Este diretório contém os scripts de migração para o banco de dados Supabase do projeto MeuAgendamento360.

## Estrutura

- `migrations/` - Contém os arquivos SQL de migração
- `apply_migration.js` - Script para aplicar migrações via API do Supabase

## Convenção de Nomenclatura

Todas as tabelas e colunas seguem a convenção snake_case:

- Tabelas têm o prefixo `tb_` (ex: `tb_users`)
- Colunas usam snake_case (ex: `first_name`)
- Chaves primárias têm o prefixo `pk_` (ex: `pk_tb_users`)
- Chaves estrangeiras têm o prefixo `fk_` (ex: `fk_tb_appointments_tb_users`)
- Índices têm o prefixo `ix_` (ex: `ix_tb_users_email`)

## Como Aplicar Migrações

### Método 1: Via Interface do Supabase

1. Acesse o [Dashboard do Supabase](https://app.supabase.io)
2. Selecione seu projeto
3. Vá para "SQL Editor"
4. Copie e cole o conteúdo do arquivo de migração
5. Execute o script

### Método 2: Via CLI do Supabase

1. Instale a CLI do Supabase:
   ```
   npm install -g supabase
   ```

2. Faça login:
   ```
   supabase login
   ```

3. Aplique a migração:
   ```
   supabase db push --db-url=sua_url_do_supabase
   ```

### Método 3: Via Script JavaScript

1. Instale as dependências:
   ```
   npm install @supabase/supabase-js
   ```

2. Configure as variáveis de ambiente:
   ```
   export SUPABASE_URL=sua_url_do_supabase
   export SUPABASE_KEY=sua_chave_do_supabase
   ```

3. Execute o script:
   ```
   node apply_migration.js
   ```

## Migrações Disponíveis

1. `20250621000000_apply_snake_case_naming_convention.sql` - Cria o esquema inicial com convenção de nomenclatura snake_case

## Notas Importantes

- Sempre faça backup do banco de dados antes de aplicar migrações
- Teste as migrações em um ambiente de desenvolvimento antes de aplicar em produção
- As migrações são aplicadas em ordem cronológica com base no prefixo numérico do nome do arquivo