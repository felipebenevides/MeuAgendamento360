-- Data Migration Script: Business to Owner/Store Structure
-- This script migrates existing Business data to the new Owner/Store relationship

-- Step 1: Create owners for existing business owners
INSERT INTO owners (id, user_id, created_at, updated_at)
SELECT 
    gen_random_uuid() as id,
    u.id as user_id,
    NOW() as created_at,
    NULL as updated_at
FROM users u
WHERE u.role = 1 -- BusinessOwner role
AND u.business_id IS NOT NULL;

-- Step 2: Create stores from existing businesses
INSERT INTO stores (id, name, slug, description, cnpj, website, type, is_onboarding_complete, address_id, owner_id, created_at, updated_at)
SELECT 
    b.id,
    b.name,
    b.slug,
    b.description,
    b.cnpj,
    b.website,
    b.type,
    b.is_onboarding_complete,
    b.address_id,
    o.id as owner_id,
    b.created_at,
    b.updated_at
FROM businesses b
INNER JOIN users u ON u.business_id = b.id AND u.role = 1 -- BusinessOwner
INNER JOIN owners o ON o.user_id = u.id;

-- Step 3: Update users to reference stores instead of businesses
UPDATE users 
SET store_id = business_id 
WHERE business_id IS NOT NULL;

-- Step 4: Update services to reference stores
UPDATE services 
SET store_id = business_id 
WHERE business_id IS NOT NULL;

-- Step 5: Update staff to reference stores
UPDATE staff 
SET store_id = business_id 
WHERE business_id IS NOT NULL;

-- Step 6: Update inventory_items to reference stores
UPDATE inventory_items 
SET store_id = business_id 
WHERE business_id IS NOT NULL;

-- Step 7: Update financial_transactions to reference stores
UPDATE financial_transactions 
SET store_id = business_id 
WHERE business_id IS NOT NULL;

-- Step 8: Clean up - Remove business_id columns (this should be done in a separate migration)
-- ALTER TABLE users DROP COLUMN business_id;
-- ALTER TABLE services DROP COLUMN business_id;
-- ALTER TABLE staff DROP COLUMN business_id;
-- ALTER TABLE inventory_items DROP COLUMN business_id;
-- ALTER TABLE financial_transactions DROP COLUMN business_id;