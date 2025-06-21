using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;

namespace myschedule360.Infrastructure.Data
{
    public static class SnakeCaseNamingConvention
    {
        public static void ApplySnakeCaseNamingConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Configure table name with tb_ prefix and snake_case
                var tableName = "tb_" + ToSnakeCase(entity.GetTableName());
                entity.SetTableName(tableName);

                // Configure column names to snake_case
                foreach (var property in entity.GetProperties())
                {
                    var columnName = ToSnakeCase(property.GetColumnName());
                    property.SetColumnName(columnName);
                }

                // Configure primary key name
                foreach (var key in entity.GetKeys())
                {
                    var keyName = "pk_" + ToSnakeCase(tableName);
                    key.SetName(keyName);
                }

                // Configure foreign key names
                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    var principalEntityName = foreignKey.PrincipalEntityType.GetTableName();
                    var dependentEntityName = foreignKey.DeclaringEntityType.GetTableName();
                    var foreignKeyName = "fk_" + ToSnakeCase(dependentEntityName) + "_" + ToSnakeCase(principalEntityName);
                    foreignKey.SetConstraintName(foreignKeyName);
                }

                // Configure index names
                foreach (var index in entity.GetIndexes())
                {
                    var indexName = "ix_" + ToSnakeCase(tableName) + "_" + string.Join("_", index.Properties.Select(p => ToSnakeCase(p.Name)));
                    index.SetDatabaseName(indexName);
                }
            }
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remove tb_ prefix if it exists (to avoid duplication when reapplying)
            if (input.StartsWith("tb_"))
                input = input.Substring(3);

            // Handle acronyms first (e.g., "URL" -> "url")
            input = Regex.Replace(input, @"([A-Z]+)([A-Z][a-z])", "$1_$2").ToLower();
            
            // Handle camelCase
            input = Regex.Replace(input, @"([a-z\d])([A-Z])", "$1_$2").ToLower();
            
            // Replace spaces and hyphens with underscores
            input = Regex.Replace(input, @"[\s\-]+", "_");
            
            return input;
        }
    }
}