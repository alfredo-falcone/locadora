
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/15/2015 00:51:52
-- Generated from EDMX file: C:\Users\Alfredo\Documents\Visual Studio 2010\Projects\Falcone.Locadora\Falcone.Locadora.Sistema\Data\Modelo.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Db];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CarroAluguel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Aluguels] DROP CONSTRAINT [FK_CarroAluguel];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteAluguel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Aluguels] DROP CONSTRAINT [FK_ClienteAluguel];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteCartoes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DadosCartaoCreditoes] DROP CONSTRAINT [FK_ClienteCartoes];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteEndereco]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clientes] DROP CONSTRAINT [FK_ClienteEndereco];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Aluguels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Aluguels];
GO
IF OBJECT_ID(N'[dbo].[Carros]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Carros];
GO
IF OBJECT_ID(N'[dbo].[Clientes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clientes];
GO
IF OBJECT_ID(N'[dbo].[DadosCartaoCreditoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DadosCartaoCreditoes];
GO
IF OBJECT_ID(N'[dbo].[Enderecos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Enderecos];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Aluguels'
CREATE TABLE [dbo].[Aluguels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DataAluguel] datetime  NOT NULL,
    [DataDevolucao] datetime  NOT NULL,
    [QuilometragemInicial] int  NOT NULL,
    [QuilometragemFinal] int  NOT NULL,
    [Carro_Id] int  NOT NULL,
    [Cliente_Id] int  NOT NULL
);
GO

-- Creating table 'Carros'
CREATE TABLE [dbo].[Carros] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Placa] nvarchar(max)  NOT NULL,
    [Modelo] nvarchar(max)  NOT NULL,
    [Ano] int  NOT NULL,
    [Quilometragem] int  NOT NULL
);
GO

-- Creating table 'Clientes'
CREATE TABLE [dbo].[Clientes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [CPF] nvarchar(max)  NOT NULL,
    [Endereco_Id] int  NOT NULL
);
GO

-- Creating table 'DadosCartaoCreditoes'
CREATE TABLE [dbo].[DadosCartaoCreditoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClienteId] int  NOT NULL
);
GO

-- Creating table 'Enderecos'
CREATE TABLE [dbo].[Enderecos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Logradouro] nvarchar(max)  NOT NULL,
    [Numero] nvarchar(max)  NOT NULL,
    [Complemento] nvarchar(max)  NOT NULL,
    [Fone] nvarchar(max)  NOT NULL,
    [Cep] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [Senha] nvarchar(max)  NOT NULL,
    [Sal] nvarchar(max)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Aluguels'
ALTER TABLE [dbo].[Aluguels]
ADD CONSTRAINT [PK_Aluguels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Carros'
ALTER TABLE [dbo].[Carros]
ADD CONSTRAINT [PK_Carros]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Clientes'
ALTER TABLE [dbo].[Clientes]
ADD CONSTRAINT [PK_Clientes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DadosCartaoCreditoes'
ALTER TABLE [dbo].[DadosCartaoCreditoes]
ADD CONSTRAINT [PK_DadosCartaoCreditoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Enderecos'
ALTER TABLE [dbo].[Enderecos]
ADD CONSTRAINT [PK_Enderecos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Carro_Id] in table 'Aluguels'
ALTER TABLE [dbo].[Aluguels]
ADD CONSTRAINT [FK_CarroAluguel]
    FOREIGN KEY ([Carro_Id])
    REFERENCES [dbo].[Carros]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CarroAluguel'
CREATE INDEX [IX_FK_CarroAluguel]
ON [dbo].[Aluguels]
    ([Carro_Id]);
GO

-- Creating foreign key on [Cliente_Id] in table 'Aluguels'
ALTER TABLE [dbo].[Aluguels]
ADD CONSTRAINT [FK_ClienteAluguel]
    FOREIGN KEY ([Cliente_Id])
    REFERENCES [dbo].[Clientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteAluguel'
CREATE INDEX [IX_FK_ClienteAluguel]
ON [dbo].[Aluguels]
    ([Cliente_Id]);
GO

-- Creating foreign key on [ClienteId] in table 'DadosCartaoCreditoes'
ALTER TABLE [dbo].[DadosCartaoCreditoes]
ADD CONSTRAINT [FK_ClienteCartoes]
    FOREIGN KEY ([ClienteId])
    REFERENCES [dbo].[Clientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteCartoes'
CREATE INDEX [IX_FK_ClienteCartoes]
ON [dbo].[DadosCartaoCreditoes]
    ([ClienteId]);
GO

-- Creating foreign key on [Endereco_Id] in table 'Clientes'
ALTER TABLE [dbo].[Clientes]
ADD CONSTRAINT [FK_ClienteEndereco]
    FOREIGN KEY ([Endereco_Id])
    REFERENCES [dbo].[Enderecos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteEndereco'
CREATE INDEX [IX_FK_ClienteEndereco]
ON [dbo].[Clientes]
    ([Endereco_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------