CREATE TABLE [dbo].[therapy_appointment_form] ([Id] INT IDENTITY (1, 1) NOT NULL, 
[PatientID] NVARCHAR(MAX) NULL, 
[EventName] NVARCHAR(MAX) NULL
, [therapy_appointment_form_complete] NVARCHAR(MAX) NULL,
 PRIMARY KEY CLUSTERED ([Id] ASC));