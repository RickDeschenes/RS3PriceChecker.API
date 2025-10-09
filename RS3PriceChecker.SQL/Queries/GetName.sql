SELECT [ID] ,[Value] ,[Description] ,[CreatedBy] ,[CreatedDate] ,[ModifiedBy] ,[ModifiedDate] 
FROM [RS3PriceChecker].[dbo].[Name]
WHERE [Value] LIKE '%golden%'

ORDER BY [ID] DESC --,[CreatedDate] DESC