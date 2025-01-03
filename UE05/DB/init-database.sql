CREATE DATABASE [person_db]
GO
USE [person_db]
GO

CREATE TABLE [dbo].[person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](20) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[date_of_birth] [date] NOT NULL,
    CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED ([Id]))
GO

SET NOCOUNT ON
SET IDENTITY_INSERT [dbo].[person] ON 
INSERT [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (1, N'Alan', N'Turing', CAST(N'1912-06-23' AS Date))
INSERT [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (2, N'Dennis', N'Ritchie', CAST(N'1914-10-06' AS Date))
INSERT [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (3, N'Anders', N'Hejlsberg ', CAST(N'1960-12-02' AS Date))
INSERT [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (4, N'James', N'Gosling', CAST(N'1955-05-19' AS Date))
INSERT [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (5, N'Bjarne', N'Stroustrup ', CAST(N'1950-12-30' AS Date))
INSERT [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES (6, N'Linus', N'Torwalds', CAST(N'1969-12-28' AS Date))
SET IDENTITY_INSERT [dbo].[person] OFF
GO
