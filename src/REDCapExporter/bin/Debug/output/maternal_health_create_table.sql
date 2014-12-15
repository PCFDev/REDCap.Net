CREATE TABLE [dbo].[maternal_health] ([Id] INT IDENTITY (1, 1) NOT NULL, 
[PatientID] NVARCHAR(MAX) NULL, 
[EventName] NVARCHAR(MAX) NULL
, [gravida] NVARCHAR(MAX) NULL,
[hx_drug] NVARCHAR(MAX) NULL,
[maternal_health_complete] NVARCHAR(MAX) NULL,
[para] NVARCHAR(MAX) NULL,
[substance] NVARCHAR(MAX) NULL,
[type_fet_loss_3] NVARCHAR(MAX) NULL,
[type_fet_loss] NVARCHAR(MAX) NULL,
[type_fet_loss_1] NVARCHAR(MAX) NULL,
 PRIMARY KEY CLUSTERED ([Id] ASC));