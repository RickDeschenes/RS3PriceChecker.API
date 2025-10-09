SELECT [I.ID] = P.[ItemID], I.[ItemID], [Date] = MAX([Date])
FROM [RS3PriceChecker].[dbo].[Price] AS P
INNER JOIN [RS3PriceChecker].[dbo].[Item] AS I ON I.[ID] = P.[ItemID]
WHERE [Date] < '2021/12/14'
GROUP BY P.[ItemID], I.[ItemID]

ORDER BY [Date] DESC --,[CreatedDate] DESC