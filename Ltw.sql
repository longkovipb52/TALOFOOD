USE [LTWEB_FFood]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 11/03/2025 3:05:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](max) NULL,
	[email] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](50) NOT NULL,
	[id_role] [int] NOT NULL,
	[address] [nvarchar](200) NULL,
	[status] [int] NULL,
	[login_attempts] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 11/03/2025 3:05:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[bill_id] [int] IDENTITY(1,1) NOT NULL,
	[ngaydat] [date] NOT NULL,
	[ngaygiao] [date] NOT NULL,
	[id_account] [int] NOT NULL,
	[status] [nvarchar](50) NULL,
	[address] [nvarchar](250) NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[bill_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill_info]    Script Date: 11/03/2025 3:05:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill_info](
	[billinfo_id] [int] IDENTITY(1,1) NOT NULL,
	[id_bill] [int] NOT NULL,
	[id_food] [int] NOT NULL,
	[id_account] [int] NOT NULL,
	[count] [int] NOT NULL,
	[price] [money] NOT NULL,
 CONSTRAINT [PK_Bill_info] PRIMARY KEY CLUSTERED 
(
	[billinfo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 11/03/2025 3:05:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Message] [nvarchar](max) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 11/03/2025 3:05:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[food_id] [int] IDENTITY(1,1) NOT NULL,
	[food_name] [nvarchar](50) NOT NULL,
	[id_catagory] [int] NOT NULL,
	[price] [money] NOT NULL,
	[image] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[food_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food_Catagory]    Script Date: 11/03/2025 3:05:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food_Catagory](
	[foodcatagory_id] [int] NOT NULL,
	[foodcategory_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Food_Catagory] PRIMARY KEY CLUSTERED 
(
	[foodcatagory_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/03/2025 3:05:22 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] NOT NULL,
	[rolename] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [login_attempts]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([id_role])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Account] FOREIGN KEY([id_account])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Account]
GO
ALTER TABLE [dbo].[Bill_info]  WITH CHECK ADD  CONSTRAINT [FK_Bill_info_Account] FOREIGN KEY([id_account])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[Bill_info] CHECK CONSTRAINT [FK_Bill_info_Account]
GO
ALTER TABLE [dbo].[Bill_info]  WITH CHECK ADD  CONSTRAINT [FK_Bill_info_Bill] FOREIGN KEY([id_bill])
REFERENCES [dbo].[Bill] ([bill_id])
GO
ALTER TABLE [dbo].[Bill_info] CHECK CONSTRAINT [FK_Bill_info_Bill]
GO
ALTER TABLE [dbo].[Bill_info]  WITH CHECK ADD  CONSTRAINT [FK_Bill_info_Food] FOREIGN KEY([id_food])
REFERENCES [dbo].[Food] ([food_id])
GO
ALTER TABLE [dbo].[Bill_info] CHECK CONSTRAINT [FK_Bill_info_Food]
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD  CONSTRAINT [FK_Food_Food_Catagory] FOREIGN KEY([id_catagory])
REFERENCES [dbo].[Food_Catagory] ([foodcatagory_id])
GO
ALTER TABLE [dbo].[Food] CHECK CONSTRAINT [FK_Food_Food_Catagory]
GO
