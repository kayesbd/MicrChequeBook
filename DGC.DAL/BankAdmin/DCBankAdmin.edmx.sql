
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/25/2022 04:36:15
-- Generated from EDMX file: D:\Personal Code\KBZ\DGC.DAL\BankAdmin\DCBankAdmin.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [KBZCheque];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_City_State]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_City_State];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BranchOfBanks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BranchOfBanks];
GO
IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[PrinterInformations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PrinterInformations];
GO
IF OBJECT_ID(N'[dbo].[Requisitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Requisitions];
GO
IF OBJECT_ID(N'[dbo].[States]', 'U') IS NOT NULL
    DROP TABLE [dbo].[States];
GO
IF OBJECT_ID(N'[dbo].[UserInformation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInformation];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BranchOfBanks'
CREATE TABLE [dbo].[BranchOfBanks] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [BranchName] nvarchar(200)  NULL,
    [BranchCode] varchar(10)  NOT NULL,
    [BranchLocation] varchar(50)  NULL,
    [Latitude] varchar(50)  NULL,
    [Longitude] varchar(50)  NULL,
    [PrinterId] int  NULL,
    [PrintStartDate] datetime  NULL,
    [LastPrintingDateTime] datetime  NULL,
    [TotalNumberOfLeafsPrinted] bigint  NULL,
    [TotalNumberOfBooks] bigint  NULL,
    [IsActive] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [DateCreated] datetime  NULL,
    [CreatedBy] bigint  NULL,
    [DateModified] datetime  NULL,
    [ModifiedBy] bigint  NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [CityName] nvarchar(50)  NULL,
    [StateId] bigint  NULL,
    [IsActive] bit  NULL,
    [IsDeleted] bit  NULL,
    [DateCreated] datetime  NOT NULL,
    [CreatedBy] bigint  NOT NULL,
    [DateModified] datetime  NULL,
    [ModifiedBy] bigint  NULL
);
GO

-- Creating table 'PrinterInformations'
CREATE TABLE [dbo].[PrinterInformations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PrinterNumber] varchar(50)  NULL,
    [PrinterLocation] varchar(50)  NULL,
    [PrinterSetupDate] datetime  NULL,
    [PrinterStartDate] datetime  NULL,
    [LastPrintingDateTime] datetime  NULL,
    [TotalNumberOfLeafsPrinted] bigint  NULL,
    [TotalNumberOfBooks] bigint  NULL,
    [IsActive] bit  NULL,
    [IsDelete] bit  NULL,
    [CreatedBy] varchar(100)  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedBy] varchar(100)  NULL,
    [ModifiedDateTime] datetime  NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [StateName] nvarchar(50)  NOT NULL,
    [CountryId] bigint  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [CreatedBy] bigint  NOT NULL,
    [DateModified] datetime  NULL,
    [ModifiedBy] bigint  NULL
);
GO

-- Creating table 'UserInformations'
CREATE TABLE [dbo].[UserInformations] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [UserName] varchar(50)  NULL,
    [Password] varchar(50)  NULL,
    [Email] varchar(50)  NULL,
    [FullName] varchar(50)  NULL,
    [Designation] varchar(50)  NULL,
    [Role] varchar(50)  NULL,
    [CreatedBy] varchar(50)  NULL,
    [DateCreated] varchar(50)  NULL,
    [AccessToken] varchar(50)  NULL,
    [AccessTokenValidityPeriod] varchar(50)  NULL,
    [IsActive] bit  NULL,
    [IsDeleted] bit  NULL,
    [DateModified] datetime  NULL,
    [ModifiedBy] bigint  NULL
);
GO

-- Creating table 'Requisitions'
CREATE TABLE [dbo].[Requisitions] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [AccountNumber] varchar(50)  NULL,
    [CustomerName] varchar(50)  NULL,
    [ChequeBranch] varchar(50)  NULL,
    [Cheque_location_code_RequestBranch_Code] varchar(50)  NULL,
    [Location] varchar(50)  NULL,
    [RequestBranchCode] varchar(50)  NULL,
    [PickupBranchId] bigint  NULL,
    [SerialNumberStart] varchar(50)  NULL,
    [ChequeBookType] varchar(50)  NULL,
    [NumberOfLeaf] int  NULL,
    [PrinterId] bigint  NULL,
    [RequsitionSlipSerialNumber] varchar(50)  NULL,
    [NRCPerson] varchar(50)  NULL,
    [Status] varchar(50)  NULL,
    [SerialNoOfCheqEnd] varchar(50)  NULL,
    [DeliveredBy] varchar(50)  NULL,
    [ChequeOrderBy] int  NULL,
    [ChequePrintedCommandBy] int  NULL,
    [OrderDateTime] datetime  NULL,
    [PrintDateTime] datetime  NULL,
    [CreatedBy] varchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedBy] nchar(10)  NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BranchOfBanks'
ALTER TABLE [dbo].[BranchOfBanks]
ADD CONSTRAINT [PK_BranchOfBanks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PrinterInformations'
ALTER TABLE [dbo].[PrinterInformations]
ADD CONSTRAINT [PK_PrinterInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserInformations'
ALTER TABLE [dbo].[UserInformations]
ADD CONSTRAINT [PK_UserInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Requisitions'
ALTER TABLE [dbo].[Requisitions]
ADD CONSTRAINT [PK_Requisitions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StateId] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [FK_City_State]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_City_State'
CREATE INDEX [IX_FK_City_State]
ON [dbo].[Cities]
    ([StateId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------