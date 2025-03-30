CREATE DATABASE SistemaCalificaciones;
GO

USE SistemaCalificaciones;
GO

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(25) NOT NULL,
    Email VARCHAR(30) UNIQUE NOT NULL,
    Password VARCHAR(15) NOT NULL,
    Rol VARCHAR(50) CHECK (Rol IN ('Administrador', 'Profesor', 'Alumno')) NOT NULL
);

-- Tabla de Profesores
CREATE TABLE Profesores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT UNIQUE FOREIGN KEY REFERENCES Usuarios(Id),
    Apellido VARCHAR(25) NOT NULL,
    Telefono VARCHAR(9),
    Dui CHAR(10),
    Direccion VARCHAR(25),
    FechaNacimiento DATE NOT NULL
);

-- Tabla de Alumnos
CREATE TABLE Alumnos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT UNIQUE FOREIGN KEY REFERENCES Usuarios(Id),
    Apellido VARCHAR(25) NOT NULL,
	NIE Char(9) NOT NULL,
    Telefono VARCHAR(9),
    Direccion VARCHAR(25),
    Encargado VARCHAR(50),
    Imagen VARBINARY(MAX), -- Campo para almacenar la imagen en formato byte
    FechaNacimiento DATE NOT NULL
);

-- Tabla de Matrícula
CREATE TABLE Matricula (
    IdMatricula INT IDENTITY(1,1) PRIMARY KEY,
    IdAlumno INT FOREIGN KEY REFERENCES Alumnos(Id),
    IdProfesor INT FOREIGN KEY REFERENCES Profesores(Id),
    YearLectivo INT NOT NULL
);

-- Tabla de Aulas
CREATE TABLE Aulas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(15) NOT NULL
);

-- Tabla de Materias
CREATE TABLE Materias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(25) NOT NULL
);

-- Tabla de Notas
CREATE TABLE Notas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdMatricula INT FOREIGN KEY REFERENCES Matricula(IdMatricula),
    IdAula INT FOREIGN KEY REFERENCES Aulas(Id),
    IdMateria INT FOREIGN KEY REFERENCES Materias(Id),
    Trimestre1 DECIMAL(5,2) CHECK (Trimestre1 BETWEEN 0 AND 10) NOT NULL,
    Trimestre2 DECIMAL(5,2) CHECK (Trimestre2 BETWEEN 0 AND 10) NOT NULL,
    Trimestre3 DECIMAL(5,2) CHECK (Trimestre3 BETWEEN 0 AND 10) NOT NULL,
    Promedio AS ((Trimestre1 + Trimestre2 + Trimestre3) / 3) PERSISTED,
    Estado AS (CASE WHEN ((Trimestre1 + Trimestre2 + Trimestre3) / 3) >= 6 THEN 'APROBADO' ELSE 'REPROBADO' END) PERSISTED
);

-- Insertar usuarios con rol de Administrador
INSERT INTO Usuarios (NombreUsuario, Email, Password, Rol) VALUES
('Ana', 'ana@gmail.com', 'ana123', 'Administrador'),
('Carlos', 'carlos@gmail.com', 'carlos456', 'Administrador');

-- Insertar usuario con rol de Profesor
INSERT INTO Usuarios (NombreUsuario, Email, Password, Rol) VALUES
('Sofía', 'sofia@gmail.com', 'sofia789', 'Profesor');

-- Insertar usuarios con rol de Alumno
INSERT INTO Usuarios (NombreUsuario, Email, Password, Rol) VALUES
('Mateo', 'mateo@gmail.com', 'mateo123', 'Alumno'),
('Valentina', 'valentina@gmail.com', 'valentina456', 'Alumno'),
('Sebastián', 'sebastian@gmail.com', 'sebastian789', 'Alumno'),
('Isabella', 'isabella@gmail.com', 'isabellaabc', 'Alumno'),
('Gabriel', 'gabriel@gmail.com', 'gabrieldef', 'Alumno'),
('Camila', 'camila@gmail.com', 'camilaghi', 'Alumno'),
('Andrés', 'andres@gmail.com', 'andresjkl', 'Alumno'),
('Martina', 'martina@gmail.com', 'martinamno', 'Alumno'),
('Nicolás', 'nicolas@gmail.com', 'nicolaspqr', 'Alumno'),
('Daniela', 'daniela@gmail.com', 'danielastu', 'Alumno'),
('Samuel', 'samuel@gmail.com', 'samuelvwx', 'Alumno'),
('Renata', 'renata@gmail.com', 'renatayz1', 'Alumno'),
('Joaquín', 'joaquin@gmail.com', 'joaquin234', 'Alumno'),
('Antonia', 'antonia@gmail.com', 'antonia567', 'Alumno'),
('Benjamín', 'benjamin@gmail.com', 'benjamin890', 'Alumno'),
('Emilia', 'emilia@gmail.com', 'emiliaqwe', 'Alumno'),
('Diego', 'diego@gmail.com', 'diegoasd', 'Alumno'),
('Paula', 'paula@gmail.com', 'paulazxc', 'Alumno'),
('Vicente', 'vicente@gmail.com', 'vicentefgh', 'Alumno'),
('Florencia', 'florencia@gmail.com', 'florenciavbn', 'Alumno'),
('Matías', 'matias@gmail.com', 'matiasmlk', 'Alumno'),
('Agustina', 'agustina@gmail.com', 'agustinajhy', 'Alumno'),
('Tomás', 'tomas@gmail.com', 'tomasoutr', 'Alumno'),
('Catalina', 'catalina@gmail.com', 'catalinaieo', 'Alumno'),
('Maximiliano', 'maximiliano@gmail.com', 'maximilianopiu', 'Alumno'),
('Josefa', 'josefa@gmail.com', 'josefalak', 'Alumno'),
('Ignacio', 'ignacio@gmail.com', 'ignaciomsn', 'Alumno'),
('Amanda', 'amanda@gmail.com', 'amandabvc', 'Alumno'),
('Cristóbal', 'cristobal@gmail.com', 'cristobalaqw', 'Alumno'),
('Julieta', 'julieta@gmail.com', 'julietased', 'Alumno'),
('Joaquín', 'joaquin2@gmail.com', 'joaquinrfv', 'Alumno'),
('Scarlett', 'scarlett@gmail.com', 'scarlettgb', 'Alumno'),
('Martín', 'martin@gmail.com', 'martinyhn', 'Alumno'),
('Constanza', 'constanza@gmail.com', 'constanzaujm', 'Alumno'),
('Lucas', 'lucas@gmail.com', 'lucasikol', 'Alumno'),
('Emilia', 'emilia2@gmail.com', 'emiliapla', 'Alumno'),
('Thiago', 'thiago@gmail.com', 'thiagozxcv', 'Alumno'),
('Renata', 'renata2@gmail.com', 'renataaqws', 'Alumno'),
('Benicio', 'benicio@gmail.com', 'benicioedfr', 'Alumno'),
('Antonella', 'antonella@gmail.com', 'antonellatgby', 'Alumno');