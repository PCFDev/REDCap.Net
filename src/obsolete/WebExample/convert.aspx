<%@ Page language="c#" Codebehind="convert.aspx.cs" AutoEventWireup="false" Inherits="rcapi.convert" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>convert</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table style="WIDTH: 80%" id="ActionsFiles">
				<tr>
					<td style="WIDTH: 10%"><u><b><FONT color="red">Action</FONT></b></u>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 10%"></td>
					<td style="WIDTH: 45%">Select File:<br>
						<INPUT style="WIDTH: 80%" id="fileFile" type="file" name="fileFiles" runat="server" title="Select the file you wish to Convert">
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 10%"></td>
					<td style="WIDTH: 45%"><asp:button id="buttonConvertFile" runat="server" Text="Convert File" ToolTip="Convert Data Dictionary File. Take out Carriage Returns."></asp:button></td>
				</tr>
			</table>
			<br>
		</form>
	</body>
</HTML>
