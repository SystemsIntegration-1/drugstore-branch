-- Habilitar la extensión pgcrypto para generar UUIDs
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- Crear la tabla de productos
CREATE TABLE IF NOT EXISTS products (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    shared_id UUID NOT NULL,
    name VARCHAR(100) NOT NULL,
    description TEXT NOT NULL,
    category VARCHAR(100) NOT NULL,
    warehouse_location VARCHAR(255) NOT NULL,
    price INT NOT NULL DEFAULT 0
    );

-- Crear la tabla de lotes
CREATE TABLE IF NOT EXISTS batches (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    product_id UUID NOT NULL,
    stock INT NOT NULL,
    entry_date BIGINT NOT NULL,
    expiration_date BIGINT NOT NULL,
    CONSTRAINT fk_product FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE
    );

-- Create the orders table
CREATE TABLE IF NOT EXISTS orders (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    productquantities JSONB NOT NULL,
    total_price INT NOT NULL DEFAULT 0,
    order_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Insertar datos de prueba en productos
INSERT INTO products (shared_id, name, description, category, warehouse_location, price)
VALUES
    (gen_random_uuid(), 'Paracetamol', 'Analgésico y antipirético', 'Medicamento', 'A1-03', 5),
    (gen_random_uuid(), 'Ibuprofeno', 'Antiinflamatorio no esteroideo', 'Medicamento', 'B2-01', 7),
    (gen_random_uuid(), 'Omeprazol', 'Inhibidor de la bomba de protones', 'Medicamento', 'C4-05', 10),
    (gen_random_uuid(), 'Amoxicilina', 'Antibiótico de amplio espectro', 'Medicamento', 'D3-02', 12),
    (gen_random_uuid(), 'Loratadina', 'Antihistamínico para alergias', 'Medicamento', 'E2-04', 8),
    (gen_random_uuid(), 'Salbutamol', 'Broncodilatador para asma', 'Medicamento', 'F5-01', 15),
    (gen_random_uuid(), 'Metformina', 'Antidiabético oral', 'Medicamento', 'G1-07', 9);

-- Insertar lotes para los productos creados
INSERT INTO batches (product_id, stock, entry_date, expiration_date)
SELECT id, 100, EXTRACT(EPOCH FROM now()), EXTRACT(EPOCH FROM now() + interval '2 years')
FROM products ORDER BY id LIMIT 1
UNION ALL
SELECT id, 50, EXTRACT(EPOCH FROM now()), EXTRACT(EPOCH FROM now() + interval '1.5 years')
FROM products ORDER BY id LIMIT 1 OFFSET 1
UNION ALL
SELECT id, 75, EXTRACT(EPOCH FROM now()), EXTRACT(EPOCH FROM now() + interval '3 years')
FROM products ORDER BY id LIMIT 1 OFFSET 2
UNION ALL
SELECT id, 120, EXTRACT(EPOCH FROM now()), EXTRACT(EPOCH FROM now() + interval '2.5 years')
FROM products ORDER BY id LIMIT 1 OFFSET 3
UNION ALL
SELECT id, 90, EXTRACT(EPOCH FROM now()), EXTRACT(EPOCH FROM now() + interval '4 years')
FROM products ORDER BY id LIMIT 1 OFFSET 4
UNION ALL
SELECT id, 30, EXTRACT(EPOCH FROM now()), EXTRACT(EPOCH FROM now() + interval '1 year')
FROM products ORDER BY id LIMIT 1 OFFSET 5
UNION ALL
SELECT id, 200, EXTRACT(EPOCH FROM now()), EXTRACT(EPOCH FROM now() + interval '5 years')
FROM products ORDER BY id LIMIT 1 OFFSET 6;
