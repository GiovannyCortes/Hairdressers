USE [master]
GO
/****** Object:  Database [PELUQUERIAS]    Script Date: 03/03/2023 13:54:28 ******/
CREATE DATABASE [PELUQUERIAS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PELUQUERIAS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESARROLLO\MSSQL\DATA\PELUQUERIAS.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PELUQUERIAS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESARROLLO\MSSQL\DATA\PELUQUERIAS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PELUQUERIAS] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PELUQUERIAS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PELUQUERIAS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET ARITHABORT OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PELUQUERIAS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PELUQUERIAS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PELUQUERIAS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PELUQUERIAS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PELUQUERIAS] SET  MULTI_USER 
GO
ALTER DATABASE [PELUQUERIAS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PELUQUERIAS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PELUQUERIAS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PELUQUERIAS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PELUQUERIAS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PELUQUERIAS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PELUQUERIAS] SET QUERY_STORE = OFF
GO
USE [PELUQUERIAS]
GO
/****** Object:  Table [dbo].[ADMINS]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADMINS](
	[hairdresser_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[role] [varchar](50) NULL,
 CONSTRAINT [PK_ADMINS] PRIMARY KEY CLUSTERED 
(
	[hairdresser_id] ASC,
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APPOINTMENT_SERVICES]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APPOINTMENT_SERVICES](
	[appointment_id] [int] NOT NULL,
	[service_id] [int] NOT NULL,
 CONSTRAINT [PK_APPOINTMENT_SERVICES] PRIMARY KEY CLUSTERED 
(
	[appointment_id] ASC,
	[service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APPOINTMENTS]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APPOINTMENTS](
	[appointment_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[hairdresser_id] [int] NOT NULL,
	[date] [date] NOT NULL,
	[time] [time](0) NOT NULL,
 CONSTRAINT [PK_APPOINTMENTS] PRIMARY KEY CLUSTERED 
(
	[appointment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FILES]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FILES](
	[hairdresser_id] [int] NOT NULL,
	[hairdresser_file_item] [tinyint] NOT NULL,
	[path_name] [nvarchar](50) NOT NULL,
	[alternate_text] [nvarchar](50) NULL,
	[logo_image] [bit] NULL,
 CONSTRAINT [PK_FILES] PRIMARY KEY CLUSTERED 
(
	[hairdresser_id] ASC,
	[hairdresser_file_item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HAIRDRESSERS]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HAIRDRESSERS](
	[hairdresser_id] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[number_phone] [nvarchar](20) NULL,
	[address] [nvarchar](200) NOT NULL,
	[postal_code] [int] NOT NULL,
 CONSTRAINT [PK_HAIRDRESSERS] PRIMARY KEY CLUSTERED 
(
	[hairdresser_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SCHEDULE_ROWS]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SCHEDULE_ROWS](
	[schedule_id] [int] NOT NULL,
	[start_time] [time](0) NOT NULL,
	[end_time] [time](0) NOT NULL,
	[monday] [bit] NOT NULL,
	[tuesday] [bit] NOT NULL,
	[wednesday] [bit] NOT NULL,
	[thursday] [bit] NOT NULL,
	[friday] [bit] NOT NULL,
	[saturday] [bit] NOT NULL,
	[sunday] [bit] NOT NULL,
 CONSTRAINT [PK_SCHEDULE_ROWS] PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC,
	[start_time] ASC,
	[end_time] ASC,
	[monday] ASC,
	[tuesday] ASC,
	[wednesday] ASC,
	[thursday] ASC,
	[friday] ASC,
	[saturday] ASC,
	[sunday] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SCHEDULES]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SCHEDULES](
	[schedule_id] [int] NOT NULL,
	[hairdresser_id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_SCHEDULES] PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SERVICES]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SERVICES](
	[service_id] [int] NOT NULL,
	[hairdresser_id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[price] [decimal](5, 2) NOT NULL,
	[daprox] [tinyint] NULL,
 CONSTRAINT [PK_SERVICES] PRIMARY KEY CLUSTERED 
(
	[service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[user_id] [int] NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[email_validated] [bit] NOT NULL,
	[last_name] [nvarchar](50) NULL,
	[name] [nvarchar](50) NOT NULL,
	[number_phone] [nvarchar](20) NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ADMINS] ([hairdresser_id], [user_id], [role]) VALUES (1, 1, NULL)
INSERT [dbo].[ADMINS] ([hairdresser_id], [user_id], [role]) VALUES (2, 1, NULL)
INSERT [dbo].[ADMINS] ([hairdresser_id], [user_id], [role]) VALUES (3, 1, NULL)
INSERT [dbo].[ADMINS] ([hairdresser_id], [user_id], [role]) VALUES (4, 1, NULL)
INSERT [dbo].[ADMINS] ([hairdresser_id], [user_id], [role]) VALUES (5, 1, NULL)
GO
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (1, N'ESTUDIO 26', NULL, N'C. del Pilar de Zaragoza, 41', 28028)
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (2, N'Blow Dry Bar - Daniele Sigigliano - Peluquería en ', N'910096594', N'C. de Pelayo, 76', 28004)
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (3, N'Peluquería Studio C', N'915312041', N'Calle del Almte., 22', 28004)
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (4, N'The MadRoom | Peluquería de Lujo en Madrid', N'914268193', N'C/ de Villalar, 1', 28001)
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (5, N'Salón 13 Madrid', N'611656557', N'C. de Calatrava, 13', 28005)
GO
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (1, 1, N'Corte de pelo básico', CAST(9.95 AS Decimal(5, 2)), 25)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (2, 1, N'Coloración completa sin amoniaco y lavado para muj', CAST(70.00 AS Decimal(5, 2)), 80)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (3, 1, N'Corte infantil, lavado y peinado', CAST(30.00 AS Decimal(5, 2)), 15)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (4, 1, N'Lavado y peinado de pelo largo', CAST(24.00 AS Decimal(5, 2)), 60)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (5, 1, N'Corte de flequillo en seco', CAST(15.00 AS Decimal(5, 2)), 15)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (6, 1, N'Tratamiento reconstructor', CAST(55.00 AS Decimal(5, 2)), 80)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (7, 1, N'Tratamiento capilar de keratina con aminoácidos', CAST(45.00 AS Decimal(5, 2)), 30)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (8, 1, N'Semirecogido de pelo medio', CAST(42.00 AS Decimal(5, 2)), 60)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (9, 1, N'Mechas classic y lavado para pelo medio', CAST(65.00 AS Decimal(5, 2)), 210)
GO
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (1, N'giovannycortes@gmail.com', 0, N'Cortés Hernández', N'Giovanny', N'445781051')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (2, N'carlosmayoralalvarez@gmail.com', 0, N'Mayoral Álvarez', N'Carlos', N'197479627')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (3, N'felixmatinexbendicho@gmail.com', 0, N'Martínez Bendicho', N'Felix', N'679720939')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (4, N'alvarogutierrez@gmail.com', 0, N'Gutierrez Carnicero', N'Álvaro', N'135603819')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (5, N'javierpantojaleon@gmail.com', 0, N'Pantoja León', N'Javier', N'819566439')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (6, N'raulgarciamoratinos@gmail.com', 0, N'García Moratinos', N'Raúl', N'569432535')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (7, N'fernandosartoriuscarreras@gmail.com', 0, N'Sartorius Carreras', N'Fernando', N'478049430')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (8, N'andreigarbea@gmail.com', 0, N'Garbea', N'Andrei', N'386243996')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (9, N'ddominguez@gmail.com', 0, N'Domínguez Blanco', N'David', N'116160313')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (10, N'sandrasevillejagonzalez@gmail.com', 0, N'Sevilleja González', N'Sandra', N'686491344')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (11, N'fernandogarciagarrido@gmail.com', 0, N'García Garrido', N'Fernando', N'873507806')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (12, N'miguelguerrabarriales@gmail.com', 0, N'Guerra Barriales', N'Miguel', N'976682624')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (13, N'sergioguijarrodecabo@gmail.com', 0, N'Guijarro de Cabo', N'Sergio', N'108748872')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (14, N'juancarlosmarañongallego@gmail.com', 0, N'Marañón Gallego', N'Juan Carlos', N'210560241')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (15, N'xiangzhouxiang@gmail.com', 0, N'Zhou Xiang', N'Xiang', N'916575847')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (16, N'aaronestradanieto@gmail.com', 0, N'Estrada Nieto', N'Aarón', N'483367155')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (17, N'josepesoavillarroel@gmail.com', 0, N'Pesoa Villarroel', N'José', N'775822957')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (18, N'saraalvarezlopez@gmail.com', 0, N'Álvarez López', N'Sara', N'461129703')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (19, N'sergioalcazarmonedero@gmail.com', 0, N'Alcázar Monedero', N'Sergio', N'373600425')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (20, N'nazarromanyuk@gmail.com', 0, N'Romanyuk', N'Nazar', N'637210405')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (21, N'jaimecalderónacero@gmail.com', 0, N'Calderón Acero', N'Jaime', N'098210181')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (22, N'enriquegarciapalacios@gmail.com', 0, N'García Palacios', N'Enrique', N'351612654')
INSERT [dbo].[USERS] ([user_id], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (23, N'jorgesalinerosanchez@gmail.com', 0, N'Salinero Sánchez', N'Jorge', N'515236376')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Unique_Email]    Script Date: 03/03/2023 13:54:29 ******/
ALTER TABLE [dbo].[USERS] ADD  CONSTRAINT [Unique_Email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[USERS] ADD  CONSTRAINT [DF_USERS_email_validated]  DEFAULT ((0)) FOR [email_validated]
GO
ALTER TABLE [dbo].[ADMINS]  WITH CHECK ADD  CONSTRAINT [FK_ADMINS_HAIRDRESSERS1] FOREIGN KEY([hairdresser_id])
REFERENCES [dbo].[HAIRDRESSERS] ([hairdresser_id])
GO
ALTER TABLE [dbo].[ADMINS] CHECK CONSTRAINT [FK_ADMINS_HAIRDRESSERS1]
GO
ALTER TABLE [dbo].[ADMINS]  WITH CHECK ADD  CONSTRAINT [FK_ADMINS_USERS] FOREIGN KEY([user_id])
REFERENCES [dbo].[USERS] ([user_id])
GO
ALTER TABLE [dbo].[ADMINS] CHECK CONSTRAINT [FK_ADMINS_USERS]
GO
ALTER TABLE [dbo].[APPOINTMENT_SERVICES]  WITH CHECK ADD  CONSTRAINT [FK_APPOINTMENT_SERVICES_APPOINTMENTS] FOREIGN KEY([appointment_id])
REFERENCES [dbo].[APPOINTMENTS] ([appointment_id])
GO
ALTER TABLE [dbo].[APPOINTMENT_SERVICES] CHECK CONSTRAINT [FK_APPOINTMENT_SERVICES_APPOINTMENTS]
GO
ALTER TABLE [dbo].[APPOINTMENT_SERVICES]  WITH CHECK ADD  CONSTRAINT [FK_APPOINTMENT_SERVICES_SERVICES1] FOREIGN KEY([service_id])
REFERENCES [dbo].[SERVICES] ([service_id])
GO
ALTER TABLE [dbo].[APPOINTMENT_SERVICES] CHECK CONSTRAINT [FK_APPOINTMENT_SERVICES_SERVICES1]
GO
ALTER TABLE [dbo].[FILES]  WITH CHECK ADD  CONSTRAINT [FK_FILES_HAIRDRESSERS] FOREIGN KEY([hairdresser_id])
REFERENCES [dbo].[HAIRDRESSERS] ([hairdresser_id])
GO
ALTER TABLE [dbo].[FILES] CHECK CONSTRAINT [FK_FILES_HAIRDRESSERS]
GO
ALTER TABLE [dbo].[SCHEDULE_ROWS]  WITH CHECK ADD  CONSTRAINT [FK_SCHEDULE_ROWS_SCHEDULES] FOREIGN KEY([schedule_id])
REFERENCES [dbo].[SCHEDULES] ([schedule_id])
GO
ALTER TABLE [dbo].[SCHEDULE_ROWS] CHECK CONSTRAINT [FK_SCHEDULE_ROWS_SCHEDULES]
GO
ALTER TABLE [dbo].[SCHEDULES]  WITH CHECK ADD  CONSTRAINT [FK_SCHEDULES_HAIRDRESSERS] FOREIGN KEY([hairdresser_id])
REFERENCES [dbo].[HAIRDRESSERS] ([hairdresser_id])
GO
ALTER TABLE [dbo].[SCHEDULES] CHECK CONSTRAINT [FK_SCHEDULES_HAIRDRESSERS]
GO
/****** Object:  StoredProcedure [dbo].[SP_ADMINS_DELETE]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ADMINS_DELETE](@HAIRDRESSER_ID INT, @USER_ID INT, @STATUS TINYINT OUT)
AS
	DECLARE @ADMINS INT;
	SELECT @ADMINS = COUNT(*) FROM ADMINS WHERE hairdresser_id = @HAIRDRESSER_ID;
	IF(@ADMINS > 1) /* Todavía quedan administradores, se puede realizar el borrado */
		BEGIN
			DELETE FROM ADMINS WHERE hairdresser_id = @HAIRDRESSER_ID AND [user_id] = @USER_ID;
			SET @STATUS = 1;
		END;
	ELSE
		BEGIN
			SET @STATUS = 4;
		END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ADMINS_INSERT]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ADMINS_INSERT](@HAIRDRESSER_ID INT, @USER_ID INT, @RANGE INT)
AS
	IF(@RANGE = 1) /* SI EL NUEVO RANGO ES PROPIETARIO, REVISAMOS LA BBDD */
		BEGIN
			DECLARE @ROOT BIT;
			SELECT @ROOT = COUNT([user_id]) FROM ADMINS WHERE ADMINS.aux2_range_id = 1 AND ADMINS.hairdresser_id = 1;
			IF(@ROOT = 1) /* SI EXISTE YA UN PROPIETARIO SE CANCELA LA INSERCCIÓN */
				BEGIN
					RETURN -1;
				END;
			ELSE /* NO HAY PROPIETARIO, EL PUESTO SE PUEDE OCUPAR */
				BEGIN
					INSERT INTO ADMINS VALUES(@HAIRDRESSER_ID, @USER_ID, 12, 1);
				END;
		END;
	ELSE /* CUALQUIER OTRO RANGO ES ADMITIDO DIRECTAMENTE */
		BEGIN
			INSERT INTO ADMINS VALUES(@HAIRDRESSER_ID, @USER_ID, 12, @RANGE);
		END;
GO
/****** Object:  StoredProcedure [dbo].[SP_HAIRDRESSERS_DELETE]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_HAIRDRESSERS_DELETE](@HAIRDRESSER_ID INT)
AS
	DELETE FROM HAIRDRESSERS WHERE hairdresser_id=@HAIRDRESSER_ID;
GO
/****** Object:  StoredProcedure [dbo].[SP_HAIRDRESSERS_GET_NEXTID]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_HAIRDRESSERS_GET_NEXTID](@NEXTID INT OUT)
AS
	SELECT @NEXTID = ISNULL(MAX(HAIRDRESSERS.hairdresser_id),0)+1 FROM HAIRDRESSERS 
GO
/****** Object:  StoredProcedure [dbo].[SP_HAIRDRESSERS_INSERT]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_HAIRDRESSERS_INSERT]
(@NAME NVARCHAR(50), @PHONE NVARCHAR(20), @ADDRESS NVARCHAR(50), @CP INT, @LOCATION NVARCHAR(50), @LOCID INT, @USERID INT)
AS
	DECLARE @NEXTID INT;
	EXEC SP_HAIRDRESSERS_GET_NEXTID @NEXTID OUT;
	INSERT INTO HAIRDRESSERS VALUES(@NEXTID, @NAME, @PHONE, @ADDRESS, @CP, @LOCATION, @LOCID);
	INSERT INTO ADMINS VALUES(@NEXTID, @USERID, 12, 1)
GO
/****** Object:  StoredProcedure [dbo].[SP_HAIRDRESSERS_UPDATE]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_HAIRDRESSERS_UPDATE]
(@HAIRDRESSER_ID INT, @NAME NVARCHAR(50), @PHONE NVARCHAR(20), @ADDRESS NVARCHAR(50), @CP INT, @LOCATION NVARCHAR(50), @LOCID INT)
AS
	UPDATE HAIRDRESSERS SET hairdresser_id=@HAIRDRESSER_ID, [name]=@NAME, number_phone=@PHONE, 
							[address]=@ADDRESS, postal_code=@CP, [location]=@LOCATION, location_id=@LOCID;
GO
/****** Object:  StoredProcedure [dbo].[SP_SCHEDULES_DELETE]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SCHEDULES_DELETE](@SCHEDULE_ID INT)
AS
	DELETE FROM SCHEDULES WHERE schedule_id = @SCHEDULE_ID
GO
/****** Object:  StoredProcedure [dbo].[SP_SCHEDULES_GET_NEXTID]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SCHEDULES_GET_NEXTID](@NEXTID INT OUT)
AS
	SELECT @NEXTID = ISNULL(MAX(SCHEDULE_ID), 0) +1 FROM SCHEDULES
GO
/****** Object:  StoredProcedure [dbo].[SP_SCHEDULES_INSERT]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SCHEDULES_INSERT](@HAIRDRESSER_ID INT, @NAME NVARCHAR(50), @ACTIVE BIT, @ASSIGNED_ID INT OUT)
AS
	DECLARE @NEXTID INT;
	EXEC SP_SCHEDULE_GET_NEXTID @NEXTID OUT;

	IF(@ACTIVE = 1)
	BEGIN
		UPDATE SCHEDULES SET ACTIVE = 0 WHERE hairdresser_id = @HAIRDRESSER_ID;
	END;

	INSERT INTO SCHEDULES VALUES(@NEXTID, @HAIRDRESSER_ID, @NAME, @ACTIVE);
	SET @ASSIGNED_ID = @NEXTID;
GO
/****** Object:  StoredProcedure [dbo].[SP_SCHEDULES_UPDATE]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SCHEDULES_UPDATE](@SCHEDULE_ID INT, @HAIRDRESSER_ID INT, @NAME NVARCHAR(50), @ACTIVE BIT)
AS
	IF(@ACTIVE = 1)
	BEGIN
		UPDATE SCHEDULES SET active = 0 WHERE hairdresser_id = @HAIRDRESSER_ID AND active = 1;
	END;

	UPDATE SCHEDULES SET [name] = @NAME, active = @ACTIVE
	WHERE schedule_id = @SCHEDULE_ID
GO
/****** Object:  StoredProcedure [dbo].[SP_SERVICES_DELETE]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SERVICES_DELETE](@SERVICE_ID INT)
AS
	DELETE FROM [SERVICES] WHERE service_id = @SERVICE_ID
GO
/****** Object:  StoredProcedure [dbo].[SP_SERVICES_GET_NEXTID]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SERVICES_GET_NEXTID](@NEXTID INT OUT)
AS
	SELECT @NEXTID = ISNULL(MAX(SERVICE_ID), 0)+1 FROM [SERVICES]
GO
/****** Object:  StoredProcedure [dbo].[SP_SERVICES_INSERT]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SERVICES_INSERT](@HAIRDRESSER_ID INT, @NAME NVARCHAR(50), @PRICE DECIMAL(5,2), @DAPROX TINYINT)
AS
	DECLARE @NEXTID INT;
	EXEC SP_SERVICES_GET_NEXTID @NEXTID OUT;
	INSERT INTO [SERVICES] VALUES(@NEXTID, @HAIRDRESSER_ID, @NAME, @PRICE, @DAPROX);
GO
/****** Object:  StoredProcedure [dbo].[SP_USERS_DELETE]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_USERS_DELETE](@USER_ID INT, @STATUS TINYINT OUT)
AS
	DECLARE @ADMIN BIT;
	SELECT @ADMIN = COUNT([USER_ID]) FROM ADMINS WHERE [user_id] = @USER_ID;
	IF(@ADMIN = 0) /* EL USUARIO ES ADMINISTRADOR DE UNA O MÁS PELUQUERÍAS */
		BEGIN
			SET @STATUS = 2;
		END;
	ELSE /* PROCEDEMOS A LA ELIMINACIÓN DEL USUARIO */
		BEGIN
			DELETE FROM USERS WHERE [user_id] = @USER_ID;
			SET @STATUS = 1;
		END;
GO
/****** Object:  StoredProcedure [dbo].[SP_USERS_GET_NEXTID]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_USERS_GET_NEXTID] (@USER_ID INT OUT)
AS
	SELECT @USER_ID = ISNULL(MAX([USER_ID]),0)+1 FROM USERS
GO
/****** Object:  StoredProcedure [dbo].[SP_USERS_UPDATE]    Script Date: 03/03/2023 13:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_USERS_UPDATE](@USERID INT, @EMAIL NVARCHAR(50), @LASTNAME NVARCHAR(50), @NAME NVARCHAR(50), @PHONE NVARCHAR(20), @LOCID INT)
AS
	UPDATE USERS SET EMAIL=@EMAIL, LAST_NAME=@LASTNAME, [NAME]=@NAME, NUMBER_PHONE=@PHONE, LOCATION_ID=@LOCID
	WHERE [USER_ID] = @USERID;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Approximate duration of the service' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SERVICES', @level2type=N'COLUMN',@level2name=N'daprox'
GO
USE [master]
GO
ALTER DATABASE [PELUQUERIAS] SET  READ_WRITE 
GO
