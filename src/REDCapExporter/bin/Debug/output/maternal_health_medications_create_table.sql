CREATE TABLE [dbo].[maternal_health_medications] ([Id] INT IDENTITY (1, 1) NOT NULL, 
[PatientID] NVARCHAR(MAX) NULL, 
[EventName] NVARCHAR(MAX) NULL
, [maternal_health_medications_complete] NVARCHAR(MAX) NULL,
[med_categ] NVARCHAR(MAX) NULL,
[other_type_med] NVARCHAR(MAX) NULL,
[ssrimed] NVARCHAR(MAX) NULL,
 PRIMARY KEY CLUSTERED ([Id] ASC));