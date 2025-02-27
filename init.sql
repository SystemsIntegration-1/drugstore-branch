CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE IF NOT EXISTS products (
                                        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(100) NOT NULL,
    stock INT NOT NULL,
    price NUMERIC(10,2) NOT NULL
    );

CREATE TABLE IF NOT EXISTS orders (
                                      id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    total_price NUMERIC(10,2) NOT NULL,
    order_date TIMESTAMP NOT NULL DEFAULT now()
    );

CREATE TABLE IF NOT EXISTS order_products (
                                              order_id UUID NOT NULL,
                                              product_id UUID NOT NULL,
                                              PRIMARY KEY (order_id, product_id),
    CONSTRAINT fk_order FOREIGN KEY(order_id) REFERENCES orders(id) ON DELETE CASCADE,
    CONSTRAINT fk_product FOREIGN KEY(product_id) REFERENCES products(id) ON DELETE CASCADE
    );

INSERT INTO products (name, stock, price) VALUES
                                              ('Product 1', 100, 9.99),
                                              ('Product 2', 200, 19.99),
                                              ('Product 3', 150, 14.99),
                                              ('Product 4', 50, 29.99),
                                              ('Product 5', 75, 24.99),
                                              ('Product 6', 300, 4.99),
                                              ('Product 7', 80, 39.99),
                                              ('Product 8', 90, 11.99),
                                              ('Product 9', 60, 49.99),
                                              ('Product 10', 120, 7.99)
    ON CONFLICT DO NOTHING;

INSERT INTO orders (total_price, order_date) VALUES
                                                 (59.97, now()),
                                                 (39.98, now()),
                                                 (19.99, now()),
                                                 (99.95, now()),
                                                 (49.95, now()),
                                                 (29.97, now()),
                                                 (89.99, now()),
                                                 (79.95, now()),
                                                 (69.95, now()),
                                                 (109.99, now())
    ON CONFLICT DO NOTHING;

INSERT INTO order_products (order_id, product_id)
SELECT o.id, p.id
FROM orders o, products p
WHERE o.id IN (SELECT id FROM orders LIMIT 3)
  AND p.id IN (SELECT id FROM products LIMIT 3)
ON CONFLICT DO NOTHING;
