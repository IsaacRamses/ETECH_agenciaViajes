USE [API_AgenciaViajes]
GO
/****** Object:  User [usr_AgenciaViajes]    Script Date: 31/1/2021 5:27:39 p. m. ******/
CREATE USER [usr_AgenciaViajes] FOR LOGIN [usr_AgenciaViajes] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [usr_AgenciaViajes]
GO
ALTER ROLE [db_datareader] ADD MEMBER [usr_AgenciaViajes]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [usr_AgenciaViajes]
GO
/****** Object:  Table [dbo].[Viajeros]    Script Date: 31/1/2021 5:27:39 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Viajeros](
	[ID_Viajeros] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [int] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Direccion] [varchar](2000) NOT NULL,
	[Telefono] [varchar](12) NOT NULL,
 CONSTRAINT [PK_Viajeros] PRIMARY KEY CLUSTERED 
(
	[ID_Viajeros] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Viajeros_Viajes]    Script Date: 31/1/2021 5:27:39 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Viajeros_Viajes](
	[ID_Viajeros_Viajes] [int] IDENTITY(1,1) NOT NULL,
	[ID_FK_Viajeros] [int] NOT NULL,
	[ID_FK_Viajes_Disponibles] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Viajeros_Viajes] PRIMARY KEY CLUSTERED 
(
	[ID_Viajeros_Viajes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Viajes_Disponibles]    Script Date: 31/1/2021 5:27:39 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Viajes_Disponibles](
	[ID_Viajes_Disponibles] [int] IDENTITY(1,1) NOT NULL,
	[COD_Viaje] [varchar](50) NOT NULL,
	[NUM_Plazas] [int] NOT NULL,
	[Lugar_Origen] [varchar](150) NOT NULL,
	[Lugar_Destino] [varchar](150) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Viajes] PRIMARY KEY CLUSTERED 
(
	[ID_Viajes_Disponibles] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Viajeros] ON 

INSERT [dbo].[Viajeros] ([ID_Viajeros], [Cedula], [Nombre], [Direccion], [Telefono]) VALUES (1, 20114020, N'Isaac Araujo', N'Los Teques', N'04141417882')
INSERT [dbo].[Viajeros] ([ID_Viajeros], [Cedula], [Nombre], [Direccion], [Telefono]) VALUES (2, 7943113, N'Hercilia Romero', N'Caracas
', N'04143233811')
SET IDENTITY_INSERT [dbo].[Viajeros] OFF
GO
SET IDENTITY_INSERT [dbo].[Viajes_Disponibles] ON 

INSERT [dbo].[Viajes_Disponibles] ([ID_Viajes_Disponibles], [COD_Viaje], [NUM_Plazas], [Lugar_Origen], [Lugar_Destino], [Precio]) VALUES (5, N'LAPRU388', 15, N'Caracas', N'Ciudad de Mexico', CAST(600.00 AS Decimal(18, 2)))
INSERT [dbo].[Viajes_Disponibles] ([ID_Viajes_Disponibles], [COD_Viaje], [NUM_Plazas], [Lugar_Origen], [Lugar_Destino], [Precio]) VALUES (6, N'LASSU135', 15, N'Nueva York', N'Barcelona', CAST(800.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Viajes_Disponibles] OFF
GO
ALTER TABLE [dbo].[Viajeros_Viajes]  WITH CHECK ADD  CONSTRAINT [FK_Viajeros] FOREIGN KEY([ID_FK_Viajeros])
REFERENCES [dbo].[Viajeros] ([ID_Viajeros])
GO
ALTER TABLE [dbo].[Viajeros_Viajes] CHECK CONSTRAINT [FK_Viajeros]
GO
ALTER TABLE [dbo].[Viajeros_Viajes]  WITH CHECK ADD  CONSTRAINT [FK_Viajes_Disponibles] FOREIGN KEY([ID_FK_Viajes_Disponibles])
REFERENCES [dbo].[Viajes_Disponibles] ([ID_Viajes_Disponibles])
GO
ALTER TABLE [dbo].[Viajeros_Viajes] CHECK CONSTRAINT [FK_Viajes_Disponibles]
GO
/****** Object:  StoredProcedure [dbo].[SP_Traer_Viajeros_Viajes]    Script Date: 31/1/2021 5:27:39 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Traer_Viajeros_Viajes] 	

AS

BEGIN

Select ID_Viajeros,ID_Viajeros_Viajes,ID_Viajes_Disponibles,COD_Viaje,Lugar_Origen,Lugar_Destino,Precio from Viajeros VIA
inner join Viajeros_Viajes VIA_VIJ on VIA.ID_Viajeros = VIA_VIJ.ID_FK_Viajeros
inner join Viajes_Disponibles VIA_D on VIA_VIJ.ID_FK_Viajes_Disponibles = VIA_D.ID_Viajes_Disponibles

END

GO
