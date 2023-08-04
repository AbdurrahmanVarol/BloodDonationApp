USE [master]
GO
/****** Object:  Database [BloodDonationDb]    Script Date: 4.08.2023 10:45:23 ******/
CREATE DATABASE [BloodDonationDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BloodDonationDb', FILENAME = N'C:\Users\User\BloodDonationDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BloodDonationDb_log', FILENAME = N'C:\Users\User\BloodDonationDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BloodDonationDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BloodDonationDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BloodDonationDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BloodDonationDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BloodDonationDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BloodDonationDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BloodDonationDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [BloodDonationDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BloodDonationDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BloodDonationDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BloodDonationDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BloodDonationDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BloodDonationDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BloodDonationDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BloodDonationDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BloodDonationDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BloodDonationDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BloodDonationDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BloodDonationDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BloodDonationDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BloodDonationDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BloodDonationDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BloodDonationDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BloodDonationDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BloodDonationDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BloodDonationDb] SET  MULTI_USER 
GO
ALTER DATABASE [BloodDonationDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BloodDonationDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BloodDonationDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BloodDonationDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BloodDonationDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BloodDonationDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BloodDonationDb] SET QUERY_STORE = OFF
GO
USE [BloodDonationDb]
GO
/****** Object:  Table [dbo].[BloodGroups]    Script Date: 4.08.2023 10:45:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BloodGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Symbol] [nvarchar](max) NULL,
 CONSTRAINT [PK_BloodGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 4.08.2023 10:45:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Plate] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genders]    Script Date: 4.08.2023 10:45:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Genders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hospitals]    Script Date: 4.08.2023 10:45:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hospitals](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[CityId] [int] NOT NULL,
 CONSTRAINT [PK_Hospitals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requests]    Script Date: 4.08.2023 10:45:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[Id] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[BloodGroupId] [int] NOT NULL,
	[HospitalId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4.08.2023 10:45:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4.08.2023 10:45:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[GenderId] [int] NOT NULL,
	[CityId] [int] NOT NULL,
	[BloodGroupId] [int] NOT NULL,
	[HospitalId] [uniqueidentifier] NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BloodGroups] ON 
GO
INSERT [dbo].[BloodGroups] ([Id], [Name], [Symbol]) VALUES (1, N'A Rh Pozitif', N'A+')
GO
INSERT [dbo].[BloodGroups] ([Id], [Name], [Symbol]) VALUES (2, N'A Rh Negatif', N'A-')
GO
INSERT [dbo].[BloodGroups] ([Id], [Name], [Symbol]) VALUES (3, N'B Rh Pozitif', N'B+')
GO
INSERT [dbo].[BloodGroups] ([Id], [Name], [Symbol]) VALUES (4, N'B Rh Negatif', N'B-')
GO
INSERT [dbo].[BloodGroups] ([Id], [Name], [Symbol]) VALUES (5, N'AB Rh Pozitif', N'AB+')
GO
INSERT [dbo].[BloodGroups] ([Id], [Name], [Symbol]) VALUES (6, N'AB Rh Negatif', N'AB-')
GO
INSERT [dbo].[BloodGroups] ([Id], [Name], [Symbol]) VALUES (7, N'0 Rh Pozitif', N'0+')
GO
INSERT [dbo].[BloodGroups] ([Id], [Name], [Symbol]) VALUES (8, N'0 Rh Negatif', N'0-')
GO
SET IDENTITY_INSERT [dbo].[BloodGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[Cities] ON 
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (1, N'Adana', N'01')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (2, N'Adıyaman', N'02')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (3, N'Afyonkarahisar', N'03')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (4, N'Ağrı', N'04')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (5, N'Aksaray', N'68')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (6, N'Amasya', N'05')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (7, N'Ankara', N'06')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (8, N'Antalya', N'07')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (9, N'Ardahan', N'75')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (10, N'Artvin', N'08')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (11, N'Aydın', N'09')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (12, N'Balıkesir', N'10')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (13, N'Bartın', N'74')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (14, N'Batman', N'72')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (15, N'Bayburt', N'69')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (16, N'Bilecik', N'11')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (17, N'Bingöl', N'12')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (18, N'Bitlis', N'13')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (19, N'Bolu', N'14')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (20, N'Burdur', N'15')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (21, N'Bursa', N'16')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (22, N'Çanakkale', N'17')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (23, N'Çankırı', N'18')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (24, N'Çorum', N'19')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (25, N'Denizli', N'20')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (26, N'Diyarbakır', N'21')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (27, N'Düzce', N'81')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (28, N'Edirne', N'22')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (29, N'Elazığ', N'23')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (30, N'Erzincan', N'24')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (31, N'Erzurum', N'25')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (32, N'Eskişehir', N'26')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (33, N'Gaziantep', N'27')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (34, N'Giresun', N'28')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (35, N'Gümüşhane', N'29')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (36, N'Hakkâri', N'30')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (37, N'Hatay', N'31')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (38, N'Iğdır', N'76')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (39, N'Isparta', N'32')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (40, N'İstanbul', N'34')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (41, N'İzmir', N'35')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (42, N'Kahramanmaraş', N'46')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (43, N'Karabük', N'78')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (44, N'Karaman', N'70')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (45, N'Kars', N'36')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (46, N'Kastamonu', N'37')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (47, N'Kayseri', N'38')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (48, N'Kırıkkale', N'71')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (49, N'Kırklareli', N'39')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (50, N'Kırşehir', N'40')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (51, N'Kilis', N'79')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (52, N'Kocaeli', N'41')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (53, N'Konya', N'42')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (54, N'Kütahya', N'43')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (55, N'Malatya', N'44')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (56, N'Manisa', N'45')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (57, N'Mardin', N'47')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (58, N'Mersin', N'33')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (59, N'Muğla', N'48')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (60, N'Muş', N'49')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (61, N'Nevşehir', N'50')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (62, N'Niğde', N'51')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (63, N'Ordu', N'52')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (64, N'Osmaniye', N'80')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (65, N'Rize', N'53')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (66, N'Sakarya', N'54')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (67, N'Samsun', N'55')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (68, N'Siirt', N'56')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (69, N'Sinop', N'57')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (70, N'Sivas', N'58')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (71, N'Şanlıurfa', N'63')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (72, N'Şırnak', N'73')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (73, N'Tekirdağ', N'59')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (74, N'Tokat', N'60')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (75, N'Trabzon', N'61')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (76, N'Tunceli', N'62')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (77, N'Uşak', N'64')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (78, N'Van', N'65')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (79, N'Yalova', N'77')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (80, N'Yozgat', N'66')
GO
INSERT [dbo].[Cities] ([Id], [Name], [Plate]) VALUES (81, N'Zonguldak', N'67')
GO
SET IDENTITY_INSERT [dbo].[Cities] OFF
GO
SET IDENTITY_INSERT [dbo].[Genders] ON 
GO
INSERT [dbo].[Genders] ([Id], [Name]) VALUES (1, N'Erkek')
GO
INSERT [dbo].[Genders] ([Id], [Name]) VALUES (2, N'Kadın')
GO
SET IDENTITY_INSERT [dbo].[Genders] OFF
GO
INSERT [dbo].[Hospitals] ([Id], [Name], [PhoneNumber], [Address], [CityId]) VALUES (N'57102768-95de-4b00-fb35-08db94bc9279', N'Mersin Şehir Hastanesi', N'+90(324)225-10-00', N'Korukent Mah. 96015 Sok. Mersin Entegre Sağlık Kampüsü, 33240 Toroslar/Mersin', 58)
GO
INSERT [dbo].[Hospitals] ([Id], [Name], [PhoneNumber], [Address], [CityId]) VALUES (N'98ef222d-ca6a-418d-fb36-08db94bc9279', N'Mersin Toros Devlet Hastanesi', N'+90(324)233-71-80', N'Mesudiye, 5117. Sk. No:34, 33060 Akdeniz/Mersin', 58)
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'Staff')
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (3, N'Donor')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [UserName], [PasswordHash], [PasswordSalt], [GenderId], [CityId], [BloodGroupId], [HospitalId], [RoleId]) VALUES (N'5531aed1-0302-4a3e-329a-08db94bc9288', N'Abdurrahman', N'Varol', N'abdurrahman@gmail.com', N'abdurrahman', N'WMA4dhrMhW2ZW3+8wIlpzcew0pVATmgSq4WZ+tjmiOW1R09J5lKdcxR16RIT1ds44FjeYM0o+ksAeTzSX6aXZQ==', N'8qjYoxBQ2SgvH7vcbDsPbus2YFpicja5cDbz9IL6hJIgS4gTgr5uq1ADDLy7GHsIEY+0otBju+h74HRuNuFnU25/HWCXOjdKqPlksusj7mNjAR6rk9K9Oy4s1wIySzCoy3xi205Kqhgb4NJ0UcryFCvT6G/9QDQ63A9NyNVQ8s0=', 1, 58, 1, NULL, 1)
GO
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [UserName], [PasswordHash], [PasswordSalt], [GenderId], [CityId], [BloodGroupId], [HospitalId], [RoleId]) VALUES (N'7c9b000e-5e5e-4519-329b-08db94bc9288', N'Faruk', N'Far', N'farukfar@gmail.com', N'faruk', N'WMA4dhrMhW2ZW3+8wIlpzcew0pVATmgSq4WZ+tjmiOW1R09J5lKdcxR16RIT1ds44FjeYM0o+ksAeTzSX6aXZQ==', N'8qjYoxBQ2SgvH7vcbDsPbus2YFpicja5cDbz9IL6hJIgS4gTgr5uq1ADDLy7GHsIEY+0otBju+h74HRuNuFnU25/HWCXOjdKqPlksusj7mNjAR6rk9K9Oy4s1wIySzCoy3xi205Kqhgb4NJ0UcryFCvT6G/9QDQ63A9NyNVQ8s0=', 1, 58, 2, N'57102768-95de-4b00-fb35-08db94bc9279', 2)
GO
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [UserName], [PasswordHash], [PasswordSalt], [GenderId], [CityId], [BloodGroupId], [HospitalId], [RoleId]) VALUES (N'a4f1bda5-cd44-4e5e-329c-08db94bc9288', N'Kazım', N'Kaz', N'kazimkaz@gmail.com', N'kazim', N'WMA4dhrMhW2ZW3+8wIlpzcew0pVATmgSq4WZ+tjmiOW1R09J5lKdcxR16RIT1ds44FjeYM0o+ksAeTzSX6aXZQ==', N'8qjYoxBQ2SgvH7vcbDsPbus2YFpicja5cDbz9IL6hJIgS4gTgr5uq1ADDLy7GHsIEY+0otBju+h74HRuNuFnU25/HWCXOjdKqPlksusj7mNjAR6rk9K9Oy4s1wIySzCoy3xi205Kqhgb4NJ0UcryFCvT6G/9QDQ63A9NyNVQ8s0=', 1, 58, 3, NULL, 3)
GO
/****** Object:  Index [IX_Hospitals_CityId]    Script Date: 4.08.2023 10:45:24 ******/
CREATE NONCLUSTERED INDEX [IX_Hospitals_CityId] ON [dbo].[Hospitals]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Requests_BloodGroupId]    Script Date: 4.08.2023 10:45:24 ******/
CREATE NONCLUSTERED INDEX [IX_Requests_BloodGroupId] ON [dbo].[Requests]
(
	[BloodGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Requests_HospitalId]    Script Date: 4.08.2023 10:45:24 ******/
CREATE NONCLUSTERED INDEX [IX_Requests_HospitalId] ON [dbo].[Requests]
(
	[HospitalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_BloodGroupId]    Script Date: 4.08.2023 10:45:24 ******/
CREATE NONCLUSTERED INDEX [IX_Users_BloodGroupId] ON [dbo].[Users]
(
	[BloodGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_CityId]    Script Date: 4.08.2023 10:45:24 ******/
CREATE NONCLUSTERED INDEX [IX_Users_CityId] ON [dbo].[Users]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_GenderId]    Script Date: 4.08.2023 10:45:24 ******/
CREATE NONCLUSTERED INDEX [IX_Users_GenderId] ON [dbo].[Users]
(
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_HospitalId]    Script Date: 4.08.2023 10:45:24 ******/
CREATE NONCLUSTERED INDEX [IX_Users_HospitalId] ON [dbo].[Users]
(
	[HospitalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_RoleId]    Script Date: 4.08.2023 10:45:24 ******/
CREATE NONCLUSTERED INDEX [IX_Users_RoleId] ON [dbo].[Users]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Hospitals]  WITH CHECK ADD  CONSTRAINT [FK_Hospitals_Cities_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Hospitals] CHECK CONSTRAINT [FK_Hospitals_Cities_CityId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_BloodGroups_BloodGroupId] FOREIGN KEY([BloodGroupId])
REFERENCES [dbo].[BloodGroups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_BloodGroups_BloodGroupId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Hospitals_HospitalId] FOREIGN KEY([HospitalId])
REFERENCES [dbo].[Hospitals] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Hospitals_HospitalId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_BloodGroups_BloodGroupId] FOREIGN KEY([BloodGroupId])
REFERENCES [dbo].[BloodGroups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_BloodGroups_BloodGroupId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Cities_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Cities_CityId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Genders_GenderId] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Genders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Genders_GenderId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Hospitals_HospitalId] FOREIGN KEY([HospitalId])
REFERENCES [dbo].[Hospitals] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Hospitals_HospitalId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles_RoleId]
GO
USE [master]
GO
ALTER DATABASE [BloodDonationDb] SET  READ_WRITE 
GO
