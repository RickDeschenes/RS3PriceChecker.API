USE [RS3PriceChecker]
GO

/****** OCreate Rune Space 3 Price Checker Tables ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[UC_Price_ItemID_DATE]', 'u') IS NOT NULL
BEGIN
	ALTER TABLE [dbo].[Price]
	DROP CONSTRAINT [UC_Price_ItemID_DATE]
END

IF OBJECT_ID('[dbo].[Price]', 'u') IS NOT NULL
BEGIN

	DROP TABLE [dbo].[Price]
END
IF OBJECT_ID('[dbo].[Item]', 'u') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[Item]
END
IF OBJECT_ID('[dbo].[Category]', 'u') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[Category]
END
IF OBJECT_ID('[dbo].[Name]', 'u') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[Name]
END
IF OBJECT_ID('[dbo].[Icon]', 'u') IS NOT NULL
BEGIN
	DROP TABLE [dbo].[Icon]
END


IF OBJECT_ID('[dbo].[Category]', 'u') IS NULL
BEGIN
	CREATE TABLE [dbo].[Category](
		[ID] INT IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](256) NOT NULL,
		[Icon] [nvarchar](256) NOT NULL,
		[CreatedBy] nvarchar(256) NOT NULL,
		[CreatedDate] DATETIME NOT NULL,
		[ModifiedBy] nvarchar(256) NOT NULL,
		[ModifiedDate] DATETIME NOT NULL,
	 CONSTRAINT [PK_Category_ID] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	UNIQUE NONCLUSTERED 
	(
		[Name] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Category] ADD  DEFAULT ([dbo].[fn_GetCurrentUserID]()) FOR [CreatedBy]

	ALTER TABLE [dbo].[Category] ADD  DEFAULT (getdate()) FOR [CreatedDate]

	ALTER TABLE [dbo].[Category] ADD  DEFAULT ([dbo].[fn_GetCurrentUserID]()) FOR [ModifiedBy]

	ALTER TABLE [dbo].[Category] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
END
GO

IF OBJECT_ID('[dbo].[Name]', 'u') IS NULL
BEGIN
	CREATE TABLE [dbo].[Name](
		[ID] INT IDENTITY(1,1) NOT NULL,
		[Value] [nvarchar](256) NOT NULL,
		[Description] [nvarchar](256) NOT NULL,
		[CreatedBy] nvarchar(256) NOT NULL,
		[CreatedDate] DATETIME NOT NULL,
		[ModifiedBy] nvarchar(256) NOT NULL,
		[ModifiedDate] DATETIME NOT NULL,
	 CONSTRAINT [PK_Name_ID] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	UNIQUE NONCLUSTERED 
	(
		[Value] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Name] ADD DEFAULT ([dbo].[fn_GetCurrentUserID]()) FOR [CreatedBy]

	ALTER TABLE [dbo].[Name] ADD DEFAULT (GETDATE()) FOR [CreatedDate]

	ALTER TABLE [dbo].[Name] ADD DEFAULT ([dbo].[fn_GetCurrentUserID]()) FOR [ModifiedBy]

	ALTER TABLE [dbo].[Name] ADD DEFAULT (GETDATE()) FOR [ModifiedDate]

END
GO


IF OBJECT_ID('[dbo].[Icon]', 'u') IS NULL
BEGIN
	CREATE TABLE [dbo].[Icon](
		[ID] INT IDENTITY(1,1) NOT NULL,
		[Small] [nvarchar](256) NOT NULL,
		[Large] [nvarchar](256) NOT NULL,
		[CreatedBy] nvarchar(256) NOT NULL,
		[CreatedDate] DATETIME NOT NULL,
		[ModifiedBy] nvarchar(256) NOT NULL,
		[ModifiedDate] DATETIME NOT NULL,
	 CONSTRAINT [PK_Icon_ID] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	UNIQUE NONCLUSTERED 
	(
		[Small] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Icon] ADD DEFAULT ([dbo].[fn_GetCurrentUserID]()) FOR [CreatedBy]

	ALTER TABLE [dbo].[Icon] ADD DEFAULT (GETDATE()) FOR [CreatedDate]

	ALTER TABLE [dbo].[Icon] ADD DEFAULT ([dbo].[fn_GetCurrentUserID]()) FOR [ModifiedBy]

	ALTER TABLE [dbo].[Icon] ADD DEFAULT (GETDATE()) FOR [ModifiedDate]

END
GO

IF OBJECT_ID('[dbo].[Item]', 'u') IS NULL
BEGIN
	CREATE TABLE [dbo].[Item](
		[ID] INT IDENTITY(1,1) NOT NULL,
		[ItemID] INT NOT NULL,
		[CategoryID] INT NOT NULL,
		[NameID] INT NOT NULL,
		[IconID] INT NOT NULL,
		[CreatedBy] nvarchar(256) NOT NULL,
		[CreatedDate] DATETIME NOT NULL,
		[ModifiedBy] nvarchar(256) NOT NULL,
		[ModifiedDate] DATETIME NOT NULL,
	 CONSTRAINT [PK_Item_ID] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	UNIQUE NONCLUSTERED 
	(
		[ItemID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Item] ADD  DEFAULT ([dbo].[fn_GetCurrentUserID]()) FOR [CreatedBy]

	ALTER TABLE [dbo].[Item] ADD  DEFAULT (getdate()) FOR [CreatedDate]

	ALTER TABLE [dbo].[Item] ADD  DEFAULT ([dbo].[fn_GetCurrentUserID]()) FOR [ModifiedBy]

	ALTER TABLE [dbo].[Item] ADD  DEFAULT (getdate()) FOR [ModifiedDate]

	ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Category_ID] FOREIGN KEY([CategoryID])
	REFERENCES [dbo].[Category] ([ID])

	ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Category_ID]

	ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Icon_ID] FOREIGN KEY([IconID])
	REFERENCES [dbo].[Icon] ([ID])

	ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Icon_ID]

	ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Name_ID] FOREIGN KEY([NameID])
	REFERENCES [dbo].[Name] ([ID])

	ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Name_ID]

END
GO

IF OBJECT_ID('[dbo].[Price]', 'u') IS NULL
BEGIN
	CREATE TABLE [dbo].[Price](
		[ID] INT IDENTITY(1,1) NOT NULL,
		[ItemID] INT NOT NULL,
		[Amount] INT NOT NULL,
		[Date] DATETIME NOT NULL,
		[CreatedBy] nvarchar(256) NOT NULL,
		[CreatedDate] DATETIME NOT NULL,
		[ModifiedBy] nvarchar(256) NOT NULL,
		[ModifiedDate] DATETIME NOT NULL,
	 CONSTRAINT [PK_Price_ID] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Price] ADD DEFAULT ([dbo].[fn_GetCurrentUserID]()) FOR [CreatedBy]

	ALTER TABLE [dbo].[Price] ADD DEFAULT (GETDATE()) FOR [CreatedDate]

	ALTER TABLE [dbo].[Price] ADD DEFAULT ([dbo].[fn_GetCurrentUserID]()) FOR [ModifiedBy]

	ALTER TABLE [dbo].[Price] ADD DEFAULT (GETDATE()) FOR [ModifiedDate]

	ALTER TABLE [dbo].[Price]  WITH CHECK ADD  CONSTRAINT [FK_Price_Item_ID] FOREIGN KEY([ItemID])
	REFERENCES [dbo].[Item] ([ID])

	ALTER TABLE [dbo].[Price] ADD CONSTRAINT [UC_Price_ItemID_DATE] UNIQUE ([ItemID], [Date])

END
GO

