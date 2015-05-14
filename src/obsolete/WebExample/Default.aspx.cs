using System;
using System.Collections; 
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace rcapi
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class _Default : System.Web.UI.Page
	{
		#region Form Variables

		protected System.Web.UI.WebControls.TextBox textBoxRCURI;
		protected System.Web.UI.WebControls.TextBox textBoxRCToken;
		protected System.Web.UI.WebControls.TextBox textBoxRecords;
		protected System.Web.UI.WebControls.TextBox textBoxForms;
		protected System.Web.UI.WebControls.TextBox textBoxEvents;
		protected System.Web.UI.WebControls.RadioButtonList comboBoxEventName;
		protected System.Web.UI.WebControls.Button buttonExportXMLFlat;
		protected System.Web.UI.WebControls.Button buttonExportXMLEav;
		protected System.Web.UI.WebControls.Button buttonImportXMLFlat;
		protected System.Web.UI.WebControls.Button buttonExportMetaXML;
		protected System.Web.UI.WebControls.Button buttonImportXMLEav;
		protected System.Web.UI.WebControls.Button buttonExportMetaCSV;
		protected System.Web.UI.WebControls.Button buttonImportCSVFlat;
		protected System.Web.UI.WebControls.Button buttonExportCSVEav;
		protected System.Web.UI.WebControls.Button buttonImportCSVEav;
		protected System.Web.UI.WebControls.Label LabelError;
		protected System.Web.UI.WebControls.RadioButtonList comboBoxOverwrite;
		protected System.Web.UI.HtmlControls.HtmlTable APIParameters;   

		protected System.Web.UI.WebControls.RadioButtonList comboBoxAPI;
		protected System.Web.UI.WebControls.Button buttonExportFile;
		protected System.Web.UI.WebControls.Button buttonImportFile;
		protected System.Web.UI.WebControls.Button buttonDeleteFile;
		protected System.Web.UI.HtmlControls.HtmlInputFile fileRecords;
		protected System.Web.UI.WebControls.Button buttonExportMetaJSON;
		protected System.Web.UI.WebControls.Button buttonExportJSONEav;
		protected System.Web.UI.WebControls.Button buttonImportJSONEav;
		protected System.Web.UI.WebControls.Button buttonExportJSONFlat;
		protected System.Web.UI.WebControls.Button buttonImportJSONFlat;
		protected System.Web.UI.WebControls.Button buttonExportCSVFlat;
		protected System.Web.UI.WebControls.Button buttonExportEventsXML;
		protected System.Web.UI.WebControls.Button buttonExportArmsXML;
		protected System.Web.UI.WebControls.Button buttonExportFormEventsXML;
		protected System.Web.UI.WebControls.Button buttonExportUsersXML;
		protected System.Web.UI.WebControls.Button buttonExportEventsCSV;
		protected System.Web.UI.WebControls.Button buttonExportArmsCSV;
		protected System.Web.UI.WebControls.Button buttonExportFormEventsCSV;
		protected System.Web.UI.WebControls.Button buttonExportUsersCSV;
		protected System.Web.UI.WebControls.Button buttonExportEventsJSON;
		protected System.Web.UI.WebControls.Button buttonExportArmsJSON;
		protected System.Web.UI.WebControls.Button buttonExportFormEventsJSON;
		protected System.Web.UI.WebControls.Button buttonExportUsersJSON;
		protected System.Web.UI.WebControls.RadioButtonList comboBoxRawOrLabel;
		protected System.Web.UI.WebControls.RadioButtonList comboBoxReturnFormat;
		protected System.Web.UI.WebControls.TextBox textBoxFields;
		protected System.Web.UI.WebControls.TextBox textBoxArms;

		protected System.Web.UI.HtmlControls.HtmlInputFile fileFile;   // rc token 

		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.buttonExportXMLFlat.Click += new System.EventHandler(this.buttonExportXMLFlat_Click);
			this.buttonImportXMLFlat.Click += new System.EventHandler(this.buttonImportXMLFlat_Click);
			this.buttonExportMetaXML.Click += new System.EventHandler(this.buttonExportMetaXML_Click);
			this.buttonExportXMLEav.Click += new System.EventHandler(this.buttonExportXMLEav_Click);
			this.buttonImportXMLEav.Click += new System.EventHandler(this.buttonImportXMLEav_Click);
			this.buttonExportMetaCSV.Click += new System.EventHandler(this.buttonExportMetaCSV_Click);
			this.buttonExportCSVFlat.Click += new System.EventHandler(this.buttonExportCSVFlat_Click);
			this.buttonImportCSVFlat.Click += new System.EventHandler(this.buttonImportCSVFlat_Click);
			this.buttonExportCSVEav.Click += new System.EventHandler(this.buttonExportCSVEav_Click);
			this.buttonImportCSVEav.Click += new System.EventHandler(this.buttonImportCSVEav_Click);
			this.buttonExportJSONFlat.Click += new System.EventHandler(this.buttonExportJSONFlat_Click);
			this.buttonImportJSONFlat.Click += new System.EventHandler(this.buttonImportJSONFlat_Click);
			this.buttonExportJSONEav.Click += new System.EventHandler(this.buttonExportJSONEav_Click);
			this.buttonImportJSONEav.Click += new System.EventHandler(this.buttonImportJSONEav_Click);
			this.buttonExportFile.Click += new System.EventHandler(this.buttonExportFile_Click);
			this.buttonImportFile.Click += new System.EventHandler(this.buttonImportFile_Click);
			this.buttonDeleteFile.Click += new System.EventHandler(this.buttonDeleteFile_Click);

			this.buttonExportEventsCSV.Click += new System.EventHandler(this.buttonExportEventsCSV_Click);
			this.buttonExportEventsJSON.Click += new System.EventHandler(this.buttonExportEventsJSON_Click);
			this.buttonExportEventsXML.Click += new System.EventHandler(this.buttonExportEventsXML_Click);
			this.buttonExportArmsCSV.Click += new System.EventHandler(this.buttonExportArmsCSV_Click);
			this.buttonExportArmsJSON.Click += new System.EventHandler(this.buttonExportArmsJSON_Click);
			this.buttonExportArmsXML.Click += new System.EventHandler(this.buttonExportArmsXML_Click);
			this.buttonExportFormEventsCSV.Click += new System.EventHandler(this.buttonExportFormEventsCSV_Click);
			this.buttonExportFormEventsJSON.Click += new System.EventHandler(this.buttonExportFormEventsJSON_Click);
			this.buttonExportFormEventsXML.Click += new System.EventHandler(this.buttonExportFormEventsXML_Click);
			this.buttonExportUsersCSV.Click += new System.EventHandler(this.buttonExportUsersCSV_Click);
			this.buttonExportUsersJSON.Click += new System.EventHandler(this.buttonExportUsersJSON_Click);
			this.buttonExportUsersXML.Click += new System.EventHandler(this.buttonExportUsersXML_Click);

			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Export Records

		private void buttonExportXMLFlat_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportXMLFlat_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "XMLFlat" + strDateTime + ".xml";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			string strPostParameters = "token=" + strToken + "&content=record&format=xml&type=flat" + GetOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportXMLEav_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportXMLEav_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "XMLEav" + strDateTime + ".xml";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=record&format=xml&type=eav" + GetOptionalParameters (false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportCSVFlat_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportCSVFlat_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "CSVFlat" + strDateTime + ".csv";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=record&format=csv&type=flat" + GetOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportCSVEav_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportCSVEav_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "CSVEav" + strDateTime + ".csv";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=record&format=csv&type=eav" + GetOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall( strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportJSONFlat_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportJSONFlat_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "JSONFlat" + strDateTime + ".json";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=record&format=json&type=flat" + GetOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportJSONEav_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportJSONEav_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "JSONEav" + strDateTime + ".json";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=record&format=json&type=eav" + GetOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall( strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		#endregion

		#region Import Records

		private void buttonImportXMLFlat_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonImportXMLFlat_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			string strFileName = fileRecords.PostedFile.FileName.Trim();
			string strPostParameters = "token=" + strToken + "&content=record&format=xml&type=flat" + GetOverwriteParameter() + "&data=";

			// check if xml file
			if ( ! strFileName.ToLower().EndsWith ( ".xml"))
			{
				LabelError.Text = "File must be an XML file for this action (Import XML Flat).";
				LabelError.Visible =  true;

				return;
			}

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters + "XML_FILE_CONTENTS");
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, false, "", true, strFileName, "");
				LabelError.Visible = true;
			}
		}

		private void buttonImportXMLEav_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonImportXMLEav_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			string strPostParameters = "token=" + strToken + "&content=record&format=xml&type=eav" + GetOverwriteParameter() + "&data=";
			string strFileName = fileRecords.PostedFile.FileName.Trim();

			// check if xml file
			if ( ! strFileName.ToLower().EndsWith ( ".xml"))
			{
				LabelError.Text = "File must be an XML file for this action (Import XML Eav).";
				LabelError.Visible =  true;

				return;
			}

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters + "XML_FILE_CONTENTS");
			}
			else
			{
				LabelError.Text = APICall( strContentType, strPostParameters, false, "", true, strFileName, "");
				LabelError.Visible = true;
			}
		}

		private void buttonImportCSVFlat_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonImportCSVFlat_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			string strFileName = fileRecords.PostedFile.FileName.Trim();
			string strPostParameters = "token=" + strToken + "&content=record&format=csv&type=flat" + GetOverwriteParameter() + "&data=";

			// check if csv file
			if ( ! strFileName.ToLower().EndsWith ( ".csv"))
			{
				LabelError.Text = "File must be a CSV file for this action (Import CSV Flat).";
				LabelError.Visible =  true;

				return;
			}

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters + "CSV_FILE_CONTENTS");
			}
			else
			{
				LabelError.Text = APICall( strContentType, strPostParameters, false, "", true, strFileName, "");
				LabelError.Visible = true;
			}
		}

		private void buttonImportCSVEav_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonImportCSVEav_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			string strFileName = fileRecords.PostedFile.FileName.Trim();
			string strPostParameters = "token=" + strToken + "&content=record&format=csv&type=eav" + GetOverwriteParameter() + "&data=";

			// check if csv file
			if ( ! strFileName.ToLower().EndsWith ( ".csv"))
			{
				LabelError.Text = "File must be a CSV file for this action (Import CSV Eav).";
				LabelError.Visible =  true;

				return;
			}
			
			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters + "CSV_FILE_CONTENTS");
			}
			else
			{
				LabelError.Text = APICall( strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonImportJSONFlat_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonImportJSONFlat_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			string strFileName = fileRecords.PostedFile.FileName.Trim();
			string strPostParameters = "token=" + strToken + "&content=record&format=json&type=flat" + GetOverwriteParameter() + "&data=";

			// check if csv file
			if ( ! strFileName.ToLower().EndsWith ( ".json"))
			{
				LabelError.Text = "File must be a JSON file for this action (Import JSON Flat).";
				LabelError.Visible =  true;

				return;
			}

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters + "JSON_FILE_CONTENTS");
			}
			else
			{
				LabelError.Text = APICall( strContentType, strPostParameters, false, "", true, strFileName, "");
				LabelError.Visible = true;
			}
		}

		private void buttonImportJSONEav_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonImportJSONEav_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			string strFileName = fileRecords.PostedFile.FileName.Trim();
			string strPostParameters = "token=" + strToken + "&content=record&format=json&type=eav" + GetOverwriteParameter() + "&data=";

			// check if csv file
			if ( ! strFileName.ToLower().EndsWith ( ".json"))
			{
				LabelError.Text = "File must be a JSON file for this action (Import JSON Eav).";
				LabelError.Visible =  true;

				return;
			}
			
			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters + "JSON_FILE_CONTENTS");
			}
			else
			{
				LabelError.Text = APICall( strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}
		#endregion

		#region MetaData Export

		private void buttonExportMetaXML_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportMetaXML_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "MetaXML" + strDateTime + ".xml";

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=metadata&format=xml" + GetOptionalParameters(true);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall( strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}			
		}

		private void buttonExportMetaCSV_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportMetaCSV_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "MetaCSV" + strDateTime + ".csv";

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=metadata&format=csv" + GetOptionalParameters(true);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall( strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}			
		}
		#endregion

		#region File Export-Import-Delete

		private void buttonExportFile_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ("buttonExportFile_Click()");

			LabelError.Text = "";
			LabelError.Visible = false;

			if (!CheckInputFile())
				return;

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=file&action=export" + GetRequiredFileParameters();

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, "", false, "", "");
				LabelError.Visible = true;
			}			
		}

		private void buttonImportFile_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ("buttonImportFile_Click()");

			// reset values
			LabelError.Text = "";
			LabelError.Visible = false;

			// check parameters
			if (!CheckInputFile())
				return;

			string strBoundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
			string strContentType = "multipart/form-data; boundary=" + strBoundary;

			/*
			HttpPostedFile myFile = fileFile.PostedFile;
			byte[] myData = new byte[myFile.ContentLength];
			myFile.InputStream.Read(myData, 0, myFile.ContentLength);
			Encoding encode = new System.Text.ASCIIEncoding();
			string strFileData = encode.GetString ( myData);
			*/

			string strFileName = fileFile.PostedFile.FileName.Trim();
			string strShortFileName = Path.GetFileName(strFileName);
			string strPostParameters = "";
			string strFileHeaderTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
			string strParametersHeaderTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n";
			string strImportCompletion = "\r\n--" + strBoundary + "--";

			if (strFileName == "")
			{
				LabelError.Text = "Enter a file name."; 
				LabelError.Visible = true;
                
				return;
			}

			/*
			// check if file exists
			if (File.Exists(strFileName) == false)
			{
				LabelError.Text = "File: " + strFileName + " does not exist";
				LabelError.Visible = true;

				return;
			}
			*/

			// write all parameters for api
			strPostParameters = String.Format(strParametersHeaderTemplate, strBoundary, "token", textBoxRCToken.Text.Trim());
			strPostParameters += String.Format(strParametersHeaderTemplate, strBoundary, "content", "file");
			strPostParameters += String.Format(strParametersHeaderTemplate, strBoundary, "action", "import");
			strPostParameters += String.Format(strParametersHeaderTemplate, strBoundary, "record", textBoxRecords.Text.Trim());
			strPostParameters += String.Format(strParametersHeaderTemplate, strBoundary, "field", textBoxFields.Text.Trim());
			if (textBoxEvents.Text.Trim() != "")
			{
				strPostParameters += String.Format(strParametersHeaderTemplate, strBoundary, "event", textBoxEvents.Text.Trim());
			}

			strPostParameters += String.Format(strFileHeaderTemplate, strBoundary, "file", strShortFileName);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, false, "", true, strFileName, strImportCompletion);
				LabelError.Visible = true;
			}			
		}

		private void buttonDeleteFile_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ("buttonDeleteFile_Click()");

			LabelError.Text = "";
			LabelError.Visible = false;

			// check parameters
			if (!CheckInputFile())
				return;

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=file&action=delete" + GetRequiredFileParameters();

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, false, "", false, "", "");
				LabelError.Visible = true;
			}			
		} 
		#endregion

		#region Events

		private void buttonExportEventsCSV_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportEventsCSV_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "EventsCSV" + strDateTime + ".csv";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=event&format=csv" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportEventsJSON_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportEventsJSON_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "EventsJSON" + strDateTime + ".json";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=event&format=json" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportEventsXML_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportEventsXML_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "EventsXML" + strDateTime + ".xml";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=event&format=xml" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportArmsCSV_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportArmsCSV_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "ArmsCSV" + strDateTime + ".csv";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=arm&format=csv" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportArmsJSON_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportArmsJSON_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "ArmsJSON" + strDateTime + ".json";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=arm&format=json" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportArmsXML_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportArmsXML_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "ArmsXML" + strDateTime + ".xml";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=arm&format=xml" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportFormEventsCSV_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportFormEventsCSV_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "FormEventMappingsCSV" + strDateTime + ".csv";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=formEventMapping&format=csv" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportFormEventsJSON_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportFormEventsJSON_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "FormEventMappingJSON" + strDateTime + ".json";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=formEventMapping&format=json" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportFormEventsXML_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportFormEventsXML_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "FormEventMappingXML" + strDateTime + ".xml";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=formEventMapping&format=xml" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportUsersCSV_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportUsersCSV_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "UsersCSV" + strDateTime + ".csv";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=user&format=csv" + GetArmsOptionalParameters(true);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportUsersJSON_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportFormEventsJSON_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "UsersJSON" + strDateTime + ".json";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=user&format=json" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}

		private void buttonExportUsersXML_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ( "buttonExportUsers_Click()");

			LabelError.Text = "";
			LabelError.Visible =  false;

			if (!CheckInput())
				return;

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "UsersXML" + strDateTime + ".xml";

			string strContentType = "application/x-www-form-urlencoded";
			string strToken = textBoxRCToken.Text.Trim();

			// parameters to pass to post
			string strPostParameters = "token=" + strToken + "&content=user&format=xml" + GetArmsOptionalParameters(false);

			if ( comboBoxAPI.SelectedValue.Trim() == "yes")
			{
				SendAPIParamsToClient (  strPostParameters);
			}
			else
			{
				LabelError.Text = APICall(strContentType, strPostParameters, true, strFileName, false, "", "");
				LabelError.Visible = true;
			}
		}


		#endregion

		#region API and various functions
		// makes API calls.  
		// ExportMetadata and ExportRecords need to provide their own file name.
		// ExportFile gets filename from ContentType returned from server, so leave blank
		// ImportRecords only sends file data after the initial parameters
		// ImportFile has to send a completion string
		private string APICall(string strContentType, string strParams, bool boolFileExport, string strExportFileName, 
			bool boolFileImport, string strImportFileName, string strImportCompletion)
		{
			Debug.WriteLine ( "APICall()");

			string strURI = textBoxRCURI.Text.Trim();
			string strResponse = "";
			HtmlInputFile htmlFile = new HtmlInputFile();

			try
			{
				// added for mono on unix server
				ServicePointManager.CertificatePolicy = new HttpWebRequestClientCertificateTest ();

				HttpWebRequest webreqRedCap = (HttpWebRequest)WebRequest.Create(strURI);

				webreqRedCap.Method = "POST";
				webreqRedCap.ContentType = strContentType;  // different for ImportFile

				// set bytes to transer
				byte[] bytePostData = Encoding.ASCII.GetBytes(strParams);

				// Get the request stream and send the API request
				Stream streamData = webreqRedCap.GetRequestStream();

				streamData.Write(bytePostData, 0, bytePostData.Length);

				// for Imports, need to send file data to server
				if (boolFileImport)
				{
					if ( fileRecords.PostedFile.FileName.Trim() != "")
						htmlFile = fileRecords;
					if ( fileFile.PostedFile.FileName.Trim() != "")
						htmlFile = fileFile;

					/*
					string strFileType = fileFile.PostedFile.ContentType;
					byte[] byteFileContents = new byte[fileFile.PostedFile.InputStream.Length];
					fileRecords.PostedFile.InputStream.Read(byteFileContents, 0, byteFileContents.Length);

					streamData.Write ( byteFileContents, 0, byteFileContents.Length);
					*/

					// write file to API in chunks
					byte[] bytesBuffer = new byte[1024];
					int bytesRead = -1;
					while ((bytesRead = htmlFile.PostedFile.InputStream.Read(bytesBuffer, 0, bytesBuffer.Length)) != 0)
					{
						//intCount = bytesRead;
						streamData.Write(bytesBuffer, 0, bytesRead);
					}

					// if FileImport, need to send a completion string (\r\n and boundary)
					if (strImportCompletion.Trim() != "")
					{
						byte[] bytesImportCompletion = Encoding.UTF8.GetBytes(strImportCompletion);
						streamData.Write(bytesImportCompletion, 0, bytesImportCompletion.Length);
					}
				}

				streamData.Close();

				HttpWebResponse webrespRedCap = (HttpWebResponse)webreqRedCap.GetResponse();

				// get the response stream.
				Stream streamResponse = webrespRedCap.GetResponseStream();

				// ExportFile and ExportRecords return file data ... all other APIs return strings
				if (boolFileExport)
				{
					// check if FileExport. it returns a file name along with the data
					if (strExportFileName.Trim() == "")
					{
						// get filename
						string strFileContentType = webrespRedCap.ContentType.ToString();
						strExportFileName = strFileContentType.Remove(0, strFileContentType.IndexOf("name=") + 5);
						string strTemp = strExportFileName.Replace('"', ' ');
						strExportFileName = strTemp.Trim();
					}

					// now write to file
					strResponse = SendFileToClient(strExportFileName, streamResponse);
				}
				else
				{
					StreamReader readerResponse = new StreamReader(streamResponse);
					strResponse = readerResponse.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				strResponse = ex.Message.ToString();
			}

			return (strResponse);
		}

		// makes sure needed text boxes are filled in.  otherwise tells error
		private bool CheckInput()
		{
			Debug.WriteLine ("CheckInput()");

			// empty results box
			LabelError.Text = "";
			LabelError.Visible = false;

			if (textBoxRCToken.Text.Trim() == "")
			{
				LabelError.Text = "A RedCap Token must be entered.";
				LabelError.Visible = true;

				return (false);
			}

			if (textBoxRCURI.Text.Trim() == "")
			{
				LabelError.Text = "A RedCap URI must be entered.";
				LabelError.Visible = true;

				return (false);
			}

			return (true);
		}

		// makes sure needed text boxes are filled in.  otherwise tells error
		private bool CheckInputFile()
		{
			Debug.WriteLine ("CheckInputFile()");

			// empty results box
			LabelError.Text = "";
			LabelError.Visible = false;


			if (textBoxRCToken.Text.Trim() == "")
			{
				LabelError.Text = "A RedCap Token must be entered.";
				LabelError.Visible = true;

				return (false);
			}

			if (textBoxRCURI.Text.Trim() == "")
			{
				LabelError.Text = "A RedCap URI must be entered.";
				LabelError.Visible = true;

				return (false);
			}

			if (textBoxRecords.Text.Trim() == "")
			{
				LabelError.Text = "A Record must be entered.";
				LabelError.Visible = true;

				return (false);
			}

			if (textBoxFields.Text.Trim() == "")
			{
				LabelError.Text = "A Field must be entered.";
				LabelError.Visible = true;

				return (false);
			}

			return (true);
		}

		private string GetOverwriteParameter ()
		{
			Debug.WriteLine ( "GetOverwriteParameter()");

			string strParameters = "";

			if ( comboBoxOverwrite.SelectedValue != "")
			{
				strParameters += String.Format("&overwriteBehavior=" + comboBoxOverwrite.SelectedValue.Trim());
			}

			return ( strParameters);
		}

		private string GetOptionalParameters( bool boolMeta)
		{
			Debug.WriteLine ( "GetOptionalParameters()");

			string strParameters = "";

			if (textBoxFields.Text.Trim() != "")
				strParameters += String.Format("&fields=" + textBoxFields.Text.Trim());

			if (textBoxForms.Text.Trim() != "")
				strParameters += String.Format("&forms=" + textBoxForms.Text.Trim());

			if (comboBoxReturnFormat.SelectedValue != "")
			{
				strParameters += String.Format("&returnFormat=" + comboBoxReturnFormat.SelectedValue.Trim());
			}

			if (!boolMeta)
			{
				if (textBoxRecords.Text.Trim() != "")
					strParameters += String.Format("&records=" + textBoxRecords.Text.Trim());

				if (textBoxEvents.Text.Trim() != "")
					strParameters += String.Format("&events=" + textBoxEvents.Text.Trim());

				if (comboBoxRawOrLabel.SelectedValue.Trim() != "")
				{
					strParameters += String.Format("&rawOrLabel=" + comboBoxRawOrLabel.SelectedValue.Trim());
				}

				if (comboBoxEventName.SelectedValue != "")
				{
					strParameters += String.Format("&eventName=" + comboBoxEventName.SelectedValue.Trim());
				}
			}

			return (strParameters);
		}

		private string GetArmsOptionalParameters( bool boolUsers)
		{
			Debug.WriteLine ( "GetArmsOptionalParameters()");

			string strParameters = "";

			if (!boolUsers)
			{
				if (textBoxArms.Text.Trim() != "")
					strParameters += String.Format("&arms=" + textBoxArms.Text.Trim());
			}

			if (comboBoxReturnFormat.SelectedValue != "")
			{
				strParameters += String.Format("&returnFormat=" + comboBoxReturnFormat.SelectedValue.Trim());
			}

			return (strParameters);
		}

		private string GetRequiredFileParameters()
		{
			Debug.WriteLine ("GetRequiredFileParameters()");

			string strParameters = "";

			if (textBoxRecords.Text.Trim() != "")
				strParameters += String.Format("&record=" + textBoxRecords.Text.Trim());

			if (textBoxFields.Text.Trim() != "")
				strParameters += String.Format("&field=" + textBoxFields.Text.Trim());

			if (textBoxEvents.Text.Trim() != "")
				strParameters += String.Format("&event=" + textBoxEvents.Text.Trim());

			return (strParameters);
		}

		private void SendAPIParamsToClient ( string strData)
		{
			Debug.WriteLine ( "SendAPIParamsToClient()");

			string strResponse = "";

			string strDateTime = DateTime.Now.ToString ( "_yyyyMMdd_hhmmss");
			string strFileName = "APIParameters" + strDateTime + ".txt";

			string strAttachment = "attachment; filename=" + strFileName;

			try
			{
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.ClearContent();
				HttpContext.Current.Response.AddHeader("content-disposition", strAttachment);
				HttpContext.Current.Response.ContentType = "application/octet-stream";
				HttpContext.Current.Response.AddHeader("Pragma", "public");
				HttpContext.Current.Response.Write ( strData);
				HttpContext.Current.Response.End();
				//HttpContext.Current.ApplicationInstance.CompleteRequest();

				strResponse = "File written to " + strFileName;
			}
			catch (Exception ex)
			{
				strResponse = ex.Message.ToString();
			}
		}
		
		private string SendFileToClient(string strFileName, Stream streamResponse)
		{
			Debug.WriteLine ("SendFileToClient()");

			string strResponse = "";
			byte[] byteChunks = new byte[4096];
			int intRead;
			MemoryStream msData = new MemoryStream();

			string strAttachment = "attachment; filename=" + strFileName;
			
			try
			{
				// read stream into memory stream and put into byte array
				while ((intRead = streamResponse.Read(byteChunks, 0, byteChunks.Length)) > 0)
				{
					msData.Write(byteChunks, 0, intRead);
				}

				byte[] bytesFileData = msData.ToArray(); 

				if ( Path.GetExtension (strFileName) == ".csv")
				{
					string strFileData = System.Text.ASCIIEncoding.Default.GetString (bytesFileData);

					string strData = strFileData.Replace("\\\"", "\"\"");
					bytesFileData = System.Text.ASCIIEncoding.Default.GetBytes(strData);
				}

				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.ClearContent();
				HttpContext.Current.Response.AddHeader("content-disposition", strAttachment);
				HttpContext.Current.Response.ContentType = "application/octet-stream";
				HttpContext.Current.Response.AddHeader("Pragma", "public");
				HttpContext.Current.Response.BinaryWrite( bytesFileData);
				HttpContext.Current.Response.End();

				strResponse = "File written to " + strFileName;
			}
			catch (Exception ex)
			{
				strResponse = ex.Message.ToString();
			}

			return (strResponse);

		}
		#endregion


	}

	// added for mono on unix server
	public class HttpWebRequestClientCertificateTest : ICertificatePolicy 
	{
		public bool CheckValidationResult (ServicePoint sp, X509Certificate certificate, WebRequest request, int error)       
		{              
			return true;          
		}      
	}  
}
