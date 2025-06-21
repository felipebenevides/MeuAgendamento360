// Script para aplicar a migração no Supabase
// Executar com: node apply_migration.js

const { createClient } = require('@supabase/supabase-js');
const fs = require('fs');
const path = require('path');

// Configurações do Supabase
const supabaseUrl = process.env.SUPABASE_URL;
const supabaseKey = process.env.SUPABASE_KEY;

if (!supabaseUrl || !supabaseKey) {
  console.error('Erro: Defina as variáveis de ambiente SUPABASE_URL e SUPABASE_KEY');
  process.exit(1);
}

// Inicializa o cliente Supabase
const supabase = createClient(supabaseUrl, supabaseKey);

async function applyMigration() {
  try {
    // Lê o arquivo de migração
    const migrationPath = path.join(__dirname, 'migrations', '20250621000000_apply_snake_case_naming_convention.sql');
    const migrationSQL = fs.readFileSync(migrationPath, 'utf8');

    // Divide o SQL em comandos individuais
    const commands = migrationSQL
      .split(';')
      .map(cmd => cmd.trim())
      .filter(cmd => cmd.length > 0);

    console.log(`Aplicando migração com ${commands.length} comandos...`);

    // Executa cada comando SQL
    for (let i = 0; i < commands.length; i++) {
      const command = commands[i];
      console.log(`Executando comando ${i + 1}/${commands.length}`);
      
      const { error } = await supabase.rpc('exec_sql', { sql: command + ';' });
      
      if (error) {
        console.error(`Erro ao executar comando ${i + 1}:`, error);
        return;
      }
    }

    console.log('Migração aplicada com sucesso!');
  } catch (error) {
    console.error('Erro ao aplicar migração:', error);
  }
}

applyMigration();