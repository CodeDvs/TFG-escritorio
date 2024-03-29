USE [DAM_AnderSabariego]
GO
/****** Object:  Table [dbo].[CabReparaciones]    Script Date: 15/05/2022 23:00:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CabReparaciones](
	[IDReparacion] [int] NOT NULL,
	[TipoDeEquipo] [varchar](50) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[DNIpropietario] [char](9) NOT NULL,
	[Reparado] [bit] NULL,
 CONSTRAINT [PK_CabReparaciones] PRIMARY KEY CLUSTERED 
(
	[IDReparacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comentarios]    Script Date: 15/05/2022 23:00:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comentarios](
	[idProducto] [int] NOT NULL,
	[comentario] [varchar](10) NULL,
	[fecha] [datetime] NULL,
 CONSTRAINT [PK_Comentarios] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CosteProducto]    Script Date: 15/05/2022 23:00:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CosteProducto](
	[idProducto] [char](5) NOT NULL,
	[CosteProducto] [smallint] NOT NULL,
	[Pvp] [smallint] NOT NULL,
 CONSTRAINT [PK_CosteProducto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialVenta]    Script Date: 15/05/2022 23:00:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialVenta](
	[idProducto] [char](5) NOT NULL,
	[CantidadVenta] [smallint] NULL,
	[fecha] [date] NULL,
 CONSTRAINT [PK_HistorialVenta] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LinReparaciones]    Script Date: 15/05/2022 23:00:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinReparaciones](
	[IDReparacion] [nchar](10) NOT NULL,
	[Comentario] [varchar](75) NOT NULL,
	[fecha] [date] NOT NULL,
 CONSTRAINT [PK_LinReparaciones] PRIMARY KEY CLUSTERED 
(
	[IDReparacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 15/05/2022 23:00:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[idProducto] [int] NOT NULL,
	[NombreProducto] [varchar](25) NOT NULL,
	[CantidadMinima] [smallint] NOT NULL,
	[CantidadTotal] [smallint] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoEquipo]    Script Date: 15/05/2022 23:00:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoEquipo](
	[id] [int] NOT NULL,
	[TipoEquipo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoEquipo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CabReparaciones] ([IDReparacion], [TipoDeEquipo], [Fecha], [DNIpropietario], [Reparado]) VALUES (1, N'Portatil', CAST(N'2020-10-20T00:00:00.000' AS DateTime), N'73058888D', 0)
INSERT [dbo].[CabReparaciones] ([IDReparacion], [TipoDeEquipo], [Fecha], [DNIpropietario], [Reparado]) VALUES (2, N'Movil', CAST(N'2022-05-03T22:16:25.493' AS DateTime), N'233214D  ', 0)
GO
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'1    ', 10, 15)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'10   ', 3, 4)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'11   ', 3, 4)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'12   ', 1, 1)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'13   ', 1, 1)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'14   ', 2, 3)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'2    ', 10, 20)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'3    ', 50, 50)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'4    ', 20, 20)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'6    ', 1, 3)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'7    ', 2, 3)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'8    ', 2, 1)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'9    ', 3, 4)
GO
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (1, N'Protector Movil', 10, 50)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (2, N'Protector Tablet', 20, 100)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (3, N'Discos SSD', 24, 10)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (4, N'Licencias Antivirus', 60, 12)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (6, N'DDD', 3, 2)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (7, N'QQQ', 1, 2)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (8, N'DDDF', 1, 2)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (9, N'WWWE', 1, 2)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (10, N'DDDSS', 1, 2)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (12, N'ssss', 12, 1)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (13, N'wweq', 12, 1)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (14, N'ander', 1, 1)
GO
INSERT [dbo].[TipoEquipo] ([id], [TipoEquipo]) VALUES (1, N'Portatil')
INSERT [dbo].[TipoEquipo] ([id], [TipoEquipo]) VALUES (2, N'Sobremesa')
INSERT [dbo].[TipoEquipo] ([id], [TipoEquipo]) VALUES (3, N'Movil')
INSERT [dbo].[TipoEquipo] ([id], [TipoEquipo]) VALUES (4, N'Tablet')
INSERT [dbo].[TipoEquipo] ([id], [TipoEquipo]) VALUES (5, N'Smartwatch')
GO
/****** Object:  StoredProcedure [dbo].[pr_AltasProductos]    Script Date: 15/05/2022 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_AltasProductos]
@p_Id char(5),
@p_Nombre varchar(25),
@p_CantidadMin smallint,
@p_Cantidad smallint,
@p_PrecioCoste money,
@p_Pvp money,
@p_Salida int output
as
begin
/*Compruebo que no exista ya un producto con ese nombre*/
if(exists(Select NombreProducto from Productos where NombreProducto=@p_Nombre))
begin
/*Si existe ese producto, devuelvo un -1 y no inserto nada*/
set @p_Salida = -1
end
else
begin


insert into Productos (idProducto,NombreProducto,CantidadMinima,CantidadTotal) values (@p_Id,@p_Nombre,@p_CantidadMin,@p_Cantidad)
insert into CosteProducto (idProducto,CosteProducto,Pvp) values (@p_Id,@p_PrecioCoste,@p_Pvp)
set @p_Salida = 0

end
end
GO
/****** Object:  StoredProcedure [dbo].[pr_ComprobarExistencias]    Script Date: 15/05/2022 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_ComprobarExistencias]
@p_NombreProducto varchar(25),
@p_Salida int output
as
declare @v_Existencias smallint
begin
if(exists(Select NombreProducto from Productos where NombreProducto=@p_NombreProducto))
begin
Set @v_Existencias=(Select CantidadTotal from Productos where NombreProducto=@p_NombreProducto)
set @p_Salida=@v_Existencias
end
else
begin
set @p_Salida = -1
end
end
GO
/****** Object:  StoredProcedure [dbo].[pr_EditarProductos]    Script Date: 15/05/2022 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_EditarProductos]
@p_Id int,
@p_Nombre varchar(25),
@p_CantidadMin smallint,
@p_Cantidad smallint,
@p_PrecioCoste money,
@p_Pvp money,
@p_Salida int output
as
begin
Update Productos set NombreProducto=@p_Nombre, CantidadMinima=@p_CantidadMin,CantidadTotal=@p_Cantidad  where  idProducto=@p_Id
Update CosteProducto set CosteProducto=@p_PrecioCoste,Pvp=@p_Pvp where idProducto=@p_Id
set @p_Salida = 0
end
GO
/****** Object:  StoredProcedure [dbo].[pr_EliminarProductos]    Script Date: 15/05/2022 23:00:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_EliminarProductos]
@p_Nombre varchar(25)
as
declare @v_id int
begin
Delete from Productos where NombreProducto = @p_Nombre
set @v_id =(Select idProducto from Productos where NombreProducto = @p_Nombre)
Delete from CosteProducto where idProducto = @v_id
end
GO
