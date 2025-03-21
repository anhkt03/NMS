USE [master]
GO
/****** Object:  Database [NMS]    Script Date: 3/19/2025 2:13:28 PM ******/
CREATE DATABASE [NMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\NMS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\NMS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [NMS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [NMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NMS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NMS] SET  MULTI_USER 
GO
ALTER DATABASE [NMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NMS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NMS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [NMS] SET QUERY_STORE = ON
GO
ALTER DATABASE [NMS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [NMS]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 3/19/2025 2:13:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
	[CategoryDescription] [nvarchar](50) NULL,
	[ParentCategoryID] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsArticle]    Script Date: 3/19/2025 2:13:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsArticle](
	[NewsArticleID] [int] IDENTITY(1,1) NOT NULL,
	[NewsTitle] [nvarchar](200) NULL,
	[Headline] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[NewsContent] [nvarchar](max) NULL,
	[NewsSource] [nvarchar](200) NULL,
	[CategoryID] [int] NULL,
	[NewsStatus] [bit] NULL,
	[CreatedByID] [int] NULL,
	[UpdateByID] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_NewsArticle] PRIMARY KEY CLUSTERED 
(
	[NewsArticleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsTag]    Script Date: 3/19/2025 2:13:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsTag](
	[NewArticleID] [int] NOT NULL,
	[TagID] [int] NOT NULL,
 CONSTRAINT [PK_NewsTag] PRIMARY KEY CLUSTERED 
(
	[NewArticleID] ASC,
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemAccount]    Script Date: 3/19/2025 2:13:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemAccount](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [nvarchar](50) NULL,
	[AccountEmail] [nvarchar](50) NULL,
	[AccountRole] [int] NULL,
	[AccountPassword] [nvarchar](50) NULL,
 CONSTRAINT [PK_SystemAccount] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 3/19/2025 2:13:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[TagID] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](50) NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDescription], [ParentCategoryID], [IsActive]) VALUES (1, N'H1', N'place for learn', 1, 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDescription], [ParentCategoryID], [IsActive]) VALUES (2, N'H2', N'place for play', 1, 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDescription], [ParentCategoryID], [IsActive]) VALUES (4, N'H3', N'good', 1, 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryDescription], [ParentCategoryID], [IsActive]) VALUES (8, N'H10', N'place for chill 31', 4, 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[NewsArticle] ON 

INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (1, N'Information of room', N'Book room test signal R 123456', CAST(N'2025-02-23T15:52:00.000' AS DateTime), N'for student follow to book new room in dom generator for u', N'https://images.unsplash.com/photo-1735825764460-c5dec05d6253?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 1, 1, NULL, 1, CAST(N'2025-03-19T14:06:41.260' AS DateTime))
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (2, N'Car in university 222', N'Car newsssss', CAST(N'2025-01-01T00:00:00.000' AS DateTime), N'information of car in universitty ', N'https://pbs.twimg.com/media/GkYJkrWWEAEGxPU?format=jpg&name=large', 1, 1, 2, 1, CAST(N'2025-02-25T00:02:44.133' AS DateTime))
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (3, N'Student Gold Toat', N'Good Student', CAST(N'2025-02-24T00:00:00.000' AS DateTime), N'have ten students get signature of information', N'https://images.unsplash.com/photo-1740141340584-a749ceac44a1?q=80&w=1970&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 1, 1, 1, NULL, NULL)
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (4, N'Student Gold Toat', N'Excelence', CAST(N'2025-01-20T00:00:00.000' AS DateTime), N'one student in FPT university make good achiverment', N'https://plus.unsplash.com/premium_photo-1739995619661-98536ed91c36?q=80&w=1931&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, 1, 2, NULL, NULL)
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (5, N'Lý do Trump muốn khoáng sản của Ukraine', N'Thương mại update 2', CAST(N'2025-02-24T12:41:00.000' AS DateTime), N'in this world will make ', N'https://images.pexels.com/photos/518543/pexels-photo-518543.jpeg?auto=compress&cs=tinysrgb&w=600', 2, 1, 2, 1, CAST(N'2025-02-24T17:49:27.290' AS DateTime))
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (1002, N'Information of room', N'Book room 2', CAST(N'2025-02-24T15:02:00.000' AS DateTime), N'for student follow to book new room in dom', NULL, 1, 1, 1, 1, CAST(N'2025-02-24T17:43:42.670' AS DateTime))
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (1015, N'Use active voice', N'Google Warns Advertisers About Third-Party Cookies', NULL, N'Active voice is more engaging and dynamic, but starting a conversation is even more powerful. The best way to do that in a short headline? Ask a question. Whether it’s a rhetorical question describing the benefits or a dare to encourage your audience to click, it’s a compelling opening. And that’s what the best headlines are. Openers.', N'https://images.pexels.com/photos/518543/pexels-photo-518543.jpeg?auto=compress&cs=tinysrgb&w=600', 2, 1, 1, NULL, NULL)
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (1020, N'Breaking News: New Park Opens', N'A beautiful new park has opened in downtown.', CAST(N'2025-02-24T17:51:29.610' AS DateTime), N'The new park features walking trails, playgrounds, and open spaces for the community to enjoy.', N'https://images.pexels.com/photos/457882/pexels-photo-457882.jpeg?cs=srgb&dl=pexels-asadphoto-457882.jpg&fm=jpg', 1, 1, 1, 1, CAST(N'2025-02-24T17:55:08.327' AS DateTime))
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (1022, N'Information of room', N'test create', CAST(N'2025-02-24T18:02:00.470' AS DateTime), N'for student follow to book new room in dom', N'https://pbs.twimg.com/media/GkYJkrWWEAEGxPU?format=jpg&name=large', 1, 1, 1, NULL, NULL)
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (1027, N'Test session 212', N'Book room', CAST(N'2025-02-24T18:08:00.773' AS DateTime), N'follow test session', N'https://images.pexels.com/photos/518543/pexels-photo-518543.jpeg?auto=compress&cs=tinysrgb&w=600', 1, 1, 1, NULL, NULL)
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (1028, N'Lý do Trump muốn khoáng sản của Ukraine', N'Book room', CAST(N'2025-02-24T22:37:23.233' AS DateTime), N'for student follow to book new room in dom', N'https://pbs.twimg.com/media/GkYJkrWWEAEGxPU?format=jpg&name=large', 4, 1, 1, NULL, NULL)
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (1029, N'Test TAGS', N'test tagss', CAST(N'2025-02-24T23:31:41.927' AS DateTime), N'for student follow to book new room in dom', N'https://pbs.twimg.com/media/GkYJkrWWEAEGxPU?format=jpg&name=large', 1, 1, 1, NULL, NULL)
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (1030, N'Test TAGS 222', N'Book room', CAST(N'2025-02-24T23:32:37.903' AS DateTime), N'follow test session abc', N'https://pbs.twimg.com/media/GkYJkrWWEAEGxPU?format=jpg&name=large', 8, 1, 1, NULL, NULL)
INSERT [dbo].[NewsArticle] ([NewsArticleID], [NewsTitle], [Headline], [CreateDate], [NewsContent], [NewsSource], [CategoryID], [NewsStatus], [CreatedByID], [UpdateByID], [ModifyDate]) VALUES (1031, N'Test TAGS 111', N'Book room', CAST(N'2025-02-25T00:03:26.493' AS DateTime), N'for student follow to book new room in dom', N'https://images.pexels.com/photos/518543/pexels-photo-518543.jpeg?auto=compress&cs=tinysrgb&w=600', 4, 1, NULL, 1, CAST(N'2025-03-18T17:14:18.923' AS DateTime))
SET IDENTITY_INSERT [dbo].[NewsArticle] OFF
GO
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (1, 17)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (1, 18)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (1, 19)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (1, 22)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (2, 18)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (2, 19)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (2, 23)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (3, 19)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (3, 25)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (5, 30)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (5, 32)
INSERT [dbo].[NewsTag] ([NewArticleID], [TagID]) VALUES (5, 33)
GO
SET IDENTITY_INSERT [dbo].[SystemAccount] ON 

INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (1, N'staffABDE123', N'staff@gmail.com', 1, N'1234')
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (2, N'staff2', N'staff@email.com', 1, N'123')
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (3, N'lecturer1', N'lecturer@gmail.com', 2, N'123')
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (4, N'lecturer2', N'lecturer@email.com', 2, N'123')
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (6, N'theanh', N'theanh@gmail.com', 1, N'123')
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (7, N'theanh121', N'theanh1@gmail.com', 1, N'123')
INSERT [dbo].[SystemAccount] ([AccountID], [AccountName], [AccountEmail], [AccountRole], [AccountPassword]) VALUES (10, N'theanh999', N'djd@gmail.com', 2, N'123')
SET IDENTITY_INSERT [dbo].[SystemAccount] OFF
GO
SET IDENTITY_INSERT [dbo].[Tag] ON 

INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (17, N'Technology', N'Articles about the latest technology trends')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (18, N'Economy', N'News and analysis on economy and finance')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (19, N'Sports', N'Updates on national and international sports')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (20, N'Entertainment', N'News about music, movies, and celebrities')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (21, N'Health', N'Knowledge and news on healthcare and wellness')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (22, N'Education', N'Information on schools, exams, and scholarships')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (23, N'Science', N'Latest research, discoveries, and innovations')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (24, N'Politics', N'Political news and government policies')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (25, N'Lifestyle', N'Articles about fashion, travel, and daily life')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (26, N'Environment', N'Climate change and environmental protection')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (27, N'Business', N'Corporate news, startups, and market trends')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (28, N'World News', N'Breaking news from around the world')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (29, N'Food', N'Recipes, restaurant reviews, and culinary trends')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (30, N'Gaming', N'News, reviews, and updates on video games')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (31, N'Automobile', N'Cars, electric vehicles, and automotive news')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (32, N'History', N'Stories and analysis of historical events')
INSERT [dbo].[Tag] ([TagID], [TagName], [Note]) VALUES (33, N'Social Media', N'Trends and updates from popular social platforms')
SET IDENTITY_INSERT [dbo].[Tag] OFF
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Category] FOREIGN KEY([ParentCategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Category]
GO
ALTER TABLE [dbo].[NewsArticle]  WITH CHECK ADD  CONSTRAINT [FK_NewsArticle_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[NewsArticle] CHECK CONSTRAINT [FK_NewsArticle_Category]
GO
ALTER TABLE [dbo].[NewsArticle]  WITH CHECK ADD  CONSTRAINT [FK_NewsArticle_SystemAccount] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[SystemAccount] ([AccountID])
GO
ALTER TABLE [dbo].[NewsArticle] CHECK CONSTRAINT [FK_NewsArticle_SystemAccount]
GO
ALTER TABLE [dbo].[NewsArticle]  WITH CHECK ADD  CONSTRAINT [FK_NewsArticle_SystemAccount1] FOREIGN KEY([UpdateByID])
REFERENCES [dbo].[SystemAccount] ([AccountID])
GO
ALTER TABLE [dbo].[NewsArticle] CHECK CONSTRAINT [FK_NewsArticle_SystemAccount1]
GO
ALTER TABLE [dbo].[NewsTag]  WITH CHECK ADD  CONSTRAINT [FK_NewsTag_NewsArticle] FOREIGN KEY([NewArticleID])
REFERENCES [dbo].[NewsArticle] ([NewsArticleID])
GO
ALTER TABLE [dbo].[NewsTag] CHECK CONSTRAINT [FK_NewsTag_NewsArticle]
GO
ALTER TABLE [dbo].[NewsTag]  WITH CHECK ADD  CONSTRAINT [FK_NewsTag_Tag] FOREIGN KEY([TagID])
REFERENCES [dbo].[Tag] ([TagID])
GO
ALTER TABLE [dbo].[NewsTag] CHECK CONSTRAINT [FK_NewsTag_Tag]
GO
USE [master]
GO
ALTER DATABASE [NMS] SET  READ_WRITE 
GO
