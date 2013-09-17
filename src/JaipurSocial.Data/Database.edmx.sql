
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/16/2013 22:19:34
-- Generated from EDMX file: D:\Projects\jvlppm\Pos\jaipur-social\src\JaipurSocial.Data\Database.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db7b5d02d1f04a4be9a76ca21e0007522d];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(256)  NOT NULL,
    [Password] varbinary(256)  NOT NULL,
    [Login] varchar(50)  NOT NULL
);
GO

-- Creating table 'GameSet'
CREATE TABLE [dbo].[GameSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EnemyTurn] bit  NOT NULL,
    [OnTable] nvarchar(max)  NOT NULL,
    [OnDeck] nvarchar(max)  NOT NULL,
    [Resources] nvarchar(max)  NOT NULL,
    [ChallengerId] int  NOT NULL,
    [ChallengerHand] nvarchar(max)  NOT NULL,
    [ChallengerPoints] int  NOT NULL,
    [ChallengerCamels] int  NOT NULL,
    [EnemyId] int  NOT NULL,
    [EnemyHand] nvarchar(max)  NOT NULL,
    [EnemyPoints] int  NOT NULL,
    [EnemyCamels] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GameSet'
ALTER TABLE [dbo].[GameSet]
ADD CONSTRAINT [PK_GameSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------