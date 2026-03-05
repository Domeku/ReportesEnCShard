CREATE DATABASE SistemaPrestamos;
GO

USE SistemaPrestamos;
GO

-- Tabla de Clientes
CREATE TABLE Clientes (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    NombreCompleto  NVARCHAR(100) NOT NULL,
    Correo          NVARCHAR(100) NOT NULL,
    Telefono        NVARCHAR(20)  NOT NULL,
    Direccion       NVARCHAR(200) NOT NULL,
    Garantia        NVARCHAR(200) NOT NULL,
    Sueldo          DECIMAL(18,2) NOT NULL,
    EsMoroso        BIT DEFAULT 0
);
GO

-- Tabla de Prestamos
CREATE TABLE Prestamos (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId   INT NOT NULL,
    Monto       DECIMAL(18,2) NOT NULL,
    PlazoMeses  INT NOT NULL,
    TasaInteres DECIMAL(5,2) NOT NULL,
    IntересGenerado DECIMAL(18,2) NOT NULL,
    MontoTotal  DECIMAL(18,2) NOT NULL,
    CuotaMensual DECIMAL(18,2) NOT NULL,
    FechaInicio DATE NOT NULL,
    Estado      NVARCHAR(20) DEFAULT 'Activo',
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);
GO

-- Tabla de Pagos
CREATE TABLE Pagos (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    PrestamoId      INT NOT NULL,
    NumeroCuota     INT NOT NULL,
    FechaPago       DATE,
    SaldoAnterior   DECIMAL(18,2) NOT NULL,
    InteresPagado   DECIMAL(18,2) NOT NULL,
    MontoAbonado    DECIMAL(18,2) NOT NULL,
    NuevoSaldo      DECIMAL(18,2) NOT NULL,
    CuotaMensual    DECIMAL(18,2) NOT NULL,
    Pagado          BIT DEFAULT 0,
    FOREIGN KEY (PrestamoId) REFERENCES Prestamos(Id)
);
GO

-- Tabla de Moras
CREATE TABLE Moras (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    PrestamoId  INT NOT NULL,
    PagoId      INT NOT NULL,
    MontoMora   DECIMAL(18,2) NOT NULL,
    Fecha       DATE NOT NULL,
    FOREIGN KEY (PrestamoId) REFERENCES Prestamos(Id),
    FOREIGN KEY (PagoId) REFERENCES Pagos(Id)
);
GO

-- Tabla del Fondo Base
CREATE TABLE FondoBase (
    Id      INT IDENTITY(1,1) PRIMARY KEY,
    Monto   DECIMAL(18,2) NOT NULL
);
GO

-- Insertar el fondo inicial de 5 millones
INSERT INTO FondoBase (Monto) VALUES (5000000);
GO