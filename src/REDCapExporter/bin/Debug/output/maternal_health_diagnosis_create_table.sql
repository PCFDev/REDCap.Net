CREATE TABLE [dbo].[maternal_health_diagnosis] ([Id] INT IDENTITY (1, 1) NOT NULL, 
[PatientID] NVARCHAR(MAX) NULL, 
[EventName] NVARCHAR(MAX) NULL
, [dx_hx] NVARCHAR(MAX) NULL,
[maternal_health_diagnosis_complete] NVARCHAR(MAX) NULL,
[dx] NVARCHAR(MAX) NULL,
 PRIMARY KEY CLUSTERED ([Id] ASC));