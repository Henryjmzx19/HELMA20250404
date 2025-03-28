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
