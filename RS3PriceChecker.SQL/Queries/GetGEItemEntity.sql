Select I.[ItemID], [CategoryName] = C.[Name], [ItemName] = N.[Value], [ItemDescription] = N.[Description], [ItemIcon] = G.[Small], [PriceAmount] = P.[Amount], [PriceDate] = P.[Date]
FROM [RS3PriceChecker].[dbo].[Item] AS I
JOIN [RS3PriceChecker].[dbo].[Icon] AS G ON G.[ID] = I.[IconID]
JOIN [RS3PriceChecker].[dbo].[Name] AS N ON N.[ID] = I.[NameID]
JOIN [RS3PriceChecker].[dbo].[Category] AS C ON C.[ID] = I.[CategoryID]
JOIN [RS3PriceChecker].[dbo].[Price] AS P ON P.[ItemID] = I.[ID]
WHERE I.[ItemID] = 4798


ORDER BY [CategoryName], [ItemName], [PriceDate]