USE [RS3PriceChecker]

DECLARE @Date DateTime = DATEDIFF(DAY, 0, DATEADD(DAY, -2, GETDATE()))
SELECT @Date

SELECT C.[Name], T.[ItemID], n.[Value], N.[Description], [Amount] = FORMAT(P.[Amount], 'C0'), P.[Date], I.[Small], I.[Large] FROM [dbo].[Price] AS P
JOIN [dbo].[Item] AS T ON T.[ID] = P.[ItemID]
JOIN [dbo].[Icon] AS I ON I.[ID] = T.[IconID]
JOIN [dbo].[Name] AS N ON N.[ID] = T.[NameID]
JOIN [dbo].[Category] AS C ON C.[ID] = T.[CategoryID]
WHERE P.[Date] >= @Date
AND T.[ID] = 4889