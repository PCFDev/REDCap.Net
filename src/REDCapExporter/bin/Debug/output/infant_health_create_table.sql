CREATE TABLE [dbo].[infant_health] ([Id] INT IDENTITY (1, 1) NOT NULL, 
[PatientID] NVARCHAR(MAX) NULL, 
[EventName] NVARCHAR(MAX) NULL
, [infant_dx] NVARCHAR(MAX) NULL,
[infant_health_complete] NVARCHAR(MAX) NULL,
[infant_label] NVARCHAR(MAX) NULL,
[meconium_confirm_done] NVARCHAR(MAX) NULL,
[meconium_result] NVARCHAR(MAX) NULL,
[meconium_screen] NVARCHAR(MAX) NULL,
[meconium_screen_confirm] NVARCHAR(MAX) NULL,
[positive_meconium] NVARCHAR(MAX) NULL,
 PRIMARY KEY CLUSTERED ([Id] ASC));