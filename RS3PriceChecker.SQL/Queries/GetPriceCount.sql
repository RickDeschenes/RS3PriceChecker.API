USE [RS3PriceChecker]

SELECT * FROM [RS3PriceChecker].[dbo].[Price]
WHERE [Date] = '2021/12/21'
--AND [ModifiedDate] >= '2021-12-15 06:59:01.407'

ORDER BY [ModifiedDate]  DESC


/*

SELECT [, [Prices] = COUNT(ItemID) FROM [RS3PriceChecker].[dbo].[Price]
Group By [ItemID]
Having COUNT(ItemID) > 1

ORDER BY [Prices]  DESC

SELECT * FROM [RS3PriceChecker].[dbo].[Price]
WHERE [ItemID] = 268
`1
ORDER BY [Date]  DESC

*/