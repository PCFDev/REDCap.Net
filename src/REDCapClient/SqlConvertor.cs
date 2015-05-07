using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PCF.REDCap
{
    public class SqlConvertor
    {
        public string GenerateCreateFormTableSQL(string form, XDocument formDoc)
        {
            var formFields = formDoc.Descendants("field_name").Select(e => e.Value).Distinct();

            var sql = string.Format("CREATE TABLE [dbo].[{0}] (", form);

            var sqlBuilder = new StringBuilder();

            sqlBuilder.Append(sql);

            sqlBuilder.AppendLine("[Id] INT IDENTITY (1, 1) NOT NULL, ");
            sqlBuilder.AppendLine("[PatientID] NVARCHAR(MAX) NULL, ");
            sqlBuilder.AppendLine("[EventName] NVARCHAR(MAX) NULL,");

            if (formFields.Count() > 0)
            {
                foreach (var field in formFields)
                {
                    sqlBuilder.AppendLine(String.Format("[{0}] NVARCHAR(MAX) NULL,", field));
                }
            }

            sqlBuilder.Append(" PRIMARY KEY CLUSTERED ([Id] ASC));");

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

                }

            }

            var sqlBuilder = new StringBuilder();

            var values = new StringBuilder();

            var rows = formDoc.Descendants("item");

            foreach (var row in rows)
            {
                values.Clear();

                values.Append(row.Element("record").Value + ", ");
                values.Append(row.Element("redcap_event_name").Value + ", ");

                var valueElements = row.Descendants("value");

                if (valueElements.Count() > 0)
                {
                    foreach (var field in valueElements)
                    {
                        values.AppendFormat("'{0}', ", field.Value);
                    }

                }

                sqlBuilder.AppendLine(string.Format("INSERT INTO [dbo].[{0}] ({1}) VALUES ({2})",
                    form, columns.ToString().TrimEnd(',', ' '), values.ToString().TrimEnd(',', ' ')));
            }

            return sqlBuilder.ToString();
        }
    }
}
