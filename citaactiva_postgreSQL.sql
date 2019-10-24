
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
	homo_clave character varying(10) NOT NULL,
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
	UNIQUE (id_marcavehiculo)
);
CREATE TABLE tipos_vehiculo(
	id_tipovehiculo BIGSERIAL NOT NULL,
	codigo VARCHAR(50) NOT NULL,
	descripcion VARCHAR(100),
	UNIQUE (id_tipovehiculo)
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
CREATE TABLE vehiculos_clientes
(
    idvehiculo_cliente BIGSERIAL NOT NULL,
    id_cliente INT NOT NULL,
    id_familiavehiculo INT NOT NULL,
    kilometraje INT NOT NULL,
    anhio INT NOT NULL,
    placa VARCHAR(100),
    vin  VARCHAR(100),
    PRIMARY KEY (idvehiculo_cliente),
    FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente), 
	FOREIGN KEY (id_familiavehiculo) REFERENCES familias_vehiculo(id_familiavehiculo)
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

CREATE TABLE kits_clientes(
	idkit_cliente BIGSERIAL NOT NULL,
	id_kit INT NOT NULL,
	id_cliente INT NOT NULL,
	idvehiculo_cliente INT NOT NULL,
	PRIMARY KEY (idkit_cliente),
	FOREIGN KEY (id_kit) REFERENCES kits (id_kit),
	FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente),
	FOREIGN KEY (idvehiculo_cliente) REFERENCES vehiculos_clientes (idvehiculo_cliente)
);

CREATE TABLE citas
(
    id_cita BIGSERIAL NOT NULL,
    id_appointment character varying(10) NOT NULL,
    idkit_cliente integer NOT NULL,
    id_cliente integer NOT NULL,
    status_cita integer NOT NULL,
    fecha_registro timestamp DEFAULT CURRENT_TIMESTAMP,
    fecha_actualizacion timestamp DEFAULT CURRENT_TIMESTAMP,
    fecha_cancelacion timestamp DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id_cita),
    FOREIGN KEY (id_cliente) REFERENCES clientes (id_cliente),
	FOREIGN KEY (idkit_cliente) REFERENCES kits_clientes (idkit_cliente)
);
	
CREATE TABLE agendamiento_citas
(
    idagendamiento_citas BIGSERIAL NOT NULL,
    id_cita integer NOT NULL,
    id_agencia integer,
    planned_date character varying(100),
    planned_time character varying(100),
    PRIMARY KEY (idagendamiento_citas),
    FOREIGN KEY (id_cita) REFERENCES citas (id_cita),
	FOREIGN KEY (id_agencia) REFERENCES agencias(id_agencia)
);

CREATE TABLE Servicios(
	id_servicio INT NOT NULL,
	descripcion VARCHAR(50) NOT NULL,
	PRIMARY KEY (id_servicio)
);

ALTER TABLE Agencias ADD active_ind int DEFAULT 0;