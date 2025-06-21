-- Apply snake_case naming convention with tb_ prefix for tables
-- Migration for Supabase

-- Create extension if not exists
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Function to convert camelCase or PascalCase to snake_case
CREATE OR REPLACE FUNCTION to_snake_case(input_text TEXT) 
RETURNS TEXT AS $$
DECLARE
    result TEXT := '';
    i INT := 1;
    c TEXT;
BEGIN
    IF input_text IS NULL THEN
        RETURN NULL;
    END IF;
    
    -- Remove tb_ prefix if it exists (to avoid duplication)
    IF LEFT(input_text, 3) = 'tb_' THEN
        input_text := SUBSTRING(input_text FROM 4);
    END IF;
    
    WHILE i <= LENGTH(input_text) LOOP
        c := SUBSTRING(input_text FROM i FOR 1);
        
        -- If uppercase and not first character, add underscore
        IF c ~ '[A-Z]' AND i > 1 THEN
            -- Check if previous character is not uppercase or next character is not uppercase
            IF SUBSTRING(input_text FROM i-1 FOR 1) !~ '[A-Z]' OR 
               (i < LENGTH(input_text) AND SUBSTRING(input_text FROM i+1 FOR 1) !~ '[A-Z]') THEN
                result := result || '_';
            END IF;
        END IF;
        
        -- Add lowercase character
        result := result || LOWER(c);
        
        i := i + 1;
    END LOOP;
    
    -- Replace spaces and hyphens with underscores
    result := REGEXP_REPLACE(result, '[\s\-]+', '_', 'g');
    
    RETURN result;
END;
$$ LANGUAGE plpgsql;

-- Create tables with snake_case naming convention and tb_ prefix

-- Users table
CREATE TABLE IF NOT EXISTS tb_users (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    email TEXT UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    full_name TEXT NOT NULL,
    role TEXT NOT NULL CHECK (role IN ('admin', 'business_owner', 'staff', 'customer')),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Businesses table
CREATE TABLE IF NOT EXISTS tb_businesses (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name TEXT NOT NULL,
    slug TEXT UNIQUE NOT NULL,
    description TEXT,
    owner_id UUID NOT NULL REFERENCES tb_users(id),
    logo_url TEXT,
    phone TEXT,
    email TEXT,
    website TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Addresses table
CREATE TABLE IF NOT EXISTS tb_addresses (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    street TEXT NOT NULL,
    number TEXT,
    complement TEXT,
    neighborhood TEXT,
    city TEXT NOT NULL,
    state TEXT NOT NULL,
    postal_code TEXT NOT NULL,
    country TEXT NOT NULL DEFAULT 'Brazil',
    latitude NUMERIC,
    longitude NUMERIC,
    business_id UUID REFERENCES tb_businesses(id),
    user_id UUID REFERENCES tb_users(id),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT address_belongs_to_one CHECK (
        (business_id IS NOT NULL AND user_id IS NULL) OR
        (business_id IS NULL AND user_id IS NOT NULL)
    )
);

-- Services table
CREATE TABLE IF NOT EXISTS tb_services (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name TEXT NOT NULL,
    description TEXT,
    duration_minutes INTEGER NOT NULL,
    price NUMERIC(10,2) NOT NULL,
    business_id UUID NOT NULL REFERENCES tb_businesses(id),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Staff table
CREATE TABLE IF NOT EXISTS tb_staff (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL REFERENCES tb_users(id),
    business_id UUID NOT NULL REFERENCES tb_businesses(id),
    position TEXT,
    bio TEXT,
    profile_image_url TEXT,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    UNIQUE(user_id, business_id)
);

-- Staff services table
CREATE TABLE IF NOT EXISTS tb_staff_services (
    staff_id UUID NOT NULL REFERENCES tb_staff(id),
    service_id UUID NOT NULL REFERENCES tb_services(id),
    PRIMARY KEY (staff_id, service_id)
);

-- Staff availability table
CREATE TABLE IF NOT EXISTS tb_staff_availability (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    staff_id UUID NOT NULL REFERENCES tb_staff(id),
    day_of_week INTEGER NOT NULL CHECK (day_of_week BETWEEN 0 AND 6),
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    is_available BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT valid_time_range CHECK (start_time < end_time)
);

-- Customers table
CREATE TABLE IF NOT EXISTS tb_customers (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL REFERENCES tb_users(id),
    phone TEXT,
    notes TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Business customers table
CREATE TABLE IF NOT EXISTS tb_business_customers (
    business_id UUID NOT NULL REFERENCES tb_businesses(id),
    customer_id UUID NOT NULL REFERENCES tb_customers(id),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (business_id, customer_id)
);

-- Appointments table
CREATE TABLE IF NOT EXISTS tb_appointments (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    customer_id UUID NOT NULL REFERENCES tb_customers(id),
    staff_id UUID NOT NULL REFERENCES tb_staff(id),
    service_id UUID NOT NULL REFERENCES tb_services(id),
    business_id UUID NOT NULL REFERENCES tb_businesses(id),
    start_time TIMESTAMPTZ NOT NULL,
    end_time TIMESTAMPTZ NOT NULL,
    status TEXT NOT NULL CHECK (status IN ('scheduled', 'confirmed', 'completed', 'cancelled', 'no_show')),
    notes TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT valid_appointment_time CHECK (start_time < end_time)
);

-- Notifications table
CREATE TABLE IF NOT EXISTS tb_notifications (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL REFERENCES tb_users(id),
    title TEXT NOT NULL,
    message TEXT NOT NULL,
    type TEXT NOT NULL CHECK (type IN ('appointment_reminder', 'appointment_confirmation', 'appointment_cancellation', 'system')),
    is_read BOOLEAN NOT NULL DEFAULT FALSE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Financial transactions table
CREATE TABLE IF NOT EXISTS tb_financial_transactions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    business_id UUID NOT NULL REFERENCES tb_businesses(id),
    amount NUMERIC(10,2) NOT NULL,
    type TEXT NOT NULL CHECK (type IN ('income', 'expense')),
    category TEXT NOT NULL,
    description TEXT,
    appointment_id UUID REFERENCES tb_appointments(id),
    staff_id UUID REFERENCES tb_staff(id),
    transaction_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Commission rules table
CREATE TABLE IF NOT EXISTS tb_commission_rules (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    business_id UUID NOT NULL REFERENCES tb_businesses(id),
    staff_id UUID REFERENCES tb_staff(id),
    service_id UUID REFERENCES tb_services(id),
    percentage NUMERIC(5,2) NOT NULL CHECK (percentage BETWEEN 0 AND 100),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT commission_rule_scope CHECK (
        (staff_id IS NOT NULL AND service_id IS NULL) OR
        (staff_id IS NULL AND service_id IS NOT NULL) OR
        (staff_id IS NOT NULL AND service_id IS NOT NULL)
    )
);

-- Inventory items table
CREATE TABLE IF NOT EXISTS tb_inventory_items (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    business_id UUID NOT NULL REFERENCES tb_businesses(id),
    name TEXT NOT NULL,
    description TEXT,
    sku TEXT,
    quantity INTEGER NOT NULL DEFAULT 0,
    unit TEXT NOT NULL,
    cost_price NUMERIC(10,2),
    min_stock_level INTEGER,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Inventory transactions table
CREATE TABLE IF NOT EXISTS tb_inventory_transactions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    inventory_item_id UUID NOT NULL REFERENCES tb_inventory_items(id),
    quantity INTEGER NOT NULL,
    type TEXT NOT NULL CHECK (type IN ('purchase', 'usage', 'adjustment')),
    notes TEXT,
    staff_id UUID REFERENCES tb_staff(id),
    appointment_id UUID REFERENCES tb_appointments(id),
    transaction_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Service inventory items table
CREATE TABLE IF NOT EXISTS tb_service_inventory_items (
    service_id UUID NOT NULL REFERENCES tb_services(id),
    inventory_item_id UUID NOT NULL REFERENCES tb_inventory_items(id),
    quantity_used NUMERIC(10,2) NOT NULL,
    PRIMARY KEY (service_id, inventory_item_id)
);

-- Enable Row Level Security
ALTER TABLE tb_users ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_businesses ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_addresses ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_services ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_staff ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_staff_services ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_staff_availability ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_customers ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_business_customers ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_appointments ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_notifications ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_financial_transactions ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_commission_rules ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_inventory_items ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_inventory_transactions ENABLE ROW LEVEL SECURITY;
ALTER TABLE tb_service_inventory_items ENABLE ROW LEVEL SECURITY;