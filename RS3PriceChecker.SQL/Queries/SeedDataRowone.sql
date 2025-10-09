USE [RS3PriceChecker]
GO

DECLARE @Amount INT = 0
DECLARE @Date DATETIME = GETDATE()

DECLARE @Name [NVARCHAR](256) = 'Miscellaneous'
DECLARE @Value [NVARCHAR](256) = 'ItemName'
DECLARE @Icon [NVARCHAR](256) = 'http://my.images.com/icon.ico'
DECLARE @Description [NVARCHAR](256) = 'Miscellaneous Category, items without a normal category'
DECLARE @Small [NVARCHAR](256) = 'http://my.images.com/smallicon.ico'
DECLARE @Large [NVARCHAR](256) = 'http://my.images.com/largeicon.ico'

DECLARE @CategoryID INT = 1
DECLARE @NameID INT = 1
DECLARE @IconID INT = 1
DECLARE @ItemID INT = 1

--IF NOT EXISTS (SELECT * FROM [dbo].[Icon] WHERE [Small] = @Small)
--BEGIN
--INSERT INTO [dbo].[Icon] ( [Small], [Large] ) VALUES ( @Small, @Large )
--END

--IF NOT EXISTS (SELECT * FROM [dbo].[Name] WHERE [Value] = @Value)
--BEGIN
--INSERT INTO [dbo].[Name] ( [Value], [Description] ) VALUES ( @Value, @Description )
--END

IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

--IF NOT EXISTS (SELECT * FROM [dbo].[Item] WHERE [ItemID] = @ItemID)
--BEGIN
--INSERT INTO [dbo].[Item] ( [ItemID], [CategoryID], [NameID], [IconID] ) VALUES ( @ItemID, @CategoryID, @NameID, @IconID )
--END

--IF NOT EXISTS (SELECT * FROM [dbo].[Price] WHERE [Amount] = @Amount)
--BEGIN
--INSERT INTO [dbo].[Price] ( [ItemID], [Amount], [Date] ) VALUES ( @ItemID, @Amount, @Date)
--END

SET @Name = 'Ammo'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Arrows'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Bolts'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Construction materials'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Construction products'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Cooking ingredients'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Costumes'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Crafting materials'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Familiars'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Farming produce'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Fletching materials'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Food and Drink'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Herblore materials'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Hunting equipment'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Hunting Produce'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Jewellery'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Mage armour'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Mage weapons'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Melee armour - low level'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Melee armour - mid level'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Melee armour - high level'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Melee weapons - low level'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Melee weapons - mid level'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Melee weapons - high level'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Mining and Smithing'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Potions'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Prayer armour'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Prayer materials'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Range armour'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Range weapons'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Runecrafting'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Runes, Spells and Teleports'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Seeds'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Summoning scrolls'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Tools and containers'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Woodcutting product'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Pocket items'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Stone spirits'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Salvage'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Firemaking products'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

SET @Name = 'Archaeology materials'
IF NOT EXISTS (SELECT * FROM [dbo].[Category] WHERE [Name] = @Name)
BEGIN
INSERT INTO [dbo].[Category] ( [Name], [Icon] ) VALUES ( @Name, @Icon)
END

GO