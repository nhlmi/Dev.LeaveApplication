USE [Dev.LeaveApplicationForm]
GO
ALTER TABLE [dbo].[Applications] DROP CONSTRAINT [FK_Applications_Employees]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 27/12/2024 2:27:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 27/12/2024 2:27:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employees]') AND type in (N'U'))
DROP TABLE [dbo].[Employees]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 27/12/2024 2:27:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Applications]') AND type in (N'U'))
DROP TABLE [dbo].[Applications]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 27/12/2024 2:27:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[StartDatetime] [datetime] NOT NULL,
	[EndDatetime] [datetime] NOT NULL,
	[Justification] [nvarchar](100) NOT NULL,
	[ManagerEmployeeId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 27/12/2024 2:27:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[EmployeeName] [nvarchar](50) NOT NULL,
	[IsManager] [bit] NOT NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 27/12/2024 2:27:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Username] [nvarchar](10) NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Employees] ([EmployeeId], [EmployeeName], [IsManager], [Email]) VALUES (N'4b343809-bf08-40e3-86a8-300453cb0cff', N'Jamie Doe', 0, N'woodrow.kovacek17@ethereal.email')
INSERT [dbo].[Employees] ([EmployeeId], [EmployeeName], [IsManager], [Email]) VALUES (N'a7cac141-97ee-4573-a13b-4aea6abf7992', N'Jessica Doe', 1, N'woodrow.kovacek17@ethereal.email')
INSERT [dbo].[Employees] ([EmployeeId], [EmployeeName], [IsManager], [Email]) VALUES (N'b57efde0-0400-4f33-9370-bb133a9ad4db', N'Jennie Doe', 0, N'woodrow.kovacek17@ethereal.email')
INSERT [dbo].[Employees] ([EmployeeId], [EmployeeName], [IsManager], [Email]) VALUES (N'21e2f1f1-00d1-4744-b0a1-fc95b397ccce', N'John Doe', 1, N'woodrow.kovacek17@ethereal.email')
GO
INSERT [dbo].[Users] ([Username], [EmployeeId]) VALUES (N'jamie.doe', N'4b343809-bf08-40e3-86a8-300453cb0cff')
INSERT [dbo].[Users] ([Username], [EmployeeId]) VALUES (N'jennie.doe', N'b57efde0-0400-4f33-9370-bb133a9ad4db')
INSERT [dbo].[Users] ([Username], [EmployeeId]) VALUES (N'jess.doe', N'a7cac141-97ee-4573-a13b-4aea6abf7992')
INSERT [dbo].[Users] ([Username], [EmployeeId]) VALUES (N'john.doe', N'21e2f1f1-00d1-4744-b0a1-fc95b397ccce')
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Employees]
GO
