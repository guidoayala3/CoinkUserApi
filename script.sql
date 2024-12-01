DROP FUNCTION IF EXISTS sp_insert_user(VARCHAR, VARCHAR, TEXT, VARCHAR, VARCHAR, VARCHAR) CASCADE;
DROP VIEW IF EXISTS user_view CASCADE;
DROP TABLE IF EXISTS user_data, municipality, department, country CASCADE;

-- Crear tablas
CREATE TABLE country (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE department (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    country_id INT NOT NULL REFERENCES country(id) ON DELETE CASCADE,
    UNIQUE (name, country_id)
);

CREATE TABLE municipality (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    department_id INT NOT NULL REFERENCES department(id) ON DELETE CASCADE,
    UNIQUE (name, department_id)
);

CREATE TABLE user_data (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    phone VARCHAR(15) NOT NULL,
    address TEXT NOT NULL,
    country_id INT NOT NULL REFERENCES country(id) ON DELETE CASCADE,
    department_id INT NOT NULL REFERENCES department(id) ON DELETE CASCADE,
    municipality_id INT NOT NULL REFERENCES municipality(id) ON DELETE CASCADE,
    UNIQUE (name, phone, address) 
);

-- Crear índices para mejorar consultas comunes
CREATE INDEX IF NOT EXISTS idx_department_country_id ON department(country_id);
CREATE INDEX IF NOT EXISTS idx_municipality_department_id ON municipality(department_id);
CREATE INDEX IF NOT EXISTS idx_user_municipality_id ON user_data(municipality_id);

-- Crear vista
CREATE OR REPLACE VIEW user_view AS
SELECT 
    u.id AS user_id, 
    u.name AS user_name, 
    u.phone AS user_phone, 
    u.address AS user_address,
    m.name AS municipality_name,
    d.name AS department_name,
    c.name AS country_name
FROM 
    user_data u
JOIN municipality m 
    ON u.municipality_id = m.id
JOIN department d 
    ON m.department_id = d.id
JOIN country c 
    ON d.country_id = c.id;

-- Crear función
CREATE OR REPLACE FUNCTION sp_insert_user(
    p_name VARCHAR,
    p_phone VARCHAR,
    p_address TEXT,
    p_country_name VARCHAR,
    p_department_name VARCHAR,
    p_municipality_name VARCHAR
) 
RETURNS INT AS
'
DECLARE
    country_id INT;
    department_id INT;
    municipality_id INT;
    inserted_id INT;
BEGIN
    -- Insertar o obtener el ID del país
    SELECT id INTO country_id
    FROM country
    WHERE name = p_country_name
    LIMIT 1;

    IF NOT FOUND THEN
        -- Si no existe el país, se inserta
        INSERT INTO country(name) 
        VALUES (p_country_name)
        RETURNING id INTO country_id;
    END IF;

    -- Insertar o obtener el ID del departamento
    SELECT id INTO department_id
    FROM department
    WHERE name = p_department_name AND country_id = country_id
    LIMIT 1;

    IF NOT FOUND THEN
        -- Si no existe el departamento, se inserta
        INSERT INTO department(name, country_id) 
        VALUES (p_department_name, country_id)
        RETURNING id INTO department_id;
    END IF;

    -- Insertar o obtener el ID del municipio
    SELECT id INTO municipality_id
    FROM municipality
    WHERE name = p_municipality_name AND department_id = department_id
    LIMIT 1;

    IF NOT FOUND THEN
        -- Si no existe el municipio, se inserta
        INSERT INTO municipality(name, department_id) 
        VALUES (p_municipality_name, department_id)
        RETURNING id INTO municipality_id;
    END IF;

    -- Insertar el usuario en la tabla user_data
    INSERT INTO user_data(name, phone, address, country_id, department_id, municipality_id)
    VALUES (p_name, p_phone, p_address, country_id, department_id, municipality_id)
    RETURNING id INTO inserted_id;

    -- Devolver el ID del usuario insertado
    RETURN inserted_id;
END;
'
LANGUAGE plpgsql;
