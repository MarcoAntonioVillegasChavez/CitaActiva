
CREATE TABLE token
(
    id BIGSERIAL NOT NULL,
    token_type character varying(100),
    access_token character varying(100),
    expires_in character varying(100),
    created_at timestamp,
    PRIMARY KEY (id)
);

CREATE TABLE clientes
(
    id_cliente BIGSERIAL NOT NULL ,
    cuenta_personal character varying(100) NOT NULL,
    nombre_cliente character varying(100) NOT NULL,
    apellido_paterno character varying(100) NOT NULL,
    apellido_materno character varying(100) NOT NULL,
    fecha_nacimiento timestamp NOT NULL,
    email_cliente character varying(100) NOT NULL,
    telefono character varying(100) NOT NULL,
    password character varying(100) NOT NULL,
    rfc character varying(50) NOT NULL,
	homo_clave character varying(10),
    fecha_registro TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
	cliente_activo integer DEFAULT 0,
    PRIMARY KEY (id_cliente),
    UNIQUE (cuenta_personal),
    UNIQUE (email_cliente)
);

CREATE TABLE marcas_vehiculo(
	id_marcavehiculo BIGSERIAL NOT NULL,
	codigo VARCHAR(50) NOT NULL,
	descripcion VARCHAR(100), 
	PRIMARY KEY (id_marcavehiculo)
);
CREATE TABLE tipos_vehiculo(
	id_tipovehiculo BIGSERIAL NOT NULL,
	codigo VARCHAR(50) NOT NULL,
	descripcion VARCHAR(100),
	PRIMARY KEY (id_tipovehiculo)
);
CREATE TABLE familias_vehiculo(
	id_familiavehiculo BIGSERIAL NOT NULL,
	codigo VARCHAR(20),
	descripcion VARCHAR(100),
	id_marcavehiculo INT,
	id_tipovehiculo INT,
	PRIMARY KEY (id_familiavehiculo),
	FOREIGN KEY (id_marcavehiculo) REFERENCES marcas_vehiculo(id_marcavehiculo),
	FOREIGN KEY (id_tipovehiculo) REFERENCES tipos_vehiculo(id_tipovehiculo)	
);
CREATE TABLE modelos_vehiculo(
	id_modelovehiculo BIGSERIAL NOT NULL,
	codigo VARCHAR(50) NOT NULL,
	descripcion VARCHAR(100),
	id_familiavehiculo INT NOT NULL, 
	UNIQUE (id_modelovehiculo),
	FOREIGN KEY (id_familiavehiculo) REFERENCES familias_vehiculo(id_familiavehiculo)
);

CREATE TABLE tipo_combustible(
	id_tipocombustible INT NOT NULL,
	descripcion VARCHAR(100),
	PRIMARY KEY (id_tipocombustible)
);

CREATE TABLE vehiculos_clientes
(
    idvehiculo_cliente BIGSERIAL NOT NULL,
    id_cliente INT NOT NULL,
    id_familiavehiculo INT NOT NULL,
    kilometraje INT NOT NULL,
    anhio INT NOT NULL,
    placa VARCHAR(100),
    vin  VARCHAR(100),
	id_tipocombustible INT NOT NULL,
    PRIMARY KEY (idvehiculo_cliente),
    FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente), 
	FOREIGN KEY (id_familiavehiculo) REFERENCES familias_vehiculo(id_familiavehiculo),
	FOREIGN KEY (id_tipocombustible) REFERENCES tipo_combustible (id_tipocombustible)
);

CREATE TABLE zonas(
	id_zona INT NOT NULL,
	nombre_zona VARCHAR(100),
	PRIMARY KEY (id_zona)
);
CREATE TABLE agencias(
	id_agencia INT NOT NULL,
	id_zona INT NOT NULL,
	nombre_agencia VARCHAR(100),
	active_ind INT NOT NULL,
	place_id VARCHAR(100) NOT NULL,
	PRIMARY KEY (id_agencia),
	FOREIGN KEY (id_zona) REFERENCES zonas (id_zona)
);
CREATE TABLE kits(
	id_kit INT NOT NULL,
	codigo VARCHAR(20) NOT NULL,
	descripcion VARCHAR(100),
	id_zona INT NOT NULL,
	PRIMARY KEY (id_kit),
	UNIQUE (codigo),
	FOREIGN KEY (id_zona) REFERENCES zonas (id_zona)
);

CREATE TABLE articulos(
	id_articulo BIGSERIAL NOT NULL,
	codigo VARCHAR(50) NOT NULL,
	descripcion VARCHAR(100),
	precio DECIMAL NOT NULL,
	PRIMARY KEY (id_articulo)
);

CREATE TABLE kits_articulos(
	id_kit INT NOT NULL,
	id_articulo INT NOT NULL,
	precio DECIMAL NOT NULL,
	descuento DECIMAL NOT NULL,
	PRIMARY KEY (id_kit, id_articulo)
);
CREATE TABLE mo (
	id_mo BIGSERIAL NOT NULL,
	codigo VARCHAR(20) NOT NULL,
	descripcion VARCHAR(100),
	PRIMARY KEY (id_mo)
);
CREATE TABLE kits_mo(
	id_kit INT NOT NULL,
	id_mo INT NOT NULL,
	precio DECIMAL NOT NULL,
	decuento DECIMAL NOT NULL, 
	PRIMARY KEY (id_kit, id_mo)
);

CREATE TABLE servicios(
	id_servicio INT NOT NULL,
	kilometraje VARCHAR(100) NOT NULL,
	anho INT NOT NULL,
	id_familiavehiculo INT NOT NULL,
	precio DECIMAL NOT NULL,
	id_tipocombustible INT NOT NULL,
	PRIMARY KEY (id_servicio),
	FOREIGN KEY (id_familiavehiculo) REFERENCES familias_vehiculo (id_familiavehiculo),
	FOREIGN KEY (id_tipocombustible) REFERENCES tipo_combustible (id_tipocombustible)
);

CREATE TABLE kits_clientes(
	idkit_cliente BIGSERIAL NOT NULL,
	id_kit INT NOT NULL,
	id_cliente INT NOT NULL,
	idvehiculo_cliente INT NOT NULL,
	PRIMARY KEY (idkit_cliente),
	FOREIGN KEY (id_kit) REFERENCES kits (id_kit),
	FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente)
);

CREATE TABLE citas
(
    id_cita BIGSERIAL NOT NULL,
    id_appointment VARCHAR(10) NOT NULL,
    idkit_cliente integer NOT NULL,
    status_cita integer NOT NULL,
    fecha_registro timestamp DEFAULT CURRENT_TIMESTAMP,
    fecha_actualizacion timestamp DEFAULT CURRENT_TIMESTAMP,
    fecha_cancelacion timestamp DEFAULT CURRENT_TIMESTAMP,
	id_servicio INT NOT NULL,
    PRIMARY KEY (id_cita),
	FOREIGN KEY (idkit_cliente) REFERENCES kits_clientes (idkit_cliente),
	FOREIGN KEY (id_servicio) REFERENCES servicios (id_servicio)
);

create table Invitados(
	id_invitado bigserial NOT NULL,
	nombre_cliente varchar(100),
	apellido_paterno varchar(100),
	apellido_materno varchar(100),
	email_cliente varchar(100),
	telefono varchar(100),
	rfc varchar(100),

	PRIMARY KEY (id_invitado)
);

CREATE TABLE agendamiento_citas
(
    idagendamiento_citas BIGSERIAL NOT NULL,
    id_cita INT NOT NULL,
    id_agencia INT NOT NULL,
    planned_date VARCHAR(100),
    planned_time VARCHAR(100),
	active_ind INT NOT NULL,
	id_invitado INT, 
    PRIMARY KEY (idagendamiento_citas),
    FOREIGN KEY (id_cita) REFERENCES citas (id_cita),
	FOREIGN KEY (id_agencia) REFERENCES agencias(id_agencia),
	FOREIGN KEY (id_invitado) REFERENCES Invitados (id_invitado)
);

CREATE TABLE promociones(
	id_promocion INT NOT NULL,
	descripcion VARCHAR(100) NOT NULL,
	fecha_inicio timestamp NOT NULL,
	fecha_vigencia timestamp NOT NULL,
	PRIMARY KEY (id_promocion)
);
CREATE TABLE descuentos(
	id_descuento INT NOT NULL,
	descripcion VARCHAR(100) NOT NULL,
	PorcentajeDescuento DECIMAL,
	fecha_inicio timestamp NOT NULL,
	fecha_vigencia timestamp NOT NULL,
	PRIMARY KEY (id_descuento)
);

CREATE TABLE kits_clientes_promociones(
	idkit_cliente INT NOT NULL,
	id_promocion INT NOT NULL,
	PRIMARY KEY (idkit_cliente, id_promocion)
);

CREATE TABLE kits_clientes_descuentos(
	idkit_cliente INT NOT NULL,
	id_descuento INT NOT NULL,
	PRIMARY KEY (idkit_cliente, id_descuento)
);



INSERT INTO clientes
(cuenta_personal, nombre_cliente, apellido_paterno, apellido_materno, fecha_nacimiento, email_cliente, password, rfc, homo_clave, telefono, cliente_activo)
VALUES ('Inv-001', 'Invitado', 'Invitado', 'Invitado', now(),  'citaactiva@autocom.mx', '', '', '', '', 1);

INSERT INTO invitados(
	 nombre_cliente, apellido_paterno, apellido_materno, email_cliente, telefono, rfc)
	VALUES ( 'Cliente Registrado', 'Cliente Registrado', 'Cliente Registrado', 'citactiva@autocom.mx', '5555555', 'CR');

INSERT INTO marcas_vehiculo(id_marcavehiculo, codigo, descripcion) VALUES (1, 'NI', 'NISSAN');
INSERT INTO marcas_vehiculo(id_marcavehiculo, codigo, descripcion) VALUES (2, 'IF', 'INFINITI');

INSERT INTO zonas(id_zona, nombre_zona) values (1, 'ACM830827CW6');
INSERT INTO zonas(id_zona, nombre_zona) values (2, 'ACQ980113CL5');
INSERT INTO zonas(id_zona, nombre_zona) values (3, 'AME110623A956');
INSERT INTO zonas(id_zona, nombre_zona) values (17, 'ARI181123KT3');
INSERT INTO zonas(id_zona, nombre_zona) values (18, 'ASE190412AIA');


INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (1, 1, 'AUTOCOM MADERO', 1, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (2, 1, 'AUTOCOM URIANGATO', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (3, 1, 'AUTOCOM PATZCUARO', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (4, 1, 'AUTOCOM TACAMBARO', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (5, 1, 'AUTOCOM CHAPULTEPEC', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (6, 1, 'AUTOCOM ACAMBARO', 0, '');

INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (7, 1, 'AUTOCOM ZITACUARO', 0, '');

INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (10, 1, 'AUTOCOM HUETAMO', 0, '');

INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (11, 1, 'AUTOCOM URUAPAN', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (12, 1, 'AUTOCOM LOS REYES', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (13, 1, 'AUTOCOM ZAMORA', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (14, 1, 'AUTOCOM SAHUAYO', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (15, 2, 'AUTOCOM CONSTITUYENTES', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (16, 2, 'AUTOCOM CRAA', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (17, 2, 'AUTOCOM ZARAGOZA', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (18, 2, 'AUTOCOM BERNARDO QUINTANA', 0, '');

INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (20, 2, 'AUTOCOM SAN JUAN DEL RIO', 0, '');

INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (21, 2, 'AUTOCOM CELAYA', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (23, 3, 'AUTOCOM POLANCO', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (24, 3, 'AUTOCOM SATELITE', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (25, 3, 'AUTOCOM QUERETARO', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (29, 17, 'AUTOCOM RIDERS PUEBLA', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (36, 18, 'SEMINUEVOS QUERETARO NTE', 0, '');

INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (37, 18, 'SEMINUEVOS MORELIA PTE', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (38, 18, 'SEMINUEVOS QUERETARO SUR', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (39, 18, 'SEMINUEVOS CELAYA', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (40, 18, 'SEMINUEVOS PUEBLA', 0, '');
	
INSERT INTO agencias(
	id_agencia, id_zona, nombre_agencia, active_ind, place_id)
	VALUES (41, 18, 'SEMINUEVOS SATELITE', 0, '');
	

INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (1,'LUXURY I', 'SERVICIO ADICIONAL LUXURY', 1);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (2, 'LUXURY II', 'SERVICIO ADICIONAL LUXURY', 2);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (3, 'LUXURY III', 'SERVICIO ADICIONAL LUXURY', 3);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (4, 'PREMIUM I', 'SERVICIO ADICIONAL PREMIUM', 1);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (5, 'PREMIUM II', 'SERVICIO ADICIONAL PREMIUM', 2);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (6, 'PREMIUM III', 'SERVICIO ADICIONAL PREMIUM', 3);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (7, 'SMART I', 'SERVICIO ADICIONAL SMART', 1);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (8, 'SMART II', 'SERVICIO ADICIONAL SMART', 2);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (9, 'SMART III', 'SERVICIO ADICIONAL SMART', 3);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (10, 'COMFORT I', 'SERVICIO ADICIONAL COMFORT', 1);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (11, 'COMFORT II', 'SERVICIO ADICIONAL COMFORT', 2);
INSERT INTO kits (id_kit, codigo, descripcion, id_zona) VALUES (12, 'COMFORT III', 'SERVICIO ADICIONAL COMFORT', 3);


ALTER TABLE Kits_articulos
	ADD FOREIGN KEY (id_kit) REFERENCES Kits(id_kit);
	
ALTER TABLE Kits_articulos
	ADD FOREIGN KEY (id_articulo) REFERENCES Articulos(id_articulo);

ALTER TABLE Kits_mo
	ADD FOREIGN KEY (id_kit) REFERENCES Kits(id_kit);
	
ALTER TABLE Kits_mo
	ADD FOREIGN KEY (id_mo) REFERENCES Mo(id_mo);

ALTER TABLE kits_clientes_promociones
	ADD FOREIGN KEY (idkit_cliente) REFERENCES Kits_clientes(idkit_cliente);
	
ALTER TABLE kits_clientes_promociones
	ADD FOREIGN KEY (id_promocion) REFERENCES promociones(id_promocion);
	
ALTER TABLE kits_clientes_descuentos
	ADD FOREIGN KEY (idkit_cliente) REFERENCES Kits_clientes(idkit_cliente);
	
ALTER TABLE kits_clientes_descuentos
	ADD FOREIGN KEY (id_descuento) REFERENCES descuentos(id_descuento);

-- Carga de servicios de transparencia

INSERT INTO servicios(
	id_servicio, kilometraje, anho, id_familiavehiculo, precio, id_tipocombustible)
	VALUES (1, '10000 km', 2019, 1, 1315, 1);
	
INSERT INTO servicios(
	id_servicio, kilometraje, anho, id_familiavehiculo, precio, id_tipocombustible)
	VALUES (2, '20000 km', 2019, 1, 1805, 1);
	
INSERT INTO servicios(
	id_servicio, kilometraje, anho, id_familiavehiculo, precio, id_tipocombustible)
	VALUES (3, '30000 km', 2019, 1, 1315, 1);
	
INSERT INTO servicios(
	id_servicio, kilometraje, anho, id_familiavehiculo, precio, id_tipocombustible)
	VALUES (4, '40000 km', 2019, 1, 2510, 1);
	
INSERT INTO servicios(
	id_servicio, kilometraje, anho, id_familiavehiculo, precio, id_tipocombustible)
	VALUES (5, '50000 km', 2019, 1, 1315, 1);
	
INSERT INTO servicios(
	id_servicio, kilometraje, anho, id_familiavehiculo, precio, id_tipocombustible)
	VALUES (6, '60000 km', 2019, 1, 1805, 1);

--Actualización de place id por agencia.

UPDATE agencias 
	SET place_id = 'ChIJHaGfvOINLYQRPC1qc8V2jKI'
WHERE id_agencia = 1;
UPDATE agencias 
	SET place_id = 'ChIJHaGfvOINLYQRfkw4jcorc3g'
WHERE id_agencia = 5;
UPDATE agencias 
	SET place_id = 'ChIJT5QnPl7iLYQRr3P0AgEKWLQ'
WHERE id_agencia = 11;
UPDATE agencias 
	SET place_id = 'ChIJR9rWYFRcwoURXL4Aw6gS09M'
WHERE id_agencia = 13;
UPDATE agencias 
	SET place_id = 'ChIJQUa9nLRE04UR4g2E-7bsI-c'
WHERE id_agencia = 15;
UPDATE agencias 
	SET place_id = 'ChIJlbHPx5Ja04URcEXCB4QjouM'
WHERE id_agencia = 18;
UPDATE agencias 
	SET place_id = 'ChIJMzLn9C1F04URnTZ9x7VfVNw'
WHERE id_agencia = 17;
UPDATE agencias 
	SET place_id = 'ChIJlTxudGcL04URPSjA2E8I-EU'
WHERE id_agencia = 20;
UPDATE agencias 
	SET place_id = 'ChIJFavsAYuwLIQR1ZY1J9Cjudo'
WHERE id_agencia = 21;
UPDATE agencias 
	SET place_id = 'ChIJhc_B7i0d0oURBztOA-uqgYk'
WHERE id_agencia = 24;
UPDATE agencias 
	SET place_id = 'ChIJrwpZzAEC0oUR70qKVkiQPTs'
WHERE id_agencia = 23;
UPDATE agencias 
	SET place_id = 'ChIJKyQTojFX04URU8m3pY0sXMw'
WHERE id_agencia = 25;