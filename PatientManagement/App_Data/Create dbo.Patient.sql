USE [PatientManagement]
GO

/****** Object: Table [dbo].[Patient] Script Date: 10/16/2018 7:15:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Patient] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]       NVARCHAR (MAX) NOT NULL,
    [LastName]        NVARCHAR (MAX) NOT NULL,
    [Phone]           NVARCHAR (MAX) NOT NULL,
    [Email]           NVARCHAR (MAX) NOT NULL,
    [Gender]          NVARCHAR (MAX) NOT NULL,
    [Notes]           TEXT           NULL,
    [CreatedDate]     DATETIME2 (7)  NOT NULL,
    [LastUpdatedDate] DATETIME2 (7)  NOT NULL,
    [IsDeleted]       BIT            NOT NULL
);


