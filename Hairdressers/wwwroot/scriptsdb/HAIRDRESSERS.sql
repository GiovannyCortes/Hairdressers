USE [master]
GO
/****** Object:  Database [PELUQUERIAS]    Script Date: 21/03/2023 23:42:26 ******/
CREATE DATABASE [PELUQUERIAS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PELUQUERIAS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PELUQUERIAS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PELUQUERIAS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PELUQUERIAS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
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
/****** Object:  Table [dbo].[ADMINS]    Script Date: 21/03/2023 23:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADMINS](
	[hairdresser_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[role] [tinyint] NOT NULL,
 CONSTRAINT [PK_ADMINS] PRIMARY KEY CLUSTERED 
(
	[hairdresser_id] ASC,
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APPOINTMENT_SERVICES]    Script Date: 21/03/2023 23:42:26 ******/
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
/****** Object:  Table [dbo].[APPOINTMENTS]    Script Date: 21/03/2023 23:42:26 ******/
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
/****** Object:  Table [dbo].[FILES]    Script Date: 21/03/2023 23:42:26 ******/
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
/****** Object:  Table [dbo].[HAIRDRESSERS]    Script Date: 21/03/2023 23:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HAIRDRESSERS](
	[hairdresser_id] [int] NOT NULL,
	[name] [nvarchar](60) NOT NULL,
	[number_phone] [nvarchar](20) NULL,
	[address] [nvarchar](200) NOT NULL,
	[postal_code] [int] NOT NULL,
 CONSTRAINT [PK_HAIRDRESSERS] PRIMARY KEY CLUSTERED 
(
	[hairdresser_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SCHEDULE_ROWS]    Script Date: 21/03/2023 23:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SCHEDULE_ROWS](
	[schedule_row_id] [int] NOT NULL,
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
	[schedule_row_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SCHEDULES]    Script Date: 21/03/2023 23:42:26 ******/
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
/****** Object:  Table [dbo].[SERVICES]    Script Date: 21/03/2023 23:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SERVICES](
	[service_id] [int] NOT NULL,
	[hairdresser_id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[price] [decimal](5, 2) NOT NULL,
	[daprox] [tinyint] NOT NULL,
 CONSTRAINT [PK_SERVICES] PRIMARY KEY CLUSTERED 
(
	[service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 21/03/2023 23:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[user_id] [int] NOT NULL,
	[password] [varbinary](max) NOT NULL,
	[password_read] [nvarchar](50) NOT NULL,
	[salt] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[email_validated] [bit] NOT NULL,
	[last_name] [nvarchar](50) NULL,
	[name] [nvarchar](50) NOT NULL,
	[number_phone] [nvarchar](20) NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[ADMINS] ([hairdresser_id], [user_id], [role]) VALUES (6, 1, 1)
GO
INSERT [dbo].[APPOINTMENT_SERVICES] ([appointment_id], [service_id]) VALUES (1, 10)
INSERT [dbo].[APPOINTMENT_SERVICES] ([appointment_id], [service_id]) VALUES (1, 13)
INSERT [dbo].[APPOINTMENT_SERVICES] ([appointment_id], [service_id]) VALUES (2, 11)
GO
INSERT [dbo].[APPOINTMENTS] ([appointment_id], [user_id], [hairdresser_id], [date], [time]) VALUES (1, 1, 6, CAST(N'2023-03-30' AS Date), CAST(N'16:00:00' AS Time))
INSERT [dbo].[APPOINTMENTS] ([appointment_id], [user_id], [hairdresser_id], [date], [time]) VALUES (2, 1, 6, CAST(N'2023-03-31' AS Date), CAST(N'17:00:00' AS Time))
GO
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (1, N'ESTUDIO 26', NULL, N'C. del Pilar de Zaragoza, 41', 28028)
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (2, N'Blow Dry Bar - Daniele Sigigliano - Peluquería en ', N'910096594', N'C. de Pelayo, 76', 28004)
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (3, N'Peluquería Studio C', N'915312041', N'Calle del Almte., 22', 28004)
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (4, N'The MadRoom | Peluquería de Lujo en Madrid', N'914268193', N'C/ de Villalar, 1', 28001)
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (5, N'Salón 13 Madrid', N'611656557', N'C. de Calatrava, 13', 28005)
INSERT [dbo].[HAIRDRESSERS] ([hairdresser_id], [name], [number_phone], [address], [postal_code]) VALUES (6, N'El Haddadi - Peluquería Caballeros', N'918 51 34 56', N'C/. Travesía de la Venta 1 Local Nº 2', 28400)
GO
INSERT [dbo].[SCHEDULE_ROWS] ([schedule_row_id], [schedule_id], [start_time], [end_time], [monday], [tuesday], [wednesday], [thursday], [friday], [saturday], [sunday]) VALUES (1, 1, CAST(N'08:30:00' AS Time), CAST(N'14:00:00' AS Time), 1, 1, 1, 1, 1, 0, 0)
INSERT [dbo].[SCHEDULE_ROWS] ([schedule_row_id], [schedule_id], [start_time], [end_time], [monday], [tuesday], [wednesday], [thursday], [friday], [saturday], [sunday]) VALUES (2, 1, CAST(N'16:00:00' AS Time), CAST(N'21:00:00' AS Time), 1, 1, 1, 1, 1, 0, 0)
INSERT [dbo].[SCHEDULE_ROWS] ([schedule_row_id], [schedule_id], [start_time], [end_time], [monday], [tuesday], [wednesday], [thursday], [friday], [saturday], [sunday]) VALUES (3, 2, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, 0, 1, 0, 1, 0, 1)
GO
INSERT [dbo].[SCHEDULES] ([schedule_id], [hairdresser_id], [name], [active]) VALUES (1, 6, N'Horario General', 1)
INSERT [dbo].[SCHEDULES] ([schedule_id], [hairdresser_id], [name], [active]) VALUES (2, 5, N'Horario General', 1)
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
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (10, 6, N'Corte de pelo caballero', CAST(15.00 AS Decimal(5, 2)), 30)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (11, 6, N'Corte de pelo mujer', CAST(25.00 AS Decimal(5, 2)), 45)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (12, 6, N'Peinado de novia', CAST(60.00 AS Decimal(5, 2)), 90)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (13, 6, N'Tratamiento capilar', CAST(35.00 AS Decimal(5, 2)), 60)
INSERT [dbo].[SERVICES] ([service_id], [hairdresser_id], [name], [price], [daprox]) VALUES (14, 6, N'Tinte de pelo', CAST(40.00 AS Decimal(5, 2)), 60)
GO
INSERT [dbo].[USERS] ([user_id], [password], [password_read], [salt], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (1, 0xB76FBB4B4B9D9E9D1E81A006379ECA35998743903225CEB1C85EC801FA2D536825C1D1243DDC34222F780AA9699C6C860205BB8D1ADF6ED9A3A712DBC83465CF, N'MCSD2023', N'2ÑCÉ~G*m£Ü=,A:¼7{äê,WíÔz0N&Ñï·?§ªÍvôm', N'giovannyandres.cortes@tajamar365.com', 0, N'Cortés Hernández', N'Giovanny', N'628 638 560')
INSERT [dbo].[USERS] ([user_id], [password], [password_read], [salt], [email], [email_validated], [last_name], [name], [number_phone]) VALUES (2, 0xB94F9556C3C6F0F47FAD3849289B327CDBEC18DBA06764912E9B7E5A736B1A3DE6432B5241560030B0C2EA3A7604A41FE7326746FDD7FC0683BADDFAFFC9356F, N'MCSD2023', N'Ü'')HÍRðIêR¸ÚÑù-.&%ºª26Q"G=FVÄ÷G¡]&Ì3ìâ?', N'usuario@gmail.com', 0, N'De Prueba', N'MiUsuario', NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Unique_Email]    Script Date: 21/03/2023 23:42:26 ******/
ALTER TABLE [dbo].[USERS] ADD  CONSTRAINT [Unique_Email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[USERS] ADD  CONSTRAINT [DF_USERS_password_read]  DEFAULT (N'MCSD2023') FOR [password_read]
GO
ALTER TABLE [dbo].[USERS] ADD  CONSTRAINT [DF_USERS_salt]  DEFAULT (N'tajamar') FOR [salt]
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
ALTER TABLE [dbo].[APPOINTMENTS]  WITH CHECK ADD  CONSTRAINT [FK_APPOINTMENTS_HAIRDRESSERS] FOREIGN KEY([hairdresser_id])
REFERENCES [dbo].[HAIRDRESSERS] ([hairdresser_id])
GO
ALTER TABLE [dbo].[APPOINTMENTS] CHECK CONSTRAINT [FK_APPOINTMENTS_HAIRDRESSERS]
GO
ALTER TABLE [dbo].[APPOINTMENTS]  WITH CHECK ADD  CONSTRAINT [FK_APPOINTMENTS_USERS] FOREIGN KEY([user_id])
REFERENCES [dbo].[USERS] ([user_id])
GO
ALTER TABLE [dbo].[APPOINTMENTS] CHECK CONSTRAINT [FK_APPOINTMENTS_USERS]
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
ALTER TABLE [dbo].[SERVICES]  WITH CHECK ADD  CONSTRAINT [FK_SERVICES_HAIRDRESSERS] FOREIGN KEY([hairdresser_id])
REFERENCES [dbo].[HAIRDRESSERS] ([hairdresser_id])
GO
ALTER TABLE [dbo].[SERVICES] CHECK CONSTRAINT [FK_SERVICES_HAIRDRESSERS]
GO
/****** Object:  StoredProcedure [dbo].[SP_COMPARE_ROLE]    Script Date: 21/03/2023 23:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    CREATE PROCEDURE [dbo].[SP_COMPARE_ROLE](@HAIRDRESSER_ID INT, @USER_ID1 INT, @USER_ID2 INT, @RES BIT OUT)
    AS
	    DECLARE @ROLE_1 INT, @ROLE_2 INT;
	    SELECT @ROLE_1 = ADMINS.role FROM ADMINS WHERE ADMINS.[user_id] = @USER_ID1 AND ADMINS.hairdresser_id = @HAIRDRESSER_ID;
	    SELECT @ROLE_2 = ADMINS.role FROM ADMINS WHERE ADMINS.[user_id] = @USER_ID2 AND ADMINS.hairdresser_id = @HAIRDRESSER_ID;
	    IF(@ROLE_1 <= @ROLE_2)
		    SET @RES = 1;
	    ELSE
		    SET @RES = 0;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Propietario = 1, Gerente = 2, Supervisor = 3, Empleado = 4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ADMINS', @level2type=N'COLUMN',@level2name=N'role'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Approximate duration of the service' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SERVICES', @level2type=N'COLUMN',@level2name=N'daprox'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Password para Paco' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'USERS', @level2type=N'COLUMN',@level2name=N'password_read'
GO
USE [master]
GO
ALTER DATABASE [PELUQUERIAS] SET  READ_WRITE 
GO
