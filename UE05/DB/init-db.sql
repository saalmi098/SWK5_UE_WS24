-- Check if the database 'person_db' exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'person_db')
BEGIN
	PRINT 'Database person_db already exists';
END
ELSE
BEGIN
	EXEC('CREATE DATABASE [person_db]');
	PRINT 'Database person_db created';
	
	EXEC('
		USE [person_db];
		CREATE TABLE [dbo].[person](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[first_name] [varchar](20) NOT NULL,
			[last_name] [varchar](50) NOT NULL,
			[date_of_birth] [date] NOT NULL,
				CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED ([Id]))
	');
	PRINT 'Table person created';

	EXEC('
		USE [person_db];
		SET NOCOUNT ON;
		SET IDENTITY_INSERT [dbo].[person] ON;
		INSERT [dbo].[person] ([Id], [first_name], [last_name], [date_of_birth]) VALUES 
				(1, N''Alan'', N''Turing'', ''1912-06-23''),
				(2, N''Dennis'', N''Ritchie'', ''1914-10-06''),
				(3, N''Anders'', N''Hejlsberg '', ''1960-12-02''),
				(4, N''James'', N''Gosling'', ''1955-05-19''),
				(5, N''Bjarne'', N''Stroustrup '', ''1950-12-30''),
				(6, N''Linus'', N''Torwalds'', ''1969-12-28'');
		SET IDENTITY_INSERT [dbo].[person] OFF
	');
	PRINT 'Table person filled with sample data';
END