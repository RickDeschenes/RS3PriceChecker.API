USE [RS3PriceChecker]

SELECT C.[Name], T.[ItemID], n.[Value], N.[Description], P.[Amount], P.[Date], I.[Small], I.[Large] FROM [dbo].[Price] AS P
JOIN [dbo].[Item] AS T ON T.[ID] = P.[ItemID]
JOIN [dbo].[Icon] AS I ON I.[ID] = T.[IconID]
JOIN [dbo].[Name] AS N ON N.[ID] = T.[NameID]
JOIN [dbo].[Category] AS C ON C.[ID] = T.[CategoryID]
WHERE P.[Date] = CONVERT(DATE, DATEADD(DAY, -1, GETDATE()))