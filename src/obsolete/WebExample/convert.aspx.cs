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
	/// Summary description for convert.
	/// </summary>
	public class convert : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button buttonConvertFile;
		protected System.Web.UI.HtmlControls.HtmlInputFile fileFile;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		private void buttonConvertFile_Click(object sender, System.EventArgs e)
		{
			Debug.WriteLine ("buttonConvertFile_Click()");

			string strFileName = fileFile.PostedFile.FileName.Trim();

			if (strFileName == "")
			{
				return;
			}

			string strShortFileName = Path.GetFileName(strFileName);
			string strAttachment = "attachment; filename=" + strShortFileName;

			HttpPostedFile myFile = fileFile.PostedFile;
			byte[] myData = new byte[myFile.ContentLength];
			myFile.InputStream.Read(myData, 0, myFile.ContentLength);

			string strFileData = System.Text.ASCIIEncoding.Default.GetString (myData);
			string strData = strFileData.Replace("\r\n", "\n");
			byte[] bytesFileData = System.Text.ASCIIEncoding.Default.GetBytes(strData);

			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.ClearHeaders();
			HttpContext.Current.Response.ClearContent();
			HttpContext.Current.Response.AddHeader("content-disposition", strAttachment);
			HttpContext.Current.Response.ContentType = "application/octet-stream";
			HttpContext.Current.Response.AddHeader("Pragma", "public");
			HttpContext.Current.Response.BinaryWrite( bytesFileData);
			HttpContext.Current.Response.End();
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
			this.buttonConvertFile.Click += new System.EventHandler(this.buttonConvertFile_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
