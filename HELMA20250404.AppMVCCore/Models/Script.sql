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

-- Tabla de Matr�cula
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
('Sof�a', 'sofia@gmail.com', 'sofia789', 'Profesor');

-- Insertar usuarios con rol de Alumno
INSERT INTO Usuarios (NombreUsuario, Email, Password, Rol) VALUES
('Mateo', 'mateo@gmail.com', 'mateo123', 'Alumno'),
('Valentina', 'valentina@gmail.com', 'valentina456', 'Alumno'),
('Sebasti�n', 'sebastian@gmail.com', 'sebastian789', 'Alumno'),
('Isabella', 'isabella@gmail.com', 'isabellaabc', 'Alumno'),
('Gabriel', 'gabriel@gmail.com', 'gabrieldef', 'Alumno'),
('Camila', 'camila@gmail.com', 'camilaghi', 'Alumno'),
('Andr�s', 'andres@gmail.com', 'andresjkl', 'Alumno'),
('Martina', 'martina@gmail.com', 'martinamno', 'Alumno'),
('Nicol�s', 'nicolas@gmail.com', 'nicolaspqr', 'Alumno'),
('Daniela', 'daniela@gmail.com', 'danielastu', 'Alumno'),
('Samuel', 'samuel@gmail.com', 'samuelvwx', 'Alumno'),
('Renata', 'renata@gmail.com', 'renatayz1', 'Alumno'),
('Joaqu�n', 'joaquin@gmail.com', 'joaquin234', 'Alumno'),
('Antonia', 'antonia@gmail.com', 'antonia567', 'Alumno'),
('Benjam�n', 'benjamin@gmail.com', 'benjamin890', 'Alumno'),
('Emilia', 'emilia@gmail.com', 'emiliaqwe', 'Alumno'),
('Diego', 'diego@gmail.com', 'diegoasd', 'Alumno'),
('Paula', 'paula@gmail.com', 'paulazxc', 'Alumno'),
('Vicente', 'vicente@gmail.com', 'vicentefgh', 'Alumno'),
('Florencia', 'florencia@gmail.com', 'florenciavbn', 'Alumno'),
('Mat�as', 'matias@gmail.com', 'matiasmlk', 'Alumno'),
('Agustina', 'agustina@gmail.com', 'agustinajhy', 'Alumno'),
('Tom�s', 'tomas@gmail.com', 'tomasoutr', 'Alumno'),
('Catalina', 'catalina@gmail.com', 'catalinaieo', 'Alumno'),
('Maximiliano', 'maximiliano@gmail.com', 'maximilianopiu', 'Alumno'),
('Josefa', 'josefa@gmail.com', 'josefalak', 'Alumno'),
('Ignacio', 'ignacio@gmail.com', 'ignaciomsn', 'Alumno'),
('Amanda', 'amanda@gmail.com', 'amandabvc', 'Alumno'),
('Crist�bal', 'cristobal@gmail.com', 'cristobalaqw', 'Alumno'),
('Julieta', 'julieta@gmail.com', 'julietased', 'Alumno'),
('Joaqu�n', 'joaquin2@gmail.com', 'joaquinrfv', 'Alumno'),
('Scarlett', 'scarlett@gmail.com', 'scarlettgb', 'Alumno'),
('Mart�n', 'martin@gmail.com', 'martinyhn', 'Alumno'),
('Constanza', 'constanza@gmail.com', 'constanzaujm', 'Alumno'),
('Lucas', 'lucas@gmail.com', 'lucasikol', 'Alumno'),
('Emilia', 'emilia2@gmail.com', 'emiliapla', 'Alumno'),
('Thiago', 'thiago@gmail.com', 'thiagozxcv', 'Alumno'),
('Renata', 'renata2@gmail.com', 'renataaqws', 'Alumno'),
('Benicio', 'benicio@gmail.com', 'benicioedfr', 'Alumno'),
('Antonella', 'antonella@gmail.com', 'antonellatgby', 'Alumno');

-- Insertar datos en la tabla Alumnos utilizando los IdUsuario de los alumnos existentes
-- Sabiendo que los Id de los alumnos en Usuarios empiezan desde 4

INSERT INTO Alumnos (IdUsuario, Apellido, NIE, Telefono, Direccion, Encargado, YearNacimiento) VALUES
(4, 'Hern�ndez', 'NIE000001', '7890-1234', 'Centro Sonsonate', 'Mar�a Hern�ndez P�rez', '2017-03-10'),
(5, 'Ch�vez', 'NIE000002', '6012-3456', 'El �ngel Sonsonate', 'Jos� Ch�vez L�pez', '2017-04-15'),
(6, 'Reyes', 'NIE000003', '7741-5896', 'El Sauce Sonsonate', 'Sof�a Reyes Castro', '2017-05-20'),
(7, 'Vega', 'NIE000004', '6987-1203', 'Sensunap�n Sonsonate', 'Ricardo Vega Morales', '2017-06-25'),
(8, 'Castillo', 'NIE000005', '7530-9874', 'San Antonio Sonsonate', 'Elena Castillo Ruiz', '2017-07-30'),
(9, 'Mendoza', 'NIE000006', '6321-4587', 'Metal�o Sonsonate', 'Javier Mendoza Torres', '2017-08-04'),
(10, 'Ortiz', 'NIE000007', '7963-0148', 'Las Delicias Sonsonate', 'Carmen Ortiz S�nchez', '2017-09-09'),
(11, 'Navarro', 'NIE000008', '6754-8219', 'El Calvario Sonsonate', 'Luis Navarro D�az', '2017-10-14'),
(12, 'Flores', 'NIE000009', '7123-5678', 'Izalco Sonsonate', 'Ana Flores �lvarez', '2017-11-19'),
(13, 'Morales', 'NIE000010', '6485-9201', 'El Progreso Sonsonate', 'Pedro Morales Ramos', '2017-12-24'),
(14, 'Romero', 'NIE000011', '7852-3690', 'La Cruz Sonsonate', 'Sandra Romero Vargas', '2017-01-29'),
(15, 'Garc�a', 'NIE000012', '6214-7935', 'Cuisnahuat Sonsonate', 'Manuel Garc�a Castro', '2017-02-03'),
(16, 'Rivas', 'NIE000013', '7589-0326', 'San Rafael Sonsonate', 'Rosa Rivas G�mez', '2017-03-08'),
(17, 'Santos', 'NIE000014', '6925-4781', 'San Isidro Sonsonate', 'Roberto Santos N��ez', '2017-04-13'),
(18, 'Aguilar', 'NIE000015', '7368-1592', 'San Juan Sonsonate', 'Marta Aguilar Silva', '2017-05-18'),
(19, 'Jim�nez', 'NIE000016', '6107-9354', 'Sta Luc�a Sonsonate', 'Fernando Jim�nez Romero', '2017-06-23'),
(20, 'Ruiz', 'NIE000017', '7640-2865', 'El Centro Sonsonate', 'Isabel Ruiz Ch�vez', '2017-07-28'),
(21, 'P�rez', 'NIE000018', '6873-1490', 'San Andr�s Sonsonate', 'Carlos P�rez Reyes', '2017-08-02'),
(22, 'L�pez', 'NIE000019', '7216-5983', 'La Paz Sonsonate', 'Elena L�pez Vega', '2017-09-07'),
(23, 'Ram�rez', 'NIE000020', '6549-0217', 'El Progreso Sonsonate', 'Andr�s Ram�rez Castillo', '2017-10-12'),
(24, 'Torres', 'NIE000021', '7982-3746', 'Sonsonate Centro', 'Sof�a Torres Mendoza', '2017-11-17'),
(25, 'S�nchez', 'NIE000022', '6315-8079', 'El Milagro Sonsonate', 'Ricardo S�nchez Ortiz', '2017-12-22'),
(26, 'D�az', 'NIE000023', '7758-1304', 'San Rafael Sonsonate', 'Mar�a D�az Navarro', '2017-01-27'),
(27, '�lvarez', 'NIE000024', '6194-2631', 'San Antonio M Sonsonate', 'Jos� �lvarez Flores', '2017-02-01'),
(28, 'Ramos', 'NIE000025', '7427-5968', 'La Uni�n Sonsonate', 'Carmen Ramos Morales', '2017-03-06'),
(29, 'Vargas', 'NIE000026', '6860-9125', 'Las Flores Sonsonate', 'Luis Vargas Romero', '2017-04-11'),
(30, 'Castro', 'NIE000027', '7293-4850', 'El Amatillo Sonsonate', 'Ana Castro Garc�a', '2017-05-16'),
(31, 'G�mez', 'NIE000028', '6652-8173', 'El Centro Sonsonate', 'Pedro G�mez Rivas', '2017-06-21'),
(32, 'N��ez', 'NIE000029', '7915-3408', 'El Calvario Sonsonate', 'Sandra N��ez Santos', '2017-07-26'),
(33, 'Silva', 'NIE000030', '6084-7531', 'Los Naranjos Sonsonate', 'Manuel Silva Aguilar', '2017-08-31'),
(34, 'Romero', 'NIE000031', '7347-1864', 'Sensunap�n Sonsonate', 'Rosa Romero Jim�nez', '2017-09-05'),
(35, 'Ch�vez', 'NIE000032', '6710-5297', 'San Antonio Sonsonate', 'Roberto Ch�vez Ruiz', '2017-10-10'),
(36, 'Reyes', 'NIE000033', '7173-9620', 'Metal�o Sonsonate', 'Marta Reyes P�rez', '2017-11-15'),
(37, 'Vega', 'NIE000034', '6409-2153', 'Las Delicias Sonsonate', 'Fernando Vega L�pez', '2017-12-20'),
(38, 'Castillo', 'NIE000035', '7832-6589', 'El �ngel Sonsonate', 'Isabel Castillo Ram�rez', '2017-01-25'),
(39, 'Mendoza', 'NIE000036', '6265-0914', 'El Sauce Sonsonate', 'Carlos Mendoza Torres', '2017-02-28'),
(40, 'Ortiz', 'NIE000037', '7598-4237', 'El Progreso Sonsonate', 'Elena Ortiz S�nchez', '2017-03-05'),
(41, 'Navarro', 'NIE000038', '6931-7640', 'La Cruz Sonsonate', 'Andr�s Navarro D�az', '2017-04-10'),
(42, 'Flores', 'NIE000039', '7384-1973', 'Izalco Sonsonate', 'Sof�a Flores �lvarez', '2017-05-15'),
(43, 'Morales', 'NIE000040', '6157-5306', 'Centro Sonsonate', 'Ricardo Morales Ramos', '2017-06-20');

-- Insertar en Profesores usando el IdUsuario obtenido
INSERT INTO Profesores (IdUsuario, Apellido, Telefono, Dui, Direccion, FechaNacimiento)  
VALUES (3, 'L�pez', '7890-2345', '12345678-9', 'Colonia Centro', '1985-06-15');

-- Inserci�n en la tabla Matricula
INSERT INTO Matricula (IdAlumno, IdProfesor, YearLectivo)
VALUES (4, 1, 2024);
