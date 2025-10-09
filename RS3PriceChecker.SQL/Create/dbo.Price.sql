CREATE TABLE [dbo].[Price] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [ItemID]       INT            NOT NULL,
    [Amount]       INT            NOT NULL,
    [Date]         DATETIME       NOT NULL,
    [CreatedBy]    NVARCHAR (256) DEFAULT ([dbo].[fn_GetCurrentUserID]()) NOT NULL,
    [CreatedDate]  DATETIME       DEFAULT (getdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (256) DEFAULT ([dbo].[fn_GetCurrentUserID]()) NOT NULL,
    [ModifiedDate] DATETIME       DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Price_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [PK_Price_ItemID_Date] UNIQUE ([ItemID], [Date] ASC),
    CONSTRAINT [FK_Price_Item_ID] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ID])
);

