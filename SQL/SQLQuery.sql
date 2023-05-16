USE [master]
GO
/****** Object:  Database [Manager]    Script Date: 08/05/2023 4:46:25 PM ******/
CREATE DATABASE [Manager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Manager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\Manager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Manager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\Manager_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Manager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Manager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Manager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Manager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Manager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Manager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Manager] SET ARITHABORT OFF 
GO
ALTER DATABASE [Manager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Manager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Manager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Manager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Manager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Manager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Manager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Manager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Manager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Manager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Manager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Manager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Manager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Manager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Manager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Manager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Manager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Manager] SET RECOVERY FULL 
GO
ALTER DATABASE [Manager] SET  MULTI_USER 
GO
ALTER DATABASE [Manager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Manager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Manager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Manager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Manager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Manager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Manager', N'ON'
GO
ALTER DATABASE [Manager] SET QUERY_STORE = ON
GO
ALTER DATABASE [Manager] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Manager]
GO
/****** Object:  Table [dbo].[Allowance]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Allowance](
	[Id_allowance] [int] IDENTITY(1,1) NOT NULL,
	[Id_u] [int] NULL,
	[AllowanceAmount] [decimal](18, 2) NULL,
	[CreateMonth] [date] NULL,
 CONSTRAINT [PK_Allowance] PRIMARY KEY CLUSTERED 
(
	[Id_allowance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attendane]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendane](
	[id_a] [int] IDENTITY(1,1) NOT NULL,
	[AttendaneDate] [date] NULL,
	[Checkin] [time](7) NULL,
	[Checkout] [time](7) NULL,
	[id_u] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_a] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id_d] [int] IDENTITY(1,1) NOT NULL,
	[Department] [nvarchar](100) NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id_d] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[form]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[form](
	[Soform] [int] IDENTITY(1,1) NOT NULL,
	[id_f] [date] NULL,
	[ID] [varchar](20) NOT NULL,
	[id_u] [int] NOT NULL,
	[TimeStart] [varchar](50) NOT NULL,
	[TineEnd] [varchar](50) NOT NULL,
	[thongso] [float] NOT NULL,
	[TrangThai] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Soform] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payoffs]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payoffs](
	[Id_pay] [int] IDENTITY(1,1) NOT NULL,
	[Id_u] [int] NULL,
	[Payoff] [decimal](18, 2) NULL,
	[PayoffDate] [date] NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Payoffs] PRIMARY KEY CLUSTERED 
(
	[Id_pay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[Id_position] [int] IDENTITY(1,1) NOT NULL,
	[Position] [nvarchar](100) NULL,
	[coefficient] [decimal](4, 2) NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[Id_position] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id_r] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id_r] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Salary]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Salary](
	[Id_salary] [int] IDENTITY(1,1) NOT NULL,
	[BasicSalary] [decimal](18, 2) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Id_u] [int] NULL,
 CONSTRAINT [PK_Salary] PRIMARY KEY CLUSTERED 
(
	[Id_salary] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[id_ur] [int] IDENTITY(1,1) NOT NULL,
	[Id_u] [int] NOT NULL,
	[Id_r] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_ur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id_u] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[PassWord] [nvarchar](50) NULL,
	[FullName] [nvarchar](100) NULL,
	[Adress] [nvarchar](500) NULL,
	[PhoneNumber] [int] NULL,
	[Gender] [nvarchar](50) NULL,
	[StartDate] [date] NULL,
	[Id_d] [int] NULL,
	[Id_position] [int] NULL,
	[IdCard] [decimal](15, 0) NULL,
	[Status] [int] NULL,
	[Avatar] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id_u] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Allowance]  WITH CHECK ADD  CONSTRAINT [FK_Allowance_Users] FOREIGN KEY([Id_u])
REFERENCES [dbo].[Users] ([Id_u])
GO
ALTER TABLE [dbo].[Allowance] CHECK CONSTRAINT [FK_Allowance_Users]
GO
ALTER TABLE [dbo].[Attendane]  WITH CHECK ADD  CONSTRAINT [FK_At_NV] FOREIGN KEY([id_u])
REFERENCES [dbo].[Users] ([Id_u])
GO
ALTER TABLE [dbo].[Attendane] CHECK CONSTRAINT [FK_At_NV]
GO
ALTER TABLE [dbo].[form]  WITH CHECK ADD  CONSTRAINT [FK_F_Us] FOREIGN KEY([id_u])
REFERENCES [dbo].[Users] ([Id_u])
GO
ALTER TABLE [dbo].[form] CHECK CONSTRAINT [FK_F_Us]
GO
ALTER TABLE [dbo].[Payoffs]  WITH CHECK ADD  CONSTRAINT [FK_Payoffs_Users] FOREIGN KEY([Id_u])
REFERENCES [dbo].[Users] ([Id_u])
GO
ALTER TABLE [dbo].[Payoffs] CHECK CONSTRAINT [FK_Payoffs_Users]
GO
ALTER TABLE [dbo].[Salary]  WITH CHECK ADD  CONSTRAINT [FK_Salary_Users] FOREIGN KEY([Id_u])
REFERENCES [dbo].[Users] ([Id_u])
GO
ALTER TABLE [dbo].[Salary] CHECK CONSTRAINT [FK_Salary_Users]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD FOREIGN KEY([Id_r])
REFERENCES [dbo].[Roles] ([Id_r])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK__UserRole__Id_u__45F365D3] FOREIGN KEY([Id_u])
REFERENCES [dbo].[Users] ([Id_u])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK__UserRole__Id_u__45F365D3]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Departments] FOREIGN KEY([Id_d])
REFERENCES [dbo].[Departments] ([Id_d])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Departments]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Position] FOREIGN KEY([Id_position])
REFERENCES [dbo].[Position] ([Id_position])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Position]
GO
/****** Object:  StoredProcedure [dbo].[sp_tblAttendance_Total]    Script Date: 08/05/2023 4:46:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tblAttendance_Total]
	-- Add the parameters for the stored procedure here
AS
BEGIN
DECLARE @startAM time = '08:05:00';
DECLARE @startPM time = '13:05:00';
DECLARE @endAM time = '12:05:00';
DECLARE @endPM time = '17:05:00';
DECLARE @breaks int = DATEDIFF(MINUTE, '00:00:00', '01:00:00');
DECLARE @midAM time = '09:00:00';
DECLARE @midPM time = '14:00:00';
DECLARE @null time = '00:00:00';

select 
  a.Id_a, 
  a.Id_u,
  u.FullName, 
  a.Checkin, 
  a.Checkout, 
  CASE 
  WHEN 
  a.Checkin IS NULL 
  AND a.Checkout IS NULL THEN @null 
  WHEN a.Checkin IS NULL 
  OR a.Checkout IS NULL THEN @null 
  ELSE 
  CASE 
  -- -----lửng lơ----------
  when(a.Checkin> @midAM and a.Checkout < @midPM)then @null
  when (a.Checkin <= @startAM and a.Checkout<=@midAM ) then @null
  when ( a.Checkin>= @midPM)then @null
  -- --------------------------cả ngày--------------------------------------
  -- vào <8h ra >17h  =>>>>>>>>>>>>>> đầy đủ
  WHEN ( 
    a.Checkin <= @startAM 
    AND a.Checkout >= @endPM
  ) THEN 
  CONVERT(time(0),DATEADD(minute,datediff(MINUTE,@startAM,@startPM)-@breaks,0),108)
  -- vào < 8h ra sau 15h30 trước 17h = về sớm -----------
  WHEN (
    a.Checkin <= @startAM  
    AND a.Checkout >= @midPM 
    AND a.Checkout <= @endPM
  ) THEN   CONVERT(time(0),DATEADD(minute,datediff(MINUTE,@startAM,a.Checkout)-@breaks,0),108)

  -- vào >8h - 9h30 ra >17h =>>>>>>>>>>>>>> đi muộn 
  WHEN (
    a.Checkin > @startAM  
    AND a.Checkin <= @midAM
    AND a.Checkout >= @endPM
  ) THEN   CONVERT(time(0),DATEADD(minute,datediff(MINUTE,a.Checkin,@startPM)-@breaks,0),108)

  
  -- vào 8h-9h30 ra 15h30-17h =>>>>>>>>>>>>>> đi muộn về sớm
  WHEN (
    a.Checkin > @startAM  
    AND a.Checkin <= @midAM 
    AND a.Checkout >= @midPM
    AND a.Checkout < @endPM
  ) THEN CONVERT(time(0),DATEADD(minute,datediff(MINUTE,a.Checkin,a.Checkout)-@breaks,0),108)
  ----------- -----------------ca sáng -----------------------------------
  -- vào <8h ra 12h-15h =>>>>>>>>>>>>>>>>>>>>>>>>> đủ công  buổi sáng = 4 giờ
  WHEN (
    a.Checkin <= @startAM 
    AND a.Checkout >= @endAM 
    AND a.Checkout < @midPM
  ) THEN   
  CONVERT(time(0),DATEADD(minute,datediff(MINUTE,@startAM,@endAM),0),108)
  
  -- vào <8h ra 9h30-12h00 =>>>>>>>>>>>>>>>>>> về sớm sáng
  WHEN (
    a.Checkin <= @startAM  
    AND a.Checkout > @midAM 
    AND a.Checkout < @endAM
  ) THEN 
  CONVERT(time(0),DATEADD(minute,datediff(MINUTE,@startAM,a.Checkout),0),108)
  
  -- vào 8h-9h30 ra 12h-15h30 =>>>>>>>>>>>>>>>>>>>> sáng đi muộn
  WHEN (
    Checkin > @startAM  
    AND a.Checkin <= @midAM 
    AND a.Checkout >= @endAM
    AND a.Checkout <= @midPM
  ) THEN 
  CONVERT(time(0),DATEADD(minute,datediff(MINUTE,a.Checkin,@endAM),0),108)

  -- vào 8h-9h30 ra 9h30-12h = đi muộn về sớm
  WHEN (
    a.Checkin > @startAM  
    AND a.Checkin <= @midAM 
    AND a.Checkout > @midAM
    AND a.Checkout < @endAM
  ) THEN 
  CONVERT(time(0),DATEADD(minute,datediff(MINUTE,a.Checkin,a.Checkout),0),108)
  ------------------- Buổi chiều ------------------
  -- vào 9h30-13h05 về >17h05 =>>>>>>>>>>>>>>>>>>>>>>>>>> đủ công chiều = 4h
  WHEN (
    a.Checkin > @midAM  
    AND a.Checkin <= @startPM 
    AND a.Checkout > @endPM
  ) THEN 
  CONVERT(time(0),DATEADD(minute,datediff(MINUTE,@startPM,@endPM),0),108)
  ---------vào 9h30 -13h05 về 15h30-17h30 =>>>>>>>>>>>>>>>> chiều về sớm
  WHEN (
    a.Checkin > @midAM  
    AND a.Checkin <= @startPM 
    AND a.Checkout >= @midPM
    AND a.Checkout < @endPM
  ) THEN 
  CONVERT(time(0),DATEADD(minute,datediff(MINUTE,@startAM,a.Checkout),0),108)
  ----------------vào 13h05 - 15h30 về >= 17h05 =>>>>>>>>>>>>>>>>>> chiều đi muộn
  WHEN (
    a.Checkin > @startPM  
    AND a.Checkin <= @midPM 
    AND a.Checkout >= @endPM
  ) THEN 
  CONVERT(time(0),DATEADD(minute,datediff(MINUTE,a.Checkin,@endPM),0),108)
  ------------vào 13h05 - 15h50 về 15h05 - 17h05 =>>>>>>>>>>>>>>>>>>>>>>> đã làm ca chiều còn đi muộn về sớm
  WHEN (
    a.Checkin > @startPM  
    AND a.Checkin < @midPM 
    AND a.Checkout >= @midPM
    AND a.Checkout < @endPM
  ) THEN 
  CONVERT(time(0),DATEADD(minute,datediff(MINUTE,a.Checkin,a.Checkout),0),108)
  ELSE @null end END as totakwork 
FROM 
  Attendane a --WHERE Id_a = 1
  join Users u on u.Id_u = a.Id_u
ORDER BY 
  AttendaneDate ASC


END
GO
USE [master]
GO
ALTER DATABASE [Manager] SET  READ_WRITE 
GO
