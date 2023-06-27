CREATE TABLE [Groups] (
    [GroupsUserId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Level] int NOT NULL,
    [Status] bit NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY ([GroupsUserId])
);
GO


CREATE TABLE [Produc] (
    [ProductCodeId] int NOT NULL IDENTITY,
    [GenerationDate] datetime2 NOT NULL,
    [Code] int NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    [CanExpire] bit NOT NULL,
    CONSTRAINT [PK_Produc] PRIMARY KEY ([ProductCodeId])
);
GO


CREATE TABLE [Products] (
    [ProductId] int NOT NULL IDENTITY,
    [Name] nvarchar(30) NOT NULL,
    [Description] nvarchar(320) NOT NULL,
    [Buy_Price] float NOT NULL,
    [Sell_Price] float NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
);
GO


CREATE TABLE [WareHouses] (
    [WareHouseId] int NOT NULL IDENTITY,
    [Name] nvarchar(60) NOT NULL,
    [Address] nvarchar(120) NOT NULL,
    CONSTRAINT [PK_WareHouses] PRIMARY KEY ([WareHouseId])
);
GO


CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [Name] nvarchar(30) NOT NULL,
    [LastName] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [UserImagePath] nvarchar(max) NULL,
    [Enable] bit NOT NULL,
    [LastLogin] datetime2 NOT NULL,
    [GroupsUserId] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_Groups_GroupsUserId] FOREIGN KEY ([GroupsUserId]) REFERENCES [Groups] ([GroupsUserId]) ON DELETE CASCADE
);
GO


CREATE TABLE [Photos] (
    [PhotoId] int NOT NULL IDENTITY,
    [FileName] nvarchar(max) NOT NULL,
    [File] varbinary(max) NOT NULL,
    [ProductId] int NOT NULL,
    CONSTRAINT [PK_Photos] PRIMARY KEY ([PhotoId]),
    CONSTRAINT [FK_Photos_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);
GO


CREATE TABLE [Inventories] (
    [InventoryId] int NOT NULL IDENTITY,
    [CreationDate] datetime2 NOT NULL,
    [QuantityOfExistances] int NOT NULL,
    [WareHouseId] int NOT NULL,
    [ProductId] int NOT NULL,
    CONSTRAINT [PK_Inventories] PRIMARY KEY ([InventoryId]),
    CONSTRAINT [FK_Inventories_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Inventories_WareHouses_WareHouseId] FOREIGN KEY ([WareHouseId]) REFERENCES [WareHouses] ([WareHouseId]) ON DELETE CASCADE
);
GO


CREATE TABLE [Entries] (
    [EntryId] int NOT NULL IDENTITY,
    [CreationDate] datetime2 NOT NULL,
    [Quantity] int NOT NULL,
    [Discounts] int NOT NULL,
    [Avariable] bit NOT NULL,
    [Author] nvarchar(max) NULL,
    [Notes] nvarchar(max) NULL,
    [InventoryId] int NOT NULL,
    [ProductCodeId] int NOT NULL,
    CONSTRAINT [PK_Entries] PRIMARY KEY ([EntryId]),
    CONSTRAINT [FK_Entries_Inventories_InventoryId] FOREIGN KEY ([InventoryId]) REFERENCES [Inventories] ([InventoryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Entries_Produc_ProductCodeId] FOREIGN KEY ([ProductCodeId]) REFERENCES [Produc] ([ProductCodeId]) ON DELETE CASCADE
);
GO


CREATE TABLE [Exits] (
    [ExistsId] int NOT NULL IDENTITY,
    [CreationDate] datetime2 NOT NULL,
    [Quantity] int NOT NULL,
    [Author] nvarchar(max) NULL,
    [CustomerName] nvarchar(max) NOT NULL,
    [Notes] nvarchar(max) NOT NULL,
    [InventoryId] int NOT NULL,
    CONSTRAINT [PK_Exits] PRIMARY KEY ([ExistsId]),
    CONSTRAINT [FK_Exits_Inventories_InventoryId] FOREIGN KEY ([InventoryId]) REFERENCES [Inventories] ([InventoryId]) ON DELETE CASCADE
);
GO


CREATE TABLE [RegisterOfExits] (
    [RegisterOfExitId] int NOT NULL IDENTITY,
    [Quantity] int NOT NULL,
    [MyProperty] int NOT NULL,
    [ProductCodeId] int NOT NULL,
    [ExistsId] int NOT NULL,
    [ExitExistsId] int NOT NULL,
    CONSTRAINT [PK_RegisterOfExits] PRIMARY KEY ([RegisterOfExitId]),
    CONSTRAINT [FK_RegisterOfExits_Exits_ExitExistsId] FOREIGN KEY ([ExitExistsId]) REFERENCES [Exits] ([ExistsId]) ON DELETE CASCADE,
    CONSTRAINT [FK_RegisterOfExits_Produc_ProductCodeId] FOREIGN KEY ([ProductCodeId]) REFERENCES [Produc] ([ProductCodeId]) ON DELETE CASCADE
);
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GroupsUserId', N'Level', N'Name', N'Status') AND [object_id] = OBJECT_ID(N'[Groups]'))
    SET IDENTITY_INSERT [Groups] ON;
INSERT INTO [Groups] ([GroupsUserId], [Level], [Name], [Status])
VALUES (1, 1, N'Administradores', CAST(1 AS bit)),
(2, 3, N'Vendedores', CAST(1 AS bit)),
(3, 2, N'Administrador Inventarios', CAST(1 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GroupsUserId', N'Level', N'Name', N'Status') AND [object_id] = OBJECT_ID(N'[Groups]'))
    SET IDENTITY_INSERT [Groups] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'Buy_Price', N'Description', N'Name', N'Sell_Price') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([ProductId], [Buy_Price], [Description], [Name], [Sell_Price])
VALUES (1, 1.0E0, N'Sample', N'Sample', 2.0E0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'Buy_Price', N'Description', N'Name', N'Sell_Price') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'Enable', N'GroupsUserId', N'LastLogin', N'LastName', N'Name', N'Password', N'UserImagePath', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([UserId], [Enable], [GroupsUserId], [LastLogin], [LastName], [Name], [Password], [UserImagePath], [UserName])
VALUES (1, CAST(1 AS bit), 1, '2023-06-27T08:47:56.7042061-06:00', N'', N'', N'admin', NULL, N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'Enable', N'GroupsUserId', N'LastLogin', N'LastName', N'Name', N'Password', N'UserImagePath', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO


CREATE INDEX [IX_Entries_InventoryId] ON [Entries] ([InventoryId]);
GO


CREATE INDEX [IX_Entries_ProductCodeId] ON [Entries] ([ProductCodeId]);
GO


CREATE INDEX [IX_Exits_InventoryId] ON [Exits] ([InventoryId]);
GO


CREATE INDEX [IX_Inventories_ProductId] ON [Inventories] ([ProductId]);
GO


CREATE INDEX [IX_Inventories_WareHouseId] ON [Inventories] ([WareHouseId]);
GO


CREATE INDEX [IX_Photos_ProductId] ON [Photos] ([ProductId]);
GO


CREATE INDEX [IX_RegisterOfExits_ExitExistsId] ON [RegisterOfExits] ([ExitExistsId]);
GO


CREATE INDEX [IX_RegisterOfExits_ProductCodeId] ON [RegisterOfExits] ([ProductCodeId]);
GO


CREATE INDEX [IX_Users_GroupsUserId] ON [Users] ([GroupsUserId]);
GO


