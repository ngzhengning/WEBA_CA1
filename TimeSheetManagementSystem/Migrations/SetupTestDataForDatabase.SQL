-- This script is not necessary if execute the SetupTheEntireDatabaseDataWithoutUsing_app_SeedData.sql

SET IDENTITY_INSERT [dbo].[CustomerAccount] ON 

INSERT [dbo].[CustomerAccount] ([CustomerAccountId], [AccountName], [Comments], [CreatedAt], [CreatedById],   [UpdatedAt], [UpdatedById]) VALUES (5, N'SCHOOL A', N'Weekly one lesson (Saturday) on dancing foundations (3 hours each week).', CAST(N'2017-05-01 15:40:15.5030000' AS DateTime2), 3,  CAST(N'2017-05-01 15:40:15.5030000' AS DateTime2), 3)
INSERT [dbo].[CustomerAccount] ([CustomerAccountId], [AccountName], [Comments], [CreatedAt], [CreatedById],  [UpdatedAt], [UpdatedById]) VALUES (6, N'SCHOOL B', N'Weekly two lessons (total 5 hours) dancing gym sessions', CAST(N'2014-12-20 15:40:15.5030000' AS DateTime2), 3,   CAST(N'2014-12-20 15:40:15.5030000' AS DateTime2), 3)
INSERT [dbo].[CustomerAccount] ([CustomerAccountId], [AccountName], [Comments], [CreatedAt], [CreatedById],  [UpdatedAt], [UpdatedById]) VALUES (7, N'SCHOOL C', N'Weekly two lessons (total 4 hours) dancing lessons', CAST(N'2017-05-01 15:40:15.5030000' AS DateTime2), 3,   CAST(N'2017-05-01 15:40:15.5030000' AS DateTime2), 3)
INSERT [dbo].[CustomerAccount] ([CustomerAccountId], [AccountName], [Comments], [CreatedAt], [CreatedById],  [UpdatedAt], [UpdatedById]) VALUES (8, N'SCHOOL D', N'Many lessons to be conducted every week day (MON to SAT)', CAST(N'2017-05-01 15:40:15.5030000' AS DateTime2), 3,  CAST(N'2017-05-01 15:40:15.5030000' AS DateTime2), 3)
SET IDENTITY_INSERT [dbo].[CustomerAccount] OFF

SET IDENTITY_INSERT [dbo].[SessionSynopsis] ON 
INSERT INTO SessionSynopsis
                         (SessionSynopsisId,SessionSynopsisName,  IsVisible, CreatedById, UpdatedById)
VALUES        (1,'Training' ,1,2,2)
INSERT INTO SessionSynopsis
                         (SessionSynopsisId,SessionSynopsisName, IsVisible, CreatedById, UpdatedById)
VALUES        (2,'Training & Rehearsal' ,1,2,2)
INSERT INTO SessionSynopsis
                         (SessionSynopsisId,SessionSynopsisName,  IsVisible, CreatedById, UpdatedById)
VALUES        (3,'Rehearsal' ,1,2,2)
INSERT INTO SessionSynopsis
                         (SessionSynopsisId,SessionSynopsisName, IsVisible, CreatedById, UpdatedById)
VALUES        (4,'Rehearsal & performance' ,1,2,2)
INSERT INTO SessionSynopsis
                         (SessionSynopsisId,SessionSynopsisName, IsVisible, CreatedById, UpdatedById)
VALUES        (5,'Performance' ,1,2,2)
SET IDENTITY_INSERT [dbo].[SessionSynopsis] OFF 

SET IDENTITY_INSERT [dbo].[AccountDetail] ON 

INSERT [dbo].[AccountDetail] ([AccountDetailId], [CustomerAccountId], [EffectiveEndDate], [EffectiveStartDate], [EndTimeInMinutes], [IsEnabled], [StartTimeInMinutes], [DayOfWeekNumber]) VALUES (1, 6, NULL, CAST(N'2017-06-04 00:00:00.0000000' AS DateTime2), 720, 1, 600, 2)
INSERT [dbo].[AccountDetail] ([AccountDetailId], [CustomerAccountId], [EffectiveEndDate], [EffectiveStartDate], [EndTimeInMinutes], [IsEnabled], [StartTimeInMinutes], [DayOfWeekNumber]) VALUES (2, 6, NULL, CAST(N'2017-06-04 00:00:00.0000000' AS DateTime2), 1200, 1, 1080, 3)
INSERT [dbo].[AccountDetail] ([AccountDetailId], [CustomerAccountId], [EffectiveEndDate], [EffectiveStartDate], [EndTimeInMinutes], [IsEnabled], [StartTimeInMinutes], [DayOfWeekNumber]) VALUES (3, 6, NULL, CAST(N'2017-06-04 00:00:00.0000000' AS DateTime2), 720, 1, 600, 1)
INSERT [dbo].[AccountDetail] ([AccountDetailId], [CustomerAccountId], [EffectiveEndDate], [EffectiveStartDate], [EndTimeInMinutes], [IsEnabled], [StartTimeInMinutes], [DayOfWeekNumber]) VALUES (5, 6, NULL, CAST(N'2017-06-05 00:00:00.0000000' AS DateTime2), 780, 1, 540, 4)
SET IDENTITY_INSERT [dbo].[AccountDetail] OFF