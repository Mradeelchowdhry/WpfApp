﻿CREATE TABLE [dbo].[students] (
    [id]       INT           IDENTITY (1, 1) NOT NULL,
    [fullname] NVARCHAR (50) NOT NULL,
    [email]    NVARCHAR (50) NOT NULL,
    [age]      INT           NOT NULL,
    [cellno]   BIGINT        NOT NULL,
    [city]     NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([email] ASC)
);

