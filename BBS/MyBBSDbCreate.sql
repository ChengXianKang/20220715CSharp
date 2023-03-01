USE [MyBBSDb]
GO
/****** Object:  Table [dbo].[PostReplys]    Script Date: 2021/8/25 23:59:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostReplys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[ReplyContent] [text] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [int] NOT NULL,
	[EditTime] [datetime] NULL,
	[EditUserId] [int] NULL,
	[Up] [text] NULL,
	[Down] [text] NULL,
 CONSTRAINT [PK_PostReplys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 2021/8/25 23:59:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostTitle] [nvarchar](100) NULL,
	[PostIcon] [nvarchar](500) NULL,
	[PostTypeId] [int] NOT NULL,
	[PostType] [nvarchar](50) NOT NULL,
	[PostContent] [text] NULL,
	[Clicks] [int] NOT NULL,
	[Replys] [int] NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [int] NOT NULL,
	[EditTime] [datetime] NULL,
	[EditUserId] [int] NOT NULL,
	[LastReplyTime] [datetime] NULL,
	[LastReplyUserId] [int] NOT NULL,
	[Up] [text] NULL,
	[Down] [text] NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostTypes]    Script Date: 2021/8/25 23:59:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostType] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
 CONSTRAINT [PK_PostTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2021/8/25 23:59:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserNo] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserLevel] [int] NOT NULL,
	[Password] [nvarchar](50) NULL,
	[IsDelete] [bit] NULL,
	[Token] [uniqueidentifier] NULL,
	[AutoLoginTag] [uniqueidentifier] NULL,
	[AutoLoginLimitTime] [datetime] NULL,
 CONSTRAINT [PK__Users__3214EC07AE3806AC] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
