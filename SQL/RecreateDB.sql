USE [CQRSWebShop]
GO

/****** Object:  Table [dbo].[EventStore]    Script Date: 10/03/2012 19:42:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventStore]') AND type in (N'U'))
DROP TABLE [dbo].[EventStore]
GO

USE [CQRSWebShop]
GO

/****** Object:  Table [dbo].[EventStore]    Script Date: 10/03/2012 19:42:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventStore](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AggregateRootType] [nvarchar](50) NOT NULL,
	[AggregateRootId] [nvarchar](50) NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[EventType] [nvarchar](255) NOT NULL,
	[EventData] [nvarchar](max) NOT NULL
) ON [PRIMARY]

GO




USE [CQRSWebShop]
GO

/****** Object:  Table [dbo].[CatalogTree]    Script Date: 10/03/2012 19:42:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CatalogTree]') AND type in (N'U'))
DROP TABLE [dbo].[CatalogTree]
GO

USE [CQRSWebShop]
GO

/****** Object:  Table [dbo].[CatalogTree]    Script Date: 10/03/2012 19:42:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CatalogTree](
	[Id] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[ParentId] [nvarchar](50) NULL
) ON [PRIMARY]

GO


USE [CQRSWebShop]
GO

/****** Object:  Table [dbo].[AvailableCategories]    Script Date: 10/03/2012 19:42:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AvailableCategories]') AND type in (N'U'))
DROP TABLE [dbo].[AvailableCategories]
GO

USE [CQRSWebShop]
GO

/****** Object:  Table [dbo].[AvailableCategories]    Script Date: 10/03/2012 19:42:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AvailableCategories](
	[Id] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](255) NOT NULL
) ON [PRIMARY]

GO


