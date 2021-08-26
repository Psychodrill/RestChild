alter table [dbo].[MGTWorkingDayWindow]
add [WindowNumber] [int] NULL
GO

/****** Object:  Table [dbo].[MGTVisitTargetMGTWorkingDayWindow]    Script Date: 11.07.2019 11:29:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MGTVisitTargetMGTWorkingDayWindow](
	[MGTVisitTarget_Id] [bigint] NOT NULL,
	[MGTWorkingDayWindow_Id] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.MGTVisitTargetMGTWorkingDayWindow] PRIMARY KEY CLUSTERED 
(
	[MGTVisitTarget_Id] ASC,
	[MGTWorkingDayWindow_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MGTVisitTargetMGTWorkingDayWindow]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MGTVisitTargetMGTWorkingDayWindow_dbo.MGTVisitTarget_MGTVisitTarget_Id] FOREIGN KEY([MGTVisitTarget_Id])
REFERENCES [dbo].[MGTVisitTarget] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MGTVisitTargetMGTWorkingDayWindow] CHECK CONSTRAINT [FK_dbo.MGTVisitTargetMGTWorkingDayWindow_dbo.MGTVisitTarget_MGTVisitTarget_Id]
GO

ALTER TABLE [dbo].[MGTVisitTargetMGTWorkingDayWindow]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MGTVisitTargetMGTWorkingDayWindow_dbo.MGTWorkingDayWindow_MGTWorkingDayWindow_Id] FOREIGN KEY([MGTWorkingDayWindow_Id])
REFERENCES [dbo].[MGTWorkingDayWindow] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MGTVisitTargetMGTWorkingDayWindow] CHECK CONSTRAINT [FK_dbo.MGTVisitTargetMGTWorkingDayWindow_dbo.MGTWorkingDayWindow_MGTWorkingDayWindow_Id]
GO


