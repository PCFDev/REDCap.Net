CREATE TABLE [dbo].[smoking_questionnaire] ([Id] INT IDENTITY (1, 1) NOT NULL, 
[PatientID] NVARCHAR(MAX) NULL, 
[EventName] NVARCHAR(MAX) NULL
, [cig_day] NVARCHAR(MAX) NULL,
[help_quit] NVARCHAR(MAX) NULL,
[not_start_again] NVARCHAR(MAX) NULL,
[smoke] NVARCHAR(MAX) NULL,
[smoke_entire_preg] NVARCHAR(MAX) NULL,
[smoke_preg] NVARCHAR(MAX) NULL,
[smoking_questionnaire_complete] NVARCHAR(MAX) NULL,
[wks_smoke] NVARCHAR(MAX) NULL,
[dir_referrals_smok] NVARCHAR(MAX) NULL,
 PRIMARY KEY CLUSTERED ([Id] ASC));