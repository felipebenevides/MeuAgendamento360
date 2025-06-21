-- Migration script to apply snake_case naming convention with tb_ prefix for tables

-- Rename tables to follow tb_ prefix and snake_case convention
ALTER TABLE IF EXISTS persons RENAME TO tb_persons;
ALTER TABLE IF EXISTS addresses RENAME TO tb_addresses;
ALTER TABLE IF EXISTS users RENAME TO tb_users;
ALTER TABLE IF EXISTS businesses RENAME TO tb_businesses;
ALTER TABLE IF EXISTS services RENAME TO tb_services;
ALTER TABLE IF EXISTS staff RENAME TO tb_staff;
ALTER TABLE IF EXISTS customers RENAME TO tb_customers;
ALTER TABLE IF EXISTS appointments RENAME TO tb_appointments;
ALTER TABLE IF EXISTS financial_transactions RENAME TO tb_financial_transactions;
ALTER TABLE IF EXISTS inventory_items RENAME TO tb_inventory_items;
ALTER TABLE IF EXISTS commission_rules RENAME TO tb_commission_rules;
ALTER TABLE IF EXISTS staff_availabilities RENAME TO tb_staff_availabilities;
ALTER TABLE IF EXISTS notifications RENAME TO tb_notifications;
ALTER TABLE IF EXISTS staff_services RENAME TO tb_staff_services;
ALTER TABLE IF EXISTS business_customers RENAME TO tb_business_customers;
ALTER TABLE IF EXISTS inventory_transactions RENAME TO tb_inventory_transactions;
ALTER TABLE IF EXISTS service_inventory_items RENAME TO tb_service_inventory_items;

-- Rename columns to follow snake_case convention
-- This would be a very long script with all columns
-- Here's an example for a few columns:

-- Example for users table
ALTER TABLE IF EXISTS tb_users RENAME COLUMN "FirstName" TO first_name;
ALTER TABLE IF EXISTS tb_users RENAME COLUMN "LastName" TO last_name;
ALTER TABLE IF EXISTS tb_users RENAME COLUMN "Email" TO email;
ALTER TABLE IF EXISTS tb_users RENAME COLUMN "PasswordHash" TO password_hash;
ALTER TABLE IF EXISTS tb_users RENAME COLUMN "IsActive" TO is_active;
ALTER TABLE IF EXISTS tb_users RENAME COLUMN "CreatedAt" TO created_at;
ALTER TABLE IF EXISTS tb_users RENAME COLUMN "UpdatedAt" TO updated_at;

-- Example for businesses table
ALTER TABLE IF EXISTS tb_businesses RENAME COLUMN "Name" TO name;
ALTER TABLE IF EXISTS tb_businesses RENAME COLUMN "Slug" TO slug;
ALTER TABLE IF EXISTS tb_businesses RENAME COLUMN "Description" TO description;
ALTER TABLE IF EXISTS tb_businesses RENAME COLUMN "Website" TO website;
ALTER TABLE IF EXISTS tb_businesses RENAME COLUMN "IsOnboardingComplete" TO is_onboarding_complete;
ALTER TABLE IF EXISTS tb_businesses RENAME COLUMN "CreatedAt" TO created_at;
ALTER TABLE IF EXISTS tb_businesses RENAME COLUMN "UpdatedAt" TO updated_at;

-- Example for services table
ALTER TABLE IF EXISTS tb_services RENAME COLUMN "Name" TO name;
ALTER TABLE IF EXISTS tb_services RENAME COLUMN "Description" TO description;
ALTER TABLE IF EXISTS tb_services RENAME COLUMN "Price" TO price;
ALTER TABLE IF EXISTS tb_services RENAME COLUMN "DurationMinutes" TO duration_minutes;
ALTER TABLE IF EXISTS tb_services RENAME COLUMN "BusinessId" TO business_id;
ALTER TABLE IF EXISTS tb_services RENAME COLUMN "IsActive" TO is_active;
ALTER TABLE IF EXISTS tb_services RENAME COLUMN "CreatedAt" TO created_at;
ALTER TABLE IF EXISTS tb_services RENAME COLUMN "UpdatedAt" TO updated_at;

-- Note: In a real migration, you would need to include ALL columns from ALL tables
-- This is just a sample to demonstrate the pattern