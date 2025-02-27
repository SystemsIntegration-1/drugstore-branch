CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE IF NOT EXISTS "Products" (
                                          "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "Name" VARCHAR(100) NOT NULL,
    "Stock" INT NOT NULL,
    "Price" NUMERIC(10,2) NOT NULL
    );

CREATE TABLE IF NOT EXISTS "Orders" (
                                        "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "TotalPrice" NUMERIC(10,2) NOT NULL,
    "OrderDate" TIMESTAMP NOT NULL
    );

CREATE TABLE IF NOT EXISTS "OrderProducts" (
                                               "OrderId" UUID NOT NULL,
                                               "ProductId" UUID NOT NULL,
                                               PRIMARY KEY ("OrderId", "ProductId"),
    CONSTRAINT fk_order
    FOREIGN KEY("OrderId")
    REFERENCES "Orders"("Id") ON DELETE CASCADE,
    CONSTRAINT fk_product
    FOREIGN KEY("ProductId")
    REFERENCES "Products"("Id") ON DELETE CASCADE
    );

INSERT INTO "Products" ("Name", "Stock", "Price") VALUES
                                                      ('Product 1', 100, 9.99),
                                                      ('Product 2', 200, 19.99),
                                                      ('Product 3', 150, 14.99),
                                                      ('Product 4', 50, 29.99),
                                                      ('Product 5', 75, 24.99),
                                                      ('Product 6', 300, 4.99),
                                                      ('Product 7', 80, 39.99),
                                                      ('Product 8', 90, 11.99),
                                                      ('Product 9', 60, 49.99),
                                                      ('Product 10', 120, 7.99);

INSERT INTO "Orders" ("TotalPrice", "OrderDate") VALUES
                                                     (59.97, now()),
                                                     (39.98, now()),
                                                     (19.99, now()),
                                                     (99.95, now()),
                                                     (49.95, now()),
                                                     (29.97, now()),
                                                     (89.99, now()),
                                                     (79.95, now()),
                                                     (69.95, now()),
                                                     (109.99, now());

INSERT INTO "OrderProducts" ("OrderId", "ProductId")
SELECT o."Id", p."Id"
FROM "Orders" o, "Products" p
WHERE o."Id" IN (
    SELECT "Id" FROM "Orders" LIMIT 3
    )
  AND p."Id" IN (
SELECT "Id" FROM "Products" LIMIT 3
    );
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE IF NOT EXISTS "Products" (
                                          "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "Name" VARCHAR(100) NOT NULL,
    "Stock" INT NOT NULL,
    "Price" NUMERIC(10,2) NOT NULL
    );

CREATE TABLE IF NOT EXISTS "Orders" (
                                        "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "TotalPrice" NUMERIC(10,2) NOT NULL,
    "OrderDate" TIMESTAMP NOT NULL
    );

CREATE TABLE IF NOT EXISTS "OrderProducts" (
                                               "OrderId" UUID NOT NULL,
                                               "ProductId" UUID NOT NULL,
                                               PRIMARY KEY ("OrderId", "ProductId"),
    CONSTRAINT fk_order
    FOREIGN KEY("OrderId")
    REFERENCES "Orders"("Id") ON DELETE CASCADE,
    CONSTRAINT fk_product
    FOREIGN KEY("ProductId")
    REFERENCES "Products"("Id") ON DELETE CASCADE
    );

INSERT INTO "Products" ("Name", "Stock", "Price") VALUES
                                                      ('Product 1', 100, 9.99),
                                                      ('Product 2', 200, 19.99),
                                                      ('Product 3', 150, 14.99),
                                                      ('Product 4', 50, 29.99),
                                                      ('Product 5', 75, 24.99),
                                                      ('Product 6', 300, 4.99),
                                                      ('Product 7', 80, 39.99),
                                                      ('Product 8', 90, 11.99),
                                                      ('Product 9', 60, 49.99),
                                                      ('Product 10', 120, 7.99);

INSERT INTO "Orders" ("TotalPrice", "OrderDate") VALUES
                                                     (59.97, now()),
                                                     (39.98, now()),
                                                     (19.99, now()),
                                                     (99.95, now()),
                                                     (49.95, now()),
                                                     (29.97, now()),
                                                     (89.99, now()),
                                                     (79.95, now()),
                                                     (69.95, now()),
                                                     (109.99, now());

INSERT INTO "OrderProducts" ("OrderId", "ProductId")
SELECT o."Id", p."Id"
FROM "Orders" o, "Products" p
WHERE o."Id" IN (
    SELECT "Id" FROM "Orders" LIMIT 3
    )
  AND p."Id" IN (
SELECT "Id" FROM "Products" LIMIT 3
    );
