<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="rcapi._Default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>REDCap API</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" encType="multipart/form-data" method="post" action="Default.aspx" runat="server">
			<table style="BORDER-BOTTOM-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none">
				<tr>
					<td><u><b><FONT color="blue">REDCap API (status: in development)</FONT></b></u>
					</td>
				</tr>
			</table>
			<br>
			<table style="WIDTH: 100%" id="Required">
				<tr>
					<td style="WIDTH: 15%"><u><b><FONT color="red">Required</FONT></b></u>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">REDCap URI:
					</td>
					<td style="WIDTH: 50%"><asp:textbox id="textBoxRCURI" runat="server" Width="100%" ToolTip="The URL for your API (normally https://your-site-name/redcap/api/ )"></asp:textbox></td>
					<td style="WIDTH: 35%"></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">REDCap Token:
					</td>
					<td style="WIDTH: 50%"><asp:textbox id="textBoxRCToken" runat="server" Width="100%" ToolTip="The API token specific to your REDCap project and username (each token is unique to each user for each project)"></asp:textbox></td>
					<td style="WIDTH: 35%"></td>
				</tr>
			</table>
			<br>
			<table style="WIDTH: 100%" id="Optional">
				<tr>
					<td style="WIDTH: 15%"><u><b><FONT color="red">Optional</FONT></b></u>
					</td>
					<td style="WIDTH: 50%"></td>
					<td style="WIDTH: 35%"><u><b>Used With</b></u>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">Records:
					</td>
					<td style="WIDTH: 50%"><asp:textbox id="textBoxRecords" runat="server" Width="100%" ToolTip="An array of RECORD names, seperated by commas, specifying specific records you wish to pull (by default, all records are pulled)"></asp:textbox></td>
					<td style="WIDTH: 35%">Export Records, Export/Import/Delete File
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">Fields:
					</td>
					<td style="WIDTH: 50%"><asp:textbox id="textBoxFields" runat="server" Width="100%" ToolTip="An array of FIELD names, seperated by commas, specifying specific FIELDS you wish to pull (by default, all records are pulled)"></asp:textbox></td>
					<td style="WIDTH: 35%">Export Records, Export Metadata, Export/Import/Delete File
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">Forms:
					</td>
					<td style="WIDTH: 50%"><asp:textbox id="textBoxForms" runat="server" Width="100%" ToolTip="An array of FORM names, seperated by commas, that you wish for pull data for.  If the FORM name has a space in it, replace the space with an underscorel (by default, all records are pulled)"></asp:textbox></td>
					<td style="WIDTH: 35%">Export Records, Export Metadata
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">Events:
					</td>
					<td style="WIDTH: 50%"><asp:textbox id="textBoxEvents" runat="server" Width="100%" ToolTip="An array of unique event  names, seperated by commas, that you wish to pull records for - only for longitudinal studies"></asp:textbox></td>
					<td style="WIDTH: 35%">Export Records, Export/Import/Delete File
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">Raw or Label:
					</td>
					<td style="WIDTH: 50%"><asp:radiobuttonlist id="comboBoxRawOrLabel" runat="server" RepeatDirection="Horizontal" ToolTip="RAW [default], LABEL, BOTH - export the RAW coded values, LABELS, or BOTH for the options of multiple choice fields">
							<asp:ListItem Value="raw">raw</asp:ListItem>
							<asp:ListItem Value="label">label</asp:ListItem>
							<asp:ListItem Value="both">both</asp:ListItem>
						</asp:radiobuttonlist></td>
					<td style="WIDTH: 35%">Export Records
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">Event Name:
					</td>
					<td style="WIDTH: 50%"><asp:radiobuttonlist id="comboBoxEventName" runat="server" RepeatDirection="Horizontal" ToolTip="LABEL [default], UNIQUE - export the UNIQUE event name or the event LABEL">
							<asp:ListItem Value="label">label</asp:ListItem>
							<asp:ListItem Value="unique">unique</asp:ListItem>
						</asp:radiobuttonlist></td>
					<td style="WIDTH: 35%">Export Records
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">Arms:
					</td>
					<td style="WIDTH: 50%"><asp:textbox id="textBoxArms" runat="server" Width="100%" ToolTip="An array of unique arm numbers, seperated by commas, that you wish to pull events for (by default, all events are pulled) - only for longitudinal studies"></asp:textbox></td>
					<td style="WIDTH: 35%">Export Events/Arms/Form-Event Mappings
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">Return Format:
					</td>
					<td style="WIDTH: 50%"><asp:radiobuttonlist id="comboBoxReturnFormat" runat="server" RepeatDirection="Horizontal" ToolTip="CSV, JSON, XML [default] - specifies the format of error messages.">
							<asp:ListItem Value="raw">csv</asp:ListItem>
							<asp:ListItem Value="label">json</asp:ListItem>
							<asp:ListItem Value="both">xml</asp:ListItem>
						</asp:radiobuttonlist></td>
					<td style="WIDTH: 35%">All
					</td>
				</tr>
			</table>
			<br>
			<table style="WIDTH: 100%" id="Actions">
				<tr>
					<td style="WIDTH: 15%"><u><b><FONT color="red">Actions - Data</FONT></b></u>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"><b>Export Records</b>
					</td>
					<td style="WIDTH: 45%"><b>Import Records</b>
					</td>
					<td style="WIDTH: 20%"><b>Export Metadata<br>
							(i.e. Data Dictionary)</b>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"></td>
					<td style="WIDTH: 45%">Select File:<br>
						<INPUT style="WIDTH: 80%" id="fileRecords" type="file" name="fileRecords" runat="server"
							title="Select the file you wish to Import">
					</td>
					<td style="WIDTH: 20%"></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"></td>
					<td style="WIDTH: 45%">
						<table>
							<tr>
								<td>OverwriteBehavior:</td>
								<td><asp:radiobuttonlist id="comboBoxOverwrite" runat="server" RepeatDirection="Horizontal" ToolTip="NORMAL - blank/empty values will be ignored [default]; OVERWRITE - blank/empty values are valid and will overwrite data">
										<asp:ListItem Value="normal">normal</asp:ListItem>
										<asp:ListItem Value="overwrite">overwrite</asp:ListItem>
									</asp:radiobuttonlist></td>
								<td>(Default- normal)
								</td>
							</tr>
						</table>
					</td>
					<td style="WIDTH: 20%"></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"><asp:button id="buttonExportXMLFlat" runat="server" Text="Export XML Flat" ToolTip="Export records with format='xml' and type='flat'"></asp:button></td>
					<td style="WIDTH: 45%"><asp:button id="buttonImportXMLFlat" runat="server" Text="Import XML Flat" ToolTip="Import records with format='xml' and type='flat'"></asp:button></td>
					<td style="WIDTH: 20%"><asp:button id="buttonExportMetaXML" runat="server" Text="Export Meta XML" ToolTip="Export Metadata with format='xml'"></asp:button></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"><asp:button id="buttonExportXMLEav" runat="server" Text="Export XML Eav" ToolTip="Export records with format='xml' and type='eav'"></asp:button></td>
					<td style="WIDTH: 45%"><asp:button id="buttonImportXMLEav" runat="server" Text="Import XML Eav" ToolTip="Import records with format='xml' and type='eav'"></asp:button></td>
					<td style="WIDTH: 20%"><asp:button id="buttonExportMetaCSV" runat="server" Text="Export Meta CSV" ToolTip="Export Metadata with format='csv'"></asp:button></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"><asp:button id="buttonExportCSVFlat" runat="server" Text="Export CSV Flat" ToolTip="Export records with format='csv' and type='flat'"></asp:button></td>
					<td style="WIDTH: 45%"><asp:button id="buttonImportCSVFlat" runat="server" Text="Import CSV Flat" ToolTip="Import records with format='csv' and type='flat'"></asp:button></td>
					<td style="WIDTH: 20%"><asp:button id="buttonExportMetaJSON" runat="server" Text="Export Meta JSON" ToolTip="Export Metadata with format='json'"></asp:button></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"><asp:button id="buttonExportCSVEav" runat="server" Text="Export CSV Eav" ToolTip="Export records with format='csv' and type='eav'"></asp:button></td>
					<td style="WIDTH: 45%"><asp:button id="buttonImportCSVEav" runat="server" Text="Import CSV Eav" ToolTip="Import records with format='csv' and type='eav'"></asp:button></td>
					<td style="WIDTH: 20%"></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"><asp:button id="buttonExportJSONFlat" runat="server" Text="Export JSON Flat" ToolTip="Export records with format='json' and type='flat'"></asp:button></td>
					<td style="WIDTH: 45%"><asp:button id="buttonImportJSONFlat" runat="server" Text="Import JSON Flat" ToolTip="Import records with format='json' and type='flat'"></asp:button></td>
					<td style="WIDTH: 20%"></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"><asp:button id="buttonExportJSONEav" runat="server" Text="Export JSON Eav" ToolTip="Export records with format='json' and type='eav'"></asp:button></td>
					<td style="WIDTH: 45%"><asp:button id="buttonImportJSONEav" runat="server" Text="Import JSON Eav" ToolTip="Import records with format='json' and type='eav'"></asp:button></td>
					<td style="WIDTH: 20%"></td>
				</tr>
			</table>
			<br>
			<table style="WIDTH: 100%" id="ActionsFiles">
				<tr>
					<td style="WIDTH: 15%"><u><b><FONT color="red">Actions - Files</FONT></b></u>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"><b>Export File</b>
					</td>
					<td style="WIDTH: 45%"><b>Import File</b>
					</td>
					<td style="WIDTH: 20%"><b>Delete File</b>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"></td>
					<td style="WIDTH: 45%">Select File:<br>
						<INPUT style="WIDTH: 80%" id="fileFile" type="file" name="fileFiles" runat="server" title="Select the file you wish to Import">
					</td>
					<td style="WIDTH: 20%"></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%">&nbsp;</td>
					<td style="WIDTH: 20%">&nbsp;</td>
					<td style="WIDTH: 45%">&nbsp;</td>
					<td style="WIDTH: 20%">&nbsp;</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 20%"><asp:button id="buttonExportFile" runat="server" Text="Export File" ToolTip="Export File from Field in Record"></asp:button></td>
					<td style="WIDTH: 45%"><asp:button id="buttonImportFile" runat="server" Text="Import File" ToolTip="Import File to Field in Record"></asp:button></td>
					<td style="WIDTH: 20%"><asp:button id="buttonDeleteFile" runat="server" Text="Delete File" ToolTip="Delete File from Field in Record"></asp:button></td>
				</tr>
			</table>
			<br>
			<table style="WIDTH: 100%" id="ActionsOther">
				<tr>
					<td style="WIDTH: 15%" colspan="3"><u><b><FONT color="red">Actions - Events, Arms, Form-Event 
									Mappings, Users</FONT></b></u>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 21%"><b>Export Events</b>
					</td>
					<td style="WIDTH: 21%"><b>Export Arms</b>
					</td>
					<td style="WIDTH: 21%"><b>Export Form-Event Mappings</b>
					</td>
					<td style="WIDTH: 21%"><b>Export Users</b>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportEventsXML" runat="server" Text="Export XML" ToolTip="Export Events with format='xml'"></asp:button></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportArmsXML" runat="server" Text="Export XML" ToolTip="Export Arms with format='xml'"></asp:button></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportFormEventsXML" runat="server" Text="Export XML" ToolTip="Export Form-Events with format='xml'"></asp:button></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportUsersXML" runat="server" Text="Export XML" ToolTip="Export Users with format='xml'"></asp:button></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportEventsCSV" runat="server" Text="Export CSV" ToolTip="Export Events with format='csv'"></asp:button></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportArmsCSV" runat="server" Text="Export CSV" ToolTip="Export Arms with format='csv'"></asp:button></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportFormEventsCSV" runat="server" Text="Export CSV" ToolTip="Export Form-Events with format='csv'"></asp:button></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportUsersCSV" runat="server" Text="Export CSV" ToolTip="Export Users with format='csv'"></asp:button></td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportEventsJSON" runat="server" Text="Export JSON" ToolTip="Export Events with format='json'"></asp:button></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportArmsJSON" runat="server" Text="Export JSON" ToolTip="Export Arms with format='json'"></asp:button></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportFormEventsJSON" runat="server" Text="Export JSON" ToolTip="Export Form-Events with format='json'"></asp:button></td>
					<td style="WIDTH: 21%"><asp:button id="buttonExportUsersJSON" runat="server" Text="Export JSON" ToolTip="Export Users with format='json'"></asp:button></td>
				</tr>
			</table>
			<br>
			<table style="WIDTH: 100%" id="Log">
				<tr>
					<td style="WIDTH: 15%"><u><b><FONT color="red">Log</FONT></b></u>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 60%"><asp:label id="LabelError" runat="server" ForeColor="Red" Font-Bold="True" Visible="False"></asp:label></td>
					<td style="WIDTH: 25%">&nbsp;</td>
				</tr>
			</table>
			<p>&nbsp;</p>
			<table style="WIDTH: 100%" id="APIParameters" runat="server">
				<tr>
					<td style="WIDTH: 15%"><u><b><FONT color="red">RC API Parameters</FONT></b></u>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 15%"></td>
					<td style="WIDTH: 60%">Do you want the API Parameters used in this call? (This is 
						for coding only, in case you like the results you&nbsp;got back&nbsp;and want 
						to use&nbsp;the API Parameters&nbsp;in some other code situation. Or maybe you 
						just need the parameters for debugging.&nbsp; If you select 'yes', this will 
						not make any calls to the RC server.)</td>
					<td style="WIDTH: 25%">
						<asp:radiobuttonlist id="comboBoxAPI" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="yes">yes</asp:ListItem>
							<asp:ListItem Value="no">no</asp:ListItem>
						</asp:radiobuttonlist></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
