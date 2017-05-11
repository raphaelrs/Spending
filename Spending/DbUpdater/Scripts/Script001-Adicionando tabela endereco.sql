USE [SpendingDb]
GO

/****** Object:  Table [dbo].[Address]    Script Date: 05/05/2017 13:58:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rua] [nvarchar](150) NOT NULL,
	[Numero] [int] NOT NULL,
	[Bairro] [nvarchar](100) NOT NULL,
	[Cidade] [nvarchar](100) NOT NULL,
	[Pais] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


