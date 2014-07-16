USE ITJakubMobileAppsDB
SET XACT_ABORT ON
BEGIN TRAN


--create table for storing database version
	CREATE TABLE [dbo].[DatabaseVersion] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[DatabaseVersion] varchar(50) NOT NULL,
		[SolutionVersion] varchar(50) NULL,
		[UpgradeDate] [datetime] NOT NULL DEFAULT GETDATE(),
		[UpgradeUser] varchar(150) NOT NULL default SYSTEM_USER,
		CONSTRAINT [PK_DatabaseVersion] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	
	)

	INSERT INTO [dbo].[DatabaseVersion]
		(DatabaseVersion)
	VALUES
		('000' )
		-- DatabaseVersion - varchar



/********************* TABLES *******************/

/* Application */
CREATE TABLE [Application](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar] NOT NULL,
    CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
)

/* Institution */
CREATE TABLE [Institution](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar] NOT NULL,
    CONSTRAINT [PK_Institution] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
)

/* User */
CREATE TABLE [User](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [FirstName] [varchar] NOT NULL,
    [LastName] [varchar] NOT NULL,
    [Email] [varchar] NOT NULL,
    [RoleId] [tinyint] NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
)

/* Group*/
CREATE TABLE [Group](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [TaskId] [bigint] NOT NULL,
    [InstitutionId] [bigint] NOT NULL,
    CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
)

/* Users role */
CREATE TABLE [Role](
    [Id] [tinyint] IDENTITY(1,1) NOT NULL,
    [Name] [varchar] NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
)


/* Synchronized object in group */
CREATE TABLE [SynchronizedObject](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [CreateTime] [datetime] NOT NULL,
    [AuthorId] [bigint] NOT NULL,
    [GroupId] [bigint] NOT NULL,
    [Name] [varchar],
    [GUID] [varchar],
    CONSTRAINT [PK_SynchronizedObject] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)      
)


/* Predefined task for application */
CREATE TABLE [Task](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [ApplicationId] [bigint] NOT NULL,
    [CreateTime] [datetime] NOT NULL,
    [AuthorId] [bigint] NOT NULL,
    [Name] [varchar],
    [GUID] [varchar],
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
)


/* Relationship User <--> Group */
CREATE TABLE [UserToGroup](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [UserId] [bigint]  NOT NULL,
    [GroupId] [bigint]  NOT NULL,
    CONSTRAINT [PK_UserToGroup] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
)

/* Relationship User <--> Institution */
CREATE TABLE [UserToInstitution](
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    [UserId] [bigint]  NOT NULL,
    [InstitutionId] [bigint] NOT NULL,
    CONSTRAINT [PK_UserToInstitution] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
)





/********************* CONSTRAINTS *******************/

/* FK User -> Role */
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO


/* FK Group -> Task */
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Task]
GO

/* FK Group -> Institution */
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Institution] FOREIGN KEY([InstitutionId])
REFERENCES [dbo].[Institution] ([Id])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Institution]
GO


/* FK SynchronizedObject -> User */
ALTER TABLE [dbo].[SynchronizedObject]  WITH CHECK ADD  CONSTRAINT [FK_SynchronizedObject_User] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SynchronizedObject] CHECK CONSTRAINT [FK_SynchronizedObject_User]
GO


/* FK SynchronizedObject -> Group */
ALTER TABLE [dbo].[SynchronizedObject]  WITH CHECK ADD  CONSTRAINT [FK_SynchronizedObject_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[SynchronizedObject] CHECK CONSTRAINT [FK_SynchronizedObject_Group]
GO

/* FK Task -> User */
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_User] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_User]
GO

/* FK Task -> Application */
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Application] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Application]
GO



/* FK UserToGroup -> User */
ALTER TABLE [dbo].[UserToGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserToGroup_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserToGroup] CHECK CONSTRAINT [FK_UserToGroup_User]
GO


/* FK UserToGroup -> Group */
ALTER TABLE [dbo].[UserToGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserToGroup_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[UserToGroup] CHECK CONSTRAINT [FK_UserToGroup_Group]
GO



/* FK UserToInstitution -> User */
ALTER TABLE [dbo].[UserToInstitution]  WITH CHECK ADD  CONSTRAINT [FK_UserToInstitution_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserToInstitution] CHECK CONSTRAINT [FK_UserToInstitution_User]
GO


/* FK UserToInstitution -> Institution */
ALTER TABLE [dbo].[UserToInstitution]  WITH CHECK ADD  CONSTRAINT [FK_UserToInstitution_Institution] FOREIGN KEY([InstitutionId])
REFERENCES [dbo].[Institution] ([Id])
GO
ALTER TABLE [dbo].[UserToInstitution] CHECK CONSTRAINT [FK_UserToInstitution_Institution]
GO



--ROLLBACK
COMMIT