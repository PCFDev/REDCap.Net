using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace REDCapClient
{
    public class SqlConvertor
    {
        public string GenerateCreateFormTableSQL(string form, XDocument formDoc)
        {
            var formFields = formDoc.Descendants("field_name").Select(e => e.Value).Distinct();

            var sql = string.Format("CREATE TABLE [dbo].[{0}] (", form);

            var sqlBuilder = new StringBuilder();

            //            CREATE TABLE [dbo].[patient_tracking] (
            //    [Id]                        INT            IDENTITY (1, 1) NOT NULL,
            //    [PatientID]                 NVARCHAR (MAX) NULL,
            //    [EventName]                 NVARCHAR (MAX) NULL,
            //    [barnes_nicu]               NVARCHAR (MAX) NULL,
            //    [case_de]                   NVARCHAR (MAX) NULL,
            //    [city]                      NVARCHAR (MAX) NULL,
            //    [clinical_serv]             NVARCHAR (MAX) NULL,
            //    [currentpsy]                NVARCHAR (MAX) NULL,
            //    [currenttherap]             NVARCHAR (MAX) NULL,
            //    [eligible]                  NVARCHAR (MAX) NULL,
            //    [hxmentill]                 NVARCHAR (MAX) NULL,
            //    [id_tracking]               NVARCHAR (MAX) NULL,
            //    [mother_mother]             NVARCHAR (MAX) NULL,
            //    [nicutrans]                 NVARCHAR (MAX) NULL,
            //    [nurse_new]                 NVARCHAR (MAX) NULL,
            //    [ob]                        NVARCHAR (MAX) NULL,
            //    [patient_tracking_complete] NVARCHAR (MAX) NULL,
            //    [ptlocation]                NVARCHAR (MAX) NULL,
            //    [ptstatus]                  NVARCHAR (MAX) NULL,
            //    [refer_by]                  NVARCHAR (MAX) NULL,
            //    [refer_made]                NVARCHAR (MAX) NULL,
            //    [timemom]                   NVARCHAR (MAX) NULL,
            //    [zip]                       NVARCHAR (MAX) NULL,
            //    [consent_baby1]             NVARCHAR (MAX) NULL,
            //    [email]                     NVARCHAR (MAX) NULL,
            //    [phone1]                    NVARCHAR (MAX) NULL,
            //    [phone2]                    NVARCHAR (MAX) NULL,
            //    [ref_type]                  NVARCHAR (MAX) NULL,
            //    [research_share]            NVARCHAR (MAX) NULL,
            //    [research_store]            NVARCHAR (MAX) NULL,
            //    [barnes_male_female]        NVARCHAR (MAX) NULL,
            //    [consent_baby2]             NVARCHAR (MAX) NULL,
            //    [consent_baby3]             NVARCHAR (MAX) NULL,
            //    [ebdsby1]                   NVARCHAR (MAX) NULL,
            //    [ebdsby2]                   NVARCHAR (MAX) NULL,
            //    [ebdsby4]                   NVARCHAR (MAX) NULL,
            //    [ebdsby5]                   NVARCHAR (MAX) NULL,
            //    [ebdsby6]                   NVARCHAR (MAX) NULL,
            //    [interpreter_lang]          NVARCHAR (MAX) NULL,
            //    [modeepdsadm1]              NVARCHAR (MAX) NULL,
            //    [modeepdsadm2]              NVARCHAR (MAX) NULL,
            //    [modeepdsadm3]              NVARCHAR (MAX) NULL,
            //    [modeepdsadm4]              NVARCHAR (MAX) NULL,
            //    [modeepdsadm5]              NVARCHAR (MAX) NULL,
            //    [modeepdsadm6]              NVARCHAR (MAX) NULL,
            //    [phone3]                    NVARCHAR (MAX) NULL,
            //    [phone4]                    NVARCHAR (MAX) NULL,
            //    [psyfactors]                NVARCHAR (MAX) NULL,
            //    [reason_inelig]             NVARCHAR (MAX) NULL,
            //    PRIMARY KEY CLUSTERED ([Id] ASC)
            //);
            sqlBuilder.Append(sql);

            sqlBuilder.AppendLine("[Id] INT IDENTITY (1, 1) NOT NULL, ");
            sqlBuilder.AppendLine("[PatientID] NVARCHAR(MAX) NULL, ");
            sqlBuilder.AppendLine("[EventName] NVARCHAR(MAX) NULL");

            if (formFields.Count() > 0)
            {
                sqlBuilder.Append(", ");
                foreach (var field in formFields)
                {
                    sqlBuilder.AppendLine(String.Format("[{0}] NVARCHAR(MAX) NULL,", field));

                    //if (field != formFields.Last())
                    //sqlBuilder.Append(", ");
                }


            }



            sqlBuilder.Append(" PRIMARY KEY CLUSTERED ([Id] ASC));");

            //Console.WriteLine(sqlBuilder.ToString());
            var sqlCmd = sqlBuilder.ToString();



            return sqlCmd;
        }

        public string GenerateInsertFormDataSQL(string form, XDocument formDoc)
        {
            //INSERT INTO [dbo].[Projects] ([Id], [Name], [Token]) VALUES (1, N'Test', N'133422d')

            var formFields = formDoc.Descendants("field_name").Select(e => e.Value).Distinct();

            var columns = new StringBuilder();

            columns.Append("[PatientID], ");
            columns.Append("[EventName], ");

            if (formFields.Count() > 0)
            {
                foreach (var field in formFields)
                {
                    columns.AppendFormat("[{0}], ", field);

                    //if (field != formFields.Last())
                    //columns.Append(", ");
                }


            }

            var sqlBuilder = new StringBuilder();

            var values = new StringBuilder();

            var rows = formDoc.Descendants("item");

            foreach (var row in rows)
            {
                values.Clear();

                sqlBuilder.AppendLine(row.Element("record").Value + ", ");
                sqlBuilder.AppendLine(row.Element("redcap_event_name").Value + ", ");

                var valueElements = row.Descendants("value");

                if (valueElements.Count() > 0)
                {
                    foreach (var field in valueElements)
                    {
                        columns.AppendFormat("[{0}], ", field.Value);

                    }

                }

                sqlBuilder.AppendLine(string.Format("INSERT INTO [dbo].[{0}] ({1}) VALUES ({2})",
                    form, columns.ToString().TrimEnd(',', ' '), values.ToString().TrimEnd(',', ' ')));
            }

            return sqlBuilder.ToString();
        }
    }
}
