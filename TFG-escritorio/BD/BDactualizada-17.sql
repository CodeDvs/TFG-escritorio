USE [DAM_AnderSabariego]
GO
/****** Object:  Table [dbo].[CabVenta]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CabVenta](
	[idVenta] [int] NOT NULL,
	[DNI] [char](9) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[CosteTotal] [money] NOT NULL,
 CONSTRAINT [PK_CabVenta] PRIMARY KEY CLUSTERED 
(
	[idVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comentarios]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comentarios](
	[DNI] [char](9) NOT NULL,
	[idComentario] [int] NOT NULL,
	[TextoComentario] [varchar](50) NULL,
	[fecha] [datetime] NULL,
 CONSTRAINT [PK_Comentarios] PRIMARY KEY CLUSTERED 
(
	[DNI] ASC,
	[idComentario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CosteProducto]    Script Date: 20/05/2022 12:29:29 ******/
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
/****** Object:  Table [dbo].[HistorialVenta]    Script Date: 20/05/2022 12:29:29 ******/
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
/****** Object:  Table [dbo].[Historico]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Historico](
	[idHistorico] [int] NOT NULL,
	[idVenta] [int] NOT NULL,
	[DNI] [char](9) NOT NULL,
	[correoresponsable] [varchar](50) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[motivo] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Historico] PRIMARY KEY CLUSTERED 
(
	[idHistorico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LinVenta]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinVenta](
	[idLineasVenta] [int] NOT NULL,
	[idVenta] [int] NOT NULL,
	[Servicio] [varchar](50) NOT NULL,
	[CosteServicio] [money] NOT NULL,
 CONSTRAINT [PK_LinVenta] PRIMARY KEY CLUSTERED 
(
	[idLineasVenta] ASC,
	[idVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 20/05/2022 12:29:29 ******/
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
/****** Object:  Table [dbo].[Reparaciones]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reparaciones](
	[IDReparacion] [int] NOT NULL,
	[TipoDeEquipo] [varchar](50) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[DNIpropietario] [char](9) NOT NULL,
	[Reparado] [bit] NOT NULL,
 CONSTRAINT [PK_CabReparaciones] PRIMARY KEY CLUSTERED 
(
	[IDReparacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicios]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicios](
	[idServicio] [int] NOT NULL,
	[Servicio] [varchar](50) NOT NULL,
	[CosteServicio] [money] NOT NULL,
 CONSTRAINT [PK_Servicios] PRIMARY KEY CLUSTERED 
(
	[idServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoEquipo]    Script Date: 20/05/2022 12:29:29 ******/
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
/****** Object:  Table [dbo].[Usuarios]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] NOT NULL,
	[correo] [varchar](50) NOT NULL,
	[pass] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Comentarios] ([DNI], [idComentario], [TextoComentario], [fecha]) VALUES (N'73058888D', 1, N'El ordenador esta roto', CAST(N'2022-05-17T10:07:44.553' AS DateTime))
INSERT [dbo].[Comentarios] ([DNI], [idComentario], [TextoComentario], [fecha]) VALUES (N'73058888D', 2, N'El ordenador echa humo', CAST(N'2022-05-17T10:08:31.553' AS DateTime))
INSERT [dbo].[Comentarios] ([DNI], [idComentario], [TextoComentario], [fecha]) VALUES (N'73058888D', 3, N'Se ha incendiado', CAST(N'2022-05-17T10:08:40.593' AS DateTime))
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'1    ', 10, 15)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'10   ', 3, 4)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'11   ', 3, 4)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'12   ', 1, 1)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'13   ', 1, 1)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'14   ', 2, 3)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'15   ', 22, 33)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'16   ', 33, 3)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'17   ', 3, 3)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'2    ', 10, 20)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'3    ', 50, 50)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'4    ', 20, 20)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'6    ', 1, 3)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'7    ', 2, 3)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'8    ', 2, 1)
INSERT [dbo].[CosteProducto] ([idProducto], [CosteProducto], [Pvp]) VALUES (N'9    ', 3, 4)
INSERT [dbo].[Historico] ([idHistorico], [idVenta], [DNI], [correoresponsable], [Fecha], [motivo]) VALUES (2, 1, N'73058888D', N'proyectofinalseim@gmail.com', CAST(N'2022-05-20T11:31:43.633' AS DateTime), N'Error')
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (1, N'Protector Movil', 10, 50)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (2, N'Protector Tablet', 20, 100)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (3, N'Discos SSD', 24, 10)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (4, N'Licencias Antivirus', 60, 12)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (9, N'WWWE', 1, 2)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (12, N'ssss', 12, 1)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (13, N'wweq', 12, 1)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (14, N'ander', 1, 1)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (15, N'ASDADSA', 22, 11)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (16, N'wewew', 1, 23)
INSERT [dbo].[Productos] ([idProducto], [NombreProducto], [CantidadMinima], [CantidadTotal]) VALUES (17, N'ddddd', 1, 2)
INSERT [dbo].[Reparaciones] ([IDReparacion], [TipoDeEquipo], [Fecha], [DNIpropietario], [Reparado]) VALUES (1, N'Portatil', CAST(N'2020-10-20T00:00:04.000' AS DateTime), N'73058888D', 1)
INSERT [dbo].[Reparaciones] ([IDReparacion], [TipoDeEquipo], [Fecha], [DNIpropietario], [Reparado]) VALUES (2, N'Portatil', CAST(N'2022-05-17T11:44:04.890' AS DateTime), N'90213321G', 0)
INSERT [dbo].[Reparaciones] ([IDReparacion], [TipoDeEquipo], [Fecha], [DNIpropietario], [Reparado]) VALUES (3, N'Movil', CAST(N'2022-05-17T11:44:18.030' AS DateTime), N'00234343Y', 0)
INSERT [dbo].[Servicios] ([idServicio], [Servicio], [CosteServicio]) VALUES (1, N'Limpieza virus', 60.0000)
INSERT [dbo].[Servicios] ([idServicio], [Servicio], [CosteServicio]) VALUES (2, N'Cambio disco duro', 50.0000)
INSERT [dbo].[Servicios] ([idServicio], [Servicio], [CosteServicio]) VALUES (3, N'Reparacion placa base', 80.0000)
INSERT [dbo].[Servicios] ([idServicio], [Servicio], [CosteServicio]) VALUES (4, N'Reparacion procesador', 120.0000)
INSERT [dbo].[Servicios] ([idServicio], [Servicio], [CosteServicio]) VALUES (5, N'Reparacion fuente alimentacion', 75.0000)
INSERT [dbo].[Servicios] ([idServicio], [Servicio], [CosteServicio]) VALUES (6, N'Formateo', 60.0000)
INSERT [dbo].[Servicios] ([idServicio], [Servicio], [CosteServicio]) VALUES (7, N'Copia de seguridad L', 15.0000)
INSERT [dbo].[Servicios] ([idServicio], [Servicio], [CosteServicio]) VALUES (8, N'Copia de seguridad M', 30.0000)
INSERT [dbo].[Servicios] ([idServicio], [Servicio], [CosteServicio]) VALUES (9, N'Copia de seguridad G', 60.0000)
INSERT [dbo].[TipoEquipo] ([id], [TipoEquipo]) VALUES (1, N'Portatil')
INSERT [dbo].[TipoEquipo] ([id], [TipoEquipo]) VALUES (2, N'Sobremesa')
INSERT [dbo].[TipoEquipo] ([id], [TipoEquipo]) VALUES (3, N'Movil')
INSERT [dbo].[TipoEquipo] ([id], [TipoEquipo]) VALUES (4, N'Tablet')
INSERT [dbo].[TipoEquipo] ([id], [TipoEquipo]) VALUES (5, N'Smartwatch')
INSERT [dbo].[Usuarios] ([idUsuario], [correo], [pass]) VALUES (1, N'ander', N'1234')
INSERT [dbo].[Usuarios] ([idUsuario], [correo], [pass]) VALUES (2, N'proyectofinalseim@gmail.com', N'seim2022')
/****** Object:  StoredProcedure [dbo].[pr_Altas]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_Altas]
    @p_codPedido char(3),
	@p_codProveedor char(3),
	@p_fechaPedido smalldatetime, 
	@p_SumaTotal smallmoney output
	--@p_lineas Comercial.t_lineasPedido readonly,
	--@p_salida int output
as

begin
--Lineas de Pedidos
     select @p_SumaTotal = sum(cantidad*Precio)
	      from LineasPedidos

--Insert de CabeceraPedidos
	insert into dbo.Pedidos(CodPedido,CodProveedor, fecha,CosteTotal)
			values (@p_codPedido,@p_codProveedor,@p_fechaPedido, @p_SumaTotal)


end
GO
/****** Object:  StoredProcedure [dbo].[pr_AltasProductos]    Script Date: 20/05/2022 12:29:29 ******/
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
/****** Object:  StoredProcedure [dbo].[pr_CargarCombo]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pr_CargarCombo]
as
begin
select codIdioma,nombre from fp.idiomas
end
GO
/****** Object:  StoredProcedure [dbo].[pr_CargarProductos]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[pr_CargarProductos]
as
begin

Select Nombre from Moviles

end
GO
/****** Object:  StoredProcedure [dbo].[pr_ComprobarExistencias]    Script Date: 20/05/2022 12:29:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pr_ComprobarExistencias]
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
/****** Object:  StoredProcedure [dbo].[pr_EditarProductos]    Script Date: 20/05/2022 12:29:29 ******/
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
/****** Object:  StoredProcedure [dbo].[pr_EliminarProductos]    Script Date: 20/05/2022 12:29:29 ******/
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
