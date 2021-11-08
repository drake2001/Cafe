CREATE TABLE [dbo].[Dishes] (
    [DishID]      INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100)  NOT NULL,
	[Author] NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR (500)  NOT NULL,
    [Type]        NVARCHAR (50)   NOT NULL,
    [Price]       DECIMAL (16, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([DishID] ASC)
);

